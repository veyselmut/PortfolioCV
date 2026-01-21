@echo off
echo ===================================================
echo   PortfolioCV Automated Deployment Script
echo ===================================================

echo.
echo [1/4] Cleaning previous builds...
rmdir /s /q Deployment
mkdir Deployment\Backend
mkdir Deployment\AdminPanel

echo.
echo [2/4] Publishing Backend API...
dotnet publish -c Release -o Deployment\Backend
if %ERRORLEVEL% NEQ 0 (
    echo Error publishing backend!
    pause
    exit /b %ERRORLEVEL%
)

echo.
echo [3/4] Building Admin Panel...
cd admin-panel
echo Installing dependencies...
call npm install
echo Building project...
call npm run build
if %ERRORLEVEL% NEQ 0 (
    echo Error building frontend!
    pause
    exit /b %ERRORLEVEL%
)
cd ..

echo.
echo [4/4] Copying Admin Panel Files...
xcopy /E /I /Y admin-panel\dist Deployment\AdminPanel

echo.
echo ===================================================
echo   DEPLOYMENT READY! 🚀
echo ===================================================
echo.
echo Backend Files:  %~dp0Deployment\Backend
echo Admin Files:    %~dp0Deployment\AdminPanel
echo.
echo You can now upload these folders to your server.
echo.
pause
