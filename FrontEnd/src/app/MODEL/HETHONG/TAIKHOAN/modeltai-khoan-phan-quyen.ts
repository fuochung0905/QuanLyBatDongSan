import { MODELPhanQuyen } from "./modelphan-quyen";
import { MODELTaiKhoan } from "./modeltai-khoan";

export class MODELTaiKhoanPhanQuyen {
    TaiKhoan: MODELTaiKhoan | null = new MODELTaiKhoan;
    Menu: MODELMenuLogin[] = [];
    PhanQuyen: MODELPhanQuyen[] = [];
    NhomQuyen: MODELNhomQuyenLogin[] = [];
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
