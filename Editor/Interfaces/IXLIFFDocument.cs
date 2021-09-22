using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// Unity translator editor namespace
/// </summary>
namespace UnityTranslatorEditor
{
    public interface IXLIFFDocument
    {
        SystemLanguage SourceLanguage { get; }

        IReadOnlyList<SystemLanguage> TargetLanguages { get; }

        XmlDocument Document { get; }
    }
}
