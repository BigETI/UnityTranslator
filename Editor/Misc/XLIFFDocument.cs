using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// Unity translator editor namespace
/// </summary>
namespace UnityTranslatorEditor
{
    public class XLIFFDocument : IXLIFFDocument
    {
        private readonly List<SystemLanguage> targetLanguages = new List<SystemLanguage>();

        public SystemLanguage SourceLanguage { get; }

        public IReadOnlyList<SystemLanguage> TargetLanguages => targetLanguages;

        public XmlDocument Document { get; }

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
