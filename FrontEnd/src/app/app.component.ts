import { Component } from '@angular/core';
import {  RouterOutlet } from '@angular/router';
import { ReactiveFormsModule, Validators } from '@angular/forms';
import { TaiKhoanComponent } from "./SERVICE/HETHONG/TAIKHOAN/tai-khoan/tai-khoan.component";
import { DashboardComponent } from "./SERVICE/HETHONG/dashboard/dashboard.component";
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule, 
    TaiKhoanComponent,
    DashboardComponent,
    RouterOutlet

  ],
  providers: [],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'] 
})
export class AppComponent {
}
