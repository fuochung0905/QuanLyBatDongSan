import React, { useState, useEffect } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import { Dialog, DialogActions, DialogContent, DialogTitle, Button, TextField, styled } from '@mui/material';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import { IconButton } from '@mui/material';
import api from "../../../api/apiService.js";
import { Select, MenuItem, FormControl, InputLabel } from '@mui/material';
import '../../../../public/css/vaitro.css'

function VaiTroPage() {
  const [open, setOpen] = useState(false);
  const [openInsert, setOpenInsert] = useState(false);
  const [openDeleteConfirm, setOpenDeleteConfirm] = useState(false);
  const [rowData, setRowData] = useState(null);
  const [newData, setNewData] = useState({ tenGoi: '', isActived: '' });
  const [rows, setRows] = useState([]);
  const [totalRecords, setTotalRecords] = useState(0);
  const [pageIndex, setPageIndex] = useState(0);
  const [pageSize, setPageSize] = useState(3);

  useEffect(() => {
    fetchData(pageIndex, pageSize);
  }, [pageIndex, pageSize]);

  const fetchData = (pageIndex, pageSize, textSearch = "") => {
    api
      .post('/VaiTro/get-list-paging', { pageIndex, pageSize, textSearch })
      .then((response) => {
        const dataWithSTT = response.data.result.data.map((item, index) => ({
          ...item,
          id: item.id || `${pageIndex * pageSize + index}`,
          stt: pageIndex * pageSize + index + 1,
        }));
        setRows(dataWithSTT);
        setTotalRecords(response.data.result.totalRow || 0);
      })
      .catch((error) => {
        toast.error('Không thể tải dữ liệu!');
      });
  };

  const handlePageChange = (newPage) => {
    setPageIndex(newPage);
  };

  const handlePageSizeChange = (newPageSize) => {
    setPageSize(newPageSize);
  };

  const handleEditClick = (row) => {
    setRowData(row);
    setOpen(true);
  };

  const handleDeleteClick = (row) => {
    setRowData(row);
    setOpenDeleteConfirm(true);
  };

  const handleSaveEdit = () => {
    console.log("Dữ liệu sẽ được gửi:", rowData);

    api.post('/VaiTro/update', rowData)
      .then(() => {
        toast.success('Cập nhật thành công!');
        fetchData(pageIndex, pageSize);
        setOpen(false);
      })
      .catch((error) => {
        toast.error('Cập nhật thất bại!');
        console.error(error);
      });
  };

  const handleSaveData = () => {
    api.post('/VaiTro/insert', newData)
      .then(() => {
        toast.success('Thêm dữ liệu thành công!');
        fetchData(pageIndex, pageSize);
        setOpenInsert(false);
      })
      .catch((error) => {
        toast.error('Thêm dữ liệu thất bại!');
        console.error(error);
      });
  };

  const handleDelete = () => {
    api.post('/VaiTro/delete', { id: rowData.id })
      .then(() => {
        toast.success('Xóa thành công!');
        fetchData(pageIndex, pageSize);
        setOpenDeleteConfirm(false);
      })
      .catch((error) => {
        toast.error('Xóa thất bại!');
        console.error(error);
      });
  };

  return (
    <div style={{ width: '100%' }}>
      <Dialog
        open={openInsert}
        onClose={() => setOpenInsert(false)}
        sx={{
          '& .MuiDialog-paper': {
            width: '600px',
            height: 'auto',
            maxWidth: '90%',
          },
        }}
      >
        <DialogTitle>Thêm dữ liệu</DialogTitle>
        <DialogContent>
          <TextField
            label="Tên vai trò"
            value={newData.tenGoi}
            onChange={(e) => setNewData({ ...newData, tenGoi: e.target.value })}
            fullWidth
            required
          />

          {/* Thay TextField bằng FormControl và Select cho Hoạt động */}
          <FormControl fullWidth style={{ marginTop: '16px' }} required>
            <InputLabel>Hoạt động</InputLabel>
            <Select
              value={newData.isActived}
              onChange={(e) => setNewData({ ...newData, isActived: e.target.value })}
              label="Hoạt động"
            >
              <MenuItem value={true}>Hoạt động</MenuItem>
              <MenuItem value={false}>Không hoạt động</MenuItem>
            </Select>
          </FormControl>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setOpenInsert(false)} color="secondary">Hủy</Button>
          <Button
            onClick={handleSaveData}
            color="primary"
          >
            Lưu
          </Button>
        </DialogActions>
      </Dialog>

      <Dialog
        open={open}
        onClose={() => setOpen(false)}
      >
        <DialogTitle>Chỉnh sửa dữ liệu</DialogTitle>
        <DialogContent>
          <TextField
            label="Tên gọi"
            value={rowData?.tenGoi || ''}
            onChange={(e) => setRowData({ ...rowData, tenGoi: e.target.value })}
            fullWidth
            required
          />

          <FormControl fullWidth style={{ marginTop: '16px' }} required>
            <InputLabel>Hoạt động</InputLabel>
            <Select
              value={rowData?.isActived || false}
              onChange={(e) => setRowData({ ...rowData, isActived: e.target.value })}
              label="Hoạt động"
            >
              <MenuItem value={true}>Hoạt động</MenuItem>
              <MenuItem value={false}>Không hoạt động</MenuItem>
            </Select>
          </FormControl>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setOpen(false)} color="secondary">Hủy</Button>
          <Button onClick={handleSaveEdit} color="primary">Lưu</Button>
        </DialogActions>
      </Dialog>

      <Dialog
        open={openDeleteConfirm}
        onClose={() => setOpenDeleteConfirm(false)}
      >
        <DialogTitle>Xác nhận xóa</DialogTitle>
        <DialogContent>
          Bạn có chắc chắn muốn xóa bản ghi này không?
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setOpenDeleteConfirm(false)} color="secondary">Hủy</Button>
          <Button onClick={handleDelete} color="primary">Xóa</Button>
        </DialogActions>
      </Dialog>
      <Button
        variant="contained"
        color="primary"
        onClick={() => setOpenInsert(true)}
        style={{ marginBottom: '16px' }}
      >
        Thêm dữ liệu mới
      </Button>
      <DataGrid
        rows={rows}
        columns={[
          { field: 'stt', headerName: 'STT', width: 100 },
          { field: 'tenGoi', headerName: 'Tên gọi', width: 350 },
          { field: 'nguoiTao', headerName: 'Người thành lập', width: 450 },
          {
            field: 'isActived',
            headerName: 'Hoạt động',
            width: 200,
            renderCell: (params) => (
              <span>{params.value ? "Hoạt động" : "Không hoạt động"}</span>
            ),
          },
          {
            field: 'action',
            headerName: 'Action',
            width: 250,
            renderCell: (params) => (
              <>
                <IconButton
                  color="primary"
                  onClick={() => handleEditClick(params.row)}
                  sx={{
                    marginRight: 1,
                    display: 'inline-block',
                    width: 'auto'
                  }}
                >
                  <EditIcon />
                </IconButton>
                <IconButton
                  color="secondary"
                  onClick={() => handleDeleteClick(params.row)}
                  sx={{
                    marginRight: 1,
                    display: 'inline-block',
                    width: 'auto'
                  }}
                >
                  <DeleteIcon />
                </IconButton>
              </>
            ),
          },
        ]}
        pagination
        paginationMode="server"
        rowCount={totalRecords}
        pageSize={pageSize}
        onPageChange={handlePageChange}
        onPageSizeChange={handlePageSizeChange}
        pageSizeOptions={[3, 20, 30, 100]}
      />



      {/* <table>
        <thead>
          <tr>
            <th >STT</th>
            <th>Tên gọi</th>
            <th>Người thành lập</th>
            <th>Hoạt động</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>1</td>
            <td>2</td>
            <td>3</td>
            <td>4</td>
            <td>
              <div className="action-btn">
                  <button className='btn-change'><i class="fa-solid fa-pen"></i></button>
                  <button className='btn-delete'><i class="fa-solid fa-trash"></i></button>
              </div>
              
            </td>
          </tr>
          <tr>
            <td>2</td>
            <td>Row 2, Col 2</td>
            <td>Row 2, Col 3</td>
            <td>Row 2, Col 4</td>
            <td>Row 2, Col 5</td>
          </tr>
          <tr>
            <td>3</td>
            <td>Row 3, Col 2</td>
            <td>Row 3, Col 3</td>
            <td>Row 3, Col 4</td>
            <td>Row 3, Col 5</td>
          </tr>
        </tbody>
        <tfoot>
          <tr>
            <td colspan="5">
              <ul className="pagination-bar">
                <li><a href="#">&laquo;</a></li>
                <li><a href="#" class="active">1</a></li>
                <li><a href="#">2</a></li>
                <li><a href="#">3</a></li>
                <li><a href="#">&raquo;</a></li>
              </ul>
            </td>
          </tr>
        </tfoot>
      </table> */}
    </div>
  );
}

export default VaiTroPage;
