# couponsltd-server

## Tech 
- .NET Core 3.1
- Hooked  up to MSSQL DB

## Installation
To set up Api locally you need a MSSQL server instance with new dedicated db 
- log in to your instance and use Windows Authentication 
- add new db eg. : lcl-coupons
- create tbales using script from 
- couponsltd-server\CouponsLtd\Data\Scripts\dbSchema.sql

## Envirenments
- there is a DEV api set up on azure 
https://dev-api.trident-ww.com/swagger/index.html

## Usage

- Mock data for user and coupons can be find in and DEV db is already populated with it 
couponsltd-server\CouponsLtd\Data\Mocks\

- Eg. User:
login: test
pwd: test

- Eg Coupons:\
{"Name":"wunderground.com","Description":"ipsum praesent blandit lacinia erat vestibulum","Code":"36987-1063"}, \
{"Name":"histats.com","Description":"vel nisl duis ac nibh fusce lacus purus","Code":"68001-115"},\
{"Name":"simplemachines.org","Description":"sit amet lobortis sapien","Code":"65076-0001"},\
{"Name":"github.io","Description":"bibendum morbi non quam nec dui luctus rutrum nulla tellus","Code":"50268-752"},\


