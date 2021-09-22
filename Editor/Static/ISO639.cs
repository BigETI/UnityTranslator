using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator editor namespace
/// </summary>
namespace UnityTranslatorEditor
{
    /// <summary>
    /// A class that describes ISO639-1
    /// </summary>
    public static class ISO639
    {
        /// <summary>
        /// Default language code
        /// </summary>
        private static readonly string defaultLanguageCode = "en";

        /// <summary>
        /// Language code to language lookup
        /// </summary>
        private static readonly IReadOnlyDictionary<string, SystemLanguage> languageCodeToLanguageLookup = new Dictionary<string, SystemLanguage>
        {
            // Afar
            { "aa", SystemLanguage.Unknown },

            // Abkhazian
            { "ab", SystemLanguage.Unknown },

            // Afrikaans
            { "af", SystemLanguage.Afrikaans },

            // Akan
            { "ak", SystemLanguage.Unknown },

            // Albanian
            { "sq", SystemLanguage.Unknown },

            // Amharic
            { "am", SystemLanguage.Unknown },

            // Arabic
            { "ar", SystemLanguage.Arabic },

            // Armenian
            { "hy", SystemLanguage.Unknown },

            // Assamese
            { "as", SystemLanguage.Unknown },

            // Avaric
            { "av", SystemLanguage.Unknown },

            // Avestan
            { "ae", SystemLanguage.Unknown },

            // Aymara
            { "ay", SystemLanguage.Unknown },

            // Azerbaijani
            { "az", SystemLanguage.Turkish },

            // Bashkir
            { "ba", SystemLanguage.Unknown },

            // Bambara
            { "bm", SystemLanguage.Unknown },

            // Basque
            { "eu", SystemLanguage.Basque },

            // Belarusian
            { "be", SystemLanguage.Belarusian },

            // Bengali
            { "bn", SystemLanguage.Unknown },

            // Bihari languages
            { "bh", SystemLanguage.Unknown },

            // Bislama
            { "bi", SystemLanguage.Unknown },

            // Tibetan
            { "bo", SystemLanguage.Unknown },

            // Bosnian
            { "bs", SystemLanguage.SerboCroatian },

            // Breton
            { "br", SystemLanguage.Unknown },

            // Bulgarian
            { "bg", SystemLanguage.Bulgarian },

            // Burmese
            { "by", SystemLanguage.Unknown },

            // Catalan
            { "ca", SystemLanguage.Catalan },

            // Czech
            { "cs", SystemLanguage.Czech },

            // Chamorro
            { "ch", SystemLanguage.Unknown },

            // Chechen
            { "ce", SystemLanguage.Unknown },

            // Chinese
            { "zh", SystemLanguage.Chinese },

            // Church Slavic
            { "cu", SystemLanguage.Unknown },

            // Chuvash
            { "cv", SystemLanguage.Unknown },

            // Cornish
            { "kw", SystemLanguage.Unknown },

            // Corsican
            { "co", SystemLanguage.Italian },

            // Cree
            { "cr", SystemLanguage.Unknown },

            // Welsh
            { "cy", SystemLanguage.English },

            // Danish
            { "da", SystemLanguage.Danish },

            // German
            { "de", SystemLanguage.German },

            // Divehi
            { "dv", SystemLanguage.Unknown },

            // Dutch
            { "nl", SystemLanguage.Dutch },

            // Dzongkha
            { "dz", SystemLanguage.Unknown },

            // Greek
            { "el", SystemLanguage.Greek },

            // English
            { "en", SystemLanguage.English },

            // Esperanto
            { "eo", SystemLanguage.Unknown },

            // Estonian
            { "et", SystemLanguage.Estonian },

            // Ewe
            { "ee", SystemLanguage.Unknown },

            // Faroese
            { "fo", SystemLanguage.Faroese },

            // Persian
            { "fa", SystemLanguage.Unknown },

            // Fijian
            { "fj", SystemLanguage.Unknown },

            // Finnish
            { "fi", SystemLanguage.Finnish },

            // French
            { "fr", SystemLanguage.French },

            // Western Frisian
            { "fy", SystemLanguage.Dutch },

            // Fulah
            { "ff", SystemLanguage.Unknown },

            // Georgian
            { "ka", SystemLanguage.Unknown },

            // Gaelic
            { "gd", SystemLanguage.English },

            // Irish
            { "ga", SystemLanguage.English },

            // Galician
            { "gl", SystemLanguage.Unknown },

            // Manx
            { "gv", SystemLanguage.Unknown },

            // Guarani
            { "gn", SystemLanguage.Unknown },

            // Gujarati
            { "gu", SystemLanguage.Unknown },

            // Haitian
            { "ht", SystemLanguage.French },

            // Hausa
            { "ha", SystemLanguage.Unknown },

            // Hebrew
            { "he", SystemLanguage.Hebrew },

            // Herero
            { "hz", SystemLanguage.Unknown },

            // Hindi
            { "hi", SystemLanguage.Unknown },

            // Hiri Motu
            { "ho", SystemLanguage.Unknown },

            // Croatian
            { "hr", SystemLanguage.SerboCroatian },

            // Hungarian
            { "hu", SystemLanguage.Hungarian },

            // Igbo
            { "ig", SystemLanguage.Unknown },

            // Icelandic
            { "is", SystemLanguage.Icelandic },

            // Ido
            { "io", SystemLanguage.Unknown },

            // Sichuan Yi
            { "ii", SystemLanguage.Unknown },

            // Inuktitut
            { "iu", SystemLanguage.Unknown },

            // Interlingue
            { "ie", SystemLanguage.Unknown },

            // Interlingua
            { "ia", SystemLanguage.Unknown },

            // Indonesian
            { "id", SystemLanguage.Indonesian },

            // Inupiak
            { "ik", SystemLanguage.Unknown },

            // Italian
            { "it", SystemLanguage.Italian },

            // Javanese
            { "jv", SystemLanguage.Unknown },

            // Japanese
            { "ja", SystemLanguage.Japanese },

            // Kalaallisut
            { "kl", SystemLanguage.Danish },

            // Kannada
            { "kn", SystemLanguage.Unknown },

            // Kashmiri
            { "ks", SystemLanguage.Unknown },

            // Kanuri
            { "kr", SystemLanguage.Unknown },

            // Kazakh
            { "kk", SystemLanguage.Turkish },

            // Central Khmer
            { "km", SystemLanguage.Unknown },

            // Kikuyu
            { "ki", SystemLanguage.Unknown },

            // Kinyarwanda
            { "rw", SystemLanguage.Unknown },

            // Kirghiz
            { "ky", SystemLanguage.Turkish },

            // Komi
            { "kv", SystemLanguage.Unknown },

            // Kongo
            { "kg", SystemLanguage.Unknown },

            // Korean
            { "ko", SystemLanguage.Korean },

            // Kuanyama
            { "kj", SystemLanguage.Unknown },

            // Kurdish
            { "ku", SystemLanguage.Unknown },

            // Lao
            { "lo", SystemLanguage.Unknown },

            // Latin
            { "la", SystemLanguage.Unknown },

            // Latvian
            { "lv", SystemLanguage.Latvian },

            // Limburgan
            { "li", SystemLanguage.Unknown },

            // Lingala
            { "ln", SystemLanguage.Unknown },

            // Lithuanian
            { "lt", SystemLanguage.Lithuanian },

            // Luxembourgish
            { "lb", SystemLanguage.German },

            // Luba-Katanga
            { "lu", SystemLanguage.Unknown },

            // Ganda
            { "lg", SystemLanguage.Unknown },

            // Macedonian
            { "mk", SystemLanguage.SerboCroatian },

            // Marshallese
            { "mh", SystemLanguage.Unknown },

            // Malayalam
            { "ml", SystemLanguage.Unknown },

            // Maori
            { "mi", SystemLanguage.Unknown },

            // Marathi
            { "mr", SystemLanguage.Unknown },

            // Malay
            { "ms", SystemLanguage.Unknown },

            // Malagasy
            { "mg", SystemLanguage.Unknown },

            // Maltese
            { "mt", SystemLanguage.Unknown },

            // Mongolian
            { "mn", SystemLanguage.Unknown },

            // Burmese
            { "my", SystemLanguage.Unknown },

            // Nauru
            { "na", SystemLanguage.Unknown },

            // Navajo
            { "nv", SystemLanguage.Unknown },

            // Ndebele, South
            { "nr", SystemLanguage.Unknown },

            // Ndebele, North
            { "nd", SystemLanguage.Unknown },

            // Ndonga
            { "ng", SystemLanguage.Unknown },

            // Nepali
            { "ne", SystemLanguage.Unknown },

            // Norwegian
            { "nn", SystemLanguage.Norwegian },

            // Bokmal, Norwegian
            { "nb", SystemLanguage.Norwegian },

            // Norwegian
            { "no", SystemLanguage.Norwegian },

            // Chichewa
            { "ny", SystemLanguage.Unknown },

            // Occitan
            { "oc", SystemLanguage.Unknown },

            // Ojibwa
            { "oj", SystemLanguage.Unknown },

            // Oriya
            { "or", SystemLanguage.Unknown },

            // Oromo
            { "om", SystemLanguage.Unknown },

            // Ossetian
            { "os", SystemLanguage.Unknown },

            // Panjabi
            { "pa", SystemLanguage.Unknown },

            // Pali
            { "pi", SystemLanguage.Unknown },

            // Polish
            { "pl", SystemLanguage.Polish },

            // Portuguese
            { "pt", SystemLanguage.Portuguese },

            // Pushto
            { "ps", SystemLanguage.Unknown },

            // Quechua
            { "qu", SystemLanguage.Unknown },

            // Romansh
            { "rm", SystemLanguage.Romanian },

            // Romanian
            { "ro", SystemLanguage.Romanian },

            // Rundi
            { "rn", SystemLanguage.Unknown },

            // Russian
            { "ru", SystemLanguage.Russian },

            // Sango
            { "sg", SystemLanguage.Unknown },

            // Sanskrit
            { "sa", SystemLanguage.Unknown },

            // Sinhala
            { "si", SystemLanguage.Unknown },

            // Slovak
            { "sk", SystemLanguage.Slovak },

            // Slovenian
            { "sl", SystemLanguage.Slovenian },

            // Northern Sami
            { "se", SystemLanguage.Unknown },

            // Samoan
            { "sm", SystemLanguage.Unknown },

            // Shona
            { "sn", SystemLanguage.Unknown },

            // Sindhi
            { "sd", SystemLanguage.Unknown },

            // Somali
            { "so", SystemLanguage.Unknown },

            // Sotho, Southern
            { "st", SystemLanguage.Unknown },

            // Spanish
            { "es", SystemLanguage.Spanish },

            // Sardinian
            { "sc", SystemLanguage.Unknown },

            // Serbian
            { "sr", SystemLanguage.SerboCroatian },

            // Swati
            { "ss", SystemLanguage.Unknown },

            // Sundanese
            { "su", SystemLanguage.Unknown },

            // Swahili
            { "sw", SystemLanguage.Unknown },

            // Swedish
            { "sv", SystemLanguage.Swedish },

            // Tahitian
            { "ty", SystemLanguage.French },

            // Tamil
            { "ta", SystemLanguage.Unknown },

            // Tatar
            { "tt", SystemLanguage.Turkish },

            // Telugu
            { "te", SystemLanguage.Unknown },

            // Tajik
            { "tg", SystemLanguage.Unknown },

            // Tagalog
            { "tl", SystemLanguage.Unknown },

            // Thai
            { "th", SystemLanguage.Thai },

            // Tigrinya
            { "ti", SystemLanguage.Unknown },

            // Tonga
            { "to", SystemLanguage.Unknown },

            // Tswana
            { "tn", SystemLanguage.Unknown },

            // Tsonga
            { "ts", SystemLanguage.Unknown },

            // Turkmen
            { "tk", SystemLanguage.Turkish },

            // Turkish
            { "tr", SystemLanguage.Turkish },

            // Twi
            { "tw", SystemLanguage.Unknown },

            // Uighur
            { "ug", SystemLanguage.Turkish },

            // Ukrainian
            { "uk", SystemLanguage.Ukrainian },

            // Urdu
            { "ur", SystemLanguage.Unknown },

            // Uzbek
            { "uz", SystemLanguage.Turkish },

            // Venda
            { "ve", SystemLanguage.Unknown },

            // Vietnamese
            { "vi", SystemLanguage.Vietnamese },

            // Volapük
            { "vo", SystemLanguage.Unknown },

            // Walloon
            { "wa", SystemLanguage.Unknown },

            // Wolof
            { "wo", SystemLanguage.Unknown },

            // Xhosa
            { "xh", SystemLanguage.Unknown },

            // Yiddish
            { "yi", SystemLanguage.German },

            // Yoruba
            { "yo", SystemLanguage.Unknown },

            // Zhuang
            { "za", SystemLanguage.Unknown },

            // Zulu
            { "zu", SystemLanguage.Unknown }
        };

