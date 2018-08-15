TASK 2 "Notebook"



You need to create a simple console application that implements a notebook. The source
 MySQL database must contain data such as name, surname, date of birth,
address, etc. At the start of the application, the database is filled with test data for 5-10 people. Data can be absolutely any, the main thing is that they differ. User interaction
occurs in an interactive mode. In the application, when you enter a number from 1 to 10, on the console displays information from the record with the corresponding number, when you enter the 'all' command - all data is displayed.
When writing an application, it is recommended that you use the libmysqlclient library.
 Also for verification, you must provide a dump of the database structure.



Additional tasks for Task 2:


1. Add a command to search for a record by last name and / or other parameters.

2. Implement the ability to add new records to the database.

3. Implement an additional table with interests (hobbies) and make a bunch of it with the main
table. In this case, when searching, display the complete information about the contact.



Completed: 1, 2.



How to start:

First of all, change connection string in ContextDB.cs [../Task_2/Notebook/Notebook/Context/ContextDB.cs].


Call a script named "NotebookStart.sh" [Example 1].



//----------Example 1----------//

cd ...Your path to folder "Summer Tasks".../Task_2/

sh NotebookStart.sh

//-----------------------------//




Additional

There is a database dump ../Task_2/Notebook_DB.sql.




If you are a Windows user, just execute the ".bat" file:
1) "NotebookStart.bat" for program start.