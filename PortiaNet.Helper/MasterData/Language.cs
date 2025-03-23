﻿namespace PortiaNet.Helper.MasterData
{
    public class Language : MasterData
    {
        public string Code { get; set; } = string.Empty;

        public bool IsPopular { get; set; } = false;

        private static List<Language> Languages => new()
        {
            new Language { RefNo = "001", Code = "af-NA", Name = "Afrikaans" },
            new Language { RefNo = "002", Code = "agq-CM", Name = "Aghem" },
            new Language { RefNo = "003", Code = "ak-GH", Name = "Akan" },
            new Language { RefNo = "004", Code = "sq-AL", Name = "Albanian", IsPopular = true },
            new Language { RefNo = "005", Code = "am-ET", Name = "Amharic" },
            new Language { RefNo = "006", Code = "ar-AE", Name = "Arabic", IsPopular = true },
            new Language { RefNo = "007", Code = "hy-AM", Name = "Armenian", IsPopular = true },
            new Language { RefNo = "008", Code = "as-IN", Name = "Assamese" },
            new Language { RefNo = "009", Code = "ast-ES", Name = "Asturian", IsPopular = true },
            new Language { RefNo = "010", Code = "asa-TZ", Name = "Asu" },
            new Language { RefNo = "011", Code = "az-AZ", Name = "Azerbaijani", IsPopular = true },
            new Language { RefNo = "012", Code = "ksf-CM", Name = "Bafia" },
            new Language { RefNo = "013", Code = "bm-ML", Name = "Bambara" },
            new Language { RefNo = "014", Code = "bn-BD", Name = "Bangla" },
            new Language { RefNo = "015", Code = "bas-CM", Name = "Basaa" },
            new Language { RefNo = "016", Code = "ba-RU", Name = "Bashkir" },
            new Language { RefNo = "017", Code = "eu-ES", Name = "Basque" },
            new Language { RefNo = "018", Code = "be-BY", Name = "Belarusian" },
            new Language { RefNo = "019", Code = "bem-ZM", Name = "Bemba" },
            new Language { RefNo = "020", Code = "bez-TZ", Name = "Bena" },
            new Language { RefNo = "021", Code = "byn-ER", Name = "Blin" },
            new Language { RefNo = "022", Code = "brx-IN", Name = "Bodo" },
            new Language { RefNo = "023", Code = "bs-BA", Name = "Bosnian", IsPopular = true },
            new Language { RefNo = "024", Code = "br-FR", Name = "Breton" },
            new Language { RefNo = "025", Code = "bg-BG", Name = "Bulgarian", IsPopular = true },
            new Language { RefNo = "026", Code = "my-MM", Name = "Burmese" },
            new Language { RefNo = "027", Code = "yue-CN", Name = "Cantonese" },
            new Language { RefNo = "028", Code = "ca-AD", Name = "Catalan" , IsPopular = true},
            new Language { RefNo = "029", Code = "ceb-PH", Name = "Cebuano" },
            new Language { RefNo = "030", Code = "tzm-MA", Name = "Central Atlas Tamazight" },
            new Language { RefNo = "031", Code = "ccp-BD", Name = "Chakma" },
            new Language { RefNo = "032", Code = "ce-RU", Name = "Chechen" },
            new Language { RefNo = "033", Code = "chr-US", Name = "Cherokee" },
            new Language { RefNo = "034", Code = "cgg-UG", Name = "Chiga" },
            new Language { RefNo = "035", Code = "zh-CN", Name = "Chinese", IsPopular = true },
            new Language { RefNo = "036", Code = "cv-RU", Name = "Chuvash" },
            new Language { RefNo = "037", Code = "ksh-DE", Name = "Colognian" },
            new Language { RefNo = "038", Code = "kw-GB", Name = "Cornish" },
            new Language { RefNo = "039", Code = "co-FR", Name = "Corsican" },
            new Language { RefNo = "040", Code = "hr-HR", Name = "Croatian" },
            new Language { RefNo = "041", Code = "cs-CZ", Name = "Czech", IsPopular = true },
            new Language { RefNo = "042", Code = "da-DK", Name = "Danish", IsPopular = true },
            new Language { RefNo = "043", Code = "dv-MV", Name = "Dhivehi" },
            new Language { RefNo = "044", Code = "dua-CM", Name = "Duala" },
            new Language { RefNo = "045", Code = "nl-NL", Name = "Dutch", IsPopular = true },
            new Language { RefNo = "046", Code = "dz-BT", Name = "Dzongkha" },
            new Language { RefNo = "047", Code = "ebu-KE", Name = "Embu" },
            new Language { RefNo = "048", Code = "en-US", Name = "English (United States)", IsPopular = true },
            new Language { RefNo = "049", Code = "myv-RU", Name = "Erzya" },
            new Language { RefNo = "050", Code = "eo-001", Name = "Esperanto" },
            new Language { RefNo = "051", Code = "et-EE", Name = "Estonian" },
            new Language { RefNo = "052", Code = "ee-GH", Name = "Ewe" },
            new Language { RefNo = "053", Code = "ewo-CM", Name = "Ewondo" },
            new Language { RefNo = "054", Code = "fo-FO", Name = "Faroese" },
            new Language { RefNo = "055", Code = "fil-PH", Name = "Filipino", IsPopular = true },
            new Language { RefNo = "056", Code = "fi-FI", Name = "Finnish", IsPopular = true },
            new Language { RefNo = "057", Code = "fr-FR", Name = "French", IsPopular = true },
            new Language { RefNo = "058", Code = "fur-IT", Name = "Friulian" },
            new Language { RefNo = "059", Code = "ff-GM", Name = "Fulah" },
            new Language { RefNo = "060", Code = "gaa-GH", Name = "Ga" },
            new Language { RefNo = "061", Code = "gl-ES", Name = "Galician" },
            new Language { RefNo = "062", Code = "lg-UG", Name = "Ganda" },
            new Language { RefNo = "063", Code = "gez-ER", Name = "Geez" },
            new Language { RefNo = "064", Code = "ka-GE", Name = "Georgian", IsPopular = true },
            new Language { RefNo = "065", Code = "de-DE", Name = "German", IsPopular = true },
            new Language { RefNo = "066", Code = "el-GR", Name = "Greek", IsPopular = true },
            new Language { RefNo = "067", Code = "gn-PY", Name = "Guarani" },
            new Language { RefNo = "068", Code = "gu-IN", Name = "Gujarati" },
            new Language { RefNo = "069", Code = "guz-KE", Name = "Gusii" },
            new Language { RefNo = "070", Code = "ha-NG", Name = "Hausa" },
            new Language { RefNo = "071", Code = "haw-US", Name = "Hawaiian" },
            new Language { RefNo = "072", Code = "he-IL", Name = "Hebrew" },
            new Language { RefNo = "073", Code = "hi-IN", Name = "Hindi", IsPopular = true },
            new Language { RefNo = "074", Code = "hu-HU", Name = "Hungarian" },
            new Language { RefNo = "075", Code = "is-IS", Name = "Icelandic" },
            new Language { RefNo = "076", Code = "io-001", Name = "Ido" },
            new Language { RefNo = "077", Code = "ig-NG", Name = "Igbo" },
            new Language { RefNo = "078", Code = "smn-FI", Name = "Inari Sami" },
            new Language { RefNo = "079", Code = "id-ID", Name = "Indonesian" },
            new Language { RefNo = "080", Code = "ia-001", Name = "Interlingua" },
            new Language { RefNo = "081", Code = "iu-CA", Name = "Inuktitut" },
            new Language { RefNo = "082", Code = "ga-IE", Name = "Irish", IsPopular = true },
            new Language { RefNo = "083", Code = "it-IT", Name = "Italian", IsPopular = true },
            new Language { RefNo = "084", Code = "ja-JP", Name = "Japanese", IsPopular = true },
            new Language { RefNo = "085", Code = "jv-ID", Name = "Javanese" },
            new Language { RefNo = "086", Code = "kaj-NG", Name = "Jju" },
            new Language { RefNo = "087", Code = "dyo-SN", Name = "Jola-Fonyi" },
            new Language { RefNo = "088", Code = "kea-CV", Name = "Kabuverdianu" },
            new Language { RefNo = "089", Code = "kab-DZ", Name = "Kabyle" },
            new Language { RefNo = "090", Code = "kkj-CM", Name = "Kako" },
            new Language { RefNo = "091", Code = "kl-GL", Name = "Kalaallisut" },
            new Language { RefNo = "092", Code = "kln-KE", Name = "Kalenjin" },
            new Language { RefNo = "093", Code = "kam-KE", Name = "Kamba" },
            new Language { RefNo = "094", Code = "kn-IN", Name = "Kannada" },
            new Language { RefNo = "095", Code = "ks-IN", Name = "Kashmiri" },
            new Language { RefNo = "096", Code = "kk-KZ", Name = "Kazakh", IsPopular = true },
            new Language { RefNo = "097", Code = "km-KH", Name = "Khmer" },
            new Language { RefNo = "098", Code = "ki-KE", Name = "Kikuyu" },
            new Language { RefNo = "099", Code = "rw-RW", Name = "Kinyarwanda" },
            new Language { RefNo = "100", Code = "kok-IN", Name = "Konkani" },
            new Language { RefNo = "101", Code = "ko-KR", Name = "Korean", IsPopular = true },
            new Language { RefNo = "102", Code = "khq-ML", Name = "Koyra Chiini" },
            new Language { RefNo = "103", Code = "ses-ML", Name = "Koyraboro Senni" },
            new Language { RefNo = "104", Code = "kpe-LR", Name = "Kpelle" },
            new Language { RefNo = "105", Code = "ku-TR", Name = "Kurdish", IsPopular = true },
            new Language { RefNo = "106", Code = "ckb-IQ", Name = "Kurdish, Sorani" },
            new Language { RefNo = "107", Code = "nmg-CM", Name = "Kwasio" },
            new Language { RefNo = "108", Code = "ky-KG", Name = "Kyrgyz" },
            new Language { RefNo = "109", Code = "lkt-US", Name = "Lakota" },
            new Language { RefNo = "110", Code = "lag-TZ", Name = "Langi" },
            new Language { RefNo = "111", Code = "lo-LA", Name = "Lao" },
            new Language { RefNo = "112", Code = "lv-LV", Name = "Latvian" },
            new Language { RefNo = "113", Code = "ln-AO", Name = "Lingala" },
            new Language { RefNo = "114", Code = "lt-LT", Name = "Lithuanian" },
            new Language { RefNo = "115", Code = "jbo-001", Name = "Lojban" },
            new Language { RefNo = "116", Code = "nds-DE", Name = "Low German" },
            new Language { RefNo = "117", Code = "dsb-DE", Name = "Lower Sorbian" },
            new Language { RefNo = "118", Code = "lu-CD", Name = "Luba-Katanga" },
            new Language { RefNo = "119", Code = "luo-KE", Name = "Luo" },
            new Language { RefNo = "120", Code = "lb-LU", Name = "Luxembourgish" },
            new Language { RefNo = "121", Code = "luy-KE", Name = "Luyia" },
            new Language { RefNo = "122", Code = "mk-MK", Name = "Macedonian" },
            new Language { RefNo = "123", Code = "jmc-TZ", Name = "Machame" },
            new Language { RefNo = "124", Code = "mgh-MZ", Name = "Makhuwa-Meetto" },
            new Language { RefNo = "125", Code = "kde-TZ", Name = "Makonde" },
            new Language { RefNo = "126", Code = "mg-MG", Name = "Malagasy" },
            new Language { RefNo = "127", Code = "ms-MY", Name = "Malay" },
            new Language { RefNo = "128", Code = "ml-IN", Name = "Malayalam", IsPopular = true },
            new Language { RefNo = "129", Code = "mt-MT", Name = "Maltese" },
            new Language { RefNo = "130", Code = "mni-IN", Name = "Manipuri" },
            new Language { RefNo = "131", Code = "gv-IM", Name = "Manx" },
            new Language { RefNo = "132", Code = "mi-NZ", Name = "Maori" },
            new Language { RefNo = "133", Code = "arn-CL", Name = "Mapuche" },
            new Language { RefNo = "134", Code = "mr-IN", Name = "Marathi" },
            new Language { RefNo = "135", Code = "mas-TZ", Name = "Masai" },
            new Language { RefNo = "136", Code = "mzn-IR", Name = "Mazanderani" },
            new Language { RefNo = "137", Code = "mer-KE", Name = "Meru" },
            new Language { RefNo = "138", Code = "mgo-CM", Name = "Metaʼ" },
            new Language { RefNo = "139", Code = "moh-CA", Name = "Mohawk" },
            new Language { RefNo = "140", Code = "mn-MN", Name = "Mongolian" },
            new Language { RefNo = "141", Code = "mfe-MU", Name = "Morisyen" },
            new Language { RefNo = "142", Code = "mua-CM", Name = "Mundang" },
            new Language { RefNo = "143", Code = "nqo-GN", Name = "N’Ko" },
            new Language { RefNo = "144", Code = "naq-NA", Name = "Nama" },
            new Language { RefNo = "145", Code = "ne-NP", Name = "Nepali" },
            new Language { RefNo = "146", Code = "nnh-CM", Name = "Ngiemboon" },
            new Language { RefNo = "147", Code = "jgo-CM", Name = "Ngomba" },
            new Language { RefNo = "148", Code = "nd-ZW", Name = "North Ndebele" },
            new Language { RefNo = "149", Code = "lrc-IQ", Name = "Northern Luri" },
            new Language { RefNo = "150", Code = "se-NO", Name = "Northern Sami" },
            new Language { RefNo = "151", Code = "nso-ZA", Name = "Northern Sotho" },
            new Language { RefNo = "152", Code = "nb-NO", Name = "Norwegian Bokmål" },
            new Language { RefNo = "153", Code = "nn-NO", Name = "Norwegian Nynorsk" },
            new Language { RefNo = "154", Code = "nus-SS", Name = "Nuer" },
            new Language { RefNo = "155", Code = "ny-MW", Name = "Nyanja" },
            new Language { RefNo = "156", Code = "nyn-UG", Name = "Nyankole" },
            new Language { RefNo = "157", Code = "oc-FR", Name = "Occitan" },
            new Language { RefNo = "158", Code = "or-IN", Name = "Odia" },
            new Language { RefNo = "159", Code = "om-ET", Name = "Oromo" },
            new Language { RefNo = "160", Code = "os-GE", Name = "Ossetic" },
            new Language { RefNo = "161", Code = "ps-PS", Name = "Pashto", IsPopular = true },
            new Language { RefNo = "162", Code = "fa-IR", Name = "Persian", IsPopular = true },
            new Language { RefNo = "163", Code = "pl-PL", Name = "Polish" },
            new Language { RefNo = "164", Code = "pt-PT", Name = "Portuguese" },
            new Language { RefNo = "165", Code = "pa-PK", Name = "Punjabi" },
            new Language { RefNo = "166", Code = "qu-BO", Name = "Quechua" },
            new Language { RefNo = "167", Code = "ro-RO", Name = "Romanian" },
            new Language { RefNo = "168", Code = "rm-CH", Name = "Romansh" },
            new Language { RefNo = "169", Code = "rof-TZ", Name = "Rombo" },
            new Language { RefNo = "170", Code = "rn-BI", Name = "Rundi" },
            new Language { RefNo = "171", Code = "ru-RU", Name = "Russian", IsPopular = true },
            new Language { RefNo = "172", Code = "rwk-TZ", Name = "Rwa" },
            new Language { RefNo = "173", Code = "sah-RU", Name = "Sakha" },
            new Language { RefNo = "174", Code = "saq-KE", Name = "Samburu" },
            new Language { RefNo = "175", Code = "sg-CF", Name = "Sango" },
            new Language { RefNo = "176", Code = "sbp-TZ", Name = "Sangu" },
            new Language { RefNo = "177", Code = "sa-IN", Name = "Sanskrit" },
            new Language { RefNo = "178", Code = "sat-IN", Name = "Santali" },
            new Language { RefNo = "179", Code = "sc-IT", Name = "Sardinian" },
            new Language { RefNo = "180", Code = "gd-GB", Name = "Scottish Gaelic" },
            new Language { RefNo = "181", Code = "seh-MZ", Name = "Sena" },
            new Language { RefNo = "182", Code = "sr-BA", Name = "Serbian" },
            new Language { RefNo = "183", Code = "ksb-TZ", Name = "Shambala" },
            new Language { RefNo = "184", Code = "sn-ZW", Name = "Shona" },
            new Language { RefNo = "185", Code = "ii-CN", Name = "Sichuan Yi" },
            new Language { RefNo = "186", Code = "scn-IT", Name = "Sicilian" },
            new Language { RefNo = "187", Code = "sd-PK", Name = "Sindhi" },
            new Language { RefNo = "188", Code = "si-LK", Name = "Sinhala" },
            new Language { RefNo = "189", Code = "sk-SK", Name = "Slovak" },
            new Language { RefNo = "190", Code = "sl-SI", Name = "Slovenian" },
            new Language { RefNo = "191", Code = "xog-UG", Name = "Soga" },
            new Language { RefNo = "192", Code = "so-SO", Name = "Somali" },
            new Language { RefNo = "193", Code = "nr-LS", Name = "Southern Sotho" },
            new Language { RefNo = "195", Code = "es-ES", Name = "Spanish", IsPopular = true },
            new Language { RefNo = "196", Code = "zgh-MA", Name = "Standard Moroccan Tamazight" },
            new Language { RefNo = "197", Code = "sw-KE", Name = "Swahili" },
            new Language { RefNo = "198", Code = "ss-SZ", Name = "Swati" },
            new Language { RefNo = "199", Code = "sv-SE", Name = "Swedish" },
            new Language { RefNo = "200", Code = "gsw-FR", Name = "Swiss German" },
            new Language { RefNo = "201", Code = "syr-SY", Name = "Syriac" },
            new Language { RefNo = "202", Code = "shi-MA", Name = "Tachelhit" },
            new Language { RefNo = "203", Code = "dav-KE", Name = "Taita" },
            new Language { RefNo = "204", Code = "tg-TJ", Name = "Tajik" },
            new Language { RefNo = "205", Code = "ta-IN", Name = "Tamil" },
            new Language { RefNo = "206", Code = "trv-TW", Name = "Taroko" },
            new Language { RefNo = "207", Code = "twq-NE", Name = "Tasawaq" },
            new Language { RefNo = "208", Code = "tt-RU", Name = "Tatar" },
            new Language { RefNo = "209", Code = "te-IN", Name = "Telugu" },
            new Language { RefNo = "210", Code = "teo-KE", Name = "Teso" },
            new Language { RefNo = "211", Code = "th-TH", Name = "Thai" },
            new Language { RefNo = "212", Code = "bo-CN", Name = "Tibetan" },
            new Language { RefNo = "213", Code = "tig-ER", Name = "Tigre" },
            new Language { RefNo = "214", Code = "ti-ET", Name = "Tigrinya" },
            new Language { RefNo = "215", Code = "to-TO", Name = "Tongan" },
            new Language { RefNo = "216", Code = "ts-ZA", Name = "Tsonga" },
            new Language { RefNo = "217", Code = "tn-BW", Name = "Tswana" },
            new Language { RefNo = "218", Code = "tr-TR", Name = "Turkish", IsPopular = true },
            new Language { RefNo = "219", Code = "tk-TM", Name = "Turkmen" },
            new Language { RefNo = "220", Code = "kcg-NG", Name = "Tyap" },
            new Language { RefNo = "221", Code = "uk-UA", Name = "Ukrainian" },
            new Language { RefNo = "222", Code = "hsb-DE", Name = "Upper Sorbian" },
            new Language { RefNo = "223", Code = "ur-IN", Name = "Urdu" },
            new Language { RefNo = "224", Code = "ug-CN", Name = "Uyghur" },
            new Language { RefNo = "225", Code = "uz-UZ", Name = "Uzbek" },
            new Language { RefNo = "226", Code = "vai-LR", Name = "Vai" },
            new Language { RefNo = "227", Code = "ve-ZA", Name = "Venda" },
            new Language { RefNo = "228", Code = "vi-VN", Name = "Vietnamese" },
            new Language { RefNo = "229", Code = "vun-TZ", Name = "Vunjo" },
            new Language { RefNo = "230", Code = "wa-BE", Name = "Walloon" },
            new Language { RefNo = "231", Code = "wae-CH", Name = "Walser" },
            new Language { RefNo = "232", Code = "cy-GB", Name = "Welsh" },
            new Language { RefNo = "233", Code = "fy-NL", Name = "Western Frisian" },
            new Language { RefNo = "234", Code = "wal-ET", Name = "Wolaytta" },
            new Language { RefNo = "235", Code = "wo-SN", Name = "Wolof" },
            new Language { RefNo = "236", Code = "xh-ZA", Name = "Xhosa" },
            new Language { RefNo = "237", Code = "yav-CM", Name = "Yangben" },
            new Language { RefNo = "238", Code = "yi-001", Name = "Yiddish" },
            new Language { RefNo = "239", Code = "yo-BJ", Name = "Yoruba" },
            new Language { RefNo = "240", Code = "dje-NE", Name = "Zarma" },
            new Language { RefNo = "241", Code = "zu-ZA", Name = "Zulu" },
        };

        public static List<Language> GetAll() => Languages;
    }
}
