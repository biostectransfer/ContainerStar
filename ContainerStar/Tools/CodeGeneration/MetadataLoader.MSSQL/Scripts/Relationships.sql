SELECT 
	sfkc.constraint_object_id AS [id],
	sfkc.parent_object_id     AS [from_table_id],
	sfkc.parent_column_id     AS [from_column_id],
	sfkc.referenced_object_id AS [to_table_id],
	sfkc.referenced_column_id AS [to_column_id]
FROM sys.foreign_key_columns sfkc
  INNER JOIN sys.objects so ON sfkc.parent_object_id = so.object_id
WHERE so.is_ms_shipped = 0 AND so.type = 'U'
ORDER BY sfkc.constraint_object_id, sfkc.constraint_column_id