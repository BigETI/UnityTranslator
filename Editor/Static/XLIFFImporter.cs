using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

/// <summary>
/// Unity translator editor namespace
/// </summary>
namespace UnityTranslatorEditor
{
    /// <summary>
    /// A class that describes a XLIFF importer
    /// </summary>
    public static class XLIFFImporter
    {
        /// <summary>
        /// Default exception message
        /// </summary>
        private static readonly string defaultExceptionMessage = "XML document does not conform XLIFF specification: ";

        /// <summary>
        /// Imports XLIFF from stream
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>XLIFF</returns>
        public static IXLIFF ImportXLIFFFromStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            if (!stream.CanRead)
            {
                throw new ArgumentException("Can't read from XLIFF stream.", nameof(stream));
            }
            IXLIFF ret = null;
            try
            {
                XmlDocument xml_document = new XmlDocument();
                using XmlTextReader xml_text_reader = new XmlTextReader(stream);
                xml_text_reader.Namespaces = false;
                xml_document.Load(xml_text_reader);
                if (!(xml_document.DocumentElement is XmlNode xliff_xml_node))
                {
                    throw new FormatException($"{ defaultExceptionMessage }Is not a XLIFF document.");
                }
                if (!(xliff_xml_node.Attributes["version"] is XmlAttribute version_xml_attribute))
                {
                    throw new FormatException($"{ defaultExceptionMessage }XLIFF version is not specified.");
                }
                EXLIFFSpecification specification = version_xml_attribute.Value switch
                {
                    "1.0" => EXLIFFSpecification.Version1,
                    "1.1" => EXLIFFSpecification.Version1Dot1,
                    "1.2" => EXLIFFSpecification.Version1Dot2,
                    "2.0" => EXLIFFSpecification.Version2,
                    _ => throw new FormatException($"{ defaultExceptionMessage }XLIFF version \"{ version_xml_attribute.Value }\" is not supported."),
                };
                Dictionary<SystemLanguage, Dictionary<string, string>> languages = new Dictionary<SystemLanguage, Dictionary<string, string>>();
                Dictionary<string, string> comments = new Dictionary<string, string>();
                if (specification == EXLIFFSpecification.Version2)
                {
                    if (!(xliff_xml_node.Attributes["trgLang"] is XmlAttribute target_language_xml_attribute))
                    {
                        throw new FormatException($"{ defaultExceptionMessage }File must contain a \"trgLang\" attribute.");
                    }
                    if (!ISO639.TryGetLanguageFromLanguageCode(target_language_xml_attribute.Value, out SystemLanguage language))
                    {
                        throw new FormatException($"{ defaultExceptionMessage }Language code \"{ target_language_xml_attribute.Value }\" is not valid.");
                    }
                    Dictionary<string, string> translations = new Dictionary<string, string>();
                    languages.Add(language, translations);
                    foreach (XmlNode file_xml_node in xliff_xml_node.SelectNodes("file"))
                    {
                        XmlNodeList translation_unit_xml_nodes = file_xml_node.SelectNodes("unit");
                        foreach (XmlNode translation_unit_xml_node in translation_unit_xml_nodes)
                        {
                            if (!(translation_unit_xml_node.Attributes["id"] is XmlAttribute id_xml_attribute))
                            {
                                throw new FormatException($"{ defaultExceptionMessage }Translation unit must contain an \"id\" attribute.");
                            }
                            if (translations.ContainsKey(id_xml_attribute.Value))
                            {
                                throw new FormatException($"{ defaultExceptionMessage }Duplicate translation unit ID \"{ id_xml_attribute.Value }\". Translation unit must contain an unique \"id\" attribute.");
                            }
                            if (translation_unit_xml_node.SelectSingleNode("notes") is XmlNode notes_xml_node && notes_xml_node.SelectSingleNode("note") is XmlNode note_xml_node && !comments.ContainsKey(id_xml_attribute.Value))
                            {
                                comments.Add(id_xml_attribute.Value, note_xml_node.InnerText);
                            }
                            if (!(translation_unit_xml_node.SelectSingleNode("segment") is XmlNode segment_xml_node))
                            {
                                throw new FormatException($"{ defaultExceptionMessage }Translation unit must contain a \"segment\" node.");
                            }
                            if (!(segment_xml_node.SelectSingleNode("target") is XmlNode target_xml_node))
                            {
                                throw new FormatException($"{ defaultExceptionMessage }Segment must contain a \"target\" node.");
                            }
                            translations.Add(id_xml_attribute.Value, target_xml_node.InnerText.Replace("\r", ""));
                        }
                    }
                }
                else
                {
                    foreach (XmlNode file_xml_node in xliff_xml_node.SelectNodes("file"))
                    {
                        string target_attribute_name = (specification == EXLIFFSpecification.Version1) ? "source-language" : "target-language";
                        if (!(file_xml_node.Attributes[target_attribute_name] is XmlAttribute target_language_xml_attribute))
                        {
                            throw new FormatException($"{ defaultExceptionMessage }File must contain \"{ target_attribute_name }\" attribute.");
                        }
                        if (!ISO639.TryGetLanguageFromLanguageCode(target_language_xml_attribute.Value, out SystemLanguage language))
                        {
                            throw new FormatException($"{ defaultExceptionMessage }Language code \"{ target_language_xml_attribute.Value }\" is not valid.");
                        }
                        if (!languages.TryGetValue(language, out Dictionary<string, string> translations))
                        {
                            translations = new Dictionary<string, string>();
                            languages.Add(language, translations);
                        }
                        if (!(file_xml_node.SelectSingleNode("body") is XmlNode body_xml_node))
                        {
                            throw new FormatException($"{ defaultExceptionMessage }File node must contain \"body\" node.");
                        }
                        XmlNodeList translation_units_xml_node_list = body_xml_node.SelectNodes("trans-unit");
                        foreach (XmlNode translation_unit_xml_node in translation_units_xml_node_list)
                        {
                            if (!(translation_unit_xml_node.Attributes["id"] is XmlAttribute id_xml_attribute))
                            {
                                throw new FormatException($"{ defaultExceptionMessage }Translation unit must contain an \"id\" attribute.");
                            }
                            if (translations.ContainsKey(id_xml_attribute.Value))
                            {
                                throw new FormatException($"{ defaultExceptionMessage }Duplicate translation unit ID \"{ id_xml_attribute.Value }\". Translation unit must contain an unique \"id\" attribute.");
                            }
                            if (specification == EXLIFFSpecification.Version1)
                            {
                                translations.Add(id_xml_attribute.Value, translation_unit_xml_node.InnerText.Replace("\r", ""));
                            }
                            else if (!(translation_unit_xml_node.SelectSingleNode("target") is XmlNode target_xml_node))
                            {
                                throw new FormatException($"{ defaultExceptionMessage }Translation unit must contain a \"target\" node.");
                            }
                            else
                            {
                                translations.Add(id_xml_attribute.Value, target_xml_node.InnerText.Replace("\r", ""));
                            }
                        }
                    }
                }
                ret = new XLIFF(specification, SystemLanguage.English, languages, comments);
                languages.Clear();
                comments.Clear();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            return ret;
        }

        /// <summary>
        /// Imports a XLIFF from the specified file path
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>XLIFF</returns>
        public static IXLIFF ImportFromFile(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            IXLIFF ret = null;
            try
            {
                if (File.Exists(filePath))
                {
                    using FileStream file_stream = File.OpenRead(filePath);
                    ret = ImportXLIFFFromStream(file_stream);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            return ret;
        }
    }
}
