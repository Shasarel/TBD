using Microsoft.EntityFrameworkCore.Migrations;

namespace TBD.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyElectricitySummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<int>(type: "INTEGER", nullable: false),
                    EnergyProduction = table.Column<int>(type: "INTEGER", nullable: false),
                    EnergyImport = table.Column<int>(type: "INTEGER", nullable: false),
                    EnergyExport = table.Column<int>(type: "INTEGER", nullable: false),
                    EnergyProductionSum = table.Column<int>(type: "INTEGER", nullable: false),
                    EnergyImportSum = table.Column<int>(type: "INTEGER", nullable: false),
                    EnergyExportSum = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxPowerProduction = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxPowerImport = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxPowerExport = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxPowerConsumption = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxPowerUse = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxPowerStore = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyElectricitySummary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyMeteoSummary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<int>(type: "INTEGER", nullable: false),
                    TemperatureMin = table.Column<int>(type: "INTEGER", nullable: false),
                    TemperatureAvg = table.Column<int>(type: "INTEGER", nullable: false),
                    TemperatureMax = table.Column<int>(type: "INTEGER", nullable: false),
                    HumidityMin = table.Column<int>(type: "INTEGER", nullable: false),
                    HumidityAvg = table.Column<int>(type: "INTEGER", nullable: false),
                    HumidityMax = table.Column<int>(type: "INTEGER", nullable: false),
                    PressureMin = table.Column<int>(type: "INTEGER", nullable: false),
                    PressureAvg = table.Column<int>(type: "INTEGER", nullable: false),
                    PressureMax = table.Column<int>(type: "INTEGER", nullable: false),
                    DustPM10Min = table.Column<int>(type: "INTEGER", nullable: false),
                    DustPM10Avg = table.Column<int>(type: "INTEGER", nullable: false),
                    DustPM10Max = table.Column<int>(type: "INTEGER", nullable: false),
                    DustPM25Min = table.Column<int>(type: "INTEGER", nullable: false),
                    DustPM25Avg = table.Column<int>(type: "INTEGER", nullable: false),
                    DustPM25Max = table.Column<int>(type: "INTEGER", nullable: false),
                    DustPM100Min = table.Column<int>(type: "INTEGER", nullable: false),
                    DustPM100Avg = table.Column<int>(type: "INTEGER", nullable: false),
                    DustPM100Max = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDataCorrect = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMeteoSummary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ElectricityMeasurement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<long>(type: "INTEGER", nullable: false),
                    EnergyProduction = table.Column<int>(type: "INTEGER", nullable: false),
                    EnergyImport = table.Column<int>(type: "INTEGER", nullable: false),
                    EnergyExport = table.Column<int>(type: "INTEGER", nullable: false),
                    PowerProduction = table.Column<int>(type: "INTEGER", nullable: false),
                    PowerImport = table.Column<int>(type: "INTEGER", nullable: false),
                    PowerExport = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricityMeasurement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnergyCorrection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<int>(type: "INTEGER", nullable: false),
                    Correction = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyCorrection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeteoMeasurement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<long>(type: "INTEGER", nullable: false),
                    Temperature = table.Column<int>(type: "INTEGER", nullable: false),
                    Humidity = table.Column<int>(type: "INTEGER", nullable: false),
                    Pressure = table.Column<int>(type: "INTEGER", nullable: false),
                    DustPM10 = table.Column<int>(type: "INTEGER", nullable: false),
                    DustPM25 = table.Column<int>(type: "INTEGER", nullable: false),
                    DustPM100 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeteoMeasurement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    ApiKey = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlindSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Device = table.Column<int>(type: "INTEGER", nullable: false),
                    Action = table.Column<int>(type: "INTEGER", nullable: false),
                    HourType = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeOffset = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlindSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlindSchedule_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlindTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<long>(type: "INTEGER", nullable: false),
                    Device = table.Column<int>(type: "INTEGER", nullable: false),
                    Action = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    BlindsScheduleId = table.Column<int>(type: "INTEGER", nullable: true),
                    Timeout = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlindTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlindTask_BlindSchedule_BlindsScheduleId",
                        column: x => x.BlindsScheduleId,
                        principalTable: "BlindSchedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlindTask_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlindSchedule_UserId",
                table: "BlindSchedule",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlindTask_BlindsScheduleId",
                table: "BlindTask",
                column: "BlindsScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_BlindTask_UserId",
                table: "BlindTask",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlindTask");

            migrationBuilder.DropTable(
                name: "DailyElectricitySummary");

            migrationBuilder.DropTable(
                name: "DailyMeteoSummary");

            migrationBuilder.DropTable(
                name: "ElectricityMeasurement");

            migrationBuilder.DropTable(
                name: "EnergyCorrection");

            migrationBuilder.DropTable(
                name: "MeteoMeasurement");

            migrationBuilder.DropTable(
                name: "BlindSchedule");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
