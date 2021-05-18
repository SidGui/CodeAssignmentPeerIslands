CREATE DATABASE CodeAssignmentPeerIslandsTest;

USE CodeAssignmentPeerIslandsTest;

CREATE TABLE Person (
    Id Int IDENTITY PRIMARY KEY,
    Name VARCHAR(100),
    Profession VARCHAR(100),
);

CREATE TABLE Users (
    Id Int IDENTITY PRIMARY KEY,
    Username VARCHAR(100),
    PersonId INT FOREIGN KEY REFERENCES Person(Id)
);

CREATE TABLE Log (
    CreatedAt DATETIME DEFAULT GETDATE(),
    UserId INT FOREIGN KEY REFERENCES Users(Id)
)

INSERT INTO Person(Name, Profession) Values ('Guilherme', 'Software Engineer');
INSERT INTO Person(Name, Profession) Values('Herique', 'Solutions architect');
INSERT INTO Users(Username, PersonId) Values ('Guilherme@Santos', 1);
INSERT INTO Users(Username, PersonId) Values('Herique@Costa', 2);
INSERT INTO Log(UserId) Values (2);
INSERT INTO Log(UserId) Values (2);
INSERT INTO Log(UserId) Values (2);