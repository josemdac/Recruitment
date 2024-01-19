using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RenovaHrEmploymentAPI.Model;
using RenovaHrEmploymentAPI.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System.Linq;
using RenovaCry;
using RenovaCommon.Models;
namespace RenovaHrEmploymentAPI.Controllers
{
    class ControllerHelpers
    {

        public static ActionResult InternalError(string message)
        {
            var _logger = new LoggerService();
                _logger.LogError(message);
            var result = new StatusCodeResult(500);
            return result;

        }

        public static decimal GetCompanyId(ControllerBase controller)
        {
            
            controller.HttpContext.Request.Headers.TryGetValue("CompanyToken", out var companyToken);
            var rawJson = RenovaCry.Encryption.Decrypt(companyToken + "", "");
            var obj = JsonConvert.DeserializeObject<dynamic>(rawJson);
            return Convert.ToDecimal(obj["CompanyId"]+"");
            

        }

        public static decimal GetCompanyId(string companyToken)
        {

            var rawJson = RenovaCry.Encryption.Decrypt(companyToken + "", "");
            var obj = JsonConvert.DeserializeObject<dynamic>(rawJson);
            return Convert.ToDecimal(obj["CompanyId"] + "");


        }

        public static IMSession GetImSession(ControllerBase controller)
        {

            controller.HttpContext.Request.Headers.TryGetValue("IMSessionToken", out var sessionToken);
            var rawJson = RenovaCry.Encryption.Decrypt(sessionToken + "", "");
            var obj = JsonConvert.DeserializeObject<IMSession>(rawJson);
            return obj;


        }
        public readonly static IList<ISO639Item> ISOLangs = new List<ISO639Item>
        {
            new ISO639Item{
             family="Afro-Asiatic",
            culture="aa",
            nativeName="Afaraf",
            name="Afar"
            },
            new ISO639Item{
             family="Northwest Caucasian",
            culture="ab",
            nativeName="аҧсуа бызшәа, аҧсшәа",
            name="Abkhaz"
            },
            new ISO639Item{
             family="Indo-European",
            culture="ae",
            nativeName="avesta",
            name="Avestan"
            },
            new ISO639Item{
             family="Indo-European",
            culture="af",
            nativeName="Afrikaans",
            name="Afrikaans"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="ak",
            nativeName="Akan",
            name="Akan"
            },
            new ISO639Item{
             family="Afro-Asiatic",
            culture="am",
            nativeName="አማርኛ",
            name="Amharic"
            },
            new ISO639Item{
             family="Indo-European",
            culture="an",
            nativeName="aragonés",
            name="Aragonese"
            },
            new ISO639Item{
             family="Afro-Asiatic",
            culture="ar",
            nativeName="العربية",
            name="Arabic"
            },
            new ISO639Item{
             family="Indo-European",
            culture="as",
            nativeName="অসমীয়া",
            name="Assamese"
            },
            new ISO639Item{
             family="Northeast Caucasian",
            culture="av",
            nativeName="авар мацӀ, магӀарул мацӀ",
            name="Avaric"
            },
            new ISO639Item{
             family="Aymaran",
            culture="ay",
            nativeName="aymar aru",
            name="Aymara"
            },
            new ISO639Item{
             family="Turkic",
            culture="az",
            nativeName="azərbaycan dili",
            name="Azerbaijani"
            },
            new ISO639Item{
             family="Turkic",
            culture="ba",
            nativeName="башҡорт теле",
            name="Bashkir"
            },
            new ISO639Item{
             family="Indo-European",
            culture="be",
            nativeName="беларуская мова",
            name="Belarusian"
            },
            new ISO639Item{
             family="Indo-European",
            culture="bg",
            nativeName="български език",
            name="Bulgarian"
            },
            new ISO639Item{
             family="Indo-European",
            culture="bh",
            nativeName="भोजपुरी",
            name="Bihari"
            },
            new ISO639Item{
             family="Creole",
            culture="bi",
            nativeName="Bislama",
            name="Bislama"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="bm",
            nativeName="bamanankan",
            name="Bambara"
            },
            new ISO639Item{
             family="Indo-European",
            culture="bn",
            nativeName="বাংলা",
            name="Bengali, Bangla"
            },
            new ISO639Item{
             family="Sino-Tibetan",
            culture="bo",
            nativeName="བོད་ཡིག",
            name="Tibetan Standard, Tibetan, Central"
            },
            new ISO639Item{
             family="Indo-European",
            culture="br",
            nativeName="brezhoneg",
            name="Breton"
            },
            new ISO639Item{
             family="Indo-European",
            culture="bs",
            nativeName="bosanski jezik",
            name="Bosnian"
            },
            new ISO639Item{
             family="Indo-European",
            culture="ca",
            nativeName="català",
            name="Catalan"
            },
            new ISO639Item{
             family="Northeast Caucasian",
            culture="ce",
            nativeName="нохчийн мотт",
            name="Chechen"
            },
            new ISO639Item{
             family="Austronesian",
            culture="ch",
            nativeName="Chamoru",
            name="Chamorro"
            },
            new ISO639Item{
             family="Indo-European",
            culture="co",
            nativeName="corsu, lingua corsa",
            name="Corsican"
            },
            new ISO639Item{
             family="Algonquian",
            culture="cr",
            nativeName="ᓀᐦᐃᔭᐍᐏᐣ",
            name="Cree"
            },
            new ISO639Item{
             family="Indo-European",
            culture="cs",
            nativeName="čeština, český jazyk",
            name="Czech"
            },
            new ISO639Item{
             family="Indo-European",
            culture="cu",
            nativeName="ѩзыкъ словѣньскъ",
            name="Old Church Slavonic, Church Slavonic, Old Bulgarian"
            },
            new ISO639Item{
             family="Turkic",
            culture="cv",
            nativeName="чӑваш чӗлхи",
            name="Chuvash"
            },
            new ISO639Item{
             family="Indo-European",
            culture="cy",
            nativeName="Cymraeg",
            name="Welsh"
            },
            new ISO639Item{
             family="Indo-European",
            culture="da",
            nativeName="dansk",
            name="Danish"
            },
            new ISO639Item{
             family="Indo-European",
            culture="de",
            nativeName="Deutsch",
            name="German"
            },
            new ISO639Item{
             family="Indo-European",
            culture="dv",
            nativeName="ދިވެހި",
            name="Divehi, Dhivehi, Maldivian"
            },
            new ISO639Item{
             family="Sino-Tibetan",
            culture="dz",
            nativeName="རྫོང་ཁ",
            name="Dzongkha"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="ee",
            nativeName="Eʋegbe",
            name="Ewe"
            },
            new ISO639Item{
             family="Indo-European",
            culture="el",
            nativeName="ελληνικά",
            name="Greek (modern)"
            },
            new ISO639Item{
             family="Indo-European",
            culture="en",
            nativeName="English",
            name="English"
            },
            new ISO639Item{
             family="Constructed",
            culture="eo",
            nativeName="Esperanto",
            name="Esperanto"
            },
            new ISO639Item{
             family="Indo-European",
            culture="es",
            nativeName="Español",
            name="Spanish"
            },
            new ISO639Item{
             family="Uralic",
            culture="et",
            nativeName="eesti, eesti keel",
            name="Estonian"
            },
            new ISO639Item{
             family="Language isolate",
            culture="eu",
            nativeName="euskara, euskera",
            name="Basque"
            },
            new ISO639Item{
             family="Indo-European",
            culture="fa",
            nativeName="فارسی",
            name="Persian (Farsi)"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="ff",
            nativeName="Fulfulde, Pulaar, Pular",
            name="Fula, Fulah, Pulaar, Pular"
            },
            new ISO639Item{
             family="Uralic",
            culture="fi",
            nativeName="suomi, suomen kieli",
            name="Finnish"
            },
            new ISO639Item{
             family="Austronesian",
            culture="fj",
            nativeName="vosa Vakaviti",
            name="Fijian"
            },
            new ISO639Item{
             family="Indo-European",
            culture="fo",
            nativeName="føroyskt",
            name="Faroese"
            },
            new ISO639Item{
             family="Indo-European",
            culture="fr",
            nativeName="français, langue française",
            name="French"
            },
            new ISO639Item{
             family="Indo-European",
            culture="fy",
            nativeName="Frysk",
            name="Western Frisian"
            },
            new ISO639Item{
             family="Indo-European",
            culture="ga",
            nativeName="Gaeilge",
            name="Irish"
            },
            new ISO639Item{
             family="Indo-European",
            culture="gd",
            nativeName="Gàidhlig",
            name="Scottish Gaelic, Gaelic"
            },
            new ISO639Item{
             family="Indo-European",
            culture="gl",
            nativeName="galego",
            name="Galician"
            },
            new ISO639Item{
             family="Tupian",
            culture="gn",
            nativeName="Avañe'ẽ",
            name="Guaraní"
            },
            new ISO639Item{
             family="Indo-European",
            culture="gu",
            nativeName="ગુજરાતી",
            name="Gujarati"
            },
            new ISO639Item{
             family="Indo-European",
            culture="gv",
            nativeName="Gaelg, Gailck",
            name="Manx"
            },
            new ISO639Item{
             family="Afro-Asiatic",
            culture="ha",
            nativeName="(Hausa) هَوُسَ",
            name="Hausa"
            },
            new ISO639Item{
             family="Afro-Asiatic",
            culture="he",
            nativeName="עברית",
            name="Hebrew (modern)"
            },
            new ISO639Item{
             family="Indo-European",
            culture="hi",
            nativeName="हिन्दी, हिंदी",
            name="Hindi"
            },
            new ISO639Item{
             family="Austronesian",
            culture="ho",
            nativeName="Hiri Motu",
            name="Hiri Motu"
            },
            new ISO639Item{
             family="Indo-European",
            culture="hr",
            nativeName="hrvatski jezik",
            name="Croatian"
            },
            new ISO639Item{
             family="Creole",
            culture="ht",
            nativeName="Kreyòl ayisyen",
            name="Haitian, Haitian Creole"
            },
            new ISO639Item{
             family="Uralic",
            culture="hu",
            nativeName="magyar",
            name="Hungarian"
            },
            new ISO639Item{
             family="Indo-European",
            culture="hy",
            nativeName="Հայերեն",
            name="Armenian"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="hz",
            nativeName="Otjiherero",
            name="Herero"
            },
            new ISO639Item{
             family="Constructed",
            culture="ia",
            nativeName="Interlingua",
            name="Interlingua"
            },
            new ISO639Item{
             family="Austronesian",
            culture="id",
            nativeName="Bahasa Indonesia",
            name="Indonesian"
            },
            new ISO639Item{
             family="Constructed",
            culture="ie",
            nativeName="Originally called Occidental; then Interlingue after WWII",
            name="Interlingue"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="ig",
            nativeName="Asụsụ Igbo",
            name="Igbo"
            },
            new ISO639Item{
             family="Sino-Tibetan",
            culture="ii",
            nativeName="ꆈꌠ꒿ Nuosuhxop",
            name="Nuosu"
            },
            new ISO639Item{
             family="Eskimo–Aleut",
            culture="ik",
            nativeName="Iñupiaq, Iñupiatun",
            name="Inupiaq"
            },
            new ISO639Item{
             family="Constructed",
            culture="io",
            nativeName="Ido",
            name="Ido"
            },
            new ISO639Item{
             family="Indo-European",
            culture="is",
            nativeName="Íslenska",
            name="Icelandic"
            },
            new ISO639Item{
             family="Indo-European",
            culture="it",
            nativeName="Italiano",
            name="Italian"
            },
            new ISO639Item{
             family="Eskimo–Aleut",
            culture="iu",
            nativeName="ᐃᓄᒃᑎᑐᑦ",
            name="Inuktitut"
            },
            new ISO639Item{
             family="Japonic",
            culture="ja",
            nativeName="日本語 (にほんご)",
            name="Japanese"
            },
            new ISO639Item{
             family="Austronesian",
            culture="jv",
            nativeName="ꦧꦱꦗꦮ, Basa Jawa",
            name="Javanese"
            },
            new ISO639Item{
             family="South Caucasian",
            culture="ka",
            nativeName="ქართული",
            name="Georgian"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="kg",
            nativeName="Kikongo",
            name="Kongo"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="ki",
            nativeName="Gĩkũyũ",
            name="Kikuyu, Gikuyu"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="kj",
            nativeName="Kuanyama",
            name="Kwanyama, Kuanyama"
            },
            new ISO639Item{
             family="Turkic",
            culture="kk",
            nativeName="қазақ тілі",
            name="Kazakh"
            },
            new ISO639Item{
             family="Eskimo–Aleut",
            culture="kl",
            nativeName="kalaallisut, kalaallit oqaasii",
            name="Kalaallisut, Greenlandic"
            },
            new ISO639Item{
             family="Austroasiatic",
            culture="km",
            nativeName="ខ្មែរ, ខេមរភាសា, ភាសាខ្មែរ",
            name="Khmer"
            },
            new ISO639Item{
             family="Dravidian",
            culture="kn",
            nativeName="ಕನ್ನಡ",
            name="Kannada"
            },
            new ISO639Item{
             family="Koreanic",
            culture="ko",
            nativeName="한국어",
            name="Korean"
            },
            new ISO639Item{
             family="Nilo-Saharan",
            culture="kr",
            nativeName="Kanuri",
            name="Kanuri"
            },
            new ISO639Item{
             family="Indo-European",
            culture="ks",
            nativeName="कश्मीरी, كشميري‎",
            name="Kashmiri"
            },
            new ISO639Item{
             family="Indo-European",
            culture="ku",
            nativeName="Kurdî, كوردی‎",
            name="Kurdish"
            },
            new ISO639Item{
             family="Uralic",
            culture="kv",
            nativeName="коми кыв",
            name="Komi"
            },
            new ISO639Item{
             family="Indo-European",
            culture="kw",
            nativeName="Kernewek",
            name="Cornish"
            },
            new ISO639Item{
             family="Turkic",
            culture="ky",
            nativeName="Кыргызча, Кыргыз тили",
            name="Kyrgyz"
            },
            new ISO639Item{
             family="Indo-European",
            culture="la",
            nativeName="latine, lingua latina",
            name="Latin"
            },
            new ISO639Item{
             family="Indo-European",
            culture="lb",
            nativeName="Lëtzebuergesch",
            name="Luxembourgish, Letzeburgesch"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="lg",
            nativeName="Luganda",
            name="Ganda"
            },
            new ISO639Item{
             family="Indo-European",
            culture="li",
            nativeName="Limburgs",
            name="Limburgish, Limburgan, Limburger"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="ln",
            nativeName="Lingála",
            name="Lingala"
            },
            new ISO639Item{
             family="Tai–Kadai",
            culture="lo",
            nativeName="ພາສາລາວ",
            name="Lao"
            },
            new ISO639Item{
             family="Indo-European",
            culture="lt",
            nativeName="lietuvių kalba",
            name="Lithuanian"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="lu",
            nativeName="Tshiluba",
            name="Luba-Katanga"
            },
            new ISO639Item{
             family="Indo-European",
            culture="lv",
            nativeName="latviešu valoda",
            name="Latvian"
            },
            new ISO639Item{
             family="Austronesian",
            culture="mg",
            nativeName="fiteny malagasy",
            name="Malagasy"
            },
            new ISO639Item{
             family="Austronesian",
            culture="mh",
            nativeName="Kajin M̧ajeļ",
            name="Marshallese"
            },
            new ISO639Item{
             family="Austronesian",
            culture="mi",
            nativeName="te reo Māori",
            name="Māori"
            },
            new ISO639Item{
             family="Indo-European",
            culture="mk",
            nativeName="македонски јазик",
            name="Macedonian"
            },
            new ISO639Item{
             family="Dravidian",
            culture="ml",
            nativeName="മലയാളം",
            name="Malayalam"
            },
            new ISO639Item{
             family="Mongolic",
            culture="mn",
            nativeName="Монгол хэл",
            name="Mongolian"
            },
            new ISO639Item{
             family="Indo-European",
            culture="mr",
            nativeName="मराठी",
            name="Marathi (Marāṭhī)"
            },
            new ISO639Item{
             family="Austronesian",
            culture="ms",
            nativeName="bahasa Melayu, بهاس ملايو‎",
            name="Malay"
            },
            new ISO639Item{
             family="Afro-Asiatic",
            culture="mt",
            nativeName="Malti",
            name="Maltese"
            },
            new ISO639Item{
             family="Sino-Tibetan",
            culture="my",
            nativeName="ဗမာစာ",
            name="Burmese"
            },
            new ISO639Item{
             family="Austronesian",
            culture="na",
            nativeName="Dorerin Naoero",
            name="Nauruan"
            },
            new ISO639Item{
             family="Indo-European",
            culture="nb",
            nativeName="Norsk bokmål",
            name="Norwegian Bokmål"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="nd",
            nativeName="isiNdebele",
            name="Northern Ndebele"
            },
            new ISO639Item{
             family="Indo-European",
            culture="ne",
            nativeName="नेपाली",
            name="Nepali"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="ng",
            nativeName="Owambo",
            name="Ndonga"
            },
            new ISO639Item{
             family="Indo-European",
            culture="nl",
            nativeName="Nederlands, Vlaams",
            name="Dutch"
            },
            new ISO639Item{
             family="Indo-European",
            culture="nn",
            nativeName="Norsk nynorsk",
            name="Norwegian Nynorsk"
            },
            new ISO639Item{
             family="Indo-European",
            culture="no",
            nativeName="Norsk",
            name="Norwegian"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="nr",
            nativeName="isiNdebele",
            name="Southern Ndebele"
            },
            new ISO639Item{
             family="Dené–Yeniseian",
            culture="nv",
            nativeName="Diné bizaad",
            name="Navajo, Navaho"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="ny",
            nativeName="chiCheŵa, chinyanja",
            name="Chichewa, Chewa, Nyanja"
            },
            new ISO639Item{
             family="Indo-European",
            culture="oc",
            nativeName="occitan, lenga d'òc",
            name="Occitan"
            },
            new ISO639Item{
             family="Algonquian",
            culture="oj",
            nativeName="ᐊᓂᔑᓈᐯᒧᐎᓐ",
            name="Ojibwe, Ojibwa"
            },
            new ISO639Item{
             family="Afro-Asiatic",
            culture="om",
            nativeName="Afaan Oromoo",
            name="Oromo"
            },
            new ISO639Item{
             family="Indo-European",
            culture="or",
            nativeName="ଓଡ଼ିଆ",
            name="Oriya"
            },
            new ISO639Item{
             family="Indo-European",
            culture="os",
            nativeName="ирон æвзаг",
            name="Ossetian, Ossetic"
            },
            new ISO639Item{
             family="Indo-European",
            culture="pa",
            nativeName="ਪੰਜਾਬੀ",
            name="(Eastern) Punjabi"
            },
            new ISO639Item{
             family="Indo-European",
            culture="pi",
            nativeName="पाऴि",
            name="Pāli"
            },
            new ISO639Item{
             family="Indo-European",
            culture="pl",
            nativeName="język polski, polszczyzna",
            name="Polish"
            },
            new ISO639Item{
             family="Indo-European",
            culture="ps",
            nativeName="پښتو",
            name="Pashto, Pushto"
            },
            new ISO639Item{
             family="Indo-European",
            culture="pt",
            nativeName="Português",
            name="Portuguese"
            },
            new ISO639Item{
             family="Quechuan",
            culture="qu",
            nativeName="Runa Simi, Kichwa",
            name="Quechua"
            },
            new ISO639Item{
             family="Indo-European",
            culture="rm",
            nativeName="rumantsch grischun",
            name="Romansh"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="rn",
            nativeName="Ikirundi",
            name="Kirundi"
            },
            new ISO639Item{
             family="Indo-European",
            culture="ro",
            nativeName="Română",
            name="Romanian"
            },
            new ISO639Item{
             family="Indo-European",
            culture="ru",
            nativeName="Русский",
            name="Russian"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="rw",
            nativeName="Ikinyarwanda",
            name="Kinyarwanda"
            },
            new ISO639Item{
             family="Indo-European",
            culture="sa",
            nativeName="संस्कृतम्",
            name="Sanskrit (Saṁskṛta)"
            },
            new ISO639Item{
             family="Indo-European",
            culture="sc",
            nativeName="sardu",
            name="Sardinian"
            },
            new ISO639Item{
             family="Indo-European",
            culture="sd",
            nativeName="सिन्धी, سنڌي، سندھی‎",
            name="Sindhi"
            },
            new ISO639Item{
             family="Uralic",
            culture="se",
            nativeName="Davvisámegiella",
            name="Northern Sami"
            },
            new ISO639Item{
             family="Creole",
            culture="sg",
            nativeName="yângâ tî sängö",
            name="Sango"
            },
            new ISO639Item{
             family="Indo-European",
            culture="si",
            nativeName="සිංහල",
            name="Sinhalese, Sinhala"
            },
            new ISO639Item{
             family="Indo-European",
            culture="sk",
            nativeName="slovenčina, slovenský jazyk",
            name="Slovak"
            },
            new ISO639Item{
             family="Indo-European",
            culture="sl",
            nativeName="slovenski jezik, slovenščina",
            name="Slovene"
            },
            new ISO639Item{
             family="Austronesian",
            culture="sm",
            nativeName="gagana fa'a Samoa",
            name="Samoan"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="sn",
            nativeName="chiShona",
            name="Shona"
            },
            new ISO639Item{
             family="Afro-Asiatic",
            culture="so",
            nativeName="Soomaaliga, af Soomaali",
            name="Somali"
            },
            new ISO639Item{
             family="Indo-European",
            culture="sq",
            nativeName="Shqip",
            name="Albanian"
            },
            new ISO639Item{
             family="Indo-European",
            culture="sr",
            nativeName="српски језик",
            name="Serbian"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="ss",
            nativeName="SiSwati",
            name="Swati"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="st",
            nativeName="Sesotho",
            name="Southern Sotho"
            },
            new ISO639Item{
             family="Austronesian",
            culture="su",
            nativeName="Basa Sunda",
            name="Sundanese"
            },
            new ISO639Item{
             family="Indo-European",
            culture="sv",
            nativeName="svenska",
            name="Swedish"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="sw",
            nativeName="Kiswahili",
            name="Swahili"
            },
            new ISO639Item{
             family="Dravidian",
            culture="ta",
            nativeName="தமிழ்",
            name="Tamil"
            },
            new ISO639Item{
             family="Dravidian",
            culture="te",
            nativeName="తెలుగు",
            name="Telugu"
            },
            new ISO639Item{
             family="Indo-European",
            culture="tg",
            nativeName="тоҷикӣ, toçikī, تاجیکی‎",
            name="Tajik"
            },
            new ISO639Item{
             family="Tai–Kadai",
            culture="th",
            nativeName="ไทย",
            name="Thai"
            },
            new ISO639Item{
             family="Afro-Asiatic",
            culture="ti",
            nativeName="ትግርኛ",
            name="Tigrinya"
            },
            new ISO639Item{
             family="Turkic",
            culture="tk",
            nativeName="Türkmen, Түркмен",
            name="Turkmen"
            },
            new ISO639Item{
             family="Austronesian",
            culture="tl",
            nativeName="Wikang Tagalog",
            name="Tagalog"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="tn",
            nativeName="Setswana",
            name="Tswana"
            },
            new ISO639Item{
             family="Austronesian",
            culture="to",
            nativeName="faka Tonga",
            name="Tonga (Tonga Islands)"
            },
            new ISO639Item{
             family="Turkic",
            culture="tr",
            nativeName="Türkçe",
            name="Turkish"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="ts",
            nativeName="Xitsonga",
            name="Tsonga"
            },
            new ISO639Item{
             family="Turkic",
            culture="tt",
            nativeName="татар теле, tatar tele",
            name="Tatar"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="tw",
            nativeName="Twi",
            name="Twi"
            },
            new ISO639Item{
             family="Austronesian",
            culture="ty",
            nativeName="Reo Tahiti",
            name="Tahitian"
            },
            new ISO639Item{
             family="Turkic",
            culture="ug",
            nativeName="ئۇيغۇرچە‎, Uyghurche",
            name="Uyghur"
            },
            new ISO639Item{
             family="Indo-European",
            culture="uk",
            nativeName="Українська",
            name="Ukrainian"
            },
            new ISO639Item{
             family="Indo-European",
            culture="ur",
            nativeName="اردو",
            name="Urdu"
            },
            new ISO639Item{
             family="Turkic",
            culture="uz",
            nativeName="Oʻzbek, Ўзбек, أۇزبېك‎",
            name="Uzbek"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="ve",
            nativeName="Tshivenḓa",
            name="Venda"
            },
            new ISO639Item{
             family="Austroasiatic",
            culture="vi",
            nativeName="Tiếng Việt",
            name="Vietnamese"
            },
            new ISO639Item{
             family="Constructed",
            culture="vo",
            nativeName="Volapük",
            name="Volapük"
            },
            new ISO639Item{
             family="Indo-European",
            culture="wa",
            nativeName="walon",
            name="Walloon"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="wo",
            nativeName="Wollof",
            name="Wolof"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="xh",
            nativeName="isiXhosa",
            name="Xhosa"
            },
            new ISO639Item{
             family="Indo-European",
            culture="yi",
            nativeName="ייִדיש",
            name="Yiddish"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="yo",
            nativeName="Yorùbá",
            name="Yoruba"
            },
            new ISO639Item{
             family="Tai–Kadai",
            culture="za",
            nativeName="Saɯ cueŋƅ, Saw cuengh",
            name="Zhuang, Chuang"
            },
            new ISO639Item{
             family="Sino-Tibetan",
            culture="zh",
            nativeName="中文 (Zhōngwén), 汉语, 漢語",
            name="Chinese"
            },
            new ISO639Item{
             family="Niger–Congo",
            culture="zu",
            nativeName="isiZulu",
            name="Zulu"
            }
        };
        public static decimal GetApplicantId(ControllerBase controller)
        {

            controller.HttpContext.Request.Headers.TryGetValue("ApplicantId", out var applicantId);
            decimal appId = 0;
            try
            {
                appId = Convert.ToDecimal(applicantId + "");
            }catch(Exception e)
            {
                appId = 0;
            }
            
            return (decimal)ValidateApplicant(appId, controller);


        }

