CREATE TABLE CompaniesBrokers
(
	CompanyId int,
	BrokerId int,
	CONSTRAINT PK_CompaniesBrokers PRIMARY KEY (CompanyId, BrokerId),
	CONSTRAINT FK_Companies_CompaniesBrokers
		FOREIGN KEY (CompanyId) REFERENCES dbo.Companies (Id),
	CONSTRAINT FK_Brokers_CompaniesBrokers
		FOREIGN KEY (BrokerId) REFERENCES dbo.Brokers (Id)
);