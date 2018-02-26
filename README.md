# ToDo List

#### .NET MVC app that allows the user to compile a list of categories and a list of items that belong to each category, 2-20-18

#### _By Sara Hamilton_

## Description
_This is an Epicodus practice project for week 3 of the C# course. Its purpose is to demonstrate understanding of SQL and databases._

#### _ToDo List_
* Allows the user to add a new category
* Allows the user to add a new item and assign the item to an existing category
* Allows the user to see a list of all items
* Allows the user to select a category, and see a list of items that belong to that category
_update-and-delete branch_
* Allows the user to edit categories and items
* Allows the user to delete categories and items, when a category is deleted, all items belonging to that category are deleted

#### _master branch_
* One to Many relationship one category to many items

```
CREATE TABLE `categories` ( `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT, `name` varchar(255) DEFAULT NULL, PRIMARY KEY (`id`), UNIQUE KEY `id` (`id`)) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8

CREATE TABLE `items` (`id` int(11) NOT NULL AUTO_INCREMENT, `description` varchar(255) DEFAULT NULL, `duedate` date DEFAULT NULL,`category_id` int(11) DEFAULT NULL, PRIMARY KEY (`id`)) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8
```

#### _many-to-many branch_
* Many to Many relationship many categories to many items

CREATE TABLE `todo_test`.`categories_items` ( `id` INT NOT NULL AUTO_INCREMENT , `category_id` INT NOT NULL , `item_id` INT NOT NULL , PRIMARY KEY (`id`)) ENGINE = InnoDB;



  ## Setup/Installation Requirements

  * _Clone this GitHub repository_

  ```
  git clone https://github.com/Sara-Hamilton/ToDoListWithDatabase.git
  ```

  * _Install the .NET Framework and MAMP_

    .NET Core 1.1 SDK (Software Development Kit)

    .NET runtime.

    MAMP

    See https://www.learnhowtoprogram.com/c/getting-started-with-c/installing-c for instructions and links.

* _Start the Apache and MySql Servers in MAMP_

*   _Setup the database_

  Either type the following commands into SQL on the command line or download the zipfile of the database that is included in this Github repository.  
  ```
  CREATE DATABASE to_do;
  USE to_do;
  CREATE TABLE categories ( id serial PRIMARY KEY, name VARCHAR(255));
  CREATE TABLE items ( id serial PRIMARY KEY, name VARCHAR(255), description VARCHAR(255), duedate DATE, category_id INT, PRIMARY KEY (`id`));
  ```

    See https://www.learnhowtoprogram.com/c/database-basics-ee7c9fd3-fcd9-4fff-8b1d-5ff7bfcbf8f0/database-practice-and-world-data for instructions and links explaining how to download the zipfile that is located inside this github repository.

  * _Run the program_
    1. In the command line, cd into the project folder.
    ```
    cd ToDoListWithDatabase.Solution
    cd ToDoListWithDatabase
    ```
    2. In the command line, type dotnet restore. Enter.  It make take a few minutes to complete this process.
    ```
    dotnet restore
    ```
    3. In the command line, type dotnet build. Enter. Any error messages will be displayed in red.  Errors will need to be corrected before the app can be run. After correcting errors and saving changes, type dotnet build again.  When message says Build succeeded in green, proceed to the next step.
    ```
    dotnet build
    ```
    4. In the command line, type dotnet run. Enter.
    ```
    dotnet run
    ```

  * _View program on web browser at port localhost:5000/items_

  * _Follow the prompts._

  ## Support and contact details

_To suggest changes, submit a pull request in the GitHub repository._

## Technologies Used

* HTML
* Bootstrap
* C#
* MAMP
* .Net Core 1.1
* Razor
* MySQL

### License

*MIT License*

Copyright (c) 2018 **_Sara Hamilton_**

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
