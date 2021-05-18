# CodeAssignmentPeerIslands

## Framework Version
> `.NET Core 3.1`

## Problem 1: 
Create a SQL query supporting various operators ( IN, LIKE, =, <=, >= , <>, BETWEEN etc)

Assume the application you write can have a JSON in the below format to parse and create the expression

> {"columns":[{"operator":"IN","fieldName":"column1","fieldValue":"value"},{"operator":"Equal","fieldName":"column2","fieldValue":"value"}}

As part of your assignment, please write C#/Java code to implement the following functionalities: -
- Read the JSON file.
- Create an SQL QUERY as an output.

## Problem 2: 
Extend the program further to support querying from multiple tables i.e., add support for sub-query or joins in the query builder
The idea of this problem should be to provide a generic solution to build SQL Query supporting any number of tables.

## Solution
For the project, an example.json file was created and this file was read.
> Note In order for the exercise to be properly tested, a database creation file containing some fakes data was attached, in case you want to do some testing.

Created a console project, divided into layers:
- Domain Layer
- Service Layer
- Infra Layer
- Test Layer

## Setup

To run the project, just make sure that the example.json file is in the Assets folder

| OS | ATTENTION |
| ------ | ------ |
| Windows | check the path inside the appsettings.json file |
| MAC OS / Linux | check the path inside the appsettings.json file |

### No Docker
Go to project folder and

```sh
dotnet restore
```

After using the command to restore all the packages
```sh
dotnet run
```

### Docker

Build the docker image and run it

```sh
docker build -t codeassignment .
```
```sh
docker run -d -p 80:80 --name=codetest codeassignment
```
