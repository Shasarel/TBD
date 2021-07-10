using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TBD.Interfaces;

namespace TBD.Services
{
    public class ValueConverterService : IValueConverterService
    {
        public ValueConverter DateTimeOffsetTimestampConverter
        {
            get
            {
                return _dateTimeOffsetTimestampConverter ??= new ValueConverter<DateTimeOffset, long>(
                    v => v.ToUnixTimeSeconds(),
                    v => DateTimeOffset.FromUnixTimeSeconds(v));
            }
        }

        public ValueConverter DateTimeOffsetDateIntConverter
        {
            get
            {
                return _dateTimeOffsetDateIntConverter ??= new ValueConverter<DateTimeOffset, int>(
                    v => Convert.ToInt32($"{v.Year}{v.Month}{v.Day}"),
                    v => new DateTimeOffset(
                        Convert.ToInt32(v.ToString().Substring(0, 4)),
                        Convert.ToInt32(v.ToString().Substring(4, 2)),
                        Convert.ToInt32(v.ToString().Substring(6, 2)),
                        0, 0, 0, new TimeSpan(0)));
            }
        }

        public ValueConverter IntDouble1000Converter
        {
            get
            {
                return _intDouble1000Converter ??= new ValueConverter<double, int>(
                    v => (int) Math.Round(v * 1000),
                    v => v / 1000.0);
            }
        }

        public ValueConverter IntDouble100Converter
        {
            get
            {
                return _intDouble100Converter ??= new ValueConverter<double, int>(
                    v => (int)Math.Round(v * 100),
                    v => v / 100.0);
            }
        }

        private ValueConverter _dateTimeOffsetTimestampConverter;
        private ValueConverter _dateTimeOffsetDateIntConverter;
        private ValueConverter _intDouble1000Converter;
        private ValueConverter _intDouble100Converter;
    }
}
