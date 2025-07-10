1. Make sure you have docker compose.yml set as a startup project
2. In order to run the project, you set up Products as startup project
3. then update database with existing migrations
4. use command : EntityFrameworkCore\update-database
5. Then revert the startup project to docker compose.yml