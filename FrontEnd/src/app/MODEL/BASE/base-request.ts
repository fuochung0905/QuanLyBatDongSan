export class BaseRequest {
    textSearch!: string;
    folderUpload!: string;
    isActived!: string;
    isEdit!: boolean;
    tuNgay!: Date;
    denNgay!: Date;
    ngayBaoCao!: Date;
    pageIndex!: number | undefined;
    rowPerPage!: number | undefined | null;
}
