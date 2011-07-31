IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ContactEntry'))
BEGIN
    DROP TABLE ContactEntry
END

CREATE TABLE ContactEntry
(
	Id integer not null IDENTITY(1,1),
	Name nvarchar(50),
	EmailAddress nvarchar(255) not null,
	[Message] nvarchar(max)
)

ALTER TABLE ContactEntry ADD PRIMARY KEY (Id)