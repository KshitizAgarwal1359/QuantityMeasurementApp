-- UC16: schema.sql — Run this once in SSMS before starting the app
CREATE DATABASE QuantityMeasurementDB;
GO
USE QuantityMeasurementDB;
GO
CREATE TABLE QuantityMeasurements (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    FirstOperand     NVARCHAR(500)  NOT NULL,
    SecondOperand    NVARCHAR(500)  NOT NULL,
    OperationType    NVARCHAR(100)  NOT NULL,
    MeasurementType  NVARCHAR(100)  NOT NULL,
    FinalResult      NVARCHAR(500)  NOT NULL,
    HasError         BIT            NOT NULL DEFAULT 0,
    ErrorMessage     NVARCHAR(1000) NOT NULL DEFAULT 'None',
    RecordedAt       DATETIME2      NOT NULL DEFAULT GETUTCDATE()
);
GO
CREATE INDEX IX_QuantityMeasurements_OperationType
    ON QuantityMeasurements(OperationType);
CREATE INDEX IX_QuantityMeasurements_MeasurementType
    ON QuantityMeasurements(MeasurementType);
GO
