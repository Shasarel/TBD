
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

function openPicker(picker) {
    picker.open();

    event.stopPropagation();
    event.preventDefault();
}

var pickerFrom;
var pickerTo;

function createPickers() {
    var firstDate = new Date(2018, 6, 21);
    var inputTo = $('#date-to').pickadate({
        format: 'yyyy-mm-dd',
        min: firstDate,
        max: new Date(),
        selectYears: true,
        selectMonths: true,
        clear: '',
        onRender: function () {
            $('#date-to_root > div > div > div > .picker__box')
                .prepend('<p class="text-center" style="padding:.25em;text-align:center; font-weight:bold;">Data do: </p>');
        },
        onClose: function () {
            if (pickerTo.get() !== "") {
                console.log(pickerFrom.get());
                console.log(pickerTo.get());
            }
        }
    });
    pickerTo = inputTo.pickadate("picker");

    var inputFrom = $('#date-from').pickadate({
        format: 'yyyy-mm-dd',
        min: firstDate,
        max: new Date(),
        selectYears: true,
        selectMonths: true,
        clear: '',
        onRender: function() {
            $('#date-from_root > div > div > div > .picker__box')
                .prepend('<p class="text-center" style="padding:.25em;text-align:center; font-weight:bold;">Data od: </p>');
        },
        onClose: function () {
            if (pickerFrom.get() !== "") {
                pickerTo.set("min", pickerFrom.get());
                pickerTo.set("select", firstDate);
                pickerTo.open();
            }
        }
    });
    pickerFrom = inputFrom.pickadate("picker");

    pickerFrom.set("select", firstDate);
}
