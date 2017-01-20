@echo off
@echo.
title HONEYMOON SHOP Runner

@echo Changing working directory...
REM cd .\hhs-p6-webshop-project

@echo Setting environment variables...
set ASPNETCORE_ENVIRONMENT=Production
set ASPNETCORE_URLS=http://local.timvisee.com:5000

@echo Running server...
dotnet run

@echo.
@echo Done.
@echo.