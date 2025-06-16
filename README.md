# MeterReadings
Meter Readings

This project is to Upload a CSV file with meter readings for account id

PreRequisite - 
  
1. Using DB first Approach so make sure  Accounts and Meterreading tables exists in DB and Account table list has valid AccountIds

  Create table MeterReadings  using following statements

  USE [AccountMeter_Test]
GO

/****** Object:  Table [dbo].[Accounts]    Script Date: 15/06/2025 20:55:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MeterReadings](
	[ReadingId] [int] IDENTITY(1,1)  Primary Key NOT NULL,
	[AccountId] [int]  NOT NULL,
	[MeterReadingDateTime] [DateTime] NOT NULL,
	[MeterReadValue] [int] NOT NULL
) ON [PRIMARY]
GO



Alter table  [dbo].[MeterReadings] 
Add Constraint FK_AccountId 
Foreign Key (AccountID)
References   Accounts (AccountID)


Go


2.Update  Connection string in src\AccountMeter.API\appsettings.Development.json  -- Ideally it should be in Secret manager if in AWS or some other secure place/


--How to Run the Project 

Open the solution in VS 2022, Run it  using ctrl+F5, Swagger will open, upload the needed file and test the fucntionality


--Things to do 
A unit test project can be added as well to verify the file inputs and data with local Db string or environment specific string 





