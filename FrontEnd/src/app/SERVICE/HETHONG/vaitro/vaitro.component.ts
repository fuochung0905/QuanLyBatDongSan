import { GetListPagingRequest } from './../../../REQUEST/BASE/get-list-paging-request';
import { MODELVaiTro } from '../../../MODEL/HETHONG/VAITRO/modelvai-tro';
import { PostVaiTroRequest } from '../../../REQUEST/HETHONG/VAITRO/post-vai-tro-request';
import { GetListPagingResponse } from '../../../RESPONSE/BASE/get-list-paging-response';
import { BaseAPIService, ResponseData } from './../../BASE/base-api.service';
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MessageService, ConfirmationService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { FileUploadModule } from 'primeng/fileupload';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RatingModule } from 'primeng/rating';
import { RippleModule } from 'primeng/ripple';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';

@Component({
  selector: 'app-vaitro',
  standalone: true,
  imports: [TableModule,  ToolbarModule, ToastModule, ButtonModule, CommonModule,RatingModule, FormsModule,TableModule, DialogModule, RippleModule, ButtonModule, ToastModule, ToolbarModule, ConfirmDialogModule, InputTextModule, InputTextareaModule, CommonModule, FileUploadModule, DropdownModule, TagModule, RadioButtonModule, RatingModule, InputTextModule, FormsModule, InputNumberModule],
  providers: [MessageService, ConfirmationService],
  templateUrl: './vaitro.component.html',
  styleUrl: './vaitro.component.css'
})
export class VaitroComponent implements OnInit{
  GetListPaging : GetListPagingResponse | undefined;
  VaiTroResponse: MODELVaiTro[]  = [];
  selectVaiTro : MODELVaiTro[] = [];
  postVaiTroRequest: PostVaiTroRequest = new PostVaiTroRequest;
  getListPagingRequest: GetListPagingRequest = new GetListPagingRequest;
  loading : boolean = true;
  totalRow : number = 0;
  constructor(private baseApi :BaseAPIService){

  }
  ngOnInit(): void {

  }
  
  loadGetListPagingVaiTro(event : any){
      this.loading = true;
    this.getListPagingRequest.pageIndex = event.first;
    this.getListPagingRequest.rowPerPage = event.rows;
    this.baseApi.PostApi("VaiTro/get-list-paging", this.getListPagingRequest).subscribe(
      (response: ResponseData)=>{
        if(response.status){
          console.log(response);

          this.GetListPaging = response.data as GetListPagingResponse;
          console.log(this.GetListPaging);
          this.VaiTroResponse = this.GetListPaging.data as MODELVaiTro[];
          console.log(this.VaiTroResponse);
          this.totalRow = this.GetListPaging.totalRow;
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

}
