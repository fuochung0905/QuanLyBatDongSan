import { MODELNhomQuyen } from '../../../MODEL/HETHONG/NHOMQUYEN/modelnhom-quyen';
import { BaseAPIService, ResponseData } from './../../BASE/base-api.service';
import { Component, OnInit } from '@angular/core';
import { TableLazyLoadEvent, TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { ToastModule } from 'primeng/toast';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogModule } from 'primeng/dialog';
import { RippleModule } from 'primeng/ripple';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { FileUploadModule } from 'primeng/fileupload';
import { DropdownModule } from 'primeng/dropdown';
import { TagModule } from 'primeng/tag';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputNumberModule } from 'primeng/inputnumber';
import { GetListPagingResponse } from '../../../RESPONSE/BASE/get-list-paging-response';
@Component({ 
  selector: 'app-nhomquyen',
  standalone: true,
  imports: [TableModule,  ToolbarModule, ToastModule, ButtonModule, CommonModule,RatingModule, FormsModule,TableModule, DialogModule, RippleModule, ButtonModule, ToastModule, ToolbarModule, ConfirmDialogModule, InputTextModule, InputTextareaModule, CommonModule, FileUploadModule, DropdownModule, TagModule, RadioButtonModule, RatingModule, InputTextModule, FormsModule, InputNumberModule],
  providers: [MessageService, ConfirmationService],
  styles: [
    `:host ::ng-deep .p-dialog .product-image {
        width: 150px;
        margin: 0 auto 2rem auto;
        display: block;
    }`
],
  templateUrl: './nhomquyen.component.html',
  styleUrl: './nhomquyen.component.css'
})
export class NhomquyenComponent implements OnInit{
  postNhomQuyenRequest! : MODELNhomQuyen;
  nhomQuyentDialog: boolean = false;
  getListPaging! : GetListPagingResponse;
  submitted: boolean = false;
  request: any;
  loading: boolean = true;
  nhomQuyenResponse : MODELNhomQuyen[] = [];
  selectedNhomQuyen : MODELNhomQuyen[] = [] ; 
  confirmationService: any;
  messageService: any;
  totalRow : number = 0;

  constructor(private service: BaseAPIService){
    this.request = {
      pageIndex: 0,
      rowPerPage: 0,
      textSearch: ''
  };
  }
  ngOnInit(): void {
   
  }
  editNhomQuyen() {
    const lastSelected = this.selectedNhomQuyen[this.selectedNhomQuyen.length - 1];
    console.log(lastSelected);
    const obj = {
      Id: lastSelected.id
  };

    this.service.PostApi("NhomQuyen/get-by-post",obj).subscribe(
      (response : ResponseData)=>{
        if(response.status){
           this.postNhomQuyenRequest = response.data as MODELNhomQuyen;
        }
        else{
          console.log(response.message);
        }
      },
      (error)=>{
        console.log("Lỗi hệ thống");
      }
     )
    this.request = {};
    this.submitted = false;
    this.nhomQuyentDialog = true;
  }
  openNew() {
    let Id: string = '00000000-0000-0000-0000-000000000000';
    const obj = {
        Id: Id
    };
    
     this.service.PostApi("NhomQuyen/get-by-post",obj).subscribe(
      (response : ResponseData)=>{
        if(response.status){
           this.postNhomQuyenRequest = response.data as MODELNhomQuyen;
           this.submitted = false;
           this.nhomQuyentDialog = true;
        }
        else{
          console.log(response.message);
        }
      },
      (error)=>{
        console.log("Lỗi hệ thống");
      }
     )
 
}

  loadNhomQuyenlazy(event: any){
      this.loading = true;
      this.request.pageIndex = event.first;
      this.request.rowPerPage = event.rows;
        console.log(this.request);
      this.service.PostApi("NhomQuyen/get-list-paging", this.request).subscribe(
        (response: ResponseData)=>{
          if(response.status){
              this.getListPaging = response.data as GetListPagingResponse;
              this.nhomQuyenResponse = this.getListPaging.data as MODELNhomQuyen[];
              this.totalRow = this.getListPaging.totalRow;
              this.loading = false;
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
  saveNhomQuyen(){
    this.submitted = true;
    const jsonString = JSON.stringify(this.postNhomQuyenRequest);
    if(this.postNhomQuyenRequest.isEdit){
      this.service.PostApi("NhomQuyen/update", jsonString).subscribe(
        (response: ResponseData)=>{
          if(response.status){
            this.loadNhomQuyenlazy(this.request);
            this.hideDialog();        
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
    else{
      this.service.PostApi("NhomQuyen/insert", jsonString).subscribe(
        (response: ResponseData)=>{
          if(response.status){
            this.loadNhomQuyenlazy(this.request);
            this.hideDialog();
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
    
  }

  hideDialog() {
    this.nhomQuyentDialog = false;
    this.submitted = false;
}




}
