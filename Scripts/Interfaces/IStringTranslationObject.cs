using System;
using UnityTranslator.Data;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// String translation object interface
    /// </summary>
    public interface IStringTranslationObject : ITranslationObject, IComparable<IStringTranslationObject>
    {
        /// <summary>
        /// String translation
        /// </summary>
        StringTranslationData StringTranslation { get; }

        /// <summary>
        /// Translated string
        /// </summary>
        string String { get; }

        /// <summary>
        /// Comment
        /// </summary>
        string Comment { get; }
    }
}
