# Financial Stock Backend

## Overview

The Financial Stock Backend is a .NET 8 API that provides functionalities for managing financial stocks, comments, and user portfolios. The backend is designed with security in mind, leveraging JWT for authentication and ASP.NET Core Identity for user management.

## Features

- User authentication and authorization using JWT.
- CRUD operations for stocks and comments.
- Portfolio management.
- Comprehensive error handling and validation.
- Swagger for API documentation.

## Technologies Used

- **Framework:** .NET 8
- **Database:** SQL Server
- **Authentication:** JWT and ASP.NET Core Identity
- **API Documentation:** Swagger

## Getting Started

### Prerequisites

Make sure you have the following installed:

- .NET SDK 8
- SQL Server
- Visual Studio or VS Code

### Installation

1. **Clone the repository:**

   ```sh
   git clone https://github.com/fuadmuhe12/Financial-Stock.git
   cd Financial-Stock
   ```

2. **Set up the database:**

   Update the connection string in `appsettings.json` to point to your SQL Server instance.

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
   },
   "JWT": {
     "Key": "your_jwt_secret_key",
     "Issuer": "your_jwt_issuer",
     "Audience": "your_jwt_audience"
   }
   ```

3. **Run database migrations:**

   ```sh
   dotnet ef database update
   ```

4. **Build and run the application:**

   ```sh
   dotnet build
   dotnet run
   ```

5. **Access the API:**

   The API will be running at `https://localhost:5001` (or the port specified in your `launchSettings.json`).

### Usage

The backend provides various endpoints to manage stocks, comments, and portfolios. You can explore these endpoints using the Swagger UI available at `https://localhost:5001/swagger`.

#### Example Requests

- **Get all stocks:**

  ```http
  GET /api/stocks
  ```



- **User login:**

  ```http
  POST /api/auth/login
  Content-Type: application/json

  {
    "username": "user@example.com",
    "password": "YourPassword123"
  }
  ```

## Contributing

Contributions are welcome! Please follow these steps to contribute:

1. Fork the repository
2. Create a new branch (`git checkout -b feature/YourFeature`)
3. Commit your changes (`git commit -m 'Add YourFeature'`)
4. Push to the branch (`git push origin feature/YourFeature`)
5. Create a Pull Request



## Contact

If you have any questions or suggestions, feel free to reach out:

- Name: Fuad Muhe
- Email: [fuaadmuhe12@gmail.com](fuaadmuhe12@gmail.com)
- GitHub: [https://github.com/fuadmuhe12](https://github.com/fuadmuhe12)


### Notes

1. **Database Setup:** Ensure that you replace `"DefaultConnection"` with your actual SQL Server connection details.
2. **JWT Configuration:** Replace the placeholder values for `JWT:Key`, `JWT:Issuer`, and `JWT:Audience` in the `appsettings.json` section with your actual values.
3. **API Endpoints:** Provide detailed API endpoint documentation as needed in the usage section.
4. **Email:** Replace `[Your Email]` with your actual email address.

Feel free to customize and expand upon this template as per your project requirements.