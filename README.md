# Book a reading room visit

## Prerequisite

Download and install:

- Visual Studio 2019 Professional
- .NET Core 5.0
- Docker Desktop
- SQL Server Express version at least
- Azure Data Studio (for Mac users)

### Windows setup instructions
1. Open the Visual Studio 2019 Professional as "administrator" and clone the ds-book-a-reading-room-visit repository to the local folder
2. To create the KewBookings database on the local SQL database (Note, the connectionstring data source in book-a-reading-room-visit.api/appsettings.json is (localdb)\\MSSQLLocalDB, which is the default SQL Server Express data source. If you changed the default settings or use a different local data source you will have to set this in the connectionstring. Or configure a new local server called (localdb)\\MSSQLLocalDB.)
	- Set the book-a-reading-room-visit.api as the start up project (right click on project and select Set as Startup Project)
	- Open the Package Manager Console (Tools -> Nuget Package Manager -> Package Manager Console) and select book-a-reading-room-visit.data as the project as shown below
	- Run update-database command as shown below
	
![Configure Loacal Database](https://user-images.githubusercontent.com/40386980/109391838-fca45d80-7910-11eb-8263-12b71ff3287b.PNG)	

3. In SQL Server run the book-a-reading-room-visit.data\Script\look up data.sql script to populate the KewBookings tables with lookup data, e.g. seat numbers
4. In Visual Studio right click on the sln and select Properties. Set the solution to be multiple project startup and select book-a-reading-room-visit.api and book-a-reading-room-visit.web as startup projects in that order
![MultipleProjectStartup](https://user-images.githubusercontent.com/25226428/109620527-e4f7ef80-7b31-11eb-81ab-dc8d3ad3e603.png)
5. The solution should be ready to run. In VS click the Start button to run. The API will open in a browser showing the swagger page and the Web app will open in another browser window. Note the solution by default runs under IIS Express. If you try to run the projects individually before performing step 4 and run them under their project profile rather than IIS Express the launchSettings.json will run both the projects under port 5001 and you will get a port conflict. To remedy, set each project individually as startup and run them under the IIS Express profile then perform step 4 again.

### Mac setup instructions
1. Open Visual Studio 2019 for Mac and clone the ds-book-a-reading-room-visit repository
2. Download SQL Server (detailed instructions here: https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver15&pivots=cs1-bash)
	- Pull and run the SQL Server 2019 container image by running the following command in the terminal: `sudo docker pull mcr.microsoft.com/mssql/server:2019-latest`
	- To run the container image with Docker, run the following command
	`sudo docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<InsertPassword>" \ -p 1433:1433 --name sql1 -h sql1 \ -d mcr.microsoft.com/mssql/server:2019-latest`
	- To view your Docker containers, run the docker ps command `sudo docker ps -a`
	- To start a bash shell within your docker container, run `sudo docker exec -it sql1 "bash"`
	- Once inside the container, connect locally with sqlcmd by running `/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "<YourPassword>"` If successful, you should get to a sqlcmd command prompt: `1>`
	- To create the database, run the following command within the container bash shell: `CREATE DATABASE [Insert database name]`
	- An SQL database should now be set up within the docker container running the SQL Server 2019 image
	- To end your sqlcmd session, type `QUIT`
	- To exit the interactive command-prompt in your container, type `exit`. Your container continues to run after you exit the interactive bash shell
3. Run database migration
	- Install the dotnet-ef tool globally by running `dotnet tool install — global dotnet-ef` in the terminal
	- Run a database migration by running `dotnet ef migrations add ‘migration-name’`
	- Update the database by running `dotnet ef database update`
4. Download Azure Data Studio (https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio?view=sql-server-ver15)
	- Open Azure Data Studio and select localhost
	- Log in using the credentials created in step 2.5
	- Navigate to the database (localhost > Databases)
	- Click 'New Query'
	- In Visual Studio, copy and paste the contents of 2.look up data.sql (book-a-reading-room-visit.data > Script > 2.look up data.sql) into the SQLQuery_1 tab in Azure Data Studio
	- Click 'Run'
	- The database should now be populated
5. Connect to the database from within Visual Studio
	- Navigate to book-a-reading-room-visit.api > appsettings.json > appsettings.Development.json
	- Paste the following code after the Logging object on line 2:
```
"ConnectionStrings": {
    "KewBookingConnection": "Server=localhost;Database=[Insert database name];User Id = SA;Password=[Insert password];Initial Catalog = [Insert database name];"
  },
  "BookingTimeLine": {
    //in number of working days
    "AvailabilityFrom": 5,
    "AvailabilityDatesToShow": 20
  },
  "AllowedHosts": "*"
  ```
6. Setup startup projects
	- 6.1 In Visual Studio, from the toolpane, navigate to Project > Set Startup Projects
	- 6.2 Navigate to Run and click 'New'
	- 6.3 Name the startup project
	- 6.4 Double click on the newly created startup project and select book-a-reading-room-visit.api and book-a-reading-room-visit.web
	- 6.5 The solution should be ready to run. In VS click the Start button to run. The API will open in a browser showing the swagger page and the Web app will open in another browser window.
   

### Entity relationship diagram

![Kew booking system](https://user-images.githubusercontent.com/40386980/108607819-9e302a00-73ba-11eb-8f92-8999e34e7911.jpg)
