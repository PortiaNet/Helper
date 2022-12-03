namespace PortiaNet.Helper.MasterData
{
    public class Continent : MasterData
    {
        private static List<Continent> Continents = new()
        {
            new Continent { RefNo = "01", Name = "Asia" },
            new Continent { RefNo = "02", Name = "Africa" },
            new Continent { RefNo = "03", Name = "Europe" },
            new Continent { RefNo = "04", Name = "North America" },
            new Continent { RefNo = "05", Name = "South America" },
            new Continent { RefNo = "06", Name = "Australia/Oceania" },
            new Continent { RefNo = "07", Name = "Antarctica" }
        };

        public List<Continent> GetAll() => Continents;
    }
}
