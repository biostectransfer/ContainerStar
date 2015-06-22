SELECT
 sc.column_id AS [id], 
 sc.name AS [name],
 so.object_id AS [parent_id],
 sc.user_type_id AS [type_id],
 sc.system_type_id AS [system_type_id],
 CONVERT(tinyint, CASE	WHEN sc.is_identity = 1 THEN 1
                    WHEN sc.is_computed = 1 THEN 2
                    WHEN sc.is_sparse = 1 THEN 3
                    WHEN sc.is_filestream = 1	THEN 4
                    WHEN sc.is_column_set = 1	THEN 5
                    ELSE 0 
               END) AS [type],	
 sc.max_length AS [length],
 sc.precision  AS [precision],
 sc.scale      AS [scale],
 sc.collation_name AS [collation],
 sc.is_nullable AS [is_nullable],
 sdc.[definition] AS [default], 
 CASE WHEN sc.is_identity = 1 THEN sidc.seed_value ELSE 0 END AS [seed],
 CASE WHEN sc.is_identity = 1 THEN sidc.increment_value ELSE 0 END AS [increment],
 sc.is_rowguidcol AS [is_rowguidcol],
 m.definition     AS [computed_body],
 m.is_persisted AS [is_persisted]
FROM sys.columns sc
INNER JOIN sys.objects so ON sc.object_id = so.object_id
LEFT OUTER JOIN sys.computed_columns m ON sc.object_id = m.object_id AND sc.column_id = m.column_id 
LEFT OUTER JOIN sys.identity_columns sidc ON sc.object_id = sidc.object_id AND sc.column_id = sidc.column_id
LEFT OUTER JOIN  sys.default_constraints sdc ON  sc.[object_id] = sdc.[parent_object_id] AND  sc.[column_id] = sdc.[parent_column_id]
WHERE so.type IN ('U','V') AND so.is_ms_shipped = 0
ORDER BY so.object_id, sc.column_id