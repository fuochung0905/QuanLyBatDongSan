import { ResponseData } from './../../../BASE/base-api.service';
import { Component } from '@angular/core';
import { BaseAPIService } from '../../../BASE/base-api.service';
import { LoginRequest } from '../../../../REQUEST/HETHONG/TAIKHOAN/login-request';
import { MODELTaiKhoanPhanQuyen } from '../../../../MODEL/HETHONG/TAIKHOAN/modeltai-khoan-phan-quyen';

@Component({
  selector: 'app-tai-khoan',
  standalone: true,
  imports: [],
  templateUrl: './tai-khoan.component.html',
  styleUrl: './tai-khoan.component.css'
})
export class TaiKhoanComponent {
  
  public constructor(private service : BaseAPIService){
  }
  login(userName: string, password: string){
    var taiKhoan = new LoginRequest(userName, password);
       this.service.LoginApi("", taiKhoan).subscribe(
        (response: ResponseData) =>{
          if(response.status){
            console.log("API gọi thành công", response.data);
            var taiKhoan = response.data as MODELTaiKhoanPhanQuyen;
            console.log(taiKhoan);
            sessionStorage.setItem(taiKhoan.TaiKhoan?.UserName+ "_menu", JSON.stringify(taiKhoan.Menu));
            sessionStorage.setItem(taiKhoan.TaiKhoan?.UserName+ "_role", JSON.stringify(taiKhoan.PhanQuyen));
            sessionStorage.setItem(taiKhoan.TaiKhoan?.UserName+ "_info", JSON.stringify(taiKhoan.TaiKhoan));
            sessionStorage.setItem(taiKhoan.TaiKhoan?.UserName+ "_grouprole", JSON.stringify(taiKhoan.NhomQuyen));
          }
          else{
            console.log("API gọi thất bại", response.message);
          }
        },
        (error)=>{
          console.error("Lỗi hệ thống", error.message);
        } 
       );
  }
}
