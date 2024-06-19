@echo off

:: Budowanie obrazu AccountsService
echo Budowanie obrazu accounts-service ...
docker build -t accounts-service . -f ./Bleeter.AccountsService/Dockerfile

if %errorlevel% neq 0 (
    echo Budowanie obrazu accounts-service nie powiodło się!
    exit /b %errorlevel%
)

:: Budowanie obrazu bleet-service
echo Budowanie obrazu bleets-service...
docker build -t bleets-service  . -f ./Bleeter.BleetsService/Dockerfile

if %errorlevel% neq 0 (
    echo Budowanie obrazu bleets-service nie powiodło się!
    exit /b %errorlevel%
)

:: Budowanie obrazu private-messages-service
echo Budowanie obrazu private-messages-service...
docker build -t private-messages-service  . -f ./Bleeter.PrivateMessagesService/Dockerfile

if %errorlevel% neq 0 (
    echo Budowanie obrazu private-messages-service nie powiodło się!
    exit /b %errorlevel%
)

:: Budowanie obrazu notifications-service
echo Budowanie obrazu notifications-service...
docker build -t notifications-service . -f ./Bleeter.NotificationsService/Dockerfile

if %errorlevel% neq 0 (
    echo Budowanie obrazu notifications-service nie powiodło się!
    exit /b %errorlevel%
)

echo Wszystkie obrazy zostały zbudowane pomyślnie!
