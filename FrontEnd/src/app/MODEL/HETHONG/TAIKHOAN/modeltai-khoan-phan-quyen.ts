import { MODELPhanQuyen } from "./modelphan-quyen";
import { MODELTaiKhoan } from "./modeltai-khoan";

export class MODELTaiKhoanPhanQuyen {
    taiKhoan: MODELTaiKhoan | null = new MODELTaiKhoan;
    menu: MODELMenuLogin[] = [];
    phanQuyen: MODELPhanQuyen[] = [];
    nhomQuyen: MODELNhomQuyenLogin[] = [];
}
export class MODELMenuLogin {
    ControllerName!: string | null;
    Action!: string | null;
    TenGoi!: string | null;
    NhomQuyenId!: string | null;
    Sort!: number | null;
}
export class MODELNhomQuyenLogin{
    Id!: string | null;
    TenGoi!: string | null;
    Sort!: number | null;
    Icon!: string | null;
}
