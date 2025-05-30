# Local Development Setup

This guide will help you set up the Events Management System for local development.

## Prerequisites

Before you begin, ensure you have the following installed:

### Required Tools
- **[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)** - For backend development
- **[Node.js 20+](https://nodejs.org/)** - For frontend development
- **[Git](https://git-scm.com/)** - For version control

### Optional Tools
- **[Visual Studio Code](https://code.visualstudio.com/)** - Recommended IDE
- **[Docker Desktop](https://www.docker.com/products/docker-desktop)** - For containerized development
- **[Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)** - For enhanced Azure Functions development

## Setup Instructions

### 1. Clone the Repository

```bash
git clone <repository-url>
cd copilot-agent-mode-test-4
```

### 2. Backend Setup

Navigate to the backend directory and set up the API:

```bash
cd backend

# Restore .NET dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

The backend API will be available at:
- **HTTP**: `http://localhost:7071`
- **API Base URL**: `http://localhost:7071/api`

#### Backend Configuration

The backend uses the following default configuration in `local.settings.json`:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated"
  }
}
```

#### Testing the Backend

You can test the API endpoints using curl or a tool like Postman:

```bash
# Get all events
curl http://localhost:7071/api/events

# Create a new event
curl -X POST http://localhost:7071/api/events \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Test Event",
    "location": "Test Location",
    "date": "2024-12-01T00:00:00Z",
    "startTime": "10:00:00"
  }'
```

### 3. Frontend Setup

Open a new terminal and navigate to the frontend directory:

```bash
cd frontend/events-app

# Install Node.js dependencies
npm install

# Start the development server
npm run dev
```

The frontend will be available at:
- **URL**: `http://localhost:3000`

#### Frontend Configuration

Create a `.env` file in `frontend/events-app/` to configure the API endpoint:

```env
VITE_API_BASE_URL=http://localhost:7071/api
```

#### Frontend Development Commands

```bash
# Start development server
npm run dev

# Build for production
npm run build

# Preview production build
npm run preview

# Run linting
npm run lint
```

## Development Workflow

### Hot Reloading

Both the frontend and backend support hot reloading:

- **Backend**: The .NET development server automatically reloads when you save C# files
- **Frontend**: Vite provides instant hot module replacement (HMR) for Vue components

### Making Changes

1. **Backend Changes**: Edit files in the `backend/` directory
   - Models: `Models/Event.cs`
   - Services: `Services/EventService.cs`
   - API Endpoints: `Functions/EventsFunctions.cs`

2. **Frontend Changes**: Edit files in the `frontend/events-app/src/` directory
   - Components: `src/components/`
   - Views: `src/views/`
   - API Client: `src/services/api.js`

### Database

The application uses an in-memory database that resets when the backend restarts. This is perfect for development but means data doesn't persist between sessions.

## IDE Configuration

### Visual Studio Code

Recommended extensions:
- **C# for Visual Studio Code** - Backend development
- **Vue Language Features (Volar)** - Frontend development
- **REST Client** - API testing
- **GitLens** - Git integration

### Workspace Settings

Add this to `.vscode/settings.json`:

```json
{
  "eslint.workingDirectories": ["frontend/events-app"],
  "dotnet.defaultSolution": "backend/EventsApi.csproj"
}
```

## Troubleshooting

### Common Issues

#### Backend Port Already in Use
```bash
# Find what's using port 7071
lsof -i :7071

# Kill the process if needed
kill -9 <PID>
```

#### Frontend Port Already in Use
```bash
# Vite will automatically try the next available port
# Or specify a different port:
npm run dev -- --port 3001
```

#### API Connection Issues

1. Ensure the backend is running on `http://localhost:7071`
2. Check the `VITE_API_BASE_URL` in your `.env` file
3. Verify there are no CORS issues (the backend includes CORS configuration)

#### Package Installation Issues

```bash
# Backend: Clear NuGet cache
dotnet nuget locals all --clear
dotnet restore

# Frontend: Clear npm cache
npm cache clean --force
rm -rf node_modules package-lock.json
npm install
```

## Testing

### Backend Testing
```bash
cd backend
dotnet test
```

### Frontend Testing
```bash
cd frontend/events-app
npm test
```

## Building for Production

### Backend
```bash
cd backend
dotnet publish -c Release -o publish
```

### Frontend
```bash
cd frontend/events-app
npm run build
```

The production builds will be available in:
- Backend: `backend/publish/`
- Frontend: `frontend/events-app/dist/`

## Next Steps

Once you have the local development environment running:

1. **Explore the API**: Use the REST endpoints to create and manage events
2. **Customize the Frontend**: Modify the Vue components to add new features
3. **Add Features**: Implement additional functionality like user authentication
4. **Deploy**: Use the provided Docker containers for deployment

For deployment instructions, see [GitHub Codespaces Setup](codespaces-setup.md).