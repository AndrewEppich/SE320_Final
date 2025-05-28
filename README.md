## Quick Start

1. **Install prerequisites:**
   - [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
   - [MySQL Server](https://dev.mysql.com/downloads/mysql/)
   - (Optional) [Google Cloud Vision API credentials](https://cloud.google.com/vision/docs/setup)

2. **Set up the database:**
   - Create a MySQL database named `ReceiptProject`.
   - Update the connection string in `appsettings.json` with your MySQL credentials.
   - you need to update the connection string in multiple files because we did not get around to allowing it to pass values in

3. **Restore, build, and run the backend:**
   ```bash
   dotnet clean receiptProject.csproj
   dotnet build receiptProject.csproj
   dotnet run receiptProject.csproj
   ```
    in another terminal run frontend:
    cd receipt-frontend
    npm install
    npm run dev

   The API will be available at:
   - https://localhost:5173 (HTTPS) - Frontend
   - http://localhost:5000 (HTTP) - Backend (5000 does not work but 5000/api/ any of the endpoints does)
   - Swagger UI: https://localhost:5000/swagger

4. **Connect the React frontend:**
   - Ensure the frontend is running on `http://localhost:5173` 

5. **(Optional) Set up Google Cloud Vision API:**
   - Place your credentials in `receiptProjectBackend/googleVisionKey.json`.
   - this part will not work on other computers because we did not have the time to set up .env or other methods of storing the json key for the api

## Design Decisions and Architecture

This backend is built with ASP.NET Core and Entity Framework Core, using a layered architecture to separate concerns:

- **Controllers** handle HTTP requests and responses.
- **Services** encapsulate business logic, including filtering and processing receipts.
- **Data** contains the `AppDbContext` and database initialization logic.
- **Models** (in Services) represent entities such as `User`, `Receipt`, and `Summary`.

**Key design decisions:**
- **Dependency Injection** is used throughout, making the codebase modular and testable.
- **Entity Framework Core** is used for data access, with support for both MySQL and in-memory databases for testing.
- **Google Cloud Vision API** is integrated for receipt image processing, with credentials managed via configuration.
- **CORS** is configured to allow frontend development on a different port.

This structure allows for easy extension (e.g., adding new filters or summary types) and supports robust automated testing.



For the singleton pattern we implemented DbInitializer which many other files use for database access

for the Observer pattern, every time a receipt is updated it is logged in the terminal window, we did not get around to adding some other form of notification for this\

for the stretegy pattern we used a list and grid view option in the frontend to view receipts

Jira: https://andreweppich.atlassian.net/jira/software/projects/SE320/boards/38?atlOrigin=eyJpIjoiODVhYzZkMmMyMTE2NDczNzhlYmFmZmExYTU0NWUwOTIiLCJwIjoiaiJ9