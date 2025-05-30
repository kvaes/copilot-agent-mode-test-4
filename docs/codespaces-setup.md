# GitHub Codespaces Setup

This guide explains how to run the complete Events Management System end-to-end in GitHub Codespaces.

## Overview

GitHub Codespaces provides a cloud-based development environment that allows you to develop, build, and test the entire application stack directly in your browser.

## Quick Start

### 1. Create a Codespace

1. Navigate to the repository on GitHub
2. Click the **Code** button
3. Select the **Codespaces** tab
4. Click **Create codespace on main**

GitHub will automatically provision a cloud environment with all necessary tools pre-installed.

### 2. Automatic Setup

The Codespace will automatically:
- Install .NET 8 SDK
- Install Node.js 20
- Restore backend dependencies
- Install frontend dependencies

## Running the Full Application

### Method 1: Using the Terminal (Recommended)

Once your Codespace is ready:

#### Start the Backend

1. Open a terminal (Terminal → New Terminal)
2. Navigate to the backend directory:
   ```bash
   cd backend
   ```
3. Start the API server:
   ```bash
   dotnet run
   ```
4. The backend will be available at `http://localhost:7071`

#### Start the Frontend

1. Open a **new terminal** (Terminal → New Terminal)
2. Navigate to the frontend directory:
   ```bash
   cd frontend/events-app
   ```
3. Start the development server:
   ```bash
   npm run dev
   ```
4. The frontend will be available at `http://localhost:3000`

### Method 2: Using VS Code Tasks

The repository includes pre-configured tasks for easier development:

1. Open the Command Palette (Ctrl+Shift+P / Cmd+Shift+P)
2. Type "Tasks: Run Task"
3. Select either:
   - **Start Backend** - Runs the C# API
   - **Start Frontend** - Runs the Vue.js app

## Accessing Your Application

### Port Forwarding

GitHub Codespaces automatically forwards ports when applications start:

1. **Backend API** (Port 7071):
   - Will be automatically forwarded
   - Accessible via the unique Codespace URL
   - Look for the notification or check the **Ports** tab

2. **Frontend App** (Port 3000):
   - Will be automatically forwarded
   - Click "Open in Browser" when prompted
   - Or find the URL in the **Ports** tab

### Environment Configuration

The frontend needs to know where to find the backend API. Create an environment file:

1. In the frontend directory, create `.env`:
   ```bash
   cd frontend/events-app
   echo "VITE_API_BASE_URL=https://YOUR-CODESPACE-URL-7071.app.github.dev/api" > .env
   ```

2. Replace `YOUR-CODESPACE-URL-7071` with your actual Codespace URL for port 7071

### Finding Your URLs

You can find your forwarded port URLs:

1. Click the **Ports** tab in the VS Code terminal panel
2. Copy the forwarded address for each port
3. The backend URL will include port 7071
4. The frontend URL will include port 3000

## End-to-End Testing

### Full Application Workflow

1. **Access the Frontend**: Open your frontend URL in a browser
2. **Browse Events**: The events list should load (initially empty)
3. **Create Test Data**: Use the API to create sample events
4. **Test Registration**: Register for an event through the UI
5. **Verify Data Flow**: Check that registrations appear in the event details

### Creating Sample Data

Use the terminal to create sample events via the API:

```bash
# Create a sample event
curl -X POST https://YOUR-CODESPACE-URL-7071.app.github.dev/api/events \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Tech Meetup",
    "location": "Virtual",
    "date": "2024-12-15T00:00:00Z",
    "startTime": "18:00:00"
  }'

# Create another event
curl -X POST https://YOUR-CODESPACE-URL-7071.app.github.dev/api/events \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Workshop: Vue.js Basics",
    "location": "Online",
    "date": "2024-12-20T00:00:00Z",
    "startTime": "14:00:00"
  }'
```

### Testing the Complete Flow

