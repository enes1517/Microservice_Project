@echo off
echo ========================================
echo    Servisleri Durduruyor...
echo ========================================
echo.

echo ApiGateway servisi durduruluyor...
for /f "tokens=5" %%a in ('netstat -aon ^| findstr :7239') do taskkill /F /PID %%a 2>nul

echo ProductService durduruluyor...
for /f "tokens=5" %%a in ('netstat -aon ^| findstr :5265') do taskkill /F /PID %%a 2>nul

echo OrchestratorService durduruluyor...
for /f "tokens=5" %%a in ('netstat -aon ^| findstr :7007') do taskkill /F /PID %%a 2>nul
for /f "tokens=5" %%a in ('netstat -aon ^| findstr :5155') do taskkill /F /PID %%a 2>nul

echo Angular Frontend durduruluyor...
for /f "tokens=5" %%a in ('netstat -aon ^| findstr :4200') do taskkill /F /PID %%a 2>nul

echo Node.js process'leri durduruluyor...
taskkill /F /IM node.exe 2>nul

echo Dotnet process'leri durduruluyor...
taskkill /F /IM dotnet.exe 2>nul

echo.
echo ========================================
echo    TUM SERVISLER DURDURULDU!
echo ========================================
echo.
pause
