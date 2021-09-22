using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// Unity translator editor namespace
/// </summary>
namespace UnityTranslatorEditor
{
    /// <summary>
    /// A class that describes a XLIFF document
    /// </summary>
    public class XLIFFDocument : IXLIFFDocument
    {
        /// <summary>
        /// Target languages
        /// </summary>
        private readonly List<SystemLanguage> targetLanguages = new List<SystemLanguage>();

        /// <summary>
        /// Source language
        /// </summary>
        public SystemLanguage SourceLanguage { get; }

        /// <summary>
        /// Target languages
        /// </summary>
        public IReadOnlyList<SystemLanguage> TargetLanguages => targetLanguages;

        /// <summary>
        /// Document
        /// </summary>
        public XmlDocument Document { get; }

        /// <summary>
        /// Constructs a new XLIFF document
        /// </summary>
        /// <param name="sourceLanguage"></param>
        /// <param name="targetLanguages"></param>
        /// <param name="document"></param>
        public XLIFFDocument(SystemLanguage sourceLanguage, IEnumerable<SystemLanguage> targetLanguages, XmlDocument document)
        {
            if (targetLanguages == null)
            {
                throw new ArgumentNullException(nameof(targetLanguages));
            }
            SourceLanguage = sourceLanguage;
            foreach (SystemLanguage target_language in targetLanguages)
            {
                this.targetLanguages.Add(target_language);
            }
            Document = document ?? throw new ArgumentNullException(nameof(document));
        }
    }
}
