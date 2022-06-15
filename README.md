
# Tripper Web API

Tripper is an application that enables the user to discover new interesting places
around him. By using the application, the customer can quickly and efficiently while in a new place
check interesting things to do in his actual area, how quickly he can get to these places (both on foot and by car), and
what is the weather in these places at the moment.

 The application also allows to add new places by owners or local guides. Each user can review a visited place, add some photos and rate it.




## Technologies

- ASP.NET Core 5
- Microsoft SQL Server
- Entity Framework
- FluentEmail
- Microsoft Bing Maps Matrix
- Open Weather



## Feautures

- User authentication via JWT tokens.
- Authorization by role
- Error handling/logging
- Sending confirmation/password reset emails

## API documentation
The API documentation is available at `~api/swagger`
## Installation
To run this application, change the credentials in `appsettings.development.json`, variables to change are:
`BingMapsKey`
`WeatherApiKey`
`Gmail`
`ConnectionStrings`
It is possible to deploy the app on any server, or run it locally. 
