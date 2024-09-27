import { BaseRequest } from "../../BASE/base-request";

export class MODELNhomQuyen extends BaseRequest{
    id!: string | null;
    tenGoi!: string | null;
    icon!: string | null;
    sort!: number | null;
}
