using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Entity.DBContent;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Model.BASE;
using Model.COMMON;
using Model.HETHONG.MENU.Dtos;
using Model.HETHONG.TAIKHOAN.Dtos;
using Model.HETHONG.TAIKHOAN.Reuquest;
using Repository;
using Service.HETHONG.NHOMQUYEN;
using System.Text.RegularExpressions;

namespace Service.HETHONG.TAIKHOAN
{
    [RegisterClassAsTransient]
    public class TAIKHOANService : ITAIKHOANService
    {
        private readonly IConfiguration _config;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private INHOMQUYENService _nhomquyenService;
        private IWebHostEnvironment _webHostEnvironment;

        public TAIKHOANService(IConfiguration config, IUnitOfWork unitOfWork, IMapper mapper, INHOMQUYENService nhomquyenService, IWebHostEnvironment webHostEnvironment)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _nhomquyenService = nhomquyenService;
            _webHostEnvironment = webHostEnvironment;
        }

        public BaseResponse<MODELTaiKhoanPhanQuyen> Login(LoginRequest request)
        {
            var response = new BaseResponse<MODELTaiKhoanPhanQuyen>();
            try
            {
                var data = new MODELTaiKhoanPhanQuyen();
                var taikhoan = _unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>()
                    .Find(x => x.UserName.ToLower() == request.UserName.ToLower());
                if (taikhoan == null)
                {
                    throw new Exception("Tài khoản hoặc mật khẩu không đúng");
                }

                var pass = Encrypt_Decrypt.EncodePassword(request.Password, taikhoan.MatKhauSalt);
                if (pass.Equals(taikhoan.MatKhau))
                {
                    throw new Exception("Tài khoản hoặc mật khẩu không đúng");
                }

                var token = Encrypt_Decrypt.GenerateJwtToken(new MODELTaiKhoan
                {
                    Id = Guid.Parse(taikhoan.Id.ToString()),
                    UserName = request.UserName

                }, _config);
                data.TaiKhoan = _mapper.Map<MODELTaiKhoan>(taikhoan);
                var vaitro = _unitOfWork.GetRepository<VAITRO>().Find(x => x.Id == data.TaiKhoan.VaiTroId);
                data.TaiKhoan.VaiTro = vaitro?.TenGoi;
                data.TaiKhoan.Guid = pass;
                data.TaiKhoan.AnhDaiDien = string.IsNullOrWhiteSpace(taikhoan.AnhDaiDien)
                    ? "Files/no-image.png"
                    : taikhoan.AnhDaiDien;
                data.TaiKhoan.Token = token;
                data.Menu = GetListMenu(new GetListMenuRequest { UserId = taikhoan.Id }).Data
                    .Select(x => new MODELMenuLogin()
                    {
                        Action = x.Action,
                        ControllerName = x.Controller,
                        NhomQuyenId = x.NhomQuyenId,
                        TenGoi = x.TenGoi,
                        Sort = x.Sort

                    }).ToList();
                data.PhanQuyen = GetPhanQuyen(new GetPhanQuyenRequest { UserId = taikhoan.Id }).Data;
                data.NhomQuyen = _nhomquyenService.GetList().Data.Where(x => x.IsActived = true)
                    .Select(x => new MODELNhomQuyenLogin
                    {
                        Id = x.Id,
                        Sort = x.Sort,
                        TenGoi = x.TenGoi,
                        Icon = x.Icon
                    }).ToList();
                response.Data = data;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        public BaseResponse<List<MODELPhanQuyen>> GetPhanQuyen(GetPhanQuyenRequest request)
        {
            var response = new BaseResponse<List<MODELPhanQuyen>>();
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@UserId", request.UserId)
                };
                response.Data = _unitOfWork.GetRepository<MODELPhanQuyen>().ExcuteStoredProcedure("sp_TaiKhoan_LayDanhSachPhanQuyenTheoUser", parameter).ToList();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        public BaseResponse<List<MODELMenu>> GetListMenu(GetListMenuRequest request)
        {
            var response = new BaseResponse<List<MODELMenu>>();
            try
            {
                var parameter = new[]
                {
                    new SqlParameter("@UserId", request.UserId)
                };
                response.Data = _unitOfWork.GetRepository<MODELMenu>().ExcuteStoredProcedure("sp_TaiKhoan_LayDanhSachMenu", parameter).ToList();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }
        //GET LIST PAGING
        public BaseResponse<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
        {
            BaseResponse<GetListPagingResponse> response = new BaseResponse<GetListPagingResponse>();

            try
            {
                SqlParameter iTotalRow = new SqlParameter()
                {
                    ParameterName = "@oTotalRow",
                    SqlDbType = System.Data.SqlDbType.BigInt,
                    Direction = System.Data.ParameterDirection.Output
                };


                var parameters = new[]
                {
                    new SqlParameter("@iTextSearch", request.TextSearch),
                    new SqlParameter("@iPageIndex", request.PageIndex),
                    new SqlParameter("@iRowsPerPage", request.RowPerPage),
                    iTotalRow
                };

                var result = _unitOfWork.GetRepository<MODELTaiKhoan>().ExcuteStoredProcedure("sp_TaiKhoan_GetListPaging", parameters).ToList();
                GetListPagingResponse resposeData = new GetListPagingResponse();
                resposeData.PageIndex = request.PageIndex;
                resposeData.Data = result;
                resposeData.TotalRow = Convert.ToInt32(iTotalRow.Value);
                response.Data = resposeData;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //GET BY ID (FIND)
        public BaseResponse<MODELTaiKhoan> GetById(GetByIdRequest request)
        {
            var response = new BaseResponse<MODELTaiKhoan>();
            try
            {
                var result = new MODELTaiKhoan();
                var data = _unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().Find(x => x.Id == request.Id && x.IsDeleted == false);
                if (data == null)
                {
                    throw new Exception("Không tìm thấy dữ liệu");
                }
                else
                {
                    result = _mapper.Map<MODELTaiKhoan>(data);
                    var vaiTro = _unitOfWork.GetRepository<Entity.DBContent.VAITRO>().Find(x => x.Id == result.VaiTroId);
                    result.VaiTro = vaiTro?.TenGoi;
                }
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //GET BY POST (INSERT/UPDATE)
        public BaseResponse<PostTaiKhoanRequest> GetByPost(GetByIdRequest request)
        {
            var response = new BaseResponse<PostTaiKhoanRequest>();
            try
            {
                var result = new PostTaiKhoanRequest();
                var data = _unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().Find(x => x.Id == request.Id && x.IsDeleted == false);
                if (data == null)
                {
                    result.IsEdit = false;
                }
                else
                {
                    result = _mapper.Map<PostTaiKhoanRequest>(data);
                    result.MatKhau = "@h1h1 Đồ Ngốc";
                    result.IsEdit = true;
                }

                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //GET BY USERNAME
        public BaseResponse<MODELTaiKhoan> GetByUserName(GetByUserNameRequest request)
        {
            var response = new BaseResponse<MODELTaiKhoan>();
            try
            {
                var data = _unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().Find(x => x.UserName == request.UserName);
                if (data == null)
                    throw new Exception("Không tìm thấy dữ liệu");
                else
                {
                    var dataMap = _mapper.Map<MODELTaiKhoan>(data);
                    dataMap.AnhDaiDien = string.IsNullOrWhiteSpace(data.AnhDaiDien) ? "~/images/no-image.png" : data.AnhDaiDien;
                    response.Data = dataMap;
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //INSERT
        public BaseResponse<MODELTaiKhoan> Insert(PostTaiKhoanRequest request)
        {
            //var response = new BaseResponse<MODELTaiKhoan>();
            //try
            //{
            //    var regexCheck = new Regex(@"^(?=[a-zA-Z])[-\w.\w@]{2,24}([a-zA-Z\d]|(?<![-.@])_)$");
            //    if (request.UserName.Contains("."))
            //        throw new Exception("Tài khoản không chứa chữ có dấu.");
            //    if (!regexCheck.IsMatch(request.UserName))
            //        throw new Exception("Tài khoản phải có độ dài từ 3 - 25. <br/>Bắt đầu phải là chữ. <br/>Chỉ bao gồm các ký tự .,_,-,@. <br/>Kết thúc không được là ký tự.");

            //    var data = _unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().GetAll(x => (x.UserName == request.UserName)
            //        && x.IsDeleted == false).FirstOrDefault();
            //    if (data != null)
            //    {
            //        throw new Exception("Tài khoản đã tồn tại");
            //    }          
             
            //    var add = _mapper.Map<Entity.DBContent.TAIKHOAN>(request);
            //    var salt = Encrypt_Decrypt.GenerateSalt();
            //    add.MatKhauSalt = salt;
            //    add.MatKhau = Encrypt_Decrypt.EncodePassword(request.MatKhau, salt);
            //    add.Id = Guid.NewGuid();
            //    //add.AnhDaiDien = UploadAvata(request.FolderUpload, "");
            //    //add.NguoiTao = _contextAccessor.HttpContext.User.Identity.Name == null ? "admin" : _contextAccessor.HttpContext.User.Identity.Name;
            //    //add.NgayTao = DateTime.Now;
            //    //add.NguoiSua = _contextAccessor.HttpContext.User.Identity.Name == null ? "admin" : _contextAccessor.HttpContext.User.Identity.Name;
            //    add.NgaySua = DateTime.Now;

            //    //_unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().Add(add);
            //    _unitOfWork.Commit();

            //    response.Data = _mapper.Map<MODELTaiKhoan>(add);
            //}
            //catch (Exception ex)
            //{
            //    response.Error = true;
            //    response.Message = ex.Message;
            //}

            //return response;
            throw new NotImplementedException();
        }

        //UPDATE
        public BaseResponse<MODELTaiKhoan> Update(PostTaiKhoanRequest request)
        {
            bool isLogout = false;
            var response = new BaseResponse<MODELTaiKhoan>();
            try
            {
                var update = _unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().Find(x => x.Id == request.Id && x.UserName == request.UserName);
                if (update != null)
                {
                    //NẾU CẬP NHẬT VAI TRÒ THÌ TÀI KHOẢN PHẢI LOGOUT
                    if (update.VaiTroId != request.VaiTroId)
                    {
                        isLogout = true;
                    }

                    _mapper.Map(request, update);
                    if (request.MatKhau != "@h1h1 Đồ Ngốc")
                    {
                        update.MatKhau = Encrypt_Decrypt.EncodePassword(request.MatKhau, update.MatKhauSalt);
                    }

                    //update.AnhDaiDien = UploadAvata(request.FolderUpload, update.AnhDaiDien);
                    //update.NguoiSua = _contextAccessor.HttpContext.User.Identity.Name;
                    update.NgaySua = DateTime.Now;

                    //_unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().Update(update);
                    _unitOfWork.Commit();

                    response.Data = _mapper.Map<MODELTaiKhoan>(update);
                }
                else
                {
                    throw new Exception("Không tìm thấy dữ liệu");
                }

            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            //NẾU CẬP NHẬT VAI TRÒ THÌ TÀI KHOẢN PHẢI LOGOUT
            //if (isLogout)
            //{
            //    _notificationHub.SendMessage(new MODELNotification
            //    {
            //        NguoiNhan = request.UserName,
            //        NguoiGui = _contextAccessor.HttpContext.User.Identity.Name,
            //        Action = "LOGOUT"
            //    });
            //}

            return response;
        }

        //UPDATE INFO
        public BaseResponse<bool> UpdateUserInfo(PostTaiKhoanInfoRequest request)
        {
            //var response = new BaseResponse<bool>();
            //try
            //{
            //    var data = _unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().GetAll(x => x.Id == request.Id && x.UserName == _contextAccessor.HttpContext.User.Identity.Name && x.IsDeleted == false).FirstOrDefault();

            //    if (data == null)
            //        throw new Exception("Không tìm thấy dữ liệu");



            //    var update = _unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().Find(x => x.Id == request.Id && x.UserName == "");
            //    if (update != null)
            //    {
            //        _mapper.Map(request, update);
            //        update.NgaySua = DateTime.Now;

            //        //_unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().Update(update);
            //        _unitOfWork.Commit();

            //        response.Data = true;
            //    }
            //    else
            //    {
            //        throw new Exception("Không tìm thấy dữ liệu");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    response.Error = true;
            //    response.Message = ex.Message;
            //}

            //return response;
            throw new Exception("");
        }

        //DELETE
        public BaseResponse<string> Delete(DeleteRequest request)
        {
            var response = new BaseResponse<string>();
            try
            {
                var delete = _unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().Find(x => x.Id == request.Id);
                if (delete != null)
                {
                    delete.IsDeleted = true;
                    //delete.NguoiXoa = _contextAccessor.HttpContext.User.Identity.Name;
                    delete.NgayXoa = DateTime.Now;

                    //_unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().Update(delete);
                }
                else
                {
                    throw new Exception("Không tìm thấy dữ liệu");
                }
                _unitOfWork.Commit();
                response.Data = request.Id.ToString();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //DELETE LIST
        public BaseResponse<string> DeleteList(DeleteListRequest request)
        {
            var response = new BaseResponse<string>();
            try
            {
                foreach (var id in request.Ids)
                {
                    var delete = _unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().Find(x => x.Id == id);
                    if (delete != null)
                    {
                        delete.IsDeleted = true;
                        //delete.NguoiXoa = _contextAccessor.HttpContext.User.Identity.Name;
                        delete.NgayXoa = DateTime.Now;

                        //_unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().Update(delete);
                    }
                    else
                    {
                        throw new Exception("Không tìm thấy dữ liệu");
                    }
                }
                _unitOfWork.Commit();
                response.Data = String.Join(',', request.Ids);
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        //CHANGE PASSWORD
        public BaseResponse<MODELTaiKhoan> ChangePassword(PostChangePasswordRequest request)
        {
            var response = new BaseResponse<MODELTaiKhoan>();
            try
            {
                var update = _unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().Find(x => x.Id == request.Id && x.UserName =="" && x.IsDeleted == false);
                if (update != null)
                {
                    if (!request.MatKhauMoi.Equals(request.XacNhanMatKhauMoi)) throw new Exception("Xác nhận mật khẩu mới không đúng");

                    // Nếu đổi mật khẩu thì cập nhật lại mật khẩu mới
                    var pass = Encrypt_Decrypt.EncodePassword(request.MatKhauCu, update.MatKhauSalt);
                    if (!pass.Equals(update.MatKhau)) throw new Exception("Mật khẩu cũ không đúng");

                    var salt = Encrypt_Decrypt.GenerateSalt();
                    update.MatKhauSalt = salt;
                    update.MatKhau = Encrypt_Decrypt.EncodePassword(request.MatKhauMoi, salt);
                    //update.NguoiSua = _contextAccessor.HttpContext.User.Identity.Name;
                    update.NgaySua = DateTime.Now;
                    //update.TimeChangePassword = DateTime.Now;
                    //update.IsFirstLogin = false;

                    //_unitOfWork.GetRepository<Entity.DBContent.TAIKHOAN>().Update(update);
                    _unitOfWork.Commit();

                    response.Data = _mapper.Map<MODELTaiKhoan>(update);
                }
                else
                {
                    throw new Exception("Không tìm thấy dữ liệu");
                }
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
            }

            return response;
        }

        private string UploadAvatar(string folderUpload, string oldImage)
        {
            string path = "";
            string folderUploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "Files\\Temp\\UploadFile\\" + folderUpload);
            if (Directory.Exists(folderUploadPath))
            {
                string[] arrFiles = Directory.GetFiles(folderUploadPath);
                if (arrFiles.Count() > 0) // có tệp đính kèm 
                {
                    FileInfo info = new FileInfo(arrFiles[0]);
                    string fileName = Guid.NewGuid().ToString() + info.Extension;
                    string avatarPath = Path.Combine(_webHostEnvironment.WebRootPath, "Files\\AnhDaiDien");
                    //Kiêm tra thư mục đã tồn tại chưa
                    if (!Directory.Exists(avatarPath))
                    {
                        Directory.CreateDirectory(avatarPath);
                    }
                    //Xóa ảnh cũ nếu tồn tại
                    if (File.Exists(avatarPath + "\\" + oldImage))
                    {
                        File.Delete(avatarPath + "\\" + oldImage);
                    }
                    // copy ảnh mới
                    File.Move(arrFiles[0], avatarPath + "\\" + fileName, true);
                    path = "Files\\AnhDaiDien\\" + fileName;
                }
            }

            return path;
        }

       
    
        public BaseResponse<List<MODELCombobox>> GetComboBoxNguoiQuanLy()
        {
            throw new NotImplementedException();
        }

        public BaseResponse<List<MODELCombobox>> GetAllForCombobox()
        {
            throw new NotImplementedException();
        }
    }
}

