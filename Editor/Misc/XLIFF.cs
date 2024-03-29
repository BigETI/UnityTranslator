﻿using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// Unity translator editor namespace
/// </summary>
namespace UnityTranslatorEditor
{
    /// <summary>
    /// A class that describes a XLIFF
    /// </summary>
    public class XLIFF : IXLIFF
    {
        /// <summary>
        /// Empty translations
        /// </summary>
        private static readonly IReadOnlyDictionary<string, string> emptyTranslations = new Dictionary<string, string>();

        /// <summary>
        /// Languages
        /// </summary>
        private readonly Dictionary<SystemLanguage, Dictionary<string, string>> languages;

        /// <summary>
        /// Comments
        /// </summary>
        private readonly Dictionary<string, string> comments;

        /// <summary>
        /// Keys
        /// </summary>
        private readonly HashSet<string> keys = new HashSet<string>();

        /// <summary>
        /// XLIFF specification
        /// </summary>
        public EXLIFFSpecification Specification { get; }

        /// <summary>
        /// Source language
        /// </summary>
        public SystemLanguage SourceLanguage { get; }

        /// <summary>
        /// Supported languages
        /// </summary>
        public IReadOnlyCollection<SystemLanguage> SupportedLanguages => languages.Keys;

        /// <summary>
        /// Comments
        /// </summary>
        public IReadOnlyDictionary<string, string> Comments => comments;

        /// <summary>
        /// XLIFF documents
        /// </summary>
        public IReadOnlyList<IXLIFFDocument> XLIFFDocuments => GetXLIFFDocuments(Specification);

        /// <summary>
        /// Constructs a new XLIFF
        /// </summary>
        /// <param name="specification">XLIFF specification</param>
        /// <param name="sourceLanguage">Source language</param>
        /// <param name="languages">Languages</param>
        /// <param name="comments">Comments</param>
        public XLIFF(EXLIFFSpecification specification, SystemLanguage sourceLanguage, IDictionary<SystemLanguage, Dictionary<string, string>> languages, IDictionary<string, string> comments)
        {
            Specification = specification;
            SourceLanguage = sourceLanguage;
            this.languages = new Dictionary<SystemLanguage, Dictionary<string, string>>(languages ?? throw new ArgumentNullException(nameof(languages)));
            this.comments = new Dictionary<string, string>(comments ?? throw new ArgumentNullException(nameof(comments)));
            foreach (Dictionary<string, string> translations in languages.Values)
            {
                keys.UnionWith(translations.Keys);
            }
        }

        /// <summary>
        /// Tries to get translations for the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="result">Result</param>
        /// <returns>"true" if translations for specified language are available, otherwise "false"</returns>
        public bool TryGetTranslations(SystemLanguage language, out IReadOnlyDictionary<string, string> result)
        {
            bool ret = languages.TryGetValue(language, out Dictionary<string, string> translations);
            result = ret ? translations : emptyTranslations;
            return ret;
        }

        /// <summary>
        /// Gets translations
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Translations</returns>
        public IReadOnlyDictionary<string, string> GetTranslations(SystemLanguage language)
        {
            TryGetTranslations(language, out IReadOnlyDictionary<string, string> ret);
            return ret;
        }

