# Book a reading room visit

## Prerequisite

Download and install:

- Visual Studio 2019 Professional
- .NET Core 5.0
- Docker Desktop

### Windows setup instructions
1. Open the Visual Studio 2019 Professional as "administrator" and clone the ds-book-a-reading-room-visit repository to the local folder
2. To configure the local SQL database
	- Set the book-a-reading-room-visit.api as the start up project
	- Open the Package Manager Console (Tools -> Nuget Package Manager -> Package Manager Console) and select book-a-reading-room-visit.data as the project as shown below
	- Run update-database command as shown below
	
![Configure Loacal Database](https://user-images.githubusercontent.com/40386980/109391838-fca45d80-7910-11eb-8263-12b71ff3287b.PNG)	

### Entity relationship diagram

![Kew booking system](https://user-images.githubusercontent.com/40386980/108607819-9e302a00-73ba-11eb-8f92-8999e34e7911.jpg)
