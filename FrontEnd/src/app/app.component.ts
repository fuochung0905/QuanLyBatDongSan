import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { BaseAPIService, ResponseData } from './SERVICE/BASE/base-api.service';
import { MODELTaiKhoanPhanQuyen } from './MODEL/HETHONG/TAIKHOAN/modeltai-khoan-phan-quyen';
import { LoginRequest } from './RESPONSE/HETHONG/login-request';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
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
