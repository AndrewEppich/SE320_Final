# Receipt Project Backend

This is the .NET backend API for the Receipt Project.

## Prerequisites

- .NET 8.0 SDK
- MySQL Server (local or remote)
- A React frontend running on `http://localhost:3000`

## Database Setup

1. Create a MySQL database named `ReceiptProject`

2. Update the connection string in `appsettings.json` with your MySQL credentials:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ReceiptProject;User=YOUR_USERNAME;Password=YOUR_PASSWORD;Port=3306;"
}
```

## Running the Application

1. Navigate to the project directory:
```bash
cd receiptProject
```

2. Restore the dependencies:
```bash
dotnet restore
```

3. Build the application:
```bash
dotnet build
```

4. Run the application:
```bash
dotnet run
```

The API will be available at:
- API: https://localhost:7243 (HTTPS)
- API: http://localhost:5264 (HTTP)
- Swagger UI: https://localhost:7243/swagger (when running in Development mode)

## API Endpoints

- `GET /api/Health` - Check API health
- `GET /api/Receipts` - Get all receipts
- `GET /api/Receipts/{id}` - Get a receipt by ID
- `POST /api/Receipts` - Create a new receipt
- `PUT /api/Receipts/{id}` - Update a receipt
- `DELETE /api/Receipts/{id}` - Delete a receipt

## Connecting to the React Frontend

The backend is configured with CORS to accept connections from `http://localhost:3000`. 
If your React app is running on a different URL, update the CORS policy in `Program.cs`.

Example code for connecting from React:
