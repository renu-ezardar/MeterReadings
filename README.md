# MeterReadings
Meter Readings

This project is to Upload a CSV file with meter readings for account id
  PreRequisite - Using DB first Approach so make sure  Accounts and Meterreading tables exists in DB and Account table list has valid AccountIds

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





