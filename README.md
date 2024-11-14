# Project: DesignCrowd

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

	GET /api/weekdays

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
1. **For more advanced testing scenarios, consider open the solution and run the automated tests wrote in xUnit with FluentAssertions.**


## Proposed Architecture

### Overview
This document outlines the architectural approach for a C# solution designed to calculate weekdays, business days, and handle complex holiday rules. The proposed layered architecture promotes modularity, testability, and maintainability.

### Layered Architecture:

The solution is structured into distinct layers:

1. Presentation Layer:
	- Handles user interaction and data presentation.
	- Can be a web application, console application, or API.
	
1. Business Logic Layer:
	- Encapsulates core business logic, including:
		- Weekday calculation
		- Business day calculation
		- Holiday rule evaluation
	- Leverages the Strategy Pattern for flexible holiday rule implementation.
3. Data Access Layer: 
	- This layer wasn't necessary because we don't have access to the database
		- But if needed will contain: Handles data persistence and retrieval, such as reading holiday data from a configuration file or database.


### Key Design Considerations:

- *Design Patterns*: Utilizes the Strategy Pattern for flexible holiday rule handling.
- *Error Handling*: Error handling mechanisms are implemented to ensure application reliability.
- *Logging*: Logging is incorporated to track application behavior and aid in troubleshooting.
- *Performance Optimization*: When data access layer will be implemented:
	- Considers performance implications and employs techniques like caching using RedisClient and asynchronous operations where necessary.

### Benefits of This Approach:

- *Modularity*: Clear separation of concerns enhances code organization and maintainability.
- *Testability*: Each layer can be tested independently, improving code quality and reducing regression risks.
- *Flexibility*: The Strategy Pattern allows for easy addition or modification of holiday rules.
- *Scalability*: The layered architecture can be scaled to accommodate growing application needs.
- *Maintainability*: Clear code structure and modular design facilitate future development and maintenance.