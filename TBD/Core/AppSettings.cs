namespace TBD.Core
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public double TokenLifetime { get; set; }
        public string MeteoAddress { get; set; }
        public string EmeterAddress { get; set; }
        public string FlaraAddress { get; set; }
        public string FlaraListAddress => FlaraAddress + "list_en.html";
        public string FlaraDataAddress => FlaraAddress + "FF00000080087B2E00010102000001EF/data_en.html";
        public int Altitude { get; set; }
        public double EnergyReturnFactor { get; set; }
    }
}
