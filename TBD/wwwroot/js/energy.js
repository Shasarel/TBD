
var interval = setInterval(refreshPowerData, 1000);

    function refreshPowerData() {
    $.ajax({
        url: "Energy/GetPowerNow",
        type: "GET",
        success: function (data) {
            $("#power-production").html(data.electricityMeasurement.powerProduction);
            $("#power-production-percentage").css("width", data.powerProductionPercentage + "%");

            $("#power-consumption").html(data.electricityMeasurement.powerConsumption);
            $("#power-consumption-percentage").css("width", data.powerConsumptionPercentage + "%");

            $("#power-use").html(data.electricityMeasurement.powerUse);
            $("#power-use-percentage").css("width", data.powerUsePercentage + "%");

            $("#power-import").html(data.electricityMeasurement.powerImport);
            $("#power-import-percentage").css("width", data.powerImportPercentage + "%");

            $("#power-export").html(data.electricityMeasurement.powerExport);
            $("#power-export-percentage").css("width", data.powerExportPercentage + "%");

            $("#power-store").html(data.electricityMeasurement.powerStore);
            $("#power-store-percentage").css("width", data.powerStorePercentage + "%");
        }
    });
}