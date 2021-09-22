using UnityEngine;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// A class that describes a translator
    /// </summary>
    public static class Translator
    {
        /// <summary>
        /// Forced language
        /// </summary>
        private static SystemLanguage forcedLanguage = SystemLanguage.English;

        /// <summary>
        /// Is language forced
        /// </summary>
        public static bool IsLanguageForced { get; private set; }

        /// <summary>
        /// Current language
        /// </summary>
        public static SystemLanguage CurrentLanguage => IsLanguageForced ? forcedLanguage : Application.systemLanguage;

        /// <summary>
        /// Force language
        /// </summary>
        /// <param name="language">Language</param>
        public static void ForceLanguage(SystemLanguage language)
        {
            forcedLanguage = language;
            IsLanguageForced = true;
        }

        /// <summary>
        /// Remove forced language
        /// </summary>
        public static void RemoveForcedLanguage()
        {
            forcedLanguage = SystemLanguage.English;
            IsLanguageForced = false;
        }
    }
}
