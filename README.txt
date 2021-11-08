#Personal Planner
The Personal Budget Planner WPF application is a more complex but yet more effecient version of the Budget Planner.
It makes use of a more recent Windows framework as wellas database structures to make storage easier and more effecient.
The Personal Budget Planner makes use of a connected MsSQL database.

##Getting Sarted
The instructions listed below will guide you on how to install and run the appliation

##What things you need to install the software and how to install them:

Any of the latest MS Visual Studio e.g. v.1.6.2019 
A device with dual core processing and at least 4GB's of RAM e.g. Acer Aspire
MsSQL Server e.g. v18.4
Adobe Acrobat Reader

###Installing

-Open MsSQL Server and create a new database by the name 'Personal_Planner_Database'.
-Open the SQlScript 'Peronal_Planner' from the file and create the tables int MSSQL Server.
-Open Ms Viual Studio.
-Click on the 'Open project or solution' optino and open the PersonalPlanner project.
-Once open, click on the Server Explorer and select the option to connect to a new database.
-When the window pops up, add your server name from you MsSQL Server and select the 'Personal_Planner_Database'.
-Click on connect.
-To test the appliaction click on run.
-When the main pops up, click on add new user.
-Enter values that are required and click on next.
-When you have reached the ExpensesPage, complete registration by clicking on the complete button and 
 follow through by viewing your balances.
-Click on edit to edit the income information and save it.
-Click on the view my expenes button to view further expenses.
-Click on the logout button to automaitally log you out.

### Break down into end to end tests

To test this program:

When on the create user page, enter a name and then remove the name to test if it will highlight red.
Enter a string value in any of the expense textboxes to check if htey can take anything that is not decimal.

Seperately calculate the expense and income details to confirm that the incomeand expense details input 
are accurate.

## Built With

* [Dropwizard](http://www.dropwizard.io/1.0.2/docs/) - The web framework used
* [Maven](https://maven.apache.org/) - Dependency Management
* [ROME](https://rometools.github.io/rome/) - Used to generate RSS Feeds

##Authors
**Koketso Baruti**

##Acknowledgements

* Youtube tutors
* Mr. Rudolph
* StackOverflow