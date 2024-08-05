C# course

### How to run?
1. go to the folder Task1/ConsoleApp1
2. run `dotnet run`


### How to install a new package?
	dotnet add package Newtonsoft.Json

	Install-Package Newtonsoft.Json


### Config File Example
Create 'appsettings.json` file at the root of the project

		{
			"ConnectionStrings": {
				"DefaultConnection": "..."
			},
			"DatabaseInfo": {
				"Catalog": "...",
				"Schema": "...",
				"DB": "..."
			}
		}
