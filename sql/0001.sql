create table Wohnungen
(
	WohnungID	int not null auto_increment,
	Bez			varchar(500) character set 'utf8' not null,
	constraint PK_Wohnungen
		primary key(WohnungID)
) Engine=InnoDB;

create table Raeume
(
	RaumID		int not null auto_increment,
	WohnungID	int not null,
	Bez			varchar(200) character set 'utf8' not null,
	constraint PK_Raeume
		primary key(RaumID),
	constraint FK_Raeume_Wohnungen
		foreign key(WohnungID)
		references Wohnungen(WohnungID)
) Engine=InnoDB;

create table Messpunkte
(
	MesspunktID	int not null auto_increment,
	RaumID		int not null,
	Bez			varchar(300) character set 'utf8' not null,	# Heizkostenverteiler, Wasseruhr
	Einheit		varchar(10) character set 'utf8' null,	# die Maﬂeinheit, wenn gegeben, in welcher der Messpunkt misst (m≥, ...)
	constraint PK_Messpunkte
		primary key(MesspunktID),
	constraint FK_Messpunkte_Raeume
		foreign key(RaumID)
		references Raeume(RaumID)
) Engine=InnoDB;

create table Werte
(
	WertID		int not null auto_increment,
	MesspunktID	int not null,
	Stamp		timestamp not null,
	Wert		decimal(10,3) not null,
	constraint PK_Werte
		primary key(WertID),
	constraint FK_Werte_Messpunkte
		foreign key(MesspunktID)
		references Messpunkte(MesspunktID)
)Engine=InnoDB;
