import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MODELMenu } from '../../../MODEL/HETHONG/MENU/modelmenu';
import { MODELNhomQuyen } from '../../../MODEL/HETHONG/NHOMQUYEN/modelnhom-quyen';
import { CommonModule } from '@angular/common';
import { RouterLinkActive } from '@angular/router';


  interface SideNavToggle{
    screenWith: number;
    collapsed: boolean;
  }
@Component({
  selector: 'app-sidenav',
  standalone: true,
  imports: [CommonModule, RouterLinkActive],
  templateUrl: './sidenav.component.html',
  styleUrl: './sidenav.component.css'
})
export class SidenavComponent implements OnInit{
  @Output() onToggleSideNav: EventEmitter<SideNavToggle> = new EventEmitter();
  collapsed = false;
  screeWidth = 0;
  menus : MODELMenu[] = [];
  nhomQuyens : MODELNhomQuyen[] = [];
  hasPermission: boolean = false;
  menuPhanQuyen : MODELMenu [] = [];
  menuList: any;
  userName : any;
  ngOnInit(): void {
    this.screeWidth = window.innerWidth;
    console.log(this.screeWidth);
    this.getMenu();
  }

 
  
  checkNhomQuyenId(menuId : any): boolean {
   if(this.menus.filter(x => x.nhomQuyenId === menuId).length > 0){
      return true;
   }
   return false;
  }
  toggleCollapse(){
    this.collapsed = !this.collapsed;
    this.onToggleSideNav.emit({collapsed: this.collapsed, screenWith: this.screeWidth});
  }
  closeCollapse(){
    this.collapsed = false;
    this.onToggleSideNav.emit({collapsed: this.collapsed, screenWith: this.screeWidth});
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
