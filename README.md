# TaskMaster

### Task 
Create a simple TODO list app with the following requirements:
- The user must be able to see their TODO list, add items to it and delete items from it
- Use the latest version of angular for the frontend
- Use the latest version of .NET web API for the backend
- You can manage the data on the backend in memory, no need to set up a database for this test
- Follow best practices around code, testing and architecture as you understand them

### Prerequisites
- .NET 8 SDK
- Node.js 18+ and npm
- Angular CLI (`npm install -g @angular/cli`)

### Backend (.NET API)
```bash
# Navigate to API directory
cd TaskMaster.API

# Restore packages
dotnet restore

# Run the API
dotnet run
```
The API will be available at `https://localhost:5001`

### Frontend (Angular)
```bash
# Navigate to UI directory
cd taskmaster-ui

# Install dependencies
npm install

# Start development server
npm start
```
The UI will be available at `http://localhost:4200`

## 📁 Project Structure

```
TaskMaster/
├── TaskMaster.API/          # .NET 8 Web API
├── TaskMaster.Core/         # Domain entities and interfaces
├── TaskMaster.Application/  # Business logic and services
├── TaskMaster.Infrastructure/ # Data access and external services
├── TaskMaster.Tests/        # Unit tests
└── taskmaster-ui/           # Angular 17 frontend
    ├── src/app/            # Angular components and services
    └── package.json        # Frontend dependencies
```

## 🛠️ Technologies

- **Backend**: .NET 8, ASP.NET Core Web API
- **Frontend**: Angular 17, Bootstrap 5, AG Grid
- **Architecture**: Clean Architecture with CQRS pattern

## 📝 Features

- Create, read, update, and delete tasks
- Modern responsive UI
- RESTful API with Swagger documentation
- Clean architecture with separation of concerns
