IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'mooc')
BEGIN
    CREATE DATABASE mooc;
END
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'backoffice')
BEGIN
    CREATE DATABASE backoffice;
END