        public static decimal? ValidateApplicant(decimal ap, ControllerBase controller)
        {
            var db = new ModelContext();
            var compId = GetCompanyId(controller);
            var uid = GetUserId(controller); var userName = db.HrRcrtUsers.Where(u => u.UserId == uid && u.CompanyId == compId).Select(x=>x.UserName).FirstOrDefault();
            var apId = db.HrRcrtApplicants.Where(a => a.UserName == userName && a.CompanyId == compId && ap == a.ApplicantId).Select(a => a.ApplicantId).FirstOrDefault();
            return apId;
        }
        public static string GetActivationToken(ControllerBase controller)
        {

            controller.HttpContext.Request.Headers.TryGetValue("ActivationToken", out var ActivationToken);
            
            return ActivationToken + "";


        }

        public static string GetControllerActionNames(ControllerBase controllerInstance)
        {
            var controller = controllerInstance.ControllerContext.ActionDescriptor.ControllerName;
            var action = controllerInstance.ControllerContext.ActionDescriptor.ActionName;

            return $"{controller} - {action}";
        }
        public static string GenerateJSONWebToken<T>(T session)
        {
            var claims = new List<Claim>();

            foreach (var prop in session.GetType().GetProperties())
            {
                claims.Add(new Claim($"{prop.Name}", prop.GetValue(session) + ""));
            }



            // var roles = await _loginRepository.GetRolesAsync(user);
            //claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));
            //context.Items["User"] =
            var _config = Startup.GetInstance().Configuration;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                , _config["Jwt:Issuer"],
                claims,
                null,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public static void MapModelDTO(object Model, object DTO)
        {
            var props = DTO.GetType().GetProperties();
            foreach(var prop in props)
            {
                var modelProp = Model.GetType().GetProperty(prop.Name);
                if(modelProp != null)
                {
                    prop.SetValue(DTO, modelProp.GetValue(Model));
                }               
            }

        }

        public static Comparison<object> DropdownSort = delegate (object P1, object P2)
        {
            var Text1 = P1.GetType().GetProperty("Text").GetValue(P1) + "";
            var Text2 = P2.GetType().GetProperty("Text").GetValue(P2) + "";
            return Text1.CompareTo(Text2);
        };


        public static void SendEmail(CoReportsService RS, MailMessage Message)
        {
            string email = RS.Username;
            string password = RS.Password;
            Message.From = new MailAddress(RS.FromEmail);

            var loginInfo = new NetworkCredential(email, password);

            var smtpClient = new SmtpClient(RS.SmtpServer, Convert.ToInt32(RS.Port));
            smtpClient.EnableSsl = RS.Port == 587 || RS.Port == 465;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            if(Message.To.Where(x=>x.Address == null).Any())
            {
                return;
            }
            smtpClient.Send(Message);
            var Logger = new LoggerService();
            Logger.LogDebug($"Sent Email to {Message.To.Select(e => e.Address).FirstOrDefault()} - {Message.Subject}");
        }
        public static bool ValidateLastLogin(ControllerBase controller)
        {
            var claimsIdentity = controller.User.Identity as System.Security.Claims.ClaimsIdentity;
            // get some claim by type
            var lastLoginStr = claimsIdentity.FindFirst("LoggedOn").Value;
            var lastLogin = Convert.ToDateTime(lastLoginStr.Replace("\"", ""));
            var diff = DateTime.Now - lastLogin;

            if(diff.TotalHours > 0 && diff.TotalHours < 5)
            {
                return true;
            }
            return false;
        }

        public static decimal GetUserId (ControllerBase controller)
        {
            try
            {
                var claimsIdentity = controller.User.Identity as System.Security.Claims.ClaimsIdentity;
                // get some claim by type
                var UserClaim = claimsIdentity.FindFirst("UserId");
                if(UserClaim == null)
                {
                    return 0;
                }
                var userId = Convert.ToDecimal(UserClaim.Value);
                return userId;
            }
            catch
            {
                return 0;
            }
            
        }

        public static string GenerateIMToken(ControllerBase controller)
        {
            return RenovaCommon.Helpers.ValueHelper.GenrateIMToken(controller);
        }
        public static string GenerateUserToken(HrRcrtUser User, ControllerBase controller)
        {
            var session = new Session
            {
                UserId = User.UserId,
                LoggedOn = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.sssZ"),
                IpAddress = controller.HttpContext.Connection.RemoteIpAddress.ToString(),
                UserAgent = controller.HttpContext.Request.Headers["User-Agent"].ToString()

            };


            var token = GenerateJSONWebToken<Session>(session);
            return token;
        }
        public static void BuildXmlFile(string path)
        {

            try
            {
                XmlDocument doc = new XmlDocument();

                var root = doc.CreateElement("doc");
                doc.AppendChild(root);
                var assembly = doc.CreateElement("assembly");
                var assemblyName = doc.CreateElement("name");
                assemblyName.InnerText = "Renova HR Employment API";
                assembly.AppendChild(assemblyName);
                root.AppendChild(assembly);

                var members = doc.CreateElement("members");
                members.InnerText = "";
                root.AppendChild(members);
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.InsertBefore(xmlDeclaration, root);

                doc.Save(path);
            }catch(Exception e)
            {
                var logger = new LoggerService();
                logger.LogError(e.Message);
            }
            
        }

        public static string GetRedirectTpl()
        {
            var str = System.IO.File.ReadAllText("Assets/redirectTpl.html");
            return str;
        }

        private string key = "RENOVANOW";
        private string iv = "RENOVANOW";

        public static byte[] GetByteKey(string text, int length=32)
        {
            if(text.Length <= length)
            {
                var str = text.PadRight(length);
                return Encoding.ASCII.GetBytes(str);
            }
            else
            {
                var str = text.Substring(0, length);
                return Encoding.ASCII.GetBytes(str);
            }            
        }
        public static string EncryptActivation(string clearText)
        {
            var aes = new Aes(System.Text.Encoding.UTF8);
            //RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();


            var key = GetByteKey("RENOVANOW");
            var iv = GetByteKey("RENOVANOW",16);
            string cipher = aes.Encrypt(clearText, key, iv);
            return cipher;
        }
        public static string DecryptActivation(string cipherText)
        {
            var aes = new Aes(System.Text.Encoding.UTF8);
            //RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            var key = GetByteKey("RENOVANOW");
            var iv = GetByteKey("RENOVANOW",16);
            string plaintext = aes.Decrypt(cipherText, key, iv);
            return plaintext;
        }

        public class Aes
        {
            private readonly Encoding encoding;

            private SicBlockCipher mode;


            public Aes(Encoding encoding)
            {
                this.encoding = encoding;
                this.mode = new SicBlockCipher(new AesFastEngine());
            }

            public static string ByteArrayToString(byte[] bytes)
            {
                return BitConverter.ToString(bytes).Replace("-", string.Empty);
            }

            public static byte[] StringToByteArray(string hex)
            {
                int numberChars = hex.Length;
                byte[] bytes = new byte[numberChars / 2];

                for (int i = 0; i < numberChars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                }

                return bytes;
            }


            public string Encrypt(string plain, byte[] key, byte[] iv)
            {
                byte[] input = this.encoding.GetBytes(plain);

                byte[] bytes = this.BouncyCastleCrypto(true, input, key, iv);

                string result = ByteArrayToString(bytes);

                return result;
            }


            public string Decrypt(string cipher, byte[] key, byte[] iv)
            {
                byte[] bytes = this.BouncyCastleCrypto(false, StringToByteArray(cipher), key, iv);

                string result = this.encoding.GetString(bytes);

                return result;
            }


            private byte[] BouncyCastleCrypto(bool forEncrypt, byte[] input, byte[] key, byte[] iv)
            {
                try
                {
                    this.mode.Init(forEncrypt, new ParametersWithIV(new KeyParameter(key), iv));

                    BufferedBlockCipher cipher = new BufferedBlockCipher(this.mode);

                    return cipher.DoFinal(input);
                }
                catch (CryptoException)
                {
                    throw;
                }
            }
        }
    }
}
