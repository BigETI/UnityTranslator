using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator editor namespace
/// </summary>
namespace UnityTranslatorEditor
{
    public interface IXLIFF
    {
        EXLIFFSpecification Specification { get; }

        SystemLanguage SourceLanguage { get; }

        IReadOnlyCollection<SystemLanguage> SupportedLanguages { get; }

        IReadOnlyDictionary<string, string> Comments { get; }

        IReadOnlyList<IXLIFFDocument> XLIFFDocuments { get; }

        bool TryGetTranslations(SystemLanguage language, out IReadOnlyDictionary<string, string> result);

        IReadOnlyDictionary<string, string> GetTranslations(SystemLanguage language);

        IReadOnlyList<IXLIFFDocument> GetXLIFFDocuments(EXLIFFSpecification specification);
    }
}
