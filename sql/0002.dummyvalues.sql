insert into Wohnungen
values (null, 'zu Hause');	#1

insert into Raeume
values
(null, 1, 'Wohnzimmer'), #1
(null, 1, 'Schlafzimmer'), #2
(null, 1, 'Bad'), #3
(null, 1, 'Küche');	#4

insert into Messpunkte
values
(null, 1, 'HKV', null),	#1
(null, 2, 'HKV', null),	#2
(null, 3, 'HKV', null),	#3
(null, 3, 'WWZ', 'm³'),	#4
(null, 3, 'KWZ', 'm³'),	#5
(null, 4, 'HKV', null);	#6

insert into Werte (MesspunktID, Wert)
values
(1, 179),
(2, 42),
(3, 582),
(4, 25.834),
(5, 68.794),
(6, 512);