1. **View Events**: Refresh the frontend to see your new events
2. **Filter Events**: Test the date and location filters
3. **Event Details**: Click on an event to view details
4. **Register**: Fill out the registration form
5. **Verify**: Check that the registration count updates

## Development in Codespaces

### Making Changes

#### Backend Changes
1. Edit files in the `backend/` directory
2. The development server will automatically reload
3. Test your changes via the API endpoints

#### Frontend Changes
1. Edit files in `frontend/events-app/src/`
2. Vite's hot module replacement will update the browser instantly
3. No need to manually refresh

### Debugging

#### Backend Debugging
1. Set breakpoints in your C# code
2. Use the integrated debugger in VS Code
3. F5 to start debugging mode

#### Frontend Debugging
1. Open browser developer tools (F12)
2. Use Vue DevTools browser extension (if installed)
3. Console logging is available for debugging

### Terminal Management

Efficiently manage multiple terminals:

1. **Split Terminal**: Click the split terminal icon
2. **Terminal Tabs**: Use multiple terminal tabs
3. **Named Terminals**: Right-click tabs to rename them
   - Name one "Backend"
   - Name another "Frontend"

## Container Development (Optional)

You can also run the application using Docker in Codespaces:

### Build Containers

```bash
# Build backend container
cd backend
docker build -t events-api .

# Build frontend container  
cd ../frontend/events-app
docker build -t events-frontend .
```

### Run Containers

```bash
# Run backend
docker run -d -p 7071:80 --name events-api events-api

# Run frontend
docker run -d -p 3000:80 --name events-frontend events-frontend
```

## Performance Tips

### Optimizing Codespace Performance

1. **Prebuilds**: The repository can be configured for faster startup with prebuilds
2. **Machine Size**: Use larger machine types for better performance
3. **Extensions**: Only install necessary VS Code extensions

### Resource Management

- **Backend**: Uses minimal resources, typically ~100MB RAM
- **Frontend**: Node.js development server uses ~150MB RAM
- **Total**: The full stack requires ~512MB RAM minimum

## Troubleshooting

### Common Issues

#### Port Forwarding Not Working
1. Check the **Ports** tab in VS Code
2. Ensure the service is actually running on the expected port
3. Try manually forwarding the port

#### API Connection Issues
1. Verify the backend is running on port 7071
2. Check the `.env` file has the correct API URL
3. Ensure CORS is properly configured

#### Build Failures
```bash
# Backend: Clear and restore
cd backend
dotnet clean
dotnet restore
dotnet build

# Frontend: Clear and reinstall
cd frontend/events-app
rm -rf node_modules package-lock.json
npm install
```

### Getting Help

If you encounter issues:
1. Check the **Terminal** output for error messages
2. Review the **Problems** tab in VS Code
3. Use the **Output** panel to see detailed logs
4. Create an issue in the repository with error details

## Codespace Configuration

### Custom Configuration

You can customize the Codespace by creating `.devcontainer/devcontainer.json`:

```json
{
  "name": "Events App Development",
  "image": "mcr.microsoft.com/devcontainers/universal:2",
  "features": {
    "ghcr.io/devcontainers/features/dotnet:1": {
      "version": "8.0"
    },
    "ghcr.io/devcontainers/features/node:1": {
      "version": "20"
    }
  },
  "forwardPorts": [3000, 7071],
  "portsAttributes": {
    "3000": {
      "label": "Frontend",
      "onAutoForward": "openBrowser"
    },
    "7071": {
      "label": "Backend API",
      "onAutoForward": "notify"
    }
  },
  "postCreateCommand": "cd backend && dotnet restore && cd ../frontend/events-app && npm install"
}
```

## Next Steps

Once you have the application running in Codespaces:

1. **Experiment**: Try modifying components and see live updates
2. **API Testing**: Use the REST Client extension to test API endpoints
3. **Feature Development**: Add new features to both frontend and backend
4. **Deployment**: Test the container builds for production deployment

This Codespace environment provides everything you need for full-stack development without any local setup requirements!