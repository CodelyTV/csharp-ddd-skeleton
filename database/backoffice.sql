USE backoffice
IF (NOT EXISTS(SELECT *
               FROM INFORMATION_SCHEMA.TABLES
               WHERE TABLE_SCHEMA = 'dbo'
                 AND TABLE_NAME = 'courses'))
    BEGIN
        CREATE TABLE courses
        (
            [id]       CHAR(36)     NOT NULL,
            [name]     VARCHAR(255) NOT NULL,
            [duration] VARCHAR(255) NOT NULL,
            PRIMARY KEY ([id])
        );
    END