        /// <summary>
        /// Gets XLIFF documents
        /// </summary>
        /// <param name="specification">XLIFF specification</param>
        /// <returns>XLIFF documents</returns>
        public IReadOnlyList<IXLIFFDocument> GetXLIFFDocuments(EXLIFFSpecification specification)
        {
            List<IXLIFFDocument> ret = new List<IXLIFFDocument>();
            IReadOnlyDictionary<string, string> source_translations = languages.TryGetValue(SourceLanguage, out Dictionary<string, string> found_source_translations) ? found_source_translations : emptyTranslations;
            if (specification == EXLIFFSpecification.Version2)
            {
                foreach (KeyValuePair<SystemLanguage, Dictionary<string, string>> language in languages)
                {
                    if (language.Key != SourceLanguage)
                    {
                        XmlDocument xliff_xml_document = new XmlDocument();
                        XmlNode xliff_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "xliff", "urn:oasis:names:tc:xliff:document:2.0");
                        XmlAttribute xliff_version_xml_attribute = xliff_xml_document.CreateAttribute("version");
                        XmlAttribute source_language_xml_attribute = xliff_xml_document.CreateAttribute("srcLang");
                        XmlAttribute target_language_xml_attribute = xliff_xml_document.CreateAttribute("trgLang");
                        xliff_version_xml_attribute.Value = "2.0";
                        XmlNode file_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "file", null);
                        XmlAttribute file_id_xml_attribute = xliff_xml_document.CreateAttribute("id");
                        string language_code = ISO639.GetLanguageCodeFromLanguage(language.Key);
                        string source_language_code = ISO639.GetLanguageCodeFromLanguage(SourceLanguage);
                        source_language_xml_attribute.Value = source_language_code;
                        target_language_xml_attribute.Value = language_code;
                        file_id_xml_attribute.Value = $"{ source_language_code.ToUpper() }To{ language_code.ToUpper() }";
                        file_xml_node.Attributes.Append(file_id_xml_attribute);
                        foreach (string key in keys)
                        {
                            if (!language.Value.TryGetValue(key, out string translation))
                            {
                                translation = string.Empty;
                            }
                            XmlNode translation_unit_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "unit", null);
                            XmlAttribute id_xml_attribute = xliff_xml_document.CreateAttribute("id");
                            id_xml_attribute.Value = key;
                            translation_unit_xml_node.Attributes.Append(id_xml_attribute);
                            if (comments.TryGetValue(key, out string comment))
                            {
                                XmlNode notes_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "notes", null);
                                XmlNode note_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "note", null);
                                XmlAttribute note_id_xml_attribute = xliff_xml_document.CreateAttribute("id");
                                note_id_xml_attribute.Value = $"n:{ key }";
                                note_xml_node.Attributes.Append(note_id_xml_attribute);
                                note_xml_node.InnerText = comment;
                                notes_xml_node.AppendChild(note_xml_node);
                                translation_unit_xml_node.AppendChild(notes_xml_node);
                            }
                            XmlNode segment_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "segment", null);
                            XmlAttribute segment_id_xml_attribute = xliff_xml_document.CreateAttribute("id");
                            segment_id_xml_attribute.Value = $"s:{ key }";
                            segment_xml_node.Attributes.Append(segment_id_xml_attribute);
                            XmlNode source_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "source", null);
                            XmlNode target_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "target", null);
                            source_xml_node.InnerText = source_translations.TryGetValue(key, out string source_translation) ? source_translation : string.Empty;
                            target_xml_node.InnerText = translation;
                            segment_xml_node.AppendChild(source_xml_node);
                            segment_xml_node.AppendChild(target_xml_node);
                            translation_unit_xml_node.AppendChild(segment_xml_node);
                            file_xml_node.AppendChild(translation_unit_xml_node);
                        }
                        xliff_xml_node.AppendChild(file_xml_node);
                        xliff_xml_node.Attributes.Append(xliff_version_xml_attribute);
                        xliff_xml_node.Attributes.Append(source_language_xml_attribute);
                        xliff_xml_node.Attributes.Append(target_language_xml_attribute);
                        xliff_xml_document.AppendChild(xliff_xml_node);
                        ret.Add(new XLIFFDocument(SourceLanguage, new SystemLanguage[] { language.Key }, xliff_xml_document));
                    }
                }
            }
            else
            {
                string xliff_version = specification switch
                {
                    EXLIFFSpecification.Version1 => "1.0",
                    EXLIFFSpecification.Version1Dot1 => "1.1",
                    EXLIFFSpecification.Version1Dot2 => "1.2",
                    _ => throw new ArgumentException($"Unknown XLIFF specification \"{ specification }\".")
                };
                XmlDocument xliff_xml_document = new XmlDocument();
                XmlNode xliff_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "xliff", $"urn:oasis:names:tc:xliff:document:{ xliff_version }");
                XmlAttribute xliff_version_xml_attribute = xliff_xml_document.CreateAttribute("version");
                xliff_version_xml_attribute.Value = xliff_version;
                foreach (KeyValuePair<SystemLanguage, Dictionary<string, string>> language in languages)
                {
                    if (language.Key != SourceLanguage)
                    {
                        XmlNode file_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "file", null);
                        XmlNode header_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "header", null);
                        XmlNode body_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "body", null);
                        XmlAttribute source_language_xml_attribute = xliff_xml_document.CreateAttribute("source-language");
                        XmlAttribute target_language_xml_attribute = null;
                        XmlAttribute datatype_xml_attribute = xliff_xml_document.CreateAttribute("datatype");
                        XmlAttribute original_xml_attribute = xliff_xml_document.CreateAttribute("original");
                        string language_code = ISO639.GetLanguageCodeFromLanguage(language.Key).ToUpper();
                        string source_language_code = ISO639.GetLanguageCodeFromLanguage(SourceLanguage).ToUpper();
                        if (specification == EXLIFFSpecification.Version1)
                        {
                            source_language_xml_attribute.Value = language_code;
                        }
                        else
                        {
                            source_language_xml_attribute.Value = source_language_code;
                            target_language_xml_attribute = xliff_xml_document.CreateAttribute("target-language");
                            target_language_xml_attribute.Value = language_code;
                        }
                        datatype_xml_attribute.Value = "plaintext";
                        original_xml_attribute.Value = $"{ language_code }.xml";
                        file_xml_node.Attributes.Append(source_language_xml_attribute);
                        if (target_language_xml_attribute != null)
                        {
                            file_xml_node.Attributes.Append(target_language_xml_attribute);
                        }
                        file_xml_node.Attributes.Append(datatype_xml_attribute);
                        file_xml_node.Attributes.Append(original_xml_attribute);
                        foreach (string key in keys)
                        {
                            if (!language.Value.TryGetValue(key, out string translation))
                            {
                                translation = string.Empty;
                            }
                            XmlNode translation_unit_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "trans-unit", null);
                            XmlAttribute id_xml_attribute = xliff_xml_document.CreateAttribute("id");
                            id_xml_attribute.Value = key;
                            translation_unit_xml_node.Attributes.Append(id_xml_attribute);
                            switch (specification)
                            {
                                case EXLIFFSpecification.Version1:
                                    translation_unit_xml_node.InnerText = translation;
                                    break;
                                case EXLIFFSpecification.Version1Dot1:
                                case EXLIFFSpecification.Version1Dot2:
                                    XmlNode source_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "source", null);
                                    XmlNode target_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "target", null);
                                    source_xml_node.InnerText = source_translations.TryGetValue(key, out string source_translation) ? source_translation : string.Empty;
                                    target_xml_node.InnerText = translation;
                                    translation_unit_xml_node.AppendChild(source_xml_node);
                                    translation_unit_xml_node.AppendChild(target_xml_node);
                                    break;
                            }
                            if (comments.TryGetValue(key, out string comment))
                            {
                                XmlNode note_xml_node = xliff_xml_document.CreateNode(XmlNodeType.Element, "note", null);
                                note_xml_node.InnerText = $"{ key }: { comment }";
                                header_xml_node.AppendChild(note_xml_node);
                            }
                            body_xml_node.AppendChild(translation_unit_xml_node);
                        }
                        file_xml_node.AppendChild(header_xml_node);
                        file_xml_node.AppendChild(body_xml_node);
                        xliff_xml_node.AppendChild(file_xml_node);
                    }
                }
                xliff_xml_node.Attributes.Append(xliff_version_xml_attribute);
                xliff_xml_document.AppendChild(xliff_xml_node);
                ret.Add(new XLIFFDocument(SourceLanguage, languages.Keys, xliff_xml_document));
            }
            return ret;
        }
    }
}
