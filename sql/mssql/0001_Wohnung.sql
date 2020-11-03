CREATE TABLE [dbo].Wohnungen
(
	WohnungId INT identity NOT NULL PRIMARY KEY,
	Bez nvarchar(500) not null,
	constraint PK_Wohnungen
		primary key(WohnungID)
)
