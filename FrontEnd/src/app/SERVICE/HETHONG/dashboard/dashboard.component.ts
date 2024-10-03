import { MODELMenu } from './../../../MODEL/HETHONG/MENU/modelmenu';
import { Component, OnInit } from '@angular/core';
import { MODELNhomQuyen } from '../../../MODEL/HETHONG/NHOMQUYEN/modelnhom-quyen';
import { HeaderComponent } from "../header/header.component";
import { RouterOutlet } from '@angular/router';
import {  PanelMenuModule } from 'primeng/panelmenu';
import { SidenavComponent } from "../sidenav/sidenav.component";
import { NhomquyenComponent } from "../nhomquyen/nhomquyen.component";
import { VaitroComponent } from "../vaitro/vaitro.component";
import { BodyComponent } from '../body/body.component';
interface SideNavToggle{
  screenWith: number;
  collapsed: boolean;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [HeaderComponent, RouterOutlet, PanelMenuModule, SidenavComponent, NhomquyenComponent, VaitroComponent, BodyComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit{
  menus : MODELMenu[] = [];
  nhomQuyens : MODELNhomQuyen[] = [];
  hasPermission: boolean = false;
  menuPhanQuyen : MODELMenu [] = [];
  menuList: any;
  userName : any;
  isSideNavCollapsed = false;
  screeWidth = 0;
  ngOnInit(): void {


  }
  checkNhomQuyenId(menuId : any): boolean {
   if(this.menus.filter(x => x.nhomQuyenId === menuId).length > 0){
       return true;
   }
   return false;
  }
  onToggleSideNav(data : SideNavToggle) : void{
    this.screeWidth = data.screenWith;
    this.isSideNavCollapsed = data.collapsed;
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

  toggleSubMenu(event: Event){
    const clickedItem = (event.target as HTMLElement).closest('.item');
    if (clickedItem) {
      const subMenu = clickedItem.querySelector('.sub-menu');
      const subBtn = clickedItem.querySelector('.sub-btn');
      if (subMenu) {
        subMenu.classList.toggle('active');
      }
      if (subBtn) {
        subBtn.classList.toggle('active');
      }
    }
  }
 
}
