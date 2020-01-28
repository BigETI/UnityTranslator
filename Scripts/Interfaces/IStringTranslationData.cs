using System;
using System.Collections.Generic;
using UnityTranslator.Data;

/// <summary>
/// Unity translator interface
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// String translation data interface
    /// </summary>
    public interface IStringTranslationData : IComparable<IStringTranslationData>
    {
        /// <summary>
        /// Translated strings
        /// </summary>
        IReadOnlyList<TranslatedStringData> Strings { get; }

        /// <summary>
        /// Translated string
        /// </summary>
        string String { get; }

        /// <summary>
        /// Add translated string
        /// </summary>
        /// <param name="stringValue">Translated string</param>
        void AddString(TranslatedStringData stringValue);
    }
}
