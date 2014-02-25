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

| Action    | Command                               | Example                              |
| --------- |:--------------------------------------:| ------------------------------------:|
| Post      | \<user name\> -> \<message\>           | Alice -> I love the weather today    |
| Read      | \<user name\>                          | Alice                                |
| Folow     | \<user name\> follows \<another user\> | Charlie follows Alice                |
| Wall      | \<user name\> wall                     | Charlie wall                         |

