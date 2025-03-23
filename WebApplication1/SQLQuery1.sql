CREATE DATABASE NMT_DB;
GO
USE NMT_DB;
GO
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    RegistrationDate DATETIME NOT NULL DEFAULT GETDATE()
);
CREATE TABLE Subjects (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    MaxScore INT NOT NULL,
    Description NVARCHAR(500) NULL
);
CREATE TABLE QuestionTypes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255) NULL
);
CREATE TABLE Questions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SubjectId INT NOT NULL,
    QuestionTypeId INT NOT NULL,
    Text NVARCHAR(1000) NOT NULL,
    Difficulty INT NOT NULL,
    Score INT NOT NULL,
    FOREIGN KEY (SubjectId) REFERENCES Subjects(Id),
    FOREIGN KEY (QuestionTypeId) REFERENCES QuestionTypes(Id)
);
CREATE TABLE Answers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    QuestionId INT NOT NULL,
    Text NVARCHAR(500) NOT NULL,
    IsCorrect BIT NOT NULL,
    FOREIGN KEY (QuestionId) REFERENCES Questions(Id)
);
CREATE TABLE TestResults (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    SubjectId INT NOT NULL,
    Date DATETIME NOT NULL DEFAULT GETDATE(),
    Score INT NOT NULL,
    MaxScore INT NOT NULL,
    Duration INT NOT NULL, 
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (SubjectId) REFERENCES Subjects(Id)
    );
    INSERT INTO Subjects (Name, MaxScore, Description) VALUES 
('Українська мова', 45, 'Предмет з української мови для НМТ'),
('Математика', 32, 'Предмет з математики для НМТ'),
('Історія України', 54, 'Предмет з історії України для НМТ'),
('Іноземні мови', 32, 'Предмет з англійської мови для НМТ'),
('Фізика', 32, 'Предмет з фізики для НМТ'),
('Хімія', 40, 'Предмет з хімії для НМТ'),
('Біологія', 46, 'Предмет з біології для НМТ'),
('Українська література', 45, 'Предмет з української літератури для НМТ'),
('Географія', 46, 'Предмет з географії для НМТ');

INSERT INTO Questions (SubjectId, QuestionTypeId, Text, Difficulty, Score) VALUES 
(1, 1, 'У якому рядку всі слова пишуться з префіксом с-?', 2, 1),
(1, 1, 'Позначте рядок, де в усіх словах однакова кількість букв і звуків', 2, 1),
(1, 3, 'Установіть відповідність між фразеологізмом та його значенням', 3, 2);
INSERT INTO Answers (QuestionId, Text, IsCorrect) VALUES 
(1, 'спалахнути, сфотографувати, спробувати', 0),
(1, 'сказати, схопити, створити', 0),
(1, 'стиха, скроїти, сховати', 0),
(1, 'стерти, сформувати, склеїти', 1),
(2, 'їжак, лялька, день', 0),
(2, 'щука, яма, ідея', 1),
(2, 'м`яч, дзвін, пюре', 0),
(2, 'юшка, єдність, плющ', 0);
INSERT INTO Answers (QuestionId, Text, IsCorrect) VALUES 
(3, 'Бити байдики - A. ледарювати', 1),
(3, 'Під носом - Б. близько', 1),
(3, 'Пасти задніх - В. відставати', 1),
(3, 'Рукою подати - Г. дуже близько', 1),
(3, 'Як кіт наплакав - Д. мало', 1);