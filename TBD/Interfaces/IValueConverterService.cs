using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TBD.Interfaces
{
    public interface IValueConverterService
    {
        ValueConverter DateTimeOffsetTimestampConverter { get; }
        ValueConverter DateTimeOffsetDateIntConverter { get; }
        ValueConverter IntDouble1000Converter { get; }
        ValueConverter IntDouble100Converter { get; }
    }
}
