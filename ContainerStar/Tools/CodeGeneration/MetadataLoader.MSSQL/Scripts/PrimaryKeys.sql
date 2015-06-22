SELECT
    sic.object_id AS [parent_id],
	sic.column_id AS [column_id]
FROM sys.index_columns sic
  INNER JOIN sys.objects so ON sic.object_id = so.object_id  
WHERE so.type = 'U' AND so.is_ms_shipped = 0 AND sic.index_id = 1
ORDER BY so.object_id, sic.index_column_id