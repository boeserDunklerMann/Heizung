#!/bin/bash

echo $1
if [ "$1" != "./0000.sql" ]
    then
     mysql -u heizung -pheizung Heizung < $1
fi