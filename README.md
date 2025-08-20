# Hello World App

A simple hello world application built with C# and .NET, demonstrating a clean separation between backend API and frontend interface.

## 🏗️ Architecture

- **Backend**: ASP.NET Core Web API that returns a simple greeting (runs on port 3000)
- **Frontend**: Blazor Server app that displays the greeting from the backend

## 📁 Project Structure

```
/
├── backend/                    # ASP.NET Core Web API
│   ├── Controllers/
│   │   └── HelloController.cs  # Simple hello endpoint
│   ├── Program.cs              # API configuration
│   ├── backend.csproj          # Project file
│   └── appsettings.json        # Configuration
├── frontend/                   # Blazor Server App
│   ├── Data/
│   │   └── HelloService.cs     # Service to communicate with backend
│   ├── Pages/
│   │   └── Index.razor         # Main hello world page
│   ├── Program.cs              # Frontend configuration
│   └── frontend.csproj         # Project file
├── HelloWorld.sln              # Solution file
└── ci.yml                      # Codesphere deployment config
```

## 🚀 Running on Codesphere

### Backend (Port 3000)
```bash
cd backend
dotnet restore
dotnet run
```

### Frontend
```bash
cd frontend
dotnet restore
dotnet run
```

The backend is configured to run on port 3000 for Codesphere deployment.

## 🌐 API Endpoints

- `GET /api/hello` - Returns a simple greeting message

## ✨ Features

- **Simple Architecture**: Clean separation between frontend and backend
- **Error Handling**: Basic error handling with retry functionality
- **Responsive UI**: Clean, modern interface with Bootstrap styling
- **Loading States**: Visual feedback during data fetching
- **Manual Refresh**: Button to fetch new greeting

## 🔧 Development

The application uses:
- **.NET 8.0** for both backend and frontend
- **Bootstrap 5** for styling
- **Blazor Server** for interactive UI

This template serves as a starting point for building more complex frontend/backend applications with C# and .NET.
- Any modern web browser

### Running the Application

1. **Start the Backend API** (in a terminal):
   ```bash
   cd backend
   dotnet run
   ```
   The API will be available at `https://localhost:7001`

2. **Start the Frontend** (in another terminal):
   ```bash
   cd frontend
   dotnet run
   ```
   The web application will be available at `https://localhost:5001`

3. **Access the Application**:
   Open your web browser and navigate to `https://localhost:5001`

### Development

- Backend API documentation is available at `https://localhost:7001/swagger` when running in development mode
- The frontend automatically connects to the backend API
- Both projects support hot reload for efficient development

## API Endpoints

### GET /api/rate
Returns current exchange rates relative to USD.

**Response Example**:
```json
{
  "result": "success",
  "base_code": "USD",
  "rates": {
    "EUR": 0.85,
    "GBP": 0.73,
    "JPY": 110.12,
    ...
  }
}
```

## Configuration

### Backend Configuration
- CORS is configured to allow requests from the frontend application
- HttpClient is configured for external API calls with proper error handling
- Logging is configured for monitoring and debugging

### Frontend Configuration
- HttpClient base address is set to the backend API URL
- Bootstrap and Bootstrap Icons are included for styling
- Error boundaries are configured for graceful error handling

## External Dependencies

- **ExchangeRate-API**: Free currency exchange rate API
- **Bootstrap 5**: For responsive UI components
- **Bootstrap Icons**: For UI icons

## Error Handling

The application includes comprehensive error handling:
- Network connectivity issues
- External API failures
- JSON parsing errors
- Timeout scenarios
- General exception handling

All errors are logged appropriately and user-friendly messages are displayed in the UI.

## Security Considerations

- CORS is properly configured between frontend and backend
- HTTPS is enforced in production
- No sensitive API keys required (using free tier of ExchangeRate-API)
- Input validation and sanitization where applicable

## Deployment

For Codesphere deployment:
1. Both projects can be deployed as separate services
2. Update CORS configuration in backend to include production frontend URL
3. Update HttpClient base address in frontend to point to production backend URL
4. Configure environment-specific settings in appsettings files
