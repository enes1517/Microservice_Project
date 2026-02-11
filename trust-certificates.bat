@echo off
echo ========================================
echo    HTTPS Sertifikalarini Guvenilir Yap
echo ========================================
echo.
echo .NET development sertifikalarini guvenilir olarak isaretliyoruz...
dotnet dev-certs https --clean
dotnet dev-certs https --trust

echo.
echo Sertifikalar guvenilir olarak isaretlendi!
echo.
pause
