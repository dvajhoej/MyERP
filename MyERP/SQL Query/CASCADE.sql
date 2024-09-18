SELECT 
    fk.name AS ForeignKeyName,
    fk.delete_referential_action_desc,
    fk.update_referential_action_desc
FROM sys.foreign_keys fk
JOIN sys.tables t ON fk.parent_object_id = t.object_id


