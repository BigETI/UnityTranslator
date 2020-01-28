using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// Audio clip translation data interface
    /// </summary>
    public interface IAudioClipTranslationData : IComparable<IAudioClipTranslationData>
    {
        /// <summary>
        /// Trnslated audio clips
        /// </summary>
        IReadOnlyList<TranslatedAudioClipData> AudioClips { get; }

        /// <summary>
        /// Translated audio clip
        /// </summary>
        AudioClip AudioClip { get; }

        /// <summary>
        /// Add translated audio
        /// </summary>
        /// <param name="audioClip">Translated audio clip</param>
        void AddAudioClip(TranslatedAudioClipData audioClip);
    }
}
