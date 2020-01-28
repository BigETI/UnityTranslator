using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// Translator class
    /// </summary>
    public static class Translator
    {
        /// <summary>
        /// Forced system language
        /// </summary>
        private static SystemLanguage forcedSystemLanguage = SystemLanguage.English;

        /// <summary>
        /// Force system language
        /// </summary>
        private static bool forceSystemLanguage = false;

        /// <summary>
        /// System language
        /// </summary>
        public static SystemLanguage SystemLanguage
        {
            get
            {
                return (forceSystemLanguage ? forcedSystemLanguage : Application.systemLanguage);
            }
        }

        /// <summary>
        /// Force system language
        /// </summary>
        /// <param name="systemLanguage">System language</param>
        public static void ForceSystemLanguage(SystemLanguage systemLanguage)
        {
            forcedSystemLanguage = systemLanguage;
            forceSystemLanguage = true;
        }

        /// <summary>
        /// Remove forced system language
        /// </summary>
        public static void RemoveForcedSystemLanguage()
        {
            forcedSystemLanguage = SystemLanguage.English;
            forceSystemLanguage = false;
        }

        /// <summary>
        /// Load from resource
        /// </summary>
        /// <param name="resourcePath">Resource path</param>
        /// <returns>Translation if successful, otherwise "null"</returns>
        public static StringTranslationObjectScript LoadFromResource(string resourcePath)
        {
            return Resources.Load<StringTranslationObjectScript>("Translations/" + resourcePath);
        }

        /// <summary>
        /// Evaluate translatable string
        /// </summary>
        /// <param name="translatableString">Translatable string</param>
        /// <returns>Translated string if successful, otherwise the input string if not null, otherwise an empty string</returns>
        /// <remarks>
        /// The input string will be evaluated, if if starts with the "{$" and ends with the "$}" suffix.
        /// The inner part should reference a valid path within the "Translation" resource path.
        /// </remarks>
        public static string EvaluateTranslatableString(string translatableString)
        {
            string ret = ((translatableString == null) ? string.Empty : translatableString);
            if (translatableString != null)
            {
                string trimmed_string = translatableString.Trim();
                if ((trimmed_string.Length > 4) && trimmed_string.StartsWith("{$") && trimmed_string.EndsWith("$}"))
                {
                    StringTranslationObjectScript translation = LoadFromResource(trimmed_string.Substring(2, trimmed_string.Length - 4));
                    if (translation != null)
                    {
                        ret = translation.ToString();
                    }
                }
            }
            return ret;
        }
    }
}
