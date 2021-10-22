# Book a reading room visit

## Infrastructure diagram
![KBS infrastructure](https://user-images.githubusercontent.com/40386980/110319498-e5e3c200-8006-11eb-9763-73cc9a394ea6.jpg)


## Prerequisite

Download and install:

- Visual Studio 2019 Professional
- .NET Core 5.0
- Docker Desktop
- SQL Server Express version at least
- Azure Data Studio (for Mac users)

### Windows setup instructions
1. Open the Visual Studio 2019 Professional as "administrator" and clone the ds-book-a-reading-room-visit repository to the local folder
2. To create the KewBookings database on your local machine

	run the following command 
	`docker run -p 1433:1433 -d docker4gnanesh/kbs-db:2`

	and update the connection string in the API launchsettings.json (under Properties) with the following value
	`"KewBookingConnection": "Data Source=localhost,1433;Initial Catalog=KewBooking;User ID=sa;Password=kbstr0ng(!)Passw0rd;"`
	
3. In Visual Studio right click on the sln and select Properties. Set the solution to be multiple project startup and select book-a-reading-room-visit.api and book-a-reading-room-visit.web as startup projects in that order
![MultipleProjectStartup](https://user-images.githubusercontent.com/25226428/109620527-e4f7ef80-7b31-11eb-81ab-dc8d3ad3e603.png)
4. The solution should be ready to run. In VS click the Start button to run. The API will open in a browser showing the swagger page and the Web app will open in another browser window. Note the solution by default runs under IIS Express. If you try to run the projects individually before performing step 4 and run them under their project profile rather than IIS Express the launchSettings.json will run both the projects under port 5001 and you will get a port conflict. To remedy, set each project individually as startup and run them under the IIS Express profile then perform step 4 again.


### Mac setup instructions
1. Open Visual Studio 2019 for Mac and clone the ds-book-a-reading-room-visit repository
2. Download SQL Server (detailed instructions here: https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver15&pivots=cs1-bash)
	- Pull and run the SQL Server 2019 container image by running the following command in the terminal: `sudo docker pull mcr.microsoft.com/mssql/server:2019-latest`
	- To run the container image with Docker, run the following command (replacing `<InsertPassword>` with your password)
	`sudo docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<InsertPassword>" \ -p 1433:1433 --name sql1 -h sql1 \ -d mcr.microsoft.com/mssql/server:2019-latest`
	- To view your Docker containers, run the docker ps command `sudo docker ps -a`
	- To start a bash shell within your docker container, run `sudo docker exec -it sql1 "bash"`
	- Once inside the container, connect locally with sqlcmd by running `/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "<YourPassword>"` If successful, you should get to a sqlcmd command prompt: `1>`
	- To create the database, run the following command within the container bash shell: `CREATE DATABASE [Insert database name]`
	- An SQL database should now be set up within the docker container running the SQL Server 2019 image
	- To end your sqlcmd session, type `QUIT`
	- To exit the interactive command-prompt in your container, type `exit`. Your container continues to run after you exit the interactive bash shell
3. Run database migration
	- Download Azure Data Studio (https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio?view=sql-server-ver15)
	- Open Azure Data Studio and select 'New connection'
	- In the Server field, enter `localhost`
	- In the Username field, enter `SA`
	- In the Password field, enter the password you entered when you created the docker image
	- Click 'Connect'
	- Navigate to your database on the lefthand pane (localhost > Databases > [Your database])
	- Right click on [Your database]
	- Click 'New Query'
	- In Visual Studio, copy and paste the contents of 1.table.sql (book-a-reading-room-visit.data > Script > 1.table.sql) into the query tab in Azure Data Studio
	- Click 'Run'
	- The database should now have the necessary tables (now they need to be populated)
	- Click 'New Query'
	- In Visual Studio, copy and paste the contents of 2.look up data.sql (book-a-reading-room-visit.data > Script > 2.look up data.sql) into the query tab in Azure Data Studio
	- Click 'Run'
	- The database should now be populated
4. Connect to the database from within Visual Studio
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
5. Setup startup projects
	- In Visual Studio, from the toolpane, navigate to Project > Set Startup Projects
	- Navigate to Run and click 'New'
	- Name the startup project
	- Double click on the newly created startup project and select book-a-reading-room-visit.api and book-a-reading-room-visit.web
	- The solution should be ready to run. In VS click the Start button to run. The API will open in a browser showing the swagger page and the Web app will open in another browser window.
6. Additional steps for Mac users
	- book-a-reading-room-visit.api changes
		- launchSettings.json (book-a-reading-room-visit.api > Properties > launchSettings.json) 
			- Change the values of all instances of `RecordCopying_WebApi_URL` to `http://localhost:8086/home/`
		- Startup.cs (book-a-reading-room-visit.api > Startup.cs) 
			- Comment out `services.AddDefaultAWSOptions(Configuration.GetAWSOptions())` on line 29 and `services.AddDataProtection().PersistKeysToAWSSystemsManager("/KBS-API/DataProtection")` on line 30
	- book-a-reading-room-visit.web changes
		- BookingController.cs (book-a-reading-room-visit.web > Controllers > BookingController.cs)
			- Comment out `private IAdvancedOrderService _advancedOrderService` on line 17, `_advancedOrderService = channelFactory.CreateChannel()` on line 24, and `var visitorDetails = _advancedOrderService.GetVisitorDetailsByTicketNo(bookingViewModel.ReadingTicket.ToString())` to `bookingViewModel.Phone = visitorDetails.Phone` on lines 67 - 80
			- Remove the `ChannelFactory<IAdvancedOrderService> channelFactory` parameter from the BookingController method on line 20
		- launchSettings.json (book-a-reading-room-visit.web > Properties > launchSettings.json) 
			- Change the values of all instances of `KBS_Root_Path` to "" (empty string)
		- Startup.cs (book-a-reading-room-visit.web > Startup.cs)
			- Comment out `services.AddDefaultAWSOptions(Configuration.GetAWSOptions())` on line 35 and `services.AddDataProtection().PersistKeysToAWSSystemsManager("/KBS-API/DataProtection")` on line 36

### Dummy data setup instructions
The application hooks into existing APIs to get "onprem" data, such as reader's ticket data and DSD holidays. By default the code is set to get the data through the APIs hosted on the on-prem application servers, which for security reasons are only available from the TNA network so not available from home etc. The dummy data and API for DSD holidays has been created in a docker image on a public docker hub. If working offsite you will have to follow these steps to run the application locally.
1. Install Docker on your device
2. Run this script from a command prompt </br>
	  docker run -d -p 8086:80 docker4gnanesh/kbs-dummy:1 </br>
This will download and install the image on your local Docker. Note, when first run you will initially get the message "Unable to find image 'docker4gnanesh/kbs-dummy:1' locally", which is expected and after a few seconds the command will start pulling the image down.
3. Set the book-a-reading-room-visit.api to use the image . In launchSettings.json under Properties set "RecordCopying_WebApi_URL": "http://localhost:8086/home/" for the profile you are using, most likely the IIS Express profile.
![Screenshot 2021-03-08 160730](https://user-images.githubusercontent.com/25226428/110347459-8dbdb780-8028-11eb-801f-f0389c8323c6.png)




### Entity relationship diagram

![Kew booking system](https://user-images.githubusercontent.com/40386980/113402255-a22d6f80-939c-11eb-8e5c-01ca9010248f.jpg)
