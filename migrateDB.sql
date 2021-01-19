ALTER TABLE user
RENAME TO user_old;

CREATE TABLE "User" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_User" PRIMARY KEY AUTOINCREMENT,
    "Login" TEXT NOT NULL,
    "PasswordHash" TEXT NOT NULL,
    "Name" TEXT NOT NULL,
    "Role" INTEGER NOT NULL,
    "ApiKey" TEXT NOT NULL
);

CREATE TABLE "BlindSchedule" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_BlindSchedule" PRIMARY KEY AUTOINCREMENT,
    "Device" INTEGER NOT NULL,
    "Action" INTEGER NOT NULL,
    "HourType" INTEGER NOT NULL,
    "TimeOffset" INTEGER NOT NULL,
    "UserId" INTEGER NOT NULL,
    "IsActive" INTEGER NOT NULL,
    CONSTRAINT "FK_BlindSchedule_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "BlindTask" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_BlindTask" PRIMARY KEY AUTOINCREMENT,
    "Timestamp" INTEGER NOT NULL,
    "Device" INTEGER NOT NULL,
    "Action" INTEGER NOT NULL,
    "UserId" INTEGER NULL,
    "BlindsScheduleId" INTEGER NULL,
    "Timeout" INTEGER NOT NULL,
    "Status" INTEGER NOT NULL,
    CONSTRAINT "FK_BlindTask_BlindSchedule_BlindsScheduleId" FOREIGN KEY ("BlindsScheduleId") REFERENCES "BlindSchedule" ("Id") ON DELETE RESTRICT,
    CONSTRAINT "FK_BlindTask_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE RESTRICT
);

CREATE TABLE "DailyElectricitySummary" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_DailyElectricitySummary" PRIMARY KEY AUTOINCREMENT,
    "Date" INTEGER NOT NULL,
    "EnergyProduction" INTEGER NOT NULL,
    "EnergyImport" INTEGER NOT NULL,
    "EnergyExport" INTEGER NOT NULL,
    "EnergyProductionSum" INTEGER NOT NULL,
    "EnergyImportSum" INTEGER NOT NULL,
    "EnergyExportSum" INTEGER NOT NULL,
    "MaxPowerProduction" INTEGER NOT NULL,
    "MaxPowerImport" INTEGER NOT NULL,
    "MaxPowerExport" INTEGER NOT NULL,
    "MaxPowerConsumption" INTEGER NOT NULL,
    "MaxPowerUse" INTEGER NOT NULL,
    "MaxPowerStore" INTEGER NOT NULL
);

CREATE TABLE "DailyMeteoSummary" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_DailyMeteoSummary" PRIMARY KEY AUTOINCREMENT,
    "Date" INTEGER NOT NULL,
    "TemperatureMin" INTEGER NOT NULL,
    "TemperatureAvg" INTEGER NOT NULL,
    "TemperatureMax" INTEGER NOT NULL,
    "HumidityMin" INTEGER NOT NULL,
    "HumidityAvg" INTEGER NOT NULL,
    "HumidityMax" INTEGER NOT NULL,
    "PressureMin" INTEGER NOT NULL,
    "PressureAvg" INTEGER NOT NULL,
    "PressureMax" INTEGER NOT NULL,
    "DustPM10Min" INTEGER NOT NULL,
    "DustPM10Avg" INTEGER NOT NULL,
    "DustPM10Max" INTEGER NOT NULL,
    "DustPM25Min" INTEGER NOT NULL,
    "DustPM25Avg" INTEGER NOT NULL,
    "DustPM25Max" INTEGER NOT NULL,
    "DustPM100Min" INTEGER NOT NULL,
    "DustPM100Avg" INTEGER NOT NULL,
    "DustPM100Max" INTEGER NOT NULL,
    "IsDataCorrect" INTEGER NOT NULL
);

CREATE TABLE "ElectricityMeasurement" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_ElectricityMeasurement" PRIMARY KEY AUTOINCREMENT,
    "Timestamp" INTEGER NOT NULL,
    "EnergyProduction" INTEGER NOT NULL,
    "EnergyImport" INTEGER NOT NULL,
    "EnergyExport" INTEGER NOT NULL,
    "PowerProduction" INTEGER NOT NULL,
    "PowerImport" INTEGER NOT NULL,
    "PowerExport" INTEGER NOT NULL
);

CREATE TABLE "EnergyCorrection" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_EnergyCorrection" PRIMARY KEY AUTOINCREMENT,
    "Date" INTEGER NOT NULL,
    "Correction" INTEGER NOT NULL
);