        /// <summary>
        /// Language to language code lookup
        /// </summary>
        private static readonly IReadOnlyDictionary<SystemLanguage, string> languageToLanguageCodeLookup = new Dictionary<SystemLanguage, string>
        {
            { SystemLanguage.Afrikaans, "af" },
            { SystemLanguage.Arabic, "ar" },
            { SystemLanguage.Basque, "eu" },
            { SystemLanguage.Belarusian, "be" },
            { SystemLanguage.Bulgarian, "bg" },
            { SystemLanguage.Catalan, "ca" },
            { SystemLanguage.Chinese, "zh" },
            { SystemLanguage.Czech, "cs" },
            { SystemLanguage.Danish, "da" },
            { SystemLanguage.Dutch, "nl" },
            { SystemLanguage.English, "en" },
            { SystemLanguage.Estonian, "et" },
            { SystemLanguage.Faroese, "fo" },
            { SystemLanguage.Finnish, "fi" },
            { SystemLanguage.French, "fr" },
            { SystemLanguage.German, "de" },
            { SystemLanguage.Greek, "el" },
            { SystemLanguage.Hebrew, "he" },
            { SystemLanguage.Hungarian, "hu" },
            { SystemLanguage.Icelandic, "is" },
            { SystemLanguage.Indonesian, "id" },
            { SystemLanguage.Italian, "it" },
            { SystemLanguage.Japanese, "ja" },
            { SystemLanguage.Korean, "ko" },
            { SystemLanguage.Latvian, "lv" },
            { SystemLanguage.Lithuanian, "lt" },
            { SystemLanguage.Norwegian, "no" },
            { SystemLanguage.Polish, "pl" },
            { SystemLanguage.Portuguese, "pt" },
            { SystemLanguage.Romanian, "ro" },
            { SystemLanguage.Russian, "ru" },
            { SystemLanguage.SerboCroatian, "sr" },
            { SystemLanguage.Slovak, "sk" },
            { SystemLanguage.Slovenian, "sl" },
            { SystemLanguage.Spanish, "es" },
            { SystemLanguage.Swedish, "sv" },
            { SystemLanguage.Thai, "th" },
            { SystemLanguage.Turkish, "tr" },
            { SystemLanguage.Ukrainian, "uk" },
            { SystemLanguage.Vietnamese, "vi" },
            { SystemLanguage.ChineseSimplified, "zh" },
            { SystemLanguage.ChineseTraditional, "zh" },
            { SystemLanguage.Unknown, defaultLanguageCode }
        };

