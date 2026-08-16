@echo off
title Build + Publish + Copy Client.dll
color 0a

echo ==== DOTNET PUBLISH ====
dotnet publish -r win-x64 -c Release --self-contained true

echo.
echo ==== DANG TIM THU MUC PUBLISH ====

set PUB="AotForms\bin\Release\net7.0-windows7.0\win-x64\publish"

if not exist %PUB% (
    echo LOI: Khong tim thay thu muc publish!
    pause
    exit /b
)

echo Thu muc publish: %PUB%

echo.
echo ==== DANG COPY Client.dll ====
copy %PUB%\Client.dll C:\Windows\Temp /Y

if %ERRORLEVEL% NEQ 0 (
    echo LOI: Khong the copy Client.dll!
    pause
    exit /b
)

echo Da copy thanh cong Client.dll vao C:\Windows\Temp
echo ============================
echo HOAN TAT!
pause