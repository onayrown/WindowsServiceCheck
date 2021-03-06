﻿# Windows Service Check - .Net Framework 4.7.2 - Windows Service Application  
A service created to check periodically if a specific windows service is running and restart it if necessary. 
- The service is configured to check every 5 seconds if the other service is running. This value is in mileseconds and can be modified in the code.
- The service is configured to save an operations log to the Base Directory \ Logs. It should be disabled if necessary.
- The service used during the initial tests was Windows Update, in which the name is "wuauserv". Change this in the code to monitor another service.

## Installing a Windows Service
1. First download / clone and compile the project.
2. Open “Command Prompt” and run as administrator
3. Fire the below command in the command prompt and press ENTER.
 
cd C:\Windows\Microsoft.NET\Framework\v4.0.30319 

4. Go to your project source folder > bin > Debug and copy the full path of your Windows Service exe file.
5. Use the syntax InstallUtil.exe + Your copied path + \your service name + .exe

Example: InstallUtil.exe C:\Users\MyUser\source\repos\WindowsServiceCheck\WindowsServiceCheck\bin\Debug\WindowsServiceCheck.exe

## Initializing the service.
- By default the service will use the local system account. 
  - If necessary, use a different account
  [![Local System Account](https://i.imgur.com/TumrrJQ.png "Local System Account")](https://i.imgur.com/TumrrJQ.png "Local System Account")
  

## Technical Requirements
 - [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/visual-studio-sdks?utm_source=getdotnetsdk&utm_medium=referral)
 - [Visual Studio 2019](https://visualstudio.microsoft.com/pt-br/downloads/)
 - [Windows Operating System](https://www.microsoft.com/software-download/windows10)
 
