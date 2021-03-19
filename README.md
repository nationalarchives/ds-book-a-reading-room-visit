# Book a reading room visit

## Infrastructure diagram
![KBS infrastructure](https://user-images.githubusercontent.com/40386980/110319498-e5e3c200-8006-11eb-9763-73cc9a394ea6.jpg)


## Prerequisite

Download and install:

- Visual Studio 2019 Professional
- .NET Core 5.0
- Docker Desktop
- SQL Server Express version at least

### Windows setup instructions
1. Open the Visual Studio 2019 Professional as "administrator" and clone the ds-book-a-reading-room-visit repository to the local folder
2. To create the KewBookings database on your local machine
	run the following command 
	`docker run -p 1433:1433 -d docker4gnanesh/kbs-db:1`
	and update the connection string in the launchsettings.json with the following value
	`"KewBookingConnection": "Data Source=localhost,1433;Initial Catalog=KewBooking;User ID=sa;Password=kbstr0ng(!)Passw0rd;"`
3. In Visual Studio right click on the sln and select Properties. Set the solution to be multiple project startup and select book-a-reading-room-visit.api and book-a-reading-room-visit.web as startup projects in that order
![MultipleProjectStartup](https://user-images.githubusercontent.com/25226428/109620527-e4f7ef80-7b31-11eb-81ab-dc8d3ad3e603.png)
4. The solution should be ready to run. In VS click the Start button to run. The API will open in a browser showing the swagger page and the Web app will open in another browser window. Note the solution by default runs under IIS Express. If you try to run the projects individually before performing step 4 and run them under their project profile rather than IIS Express the launchSettings.json will run both the projects under port 5001 and you will get a port conflict. To remedy, set each project individually as startup and run them under the IIS Express profile then perform step 4 again.

### Dummy data setup instructions
The application hooks into existing APIs to get "onprem" data, such as reader's ticket data and DSD holidays. By default the code is set to get the data through the APIs hosted on the on-prem application servers, which for security reasons are only available from the TNA network so not available from home etc. The dummy data and API for DSD holidays has been created in a docker image on a public docker hub. If working offsite you will have to follow these steps to run the application locally.
1. Install Docker on your device
2. Run this script from a command prompt </br>
	  docker run -d -p 8086:80 docker4gnanesh/kbs-dummy:1 </br>
This will download and install the image on your local Docker. Note, when first run you will initially get the message "Unable to find image 'docker4gnanesh/kbs-dummy:1' locally", which is expected and after a few seconds the command will start pulling the image down.
3. Set the book-a-reading-room-visit.api to use the image . In launchSettings.json under Properties set "RecordCopying_WebApi_URL": "http://localhost:8086/home/" for the profile you are using, most likely the IIS Express profile.
![Screenshot 2021-03-08 160730](https://user-images.githubusercontent.com/25226428/110347459-8dbdb780-8028-11eb-801f-f0389c8323c6.png)




### Entity relationship diagram

![Kew booking system](https://user-images.githubusercontent.com/40386980/110785186-8da8fc00-8262-11eb-98e3-d6b74c9b897e.jpg)
