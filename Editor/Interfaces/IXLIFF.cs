using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator editor namespace
/// </summary>
namespace UnityTranslatorEditor
{
    /// <summary>
    /// An interface that represents a XLIFF
    /// </summary>
    public interface IXLIFF
    {
        /// <summary>
        /// XLIFF specification
        /// </summary>
        EXLIFFSpecification Specification { get; }

        /// <summary>
        /// Source language
        /// </summary>
        SystemLanguage SourceLanguage { get; }

        /// <summary>
        /// Supported languages
        /// </summary>
        IReadOnlyCollection<SystemLanguage> SupportedLanguages { get; }

        /// <summary>
        /// Comments
        /// </summary>
        IReadOnlyDictionary<string, string> Comments { get; }

        /// <summary>
        /// XLIFF documents
        /// </summary>
        IReadOnlyList<IXLIFFDocument> XLIFFDocuments { get; }

        /// <summary>
        /// Tries to get translations for the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="result">Result</param>
        /// <returns>"true" if translations for specified language are available, otherwise "false"</returns>
        bool TryGetTranslations(SystemLanguage language, out IReadOnlyDictionary<string, string> result);

        /// <summary>
        /// Gets translations
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Translations</returns>
        IReadOnlyDictionary<string, string> GetTranslations(SystemLanguage language);

        /// <summary>
        /// Gets XLIFF documents
        /// </summary>
        /// <param name="specification">XLIFF specification</param>
        /// <returns>XLIFF documents</returns>
        IReadOnlyList<IXLIFFDocument> GetXLIFFDocuments(EXLIFFSpecification specification);
    }
}
