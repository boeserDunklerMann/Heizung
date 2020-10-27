#!/bin/bash

mysql -u root -p < ./0000.sql

find ./ -maxdepth 2 -type f -name '*.sql' | sort | xargs -i ./exec.sh {}
