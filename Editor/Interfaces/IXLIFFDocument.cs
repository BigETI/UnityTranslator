using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// Unity translator editor namespace
/// </summary>
namespace UnityTranslatorEditor
{
    /// <summary>
    /// An interface that represents a XLIFF document
    /// </summary>
    public interface IXLIFFDocument
    {
        /// <summary>
        /// Source language
        /// </summary>
        SystemLanguage SourceLanguage { get; }

        /// <summary>
        /// Target languages
        /// </summary>
        IReadOnlyList<SystemLanguage> TargetLanguages { get; }

        /// <summary>
        /// Document
        /// </summary>
        XmlDocument Document { get; }
    }
}
