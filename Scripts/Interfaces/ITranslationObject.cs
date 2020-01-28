using UnityEngine;
/// <summary>
/// Unity translator interface
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// Translation object interface
    /// </summary>
    public interface ITranslationObject
    {
        /// <summary>
        /// Name
        /// </summary>
        string name { get; set; }

        /// <summary>
        /// Contains language
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>"true" if contains language, otherwise "false"</returns>
        bool ContainsLanguage(SystemLanguage language);
    }
}
