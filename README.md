# ShoppingSite
Entity Framework Version : v6.1.3
Bootstrap Version : v3.4.1
Note : if enable-migrations does not work.

1.right click on project name -> Manage NuGet packages
2.Search EntityFramework and install v6.1.3 in the project

Steps to to download and load project in visual studio:

1.Delete Migrations folder from project after downloading project.

2.Go to Tools -> NuGet Package Manager -> Package Manager Console.

3.If warning message at top of package manager console : similar to - some packages are missing : restore

4.if(above warning) click on restore.

5.In Package Manager Console -> Run the below No. 4 & 5 commands.

6.enable-migrations.

7.add-migration FirstMigration.

8.At the end of inside Up() method in 2020..._ FirstMigration of Migrations folder, paste -> Pending_Migration.txt content of PendingMigration folder and save the files.

9.Delete App_Data folder from project, right click on project -> Add Asp.Net folder -> App_Data..

10.in Package Manager Console -> run below No. 11 command.

11.update-database.

12.Now run the project using control + F5.

13.If it shows risk to enter website -> go to Advanced button -> Visit Anyways.


If Still Errors in packages:
Tools > NuGet Package Manager > Package manager setting >. Under General, select Allow NuGet to download missing packages.

In Solution Explorer, right click the solution and select Restore NuGet Packages.

In order to access the StoreManager section use -
Mail-Abhishek@gmail.com
Password-Abhishek@123


# functionality
For first time user to interact with website UI following are the instructions:
1.Use Regsiter button at navbar to register.
2.Now you can visit the store section and select the item and add to cart and then at Checkout fill your detail and order for items.
3.In case, you had not register at start then you need to Register at time of Checkout.
4.In order to access the StoreManager section use above mentioned details to login.

# Commits
In first Commit-
1.enabled Entity Framework
2.Created Table of Item Model 
3.Created StoreManager Controller for add,delete,update.
4.Added Annotations

In second commit added-
Authorization for admin
Order class
Cart for items

in third commit added-
Authorization for user

in Fourth commit added-
Cart View

in last commit added-
1.Searching option
2.Look into the style part of application

