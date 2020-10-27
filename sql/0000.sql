drop user 'heizung';
drop DATABASE Heizung;
CREATE DATABASE if not exists Heizung;

create USER 'heizung'@'%' identified by 'heizung';
grant all on Heizung.* to 'heizung'@'%' with grant option;

use Heizung;
