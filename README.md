# ErrandPayAPI

Step 1: Clone the Repository
If the existing application is hosted in a version control system like Git, clone the repository to your local machine.
git clone https://github.com/patreecio1/Usser-Management-Errandpay-API.git

Step 2: Open Solution in Visual Studio
Open the solution file (.sln) of your existing ErrandPayAPI project in Visual Studio.

Step 3: Update Connection String
Locate the appsettings.json file in your project. Update the connection string in this file with the appropriate database connection details.

{
  "ConnectionStrings": {
    "DefaultConnection": "Your_Connection_String_Here"
  }
}
Step 4: Update Database
Open Package Manager Console and set the default project to your Data project. Then, run the Update-Database command to apply any pending migrations.

Step 5: Run the Application
Build the solution and run the ErrandPayAPI project. You can do this by pressing F5 or using the "Start" button in Visual Studio.

Step 6: Test the Application
Once the application is running, test its functionality to ensure that it's working as expected. You can use tools like Postman or Swagger UI to send requests to the API endpoints and verify the responses.
