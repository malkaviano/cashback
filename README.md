# cashback
Cashback API - Dev Test.

This is a programming test, do not copy use it for learning only.

Deploy:
- Copy Dockerfile, docker-compose.yml and DockerMigrations outside the project folder (global.json conflict)
- Run docker-compose build && docker-compose up -d (migrations may take some time to finish)

Some Endpoins require CPF others not (look for logged user), this is intended since the test did not specified about it, so I did both cases

How to use:
- Add a reseller user
- Login with that user and get a token
- Consume Sales and Cashback endpoints