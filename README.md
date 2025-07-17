# State-tracking-API

A simple ASP.NET Core Web API for defining and tracking workflows with states and actions.

## Features

- Define workflow definitions with states and actions
- Start workflow instances
- Execute actions to transition between states
- In-memory storage (no database required)
- OpenAPI/Swagger UI for API exploration

## Getting Started

1. **Build and Run**
   ```sh
   git clone https://github.com/Hedes2/State-tracking-API.git
   cd ./Api/
   cd ./api/
   dotnet watch run   ```

2. **API Documentation**
   - Visit [http://localhost:5062/swagger](http://localhost:5062/swagger) (default) for interactive API docs.

## API Endpoints

- `POST /api/workflow/definitions` — Create a workflow definition
- `GET /api/workflow/definitions/{id}` — Get a workflow definition
- `POST /api/workflow/instances` — Start a workflow instance
- `POST /api/workflow/instances/{id}/actions/{actionId}` — Execute an action on an instance
- `GET /api/workflow/instances/{id}` — Get a workflow instance

## Project Structure

- `api/Controllers/WorkflowController.cs` — API controllers
- `api/Models/` — Data models (State, Action, WorkflowDefinition, WorkflowInstance)
- `api/Services/` — Business logic
- `api/Storage/` — In-memory storage implementation
