# Book a reading room visit

## Prerequisite

Download and install:

- Visual Studio 2019 Professional
- .NET Core 5.0
- Docker Desktop
- SQL Server Express version at least

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



### Entity relationship diagram

![Kew booking system](https://user-images.githubusercontent.com/40386980/108607819-9e302a00-73ba-11eb-8f92-8999e34e7911.jpg)