CREATE TABLE "MeteoMeasurement" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_MeteoMeasurement" PRIMARY KEY AUTOINCREMENT,
    "Timestamp" INTEGER NOT NULL,
    "Temperature" INTEGER NOT NULL,
    "Humidity" INTEGER NOT NULL,
    "Pressure" INTEGER NOT NULL,
    "DustPM10" INTEGER NOT NULL,
    "DustPM25" INTEGER NOT NULL,
    "DustPM100" INTEGER NOT NULL
);

CREATE TABLE "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);


INSERT INTO User(Id, Login, PasswordHash, Name, Role, ApiKey)
SELECT id, username, password_hash, name, role, api_key FROM user_old;

INSERT INTO BlindSchedule(Id, Device, Action, HourType, TimeOffset, UserId, IsActive)
SELECT id, device-1, action-1, hour_type-1, time_offset, user_id, 1 FROM blinds_schedule;

INSERT INTO BlindTask(Id, Timestamp, Device, Action, UserId, BlindsScheduleId, Timeout, Status)
SELECT id, time, device-1, action-1, user_id, schedule_id, timeout, (CASE WHEN active=0 THEN 1 ELSE 0 END) AS Status FROM blinds_task;

INSERT INTO BlindTask(Timestamp, Device, Action, UserId, BlindsScheduleId, Timeout, Status)
SELECT time, device-1, action-1, (CASE WHEN user_id=3 THEN NULL ELSE user_id END) AS Status, schedule_id, 0, (CASE WHEN status=0 THEN 4 ELSE 3 END) AS Status FROM blinds_task_history;

INSERT INTO DailyElectricitySummary(Id, Date, EnergyProduction, EnergyImport, EnergyExport, EnergyProductionSum, EnergyImportSum, EnergyExportSum, MaxPowerProduction, MaxPowerImport, MaxPowerExport, MaxPowerConsumption, MaxPowerUse, MaxPowerStore)
Select id, CAST((strftime('%Y%m%d',DateTime('2100-01-01', '-' || (766645 - day_ordinal) || ' Day'))) AS INTEGER), production, import, export, production_offset, import_offset, export_offset, max_power_production, max_power_import, max_power_export, max_power_consumption, max_power_use, max_power_store FROM energy_daily;

INSERT INTO DailyMeteoSummary(Id, Date, TemperatureMin, TemperatureAvg, TemperatureMax, HumidityMin, HumidityAvg, HumidityMax, PressureMin, PressureAvg, PressureMax, DustPM10Min, DustPM10Avg, DustPM10Max, DustPM25Min, DustPM25Avg, DustPM25Max, DustPM100Min, DustPM100Avg, DustPM100Max, IsDataCorrect)
SELECT id, CAST((strftime('%Y%m%d',DateTime('2100-01-01', '-' || (766645 - day_ordinal) || ' Day'))) AS INTEGER), temperature_min, temperature_avg, temperature_max, humidity_min, humidity_avg, humidity_max, pressure_min, pressure_avg, pressure_max, dust_PM10_min, dust_PM10_avg, dust_PM10_max, dust_PM25_min, dust_PM25_avg, dust_PM25_max, dust_PM100_min, dust_PM100_avg, dust_PM100_max, is_data_correct FROM meteo_daily;

INSERT INTO EnergyCorrection(Id, Date, Correction)
SELECT id, date, correction FROM energy_corrections;

INSERT INTO ElectricityMeasurement(Id, Timestamp, EnergyProduction, EnergyImport, EnergyExport, PowerProduction, PowerImport, PowerExport)
SELECT id, time, production, import, export, power_production, power_import, power_export FROM energy;

INSERT INTO MeteoMeasurement(Id, Timestamp, Temperature, Humidity, Pressure,DustPM10, DustPM25, DustPM100)
SELECT id, time, temperature, humidity, pressure, dust_PM10, dust_PM25, dust_PM100 FROM meteo;

INSERT INTO __EFMigrationsHistory(MigrationId, ProductVersion)
VALUES("20210118180139_InitMigration", "5.0.2");


DROP TABLE blinds_task;
DROP TABLE blinds_task_history;
DROP TABLE blinds_schedule;
DROP TABLE energy;
DROP TABLE energy_corrections;
DROP TABLE energy_daily;
DROP TABLE meteo_daily;
DROP TABLE meteo;
DROP TABLE user_mac_address;
DROP TABLE user_old;

VACUUM;
