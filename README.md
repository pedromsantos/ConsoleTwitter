ConsoleTwitter
==============

## HOW TO:

### Build and run

##### Visual Studio
Open the solution build and run using CTRL-F5. You may need to download NUget packages.

##### Xamarin Studio
Open the solution, right click on ConsoleTwitter project to bring the "Options" dialog. Select General under Run. Make sure "Run on External Console" is checked. From the "Run" menu select "Start Without Debugging". If you don't have Nuget extension you may need to install it (https://github.com/mrward/monodevelop-nuget-addin).

##### Mono Console
Navigate to the solution folder. Download Nuget from http://nuget.codeplex.com/downloads/get/806205# Copy NuGet.exe to the solution folder. Type "mono NuGet.exe restore", this should update project dependencies. Type "xbuild", this should build the application. Navigate to \ConsoleTwitter\bin\Debug type "mono ConsoleTwitter.exe", this should run the application.


### Application instructions

Post: <user name> -> <message> 
Read: <user name> 
Follow: <user name> follows <another user> 
Wall: <user name> wall

### Sample usage

Alice -> I love the weather today
Bob -> Damn! We lost! 
Bob -> Good game though.
Alice
I love the weather today (5 minutes ago)
Bob
Good game though. (1 minute ago)
Damn! We lost! (2 minutes ago)
Charlie -> I'm in New York today! Anyone wants to have a coffee? 
Charlie follows Alice 
Charlie wall
Charlie - I'm in New York today! Anyone wants to have a coffee? (2 seconds ago) 
Alice - I love the weather today (5 minutes ago)
Charlie follows Bob 
Charlie wall
Charlie - I'm in New York today! Anyone wants to have a coffee? (15 seconds ago) 
Bob - Good game though. (1 minutes ago) 
Bob - Damn! We lost! (2 minute ago) 
Alice - I love the weather today (5 minutes ago)