        /// <summary>
        /// Gets the language code key
        /// </summary>
        /// <param name="languageCode">Language code</param>
        /// <returns>Language code key</returns>
        private static string GetLanguageCodeKey(string languageCode)
        {
            if (languageCode == null)
            {
                throw new ArgumentNullException(nameof(languageCode));
            }
            return languageCode.Trim().ToLower();
        }

        /// <summary>
        /// Is language code valid
        /// </summary>
        /// <param name="languageCode">Language code</param>
        /// <returns>"true" if language code is valid, otherwise "false"</returns>
        public static bool IsLanguageCodeValid(string languageCode) => languageCodeToLanguageLookup.TryGetValue(GetLanguageCodeKey(languageCode), out SystemLanguage language) && (language != SystemLanguage.Unknown);

        /// <summary>
        /// Tries to get language from language code
        /// </summary>
        /// <param name="languageCode">Language code</param>
        /// <param name="result">Result</param>
        /// <returns>"true" if language is available, otherwise "false"</returns>
        public static bool TryGetLanguageFromLanguageCode(string languageCode, out SystemLanguage result) => languageCodeToLanguageLookup.TryGetValue(GetLanguageCodeKey(languageCode), out result) && (result != SystemLanguage.Unknown);

        /// <summary>
        /// Gets language from language code
        /// </summary>
        /// <param name="languageCode">Language code</param>
        /// <returns>Language</returns>
        public static SystemLanguage GetLanguageFromLanguageCode(string languageCode) => languageCodeToLanguageLookup.TryGetValue(GetLanguageCodeKey(languageCode), out SystemLanguage ret) ? ret : SystemLanguage.Unknown;

        /// <summary>
        /// Gets language code from language
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Language code</returns>
        public static string GetLanguageCodeFromLanguage(SystemLanguage language) => languageToLanguageCodeLookup.TryGetValue(language, out string ret) ? ret : defaultLanguageCode;
    }
}
