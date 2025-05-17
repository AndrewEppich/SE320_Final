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

## Google Cloud Vision API Setup

To use the receipt scanning functionality, you'll need to set up Google Cloud Vision API credentials:

1. For repository members:
   - Go to the repository on GitHub
   - Click on "Actions"
   - Select the "Setup Credentials" workflow
   - Click "Run workflow"
   - Select "development" environment
   - Click "Run workflow"
   - This will create a pull request with the credentials
   - Merge the pull request to update your local credentials

2. For local development without repository access:
   - Create a Google Cloud project and enable the Vision API
   - Create a service account and download the JSON credentials file
   - Copy the contents of your credentials JSON file
   - Open `appsettings.Development.json`
   - Replace `"YOUR_CREDENTIALS_JSON_HERE"` with your actual credentials JSON

Note: The Google Cloud Vision API is a paid service. Make sure to monitor your usage and set up billing alerts in your Google Cloud Console.

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
