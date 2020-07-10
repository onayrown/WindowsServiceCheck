# Windows Service Check - .Net Framework 4.7.2 - Windows Service Application  
A basic service created to check periodically if a specific windows service is running and restart it if necessary. 

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
  https://i.imgur.com/TumrrJQ.png
  

## Technical Requirements
 - [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/visual-studio-sdks?utm_source=getdotnetsdk&utm_medium=referral)
 - [Visual Studio 2019](https://visualstudio.microsoft.com/pt-br/downloads/)
