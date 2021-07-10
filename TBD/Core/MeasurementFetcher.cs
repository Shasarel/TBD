using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TBD.Interfaces;
using Newtonsoft.Json;
using TBD.DbModels;

namespace TBD.Core
{
    public class MeasurementFetcher:IMeasurementFetcher
    {
        private readonly AppSettings _appSettings;
        private readonly IValueConverterService _dbValueConverter;
        private const double EnergyProductionOffset = 1609279.270;

        public MeasurementFetcher(IOptions<AppSettings> appSettings, IValueConverterService dbValueConverter)
        {
            _appSettings = appSettings.Value;
            _dbValueConverter = dbValueConverter;
        }

        public async Task<Tuple<ElectricityMeasurement, MeteoMeasurement>> GetAllMeasurements()
        {
            var electricityMeasurementTask = GetElectricityMeasurement();
            var meteoMeasurementTask = GetMeteoMeasurement();

            return new Tuple<ElectricityMeasurement, MeteoMeasurement>(await electricityMeasurementTask, await meteoMeasurementTask);
        }

        public async Task<ElectricityMeasurement> GetElectricityMeasurement()
        {
            dynamic eMeterData;
            string flaraList;
            string flaraData;

            var remainingTrials = 3;

            do
            {
                var flaraListTask = GetResponse(_appSettings.FlaraListAddress);
                var eMeterDataTask = GetJsonResponse(_appSettings.EmeterAddress);
                var flaraDataTask = GetResponse(_appSettings.FlaraDataAddress);

                eMeterData = await eMeterDataTask;
                flaraList = await flaraListTask;
                flaraData = await flaraDataTask;

                remainingTrials--;

                if(remainingTrials == 0)
                    return new ElectricityMeasurement();

            } while (eMeterData == null || flaraList == null
                                        || flaraList == "<tr class='msgfail'><td>Devices not found.</td></tr>"
                                        || flaraData == null);


            var matches = Regex.Matches(flaraData!, "((?<=Active power</div><div class='pvalue vok'>)[0-9.]*)|" +
                                                    "((?<=Total energy</div><div class='pvalue vok'>)[0-9.]*)").ToArray();

            var powerImport = 0.0;
            var powerExport = 0.0;

            foreach (var x in new List<string>{"7","8","9"})
            {
                double power = eMeterData![x];
                if (power < 0)
                    powerExport -= power;
                else
                {
                    powerImport += power;
                }
            }

            var electricityMeasurement = new ElectricityMeasurement
            {
                EnergyProduction = double.Parse(matches[1].Value, CultureInfo.InvariantCulture) - EnergyProductionOffset,
                EnergyImport = eMeterData!["37"],
                EnergyExport = eMeterData!["38"],
                PowerProduction = int.Parse(matches[0].Value, CultureInfo.InvariantCulture),
                PowerImport = (int)Math.Round(powerImport),
                PowerExport = (int)Math.Round(powerExport),
                Correct = true
            };

            electricityMeasurement.PowerUse =
                Math.Max(0, electricityMeasurement.PowerProduction - electricityMeasurement.PowerExport);

            electricityMeasurement.PowerConsumption =
                Math.Max(0, electricityMeasurement.PowerImport + electricityMeasurement.PowerUse);

            electricityMeasurement.PowerStore =
                (int) Math.Round((electricityMeasurement.PowerExport * _appSettings.EnergyReturnFactor) - electricityMeasurement.PowerImport);

            return electricityMeasurement;
        }

        public async Task<MeteoMeasurement> GetMeteoMeasurement()
        {
            dynamic meteoData = await GetJsonResponse(_appSettings.MeteoAddress);
            if (meteoData == null)
                return null;

            var isDataCorrect = true;
            if (meteoData["cold"] == 1 || meteoData["bmpcon"] == 0 || meteoData["pmscon"] == 0 || meteoData["sicon"] == 0) 
                isDataCorrect = false;

            var temperature = (double) _dbValueConverter.IntDouble100Converter.ConvertFromProvider.Invoke(meteoData["sitemp"]);

            return new MeteoMeasurement()
            {
                DateTime = DateTimeOffset.UtcNow,
                Temperature = temperature,
                Pressure = CalculateRelativePressure((double)_dbValueConverter.IntDouble100Converter.ConvertFromProvider.Invoke(meteoData["bmppress"]), temperature),
                DustPM10 = meteoData["pms3"],
                DustPM25 = meteoData["pms4"],
                DustPM100 = meteoData["pms5"],
                IsDataCorrect = isDataCorrect
            };
        }
        private double CalculateRelativePressure(double pressure, double temperature)
        {
            var f1 = 0.0065 * _appSettings.Altitude;
            var f2 = 1.0 - f1 / (f1 + temperature + 273.15);
            var f3 = Math.Pow(f2, -5.257);
            return Math.Round(pressure * f3, 1);
        }

        private async Task<object> GetJsonResponse(string url)
        {
            try
            {
                return JsonConvert.DeserializeObject(await GetResponse(url));
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<string> GetResponse(string url)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(url);
                var responseAsync = request.GetResponseAsync();

                using var response = (HttpWebResponse) await responseAsync;
                await using var stream = response.GetResponseStream();

                if (stream == null)
                    return null;
                using var reader = new StreamReader(stream);
                var html = await reader.ReadToEndAsync();

                return html;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
