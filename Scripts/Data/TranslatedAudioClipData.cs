using System;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// Translated audio clip data class
    /// </summary>
    [Serializable]
    public struct TranslatedAudioClipData
    {
        /// <summary>
        /// Default translated audio clip
        /// </summary>
        public static readonly TranslatedAudioClipData defaultTranslatedAudioClip = new TranslatedAudioClipData(null, SystemLanguage.English);

        /// <summary>
        /// Translated audio clip
        /// </summary>
        [SerializeField]
        private AudioClip audioClip;

        /// <summary>
        /// Language
        /// </summary>
        [SerializeField]
        private SystemLanguage language;

        /// <summary>
        /// Translated audio clip
        /// </summary>
        public AudioClip AudioClip => audioClip;

        /// <summary>
        /// Language
        /// </summary>
        public SystemLanguage Language => language;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="audioClip">Translated audio clip</param>
        /// <param name="language">Language</param>
        public TranslatedAudioClipData(AudioClip audioClip, SystemLanguage language)
        {
            this.audioClip = audioClip;
            this.language = language;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => ((audioClip == null) ? string.Empty : audioClip.name);
    }
}
