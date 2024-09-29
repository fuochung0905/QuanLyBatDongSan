USE [DoAnProject1]
GO
/****** Object:  StoredProcedure [dbo].[sp_DM_DONVI_GetListPaging]    Script Date: 9/27/2024 2:45:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_HT_NhomQuyen_GetListPaging]
	@iTextSearch NVARCHAR(4000),
	@iPageIndex INT,
	@iRowsPerPage INT,
	@oTotalRow	BIGINT OUTPUT
AS
BEGIN

	SELECT *
	INTO #TEMP
	FROM PHANQUYEN_NHOMQUYEN
	WHERE 
	 (ISNULL(@iTextSearch, '') = '' OR 
		TenGoi LIKE N'%' + @iTextSearch + '%' 
	)
    SET @oTotalRow = (SELECT COUNT(*) FROM #TEMP)
	SELECT *
	FROM #TEMP 
	ORDER BY Sort DESC
	OFFSET @iPageIndex * @iRowsPerPage  ROWS 
	FETCH NEXT @iRowsPerPage ROWS ONLY 
END

USE [DoAnProject1]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[sp_TaiKhoan_LayDanhSachMenu]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN
	
	SET NOCOUNT ON;

  
	SELECT SM.*, NQ.TenGoi AS TenNhom, NQ.Sort AS NhomSort
	FROM SYS_MENU SM
	INNER JOIN PHANQUYEN_NHOMQUYEN NQ ON NQ.Id = SM.NhomQuyenId AND NQ.IsActived = 1
	INNER JOIN PHANQUYEN Q ON Q.ControllerName = SM.ControllerName
	INNER JOIN VAITRO VT ON VT.Id = Q.VaiTroId AND VT.IsDeleted = 0 AND VT.IsActived = 1
	INNER JOIN TAIKHOAN TK ON TK.VaiTroId = VT.Id
	WHERE TK.Id = @UserId AND Q.IsXem = 1 AND SM.IsActived = 1
END

====================================================================================================================

USE [DoAnProject1]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TaiKhoan_LayDanhSachPhanQuyenTheoUser]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN
	
	SET NOCOUNT ON;


	DECLARE @VaiTroId UNIQUEIDENTIFIER
	SELECT @VaiTroId = VaiTroId FROM TAIKHOAN WHERE Id = @UserId
	SELECT QU.ControllerName, ISNULL(PQ.IsXem, 0) AS IsXem
	, ISNULL(PQ.IsCapNhat, 0) AS IsCapNhat, ISNULL(PQ.IsXoa, 0) AS IsXoa
	, ISNULL(PQ.IsDuyet, 0) AS IsDuyet, ISNULL(PQ.IsThongKe, 0) AS IsThongKe
	FROM SYS_MENU QU
	INNER JOIN PHANQUYEN_NHOMQUYEN NQ ON QU.NhomQuyenId = NQ.Id AND NQ.IsActived = 1
	LEFT JOIN PHANQUYEN PQ ON PQ.ControllerName = QU.ControllerName AND PQ.VaiTroId = @VaiTroId
	WHERE QU.IsActived = 1
END
===========================================================================================================================

USE [DoAnProject1]
GO
/****** Object:  StoredProcedure [dbo].[sp_USER_GetListPaging]    Script Date: 9/26/2024 1:06:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_USER_GetListPaging]

	@iPageIndex INT,
	@iRowsPerPage INT,
	
	@oTotalRow	BIGINT OUTPUT
AS
BEGIN

	SELECT	*
	INTO #TEMP
	FROM [User] U 
	WHERE ISNULL(U.IsDelete,0)=0


    SET @oTotalRow = (SELECT COUNT(*) FROM #TEMP)

	SELECT *
	FROM #TEMP 
	ORDER BY NgaySua DESC,NgayTao DESC
	OFFSET @iPageIndex * @iRowsPerPage  ROWS 
	FETCH NEXT @iRowsPerPage ROWS ONLY 
	
END
