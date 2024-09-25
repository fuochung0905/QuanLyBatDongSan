import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { BaseAPIService, ResponseData } from './SERVICE/BASE/base-api.service';
import { MODELTaiKhoanPhanQuyen } from './MODEL/HETHONG/TAIKHOAN/modeltai-khoan-phan-quyen';
import { LoginRequest } from './RESPONSE/HETHONG/login-request';
import { TaiKhoanComponent } from "./SERVICE/HETHONG/TAIKHOAN/tai-khoan/tai-khoan.component";
import { DashboardComponent } from "./SERVICE/HETHONG/dashboard/dashboard.component";
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule,TaiKhoanComponent, DashboardComponent, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  
}
