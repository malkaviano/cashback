# cashback
Cashback API - Dev Test.

This is a programming test, do not copy, use it for learning only. Ofc you can do whatever you want, it's just a TIP.

Deploy:
- Copy Dockerfile, docker-compose.yml and DockerMigrations outside the project folder (global.json conflict)
- Run docker-compose build && docker-compose up -d (migrations may take some time to finish)

Some Endpoins require CPF others not (look for logged user), this is intended since the test was not specific about it, so I did both cases

How to use:
- Add a reseller user
- Login with that user and get a token
- Consume Sales and Cashback endpoints

Documentation:
- Root path will show the swagger

Tests:
- For now only relevant parts of Domain have unit tests
- Using xUnit and Moq

About:

This project was made in VSCode and Ubuntu 18.

Developed with .net core 2.2 with EF core and migrations

If you have any problems using it in Visual Studio, plz open a ticket.

TODO:
- Better error handling
- Refactor some code
- Improve Tests
