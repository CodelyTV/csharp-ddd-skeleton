CREATE TABLE courses (
  [id] CHAR(36) NOT NULL,
  [name] VARCHAR(255) NOT NULL,
  [duration] VARCHAR(255) NOT NULL,
  PRIMARY KEY ([id])
)  ;

CREATE TABLE courses_counter (
    [id] CHAR(36) NOT NULL,
    [total] INT NOT NULL,
    [existing_courses] nvarchar(max) NOT NULL,
    PRIMARY KEY ([id])
)  ;