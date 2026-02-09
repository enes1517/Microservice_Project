@echo off
echo ========================================
echo    Microservices Demo Baslatiyor...
echo ========================================
echo.

REM Backend Servisleri
echo [1/4] ApiGateway baslatiyor...
start "ApiGateway (Port 7239)" cmd /k "cd ApiGateway && dotnet run --launch-profile https"
timeout /t 3 /nobreak >nul

echo [2/4] ProductService baslatiyor...
start "ProductService (Port 7265)" cmd /k "cd ProductService && dotnet run --launch-profile https"
timeout /t 2 /nobreak >nul

echo [3/4] OrchestratorService baslatiyor...
start "OrchestratorService (Port 7007)" cmd /k "cd OrchestratorService && dotnet run --launch-profile https"
timeout /t 2 /nobreak >nul

echo [4/4] Angular Frontend baslatiyor...
start "Angular Frontend (Port 4200)" cmd /k "cd frontend && npm start"

echo.
echo ========================================
echo    TUM SERVISLER BASLATILDI!
echo ========================================
echo.
echo Backend Swagger: https://localhost:7239/swagger
echo Frontend URL:    http://localhost:4200
echo.
echo Servisleri durdurmak icin stop-all.bat calistirin
echo veya her pencerede Ctrl+C yapin.
echo.
pause
