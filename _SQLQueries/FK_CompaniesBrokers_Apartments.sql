ALTER TABLE dbo.Apartments
ADD CONSTRAINT FK_CompaniesBrokers_Apartments
	FOREIGN KEY(CompanyId, BrokerId)
	REFERENCES dbo.CompaniesBrokers(CompanyId, BrokerId)