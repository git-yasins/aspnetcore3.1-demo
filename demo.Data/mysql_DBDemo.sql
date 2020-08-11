CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Leagues` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Country` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Leagues` PRIMARY KEY (`Id`)
);

CREATE TABLE `Clubs` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `City` longtext CHARACTER SET utf8mb4 NULL,
    `DateOfEstablishment` datetime(6) NOT NULL,
    `History` longtext CHARACTER SET utf8mb4 NULL,
    `LeagueId` int NULL,
    CONSTRAINT `PK_Clubs` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Clubs_Leagues_LeagueId` FOREIGN KEY (`LeagueId`) REFERENCES `Leagues` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Players` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `DateOfBirth` datetime(6) NOT NULL,
    `ClubId` int NULL,
    CONSTRAINT `PK_Players` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Players_Clubs_ClubId` FOREIGN KEY (`ClubId`) REFERENCES `Clubs` (`Id`) ON DELETE RESTRICT
);

CREATE INDEX `IX_Clubs_LeagueId` ON `Clubs` (`LeagueId`);

CREATE INDEX `IX_Players_ClubId` ON `Players` (`ClubId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20200628121225_init', '3.1.5');

