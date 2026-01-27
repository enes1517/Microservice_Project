@echo off
set ASPNETCORE_ENVIRONMENT=Development
echo Starting Microservices Project...

:: 1. ProductService
echo Starting ProductService...
start "ProductService" dotnet run --project ".\ProductService\ProductService.csproj"

:: 2. ApiGateway
echo Starting ApiGateway...
start "ApiGateway" dotnet run --project ".\ApiGateway\ApiGateway.csproj"

:: 3. Frontend
echo Starting Frontend (Angular)...
cd Frontend
start "Frontend" npm start -- -o
cd ..

echo Waiting for services to initialize...
timeout /t 10

:: 4. Open Swagger (ProductService)
start http://localhost:5225/swagger/index.html

echo All services started!
echo Frontend: http://localhost:4200
echo Product Swagger: http://localhost:5225/swagger
echo Gateway: http://localhost:5272
pause
