SELECT
	sep.class,
	sep.major_id,
	sep.minor_id,
	sep.name AS [name],
	sep.value AS [value] 
FROM sys.extended_properties sep
LEFT JOIN sys.objects so ON sep.class IN (1, 2, 7) AND sep.major_id = so.object_id
LEFT JOIN sys.indexes si ON sep.class = 7 AND sep.major_id = si.object_id AND sep.minor_id = si.index_id
WHERE sep.class = 1 AND so.is_ms_shipped = 0 AND so.type IN ('U','V')