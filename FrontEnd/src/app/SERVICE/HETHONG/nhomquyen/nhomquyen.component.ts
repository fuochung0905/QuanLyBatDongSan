import { MODELNhomQuyen } from '../../../MODEL/HETHONG/NHOMQUYEN/modelnhom-quyen';
import { PostNhomQuyenGetListRequest } from './../../../REQUEST/HETHONG/NHOMQUYEN/post-nhom-quyen-get-list-request';
import { BaseAPIService, ResponseData } from './../../BASE/base-api.service';
import { Component, OnInit } from '@angular/core';
import { TableLazyLoadEvent, TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { ToastModule } from 'primeng/toast';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
import { ConfirmationService, LazyLoadEvent, MessageService } from 'primeng/api';
@Component({ 
  selector: 'app-nhomquyen',
  standalone: true,
  imports: [TableModule,  ToolbarModule, ToastModule, ButtonModule, CommonModule,RatingModule, FormsModule],
  providers: [MessageService, ConfirmationService],
  templateUrl: './nhomquyen.component.html',
  styleUrl: './nhomquyen.component.css'
})
export class NhomquyenComponent implements OnInit{
  request: any;
  loading: boolean = true;
  nhomQuyenResponse : MODELNhomQuyen[] = [];
  selectedNhomQuyen : MODELNhomQuyen[] = []; 


  constructor(private service: BaseAPIService){
    this.request = {
      pageIndex: 0,
      rowPerPage: 0,
      textSearch: ''
  };
  }
  ngOnInit(): void {
   
  }
  onRowSelect(event: any) {
    console.log('Row selected: ', event.data);  
  }
  refreshGrid(){

  }
  loadNhomQuyenlazy(event: TableLazyLoadEvent){
      this.loading = true;
      this.request.pageIndex = event.first;
      this.request.rowPerPage = event.rows;
        console.log(this.request);
      this.service.PostApi("NhomQuyen/get-list-paging", this.request).subscribe(
        (response: ResponseData)=>{
          if(response.status){
              this.nhomQuyenResponse = response.data as MODELNhomQuyen[];
              this.loading = false;
              console.log(this.nhomQuyenResponse);
          }
          else{
            console.log(response.message);
          }
        },
        (error)=>{
          this.loading = false;
          console.log("Lỗi hệ thống");
        }
      )
  }
  // Hàm này sẽ được gọi khi một hàng bị bỏ chọn
  onRowUnselect(event: any) {
    console.log('Row unselected: ', event.data);  // In ra hàng bị bỏ chọn
  }
 

}
