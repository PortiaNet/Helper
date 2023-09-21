namespace PortiaNet.Helper.MasterData
{
    public class Region : MasterData
    {
        public List<string> Countries { get; set; } = new();

        public static List<Region> Regions => new()
        {
            new Region{ RefNo = "001", Name = "AGOA - African Growth and Opportunity", Countries = new List<string> { "005", "021", "026", "030", "031", "033", "036", "042", "049", "057", "058", "064", "065", "068", "075", "076", "095", "101", "102", "108", "109", "112", "115", "116", "124", "126", "133", "134", "153", "158", "160", "167", "179", "189", "200" } },
            new Region{ RefNo = "002", Name = "All Africa", Countries = new List<string> { "003", "005", "021", "026", "030", "031", "033", "035", "036", "041", "042", "049", "053", "055", "057", "058", "064", "065", "068", "075", "076", "095", "101", "102", "103", "108", "109", "112", "115", "116", "123", "124", "126", "133", "134", "153", "158", "160", "166", "167", "169", "172", "179", "182", "185", "189", "198", "200", "201" } },
            new Region{ RefNo = "003", Name = "Andean Community", Countries = new List<string> { "024", "040", "052", "144", "196", } },
            new Region{ RefNo = "004", Name = "APEC - Asia, Pacific Economic Cooperation", Countries = new List<string> { "011", "028", "034", "037", "038", "084", "092", "110", "117", "131", "142", "144", "145", "152", "161", "168", "177", "180", "193", "197", } },
            new Region{ RefNo = "005", Name = "Arab African Countries", Countries = new List<string> { "003", "041", "049", "053", "103", "123", "172", "185", } },
            new Region{ RefNo = "006", Name = "ASEAN - Association of Southeast Asian Nations", Countries = new List<string> { "028", "032", "084", "110", "125", "145", "161", "180", "197", "", } },
            new Region{ RefNo = "007", Name = "Australia and New Zealand", Countries = new List<string> { "011", "131" } },
            new Region{ RefNo = "008", Name = "CAFTA-DR - Central American Free Trade Agreement - Dominic Republic", Countries = new List<string> { "043", "051", "054", "074", "079", "132", } },
            new Region{ RefNo = "009", Name = "Caribbean", Countries = new List<string> { "007", "010", "014", "017", "020", "045", "050", "051", "072", "078", "091", "148", "162", "184" } },
            new Region{ RefNo = "010", Name = "Central America and Caribbean", Countries = new List<string> { "020", "043", "045", "051", "054", "074", "078", "079", "091", "117", "132", "141", "148", "184", } },
            new Region{ RefNo = "011", Name = "Central Asia", Countries = new List<string> { "094", "098", "178", "187", "195" } },
            new Region{ RefNo = "012", Name = "COMESA - Common Market for Eastern and Southern Africa", Countries = new List<string> { "005", "031", "041", "042", "049", "053", "055", "057", "058", "095", "103", "108", "109", "116", "153", "172", "189", "200", "201", "", } },
            new Region{ RefNo = "013", Name = "Eastern Asia", Countries = new List<string> { "028", "032", "038", "080", "084", "092", "107", "110", "121", "125", "135", "145", "161", "168", "180", "181", "197" } },
            new Region{ RefNo = "014", Name = "ECOWAS - Economic Community of West African States", Countries = new List<string> { "021", "030", "065", "068", "075", "076", "102", "112", "133", "134", "158", "160", "182" } },
            new Region{ RefNo = "015", Name = "Europe", Countries = new List<string> { "002", "004", "009", "012", "013", "018", "019", "025", "029", "043", "044", "046", "047", "048", "056", "061", "062", "066", "067", "070", "081", "082", "087", "090", "099", "104", "105", "106", "113", "119", "120", "122", "129", "136", "146", "147", "151", "152", "156", "159", "163", "164", "170", "174", "186", "190", "192" } },
            new Region{ RefNo = "016", Name = "European Union", Countries = new List<string> { "012", "019", "029", "046", "047", "048", "056", "061", "062", "067", "070", "081", "087", "090", "099", "129", "146", "147", "151", "163", "164", "170", "174"} },
            new Region{ RefNo = "017", Name = "Far East", Countries = new List<string> { "028", "032", "038", "080", "084", "092", "107", "110", "121", "125", "142", "145", "161", "177", "180", "181", "197" } },
            new Region{ RefNo = "018", Name = "GCC", Countries = new List<string> { "015", "097", "137", "149", "157", "191", } },
            new Region{ RefNo = "019", Name = "Levant", Countries = new List<string> { "046", "053", "093", "100", "140", "176", "186", } },
            new Region{ RefNo = "020", Name = "Melanesia", Countries = new List<string> { "060", "130", "142", "165" } },
            new Region{ RefNo = "021", Name = "Micronesia", Countries = new List<string> { "073", "096", "114", "118", "127", "139", } },
            new Region{ RefNo = "022", Name = "Middle East", Countries = new List<string> { "015", "053", "085", "086", "093", "097", "100", "137", "140", "149", "157", "176", "186", "191", "199", } },
            new Region{ RefNo = "023", Name = "Non-GCC PAN Arab", Countries = new List<string> { "003", "041", "049", "053", "086", "093", "100", "103", "115", "123", "140", "166", "172", "176", "185", "198", "199" } },
            new Region{ RefNo = "024", Name = "Nordic", Countries = new List<string> { "048", "059", "061", "071", "082", "136", "174", } },
            new Region{ RefNo = "025", Name = "North America", Countries = new List<string> { "007", "014", "017", "020", "034", "043", "045", "051", "054", "071", "072", "074", "078", "079", "091", "117", "132", "141", "184", "193", } },
            new Region{ RefNo = "026", Name = "Oceania", Countries = new List<string> { "012", "060", "063", "073", "096", "114", "118", "130", "131", "142", "155", "165", "183" } },
            new Region{ RefNo = "027", Name = "PAN Arab", Countries = new List<string> { "199", "191", "198", "185", } },
            new Region{ RefNo = "028", Name = "Polynesian", Countries = new List<string> { "063", "130", "131", "155", "165", "183", "188" } },
            new Region{ RefNo = "029", Name = "Scandinavia", Countries = new List<string> { "048", "136", "174", "059", "061", "082" } },
            new Region{ RefNo = "030", Name = "South America", Countries = new List<string> { "008", "024", "027", "037", "040", "052", "077", "143", "144", "173", "194", "196" } },
            new Region{ RefNo = "031", Name = "South Asia", Countries = new List<string> { "001", "016", "023", "083", "128", "111", "138", "171", } },
            new Region{ RefNo = "032", Name = "SUB-Saharan Africa", Countries = new List<string> { "005", "031", "042", "033", "035", "036", "075", "064", "095", "134", "153", "179", "189", "172", "169", "049", "055", "058", "166", "026", "041", "101", "108", "109", "116", "124", "126", "167", "057", "200", "201", "112", "021", "030", "065", "068", "076", "103", "115", "133", "158", "160", "182" } },
            new Region{ RefNo = "033", Name = "West Asia", Countries = new List<string> { "186", "015", "097", "137", "149", "157", "191", "199", "009", "013", "066", "086", "089", "093", "100", "140", "176", "085", "046", "", "", "", "", "", "", } },
        };
    }
}
