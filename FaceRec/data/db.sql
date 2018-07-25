CREATE TABLE IF NOT EXISTS `tblUsers` ( `Id` TEXT NOT NULL, `Name` TEXT NOT NULL, `Face` BLOB NOT NULL, `Encoding` BLOB NOT NULL, `GroupId` TEXT NOT NULL, `Comment` TEXT, PRIMARY KEY(`Id`) );

CREATE TABLE IF NOT EXISTS `tblUserGroups` ( `Id` TEXT NOT NULL, `Name` TEXT, PRIMARY KEY(`Id`) );

CREATE UNIQUE INDEX IF NOT EXISTS `ux_tblUserGroups_Name` ON `tblUserGroups` ( `Name` );

CREATE UNIQUE INDEX IF NOT EXISTS `ux_tblUsers_name` ON `tblUsers` ( `Name` );

create view if not exists vwUsers as select u.*, ug.name as GroupName from tblUsers u inner join tblUserGroups ug on u.GroupId = ug.Id;
