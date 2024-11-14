# Project: DesignCrowd.Api

This repository contains a .NET Core application that exposes an API endpoint for calculating the number of weekdays between two dates.

## Prerequisites

* **.NET Core SDK:** Download and install the latest .NET Core SDK from https://dotnet.microsoft.com/download. Ensure you have the appropriate version matching your project's requirements.
* **Code Editor or IDE:** Visual Studio, Visual Studio Code, or any other code editor/IDE that supports .NET Core development.

## Running the Application

1. **Clone the Repository:**
	```bash
   git clone https://github.com/rdiass/DesignCrowd.git
	```
1. Navigate to DesignCrowd root folder project
1. Open the terminal
1. Restore NuGet Packages:
	```bash
	dotnet restore
	```
1. Build the Application:
	```bash
	dotnet build
	```
1. Run the Application:
	```bash
	dotnet run --project .\DesignCrowd.Api\
    ```
1. Run the tests
	```bash
	dotnet test
    ```
## Testing with Swagger

This project leverages Swashbuckle.AspNetCore to generate Swagger documentation for the API. Once you run the application, the Swagger UI will be accessible at the following URL by default:

	https://localhost:7216/swagger/index.html

The Swagger UI provides a user-friendly interface to explore the API endpoints, including the WeekdaysBetweenTwoDates method defined in the BusinessDayCounterController. You can:

* View the API documentation with detailed descriptions of the endpoint, parameters, and expected responses.
* Verify the functionality of the WeekdaysBetweenTwoDates endpoint by providing two dates and checking the returned number of weekdays.

API Documentation for **WeekdaysBetweenTwoDates** 

### Endpoint:

	GET /BusinessDayCounter/WeekdaysBetweenTwoDates

### Parameters:

	1. firstDate (query): The first date in the range (DateTime).
	2. secondDate (query): The second date in the range (DateTime).

### Response:

1. *200 OK*: Returns the number of weekdays between the provided dates as an integer.
1. *400 BadRequest*: Indicates an invalid date range (second date is before or equal to the first date).
1. *500 InternalServerError*: Indicates an unexpected error occurred while processing the request.


## Example Usage in Swagger

1. Open the Swagger UI at the URL mentioned above.
1. Locate the BusinessDayCounter section and expand it.
1. Click on the WeekdaysBetweenTwoDates operation.
1. Enter valid dates for firstDate and secondDate in the corresponding fields. For example:

```bash
firstDate: 2024-11-15
secondDate: 2024-11-22
```

5. Click the "Try it out" button.
1. Observe the response body, which should display the number of weekdays between the provided dates.

## Additional Notes
1. This document provides a basic overview of running and testing the API. Refer to the project code and any additional documentation for detailed information about the implementation and configuration.
1. For more advanced testing scenarios, consider open the solution and run the automated tests wrote in xUnit with FluentAssertions.