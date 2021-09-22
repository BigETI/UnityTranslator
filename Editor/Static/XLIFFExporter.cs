using System;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;

/// <summary>
/// Unity translator editor namespace
/// </summary>
namespace UnityTranslatorEditor
{
    public static class XLIFFExporter
    {
        public static void ExportXLIFFDocumentToStream(IXLIFFDocument xliffDocument, Stream stream)
        {
            if (xliffDocument == null)
            {
                throw new ArgumentNullException(nameof(xliffDocument));
            }
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            if (!stream.CanWrite)
            {
                throw new ArgumentNullException("Can't write to XLIFF stream.");
            }
            XmlWriterSettings xml_writer_settings = new XmlWriterSettings
            {
                Indent = true,
                Encoding = Encoding.UTF8
            };
            using XmlWriter xml_writer = XmlWriter.Create(stream, xml_writer_settings);
            xml_writer.WriteDocType("xliff", "-//XLIFF//DTD XLIFF//EN", "http://www.oasis-open.org/committees/xliff/documents/xliff.dtd", null);
            xliffDocument.Document.WriteTo(xml_writer);
            xml_writer.Flush();
        }

        public static bool ExportXLIFFDocumetToFile(IXLIFFDocument xliffDocument, string filePath)
        {
            if (xliffDocument == null)
            {
                throw new ArgumentNullException(nameof(xliffDocument));
            }
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            bool ret = false;
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                using FileStream file_stream = File.OpenWrite(filePath);
                ExportXLIFFDocumentToStream(xliffDocument, file_stream);
                ret = true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            return ret;
        }

        public static string ExportXLIFFDocumentToString(IXLIFFDocument xliffDocument)
        {
            using MemoryStream memory_stream = new MemoryStream();
            ExportXLIFFDocumentToStream(xliffDocument, memory_stream);
            memory_stream.Seek(0L, SeekOrigin.Begin);
            using StreamReader stream_reader = new StreamReader(memory_stream);
            return stream_reader.ReadToEnd();
        }
    }
}
