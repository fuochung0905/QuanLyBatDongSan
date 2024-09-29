import { Component, NgModule, OnInit } from '@angular/core';
import { MODELMenu } from '../../../MODEL/HETHONG/MENU/modelmenu';
import { MODELNhomQuyen } from '../../../MODEL/HETHONG/NHOMQUYEN/modelnhom-quyen';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sidenav',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './sidenav.component.html',
  styleUrl: './sidenav.component.css'
})
export class SidenavComponent implements OnInit{
  collapsed = true;
  menus : MODELMenu[] = [];
  nhomQuyens : MODELNhomQuyen[] = [];
  hasPermission: boolean = false;
  menuPhanQuyen : MODELMenu [] = [];
  menuList: any;
  userName : any;
  ngOnInit(): void {
    this.getMenu();
  }
  checkNhomQuyenId(menuId : any): boolean {
   if(this.menus.filter(x => x.nhomQuyenId === menuId).length > 0){
      return true;
   }
   return false;
  }
  toggleCollapse(){
    this.collapsed = true;
  }
  closeCollapse(){
    this.collapsed = false;
  }
  getMenu(): void{
     this.userName = sessionStorage.getItem("userName");
    let menuData = sessionStorage.getItem(this.userName + "_menu");
    if(menuData){
      this.menus = JSON.parse(menuData)  as MODELMenu[];
    } 
    let nhomQuyen = sessionStorage.getItem(this.userName + "_grouprole");
    if(nhomQuyen){
      this.nhomQuyens = JSON.parse(nhomQuyen) as MODELNhomQuyen[];
    }
  }
  getMenuPhanQuyen(menuPhanQuyenId: any): MODELMenu[] {
    console.log(this.menus.filter(x => x.nhomQuyenId === menuPhanQuyenId).map(x=>x.tenGoi));
    return this.menus.filter(x => x.nhomQuyenId === menuPhanQuyenId);
  }

}
