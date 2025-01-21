# Production Equipment Hosting Service

## Required Software
To run this project, ensure you have the following software installed on your machine:

- **Docker**: [Installation Guide](https://docs.docker.com/get-docker/)
- **Docker Compose**: Comes bundled with Docker Desktop or install it separately [here](https://docs.docker.com/compose/install/).

## How to Run

Follow these steps to set up and run the project locally using Docker Compose:

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/sentemon/EquipLease
   cd EquipLease
   ```

2. **Build and Start the Services**:
   Run the following command to start the application:
   ```bash
   docker-compose up --build
   ```

3. **Access the Service**:
   - API_KEY: `secret_api_key`
   - API: `http://localhost:5242`
   - Swagger documentation: `http://localhost:5242/swagger/index.html`

## Project Description

### Task
Development of a service for hosting process equipment for production facilities.

### Summary
The service allows for the administration of contracts related to the placement of equipment within production facilities. It includes validation to ensure that facilities have adequate space for the specified equipment.

### Features
- Create a new equipment placement contract.
- Retrieve a list of all contracts.
- Validate equipment placement based on facility capacity.
- Secure API access using a static API key.
- Asynchronous background processing for logging or additional business logic.

### Technology Stack
- **Backend**: ASP.NET Core Web API
- **Database**: MS SQL (Code First with Entity Framework Core)
- **Background Processing**: Logger
- **Containerization**: Docker, Docker Compose
