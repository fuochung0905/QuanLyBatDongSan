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
