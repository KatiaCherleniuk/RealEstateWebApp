CREATE TABLE CategoryDisplaySettings
(
    Id INT PRIMARY KEY IDENTITY,
    CategoryId INT NOT NULL,
    PropertyId INT NOT NULL,
    DisplayOrder INT,
    CONSTRAINT FK_CategoryDisplaySettings_CategoryId FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
    CONSTRAINT FK_CategoryDisplaySettings_PropertyId FOREIGN KEY (PropertyId) REFERENCES Properties(Id)
)