using System;
using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// Audio clip translation object interface
    /// </summary>
    public interface IAudioClipTranslationObject : ITranslationObject, IComparable<IAudioClipTranslationObject>
    {
        /// <summary>
        /// Audio clip translation
        /// </summary>
        AudioClipTranslationData AudioClipTranslation { get; }

        /// <summary>
        /// Translated audio clip
        /// </summary>
        AudioClip AudioClip { get; }

        /// <summary>
        /// Comment
        /// </summary>
        string Comment { get; }
    }
}
