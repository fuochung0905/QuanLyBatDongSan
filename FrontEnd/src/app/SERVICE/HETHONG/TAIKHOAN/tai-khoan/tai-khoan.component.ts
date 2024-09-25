import { ResponseData } from './../../../BASE/base-api.service';
import { Component } from '@angular/core';
import { BaseAPIService } from '../../../BASE/base-api.service';
import { LoginRequest } from '../../../../REQUEST/HETHONG/TAIKHOAN/login-request';
import { MODELTaiKhoanPhanQuyen } from '../../../../MODEL/HETHONG/TAIKHOAN/modeltai-khoan-phan-quyen';
import { Router, RouterOutlet } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DashboardComponent } from '../../dashboard/dashboard.component';

@Component({
  selector: 'app-tai-khoan',
  standalone: true,
  imports: [RouterOutlet, ReactiveFormsModule,DashboardComponent],
  templateUrl: './tai-khoan.component.html',
  styleUrl: './tai-khoan.component.css'
})
export class TaiKhoanComponent {
  loginForm!:FormGroup;
  constructor(private service: BaseAPIService,
              private fb:FormBuilder,
              private router:Router){
  }
  ngOnInit(){
    this.loginForm= new FormGroup({
      username:new FormControl(),
    password:new FormControl()
    });
    this.loginForm=this.fb.group({
    username:  ['',Validators.required],
    password:  ['',Validators.required]
    });
  }
  login(){
    const userName = this.loginForm.get('username')!.value;
    const password = this.loginForm.get('password')!.value;
    var taiKhoan = new LoginRequest(userName, password);
      this.service.LoginApi("taiKhoan/login", taiKhoan).subscribe(
        (response: ResponseData) =>{
          if(response.status){
            console.log("API gọi thành công", response.data);
            var taiKhoan = response.data as MODELTaiKhoanPhanQuyen;
            console.log(taiKhoan);
            sessionStorage.clear();
            if (taiKhoan?.taiKhoan?.userName) {
              sessionStorage.setItem("userName", taiKhoan.taiKhoan.userName.toString());
            } else {
              console.error("UserName không có giá trị hợp lệ");
            }
            sessionStorage.setItem(taiKhoan.taiKhoan?.userName+ "_menu", JSON.stringify(taiKhoan.menu));
            sessionStorage.setItem(taiKhoan.taiKhoan?.userName+ "_role", JSON.stringify(taiKhoan.phanQuyen));
            sessionStorage.setItem(taiKhoan.taiKhoan?.userName+ "_info", JSON.stringify(taiKhoan.taiKhoan));
            sessionStorage.setItem(taiKhoan.taiKhoan?.userName+ "_grouprole", JSON.stringify(taiKhoan.nhomQuyen));
            this.service.setCurrentUser(taiKhoan.taiKhoan?.userName);
            this.router.navigate(['/Home']);
            console.log("Đã vào");
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
