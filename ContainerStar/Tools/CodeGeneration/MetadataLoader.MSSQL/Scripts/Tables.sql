SELECT 
    so.object_id                                            AS [id],
    so.name                                                 AS [name],
    SCHEMA_NAME(so.schema_id)                               AS [schema],
    CONVERT(bit, CASE WHEN so.type = 'V' THEN 1 ELSE 0 END) AS [is_view]
FROM sys.objects so
WHERE so.type IN ('U','V') AND so.is_ms_shipped = 0