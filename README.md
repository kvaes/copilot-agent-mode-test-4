# Events Management System

A modern events management system built with Vue.js frontend and C# Azure Functions backend.

## Overview

This application allows users to:
- Browse and filter events by date and location
- View detailed event information
- Register for events with personal information
- Manage event data through a REST API

## Architecture

### Frontend
- **Framework**: Vue.js 3 with Vite
- **Routing**: Vue Router
- **HTTP Client**: Axios
- **Styling**: Custom CSS with responsive design

### Backend
- **Runtime**: C# Azure Functions (Isolated Worker)
- **Framework**: .NET 8
- **Database**: Entity Framework Core with In-Memory provider
- **API**: RESTful HTTP endpoints

## Project Structure

```
├── backend/                 # C# Azure Functions API
│   ├── Data/               # Entity Framework DbContext
│   ├── Functions/          # Azure Functions HTTP triggers
│   ├── Models/             # Data models and DTOs
│   ├── Services/           # Business logic services
│   ├── Dockerfile          # Container configuration
│   └── EventsApi.csproj    # Project file
├── frontend/               # Vue.js application
│   └── events-app/
│       ├── src/
│       │   ├── components/ # Reusable Vue components
│       │   ├── views/      # Page components
│       │   ├── services/   # API client
│       │   └── main.js     # Application entry point
│       ├── Dockerfile      # Container configuration
│       └── package.json    # Dependencies
├── docs/                   # Documentation
├── .github/
│   ├── workflows/          # CI/CD pipelines
│   ├── dependabot.yml      # Dependency management
│   └── SECURITY.md         # Security policy
└── README.md               # This file
```

## Quick Start

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 20+](https://nodejs.org/)
- [Docker](https://www.docker.com/) (optional, for containerized deployment)

### Local Development

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd copilot-agent-mode-test-4
   ```

2. **Start the backend**
   ```bash
   cd backend
   dotnet restore
   dotnet run
   ```
   The API will be available at `http://localhost:7071`

3. **Start the frontend** (in a new terminal)
   ```bash
   cd frontend/events-app
   npm install
   npm run dev
   ```
   The frontend will be available at `http://localhost:3000`

### Using Docker

Build and run both services using Docker:

```bash
# Backend
cd backend
docker build -t events-api .
docker run -p 7071:80 events-api

# Frontend
cd frontend/events-app
docker build -t events-frontend .
docker run -p 3000:80 events-frontend
```

## API Endpoints

### Events
- `GET /api/events` - Get all events (with optional date and location filters)
- `GET /api/events/{id}` - Get specific event
- `POST /api/events` - Create new event
- `PUT /api/events/{id}` - Update event
- `DELETE /api/events/{id}` - Delete event

### Registrations
- `POST /api/events/{id}/register` - Register for an event

## Development

### Backend Development
- Built with C# and Azure Functions
- Uses Entity Framework Core for data access
- Includes comprehensive error handling
- Follows REST API best practices

### Frontend Development
- Built with Vue.js 3 and Composition API
- Responsive design with CSS Grid and Flexbox
- Form validation and error handling
- Progressive enhancement for better UX

### Testing
- Backend: Run `dotnet test` in the backend directory
- Frontend: Run `npm test` in the frontend/events-app directory

### Linting
- Backend: Built-in .NET analyzers
- Frontend: ESLint configuration included

## CI/CD

The project includes GitHub Actions workflows for:
- **Backend CI/CD**: Build, test, and containerize the API
- **Frontend CI/CD**: Build, test, and containerize the frontend
- **Dependabot**: Automated dependency updates
- **Security**: Automated security scanning

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Ensure all CI checks pass
6. Submit a pull request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

For questions, issues, or contributions:
- Create an issue in the GitHub repository
- Follow our [Security Policy](.github/SECURITY.md) for security-related concerns