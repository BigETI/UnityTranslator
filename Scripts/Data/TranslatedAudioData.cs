using System;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// Translated audio data class
    /// </summary>
    [Serializable]
    public struct TranslatedAudioData
    {
        /// <summary>
        /// Default translated audio
        /// </summary>
        public static readonly TranslatedAudioData defaultTranslatedAudio = new TranslatedAudioData(null, SystemLanguage.English);

        /// <summary>
        /// Audio clip
        /// </summary>
        [SerializeField]
        private AudioClip audioClip;

        /// <summary>
        /// Language
        /// </summary>
        [SerializeField]
        private SystemLanguage language;

        /// <summary>
        /// Text
        /// </summary>
        public AudioClip AudioClip => audioClip;

        /// <summary>
        /// Language
        /// </summary>
        public SystemLanguage Language => language;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="audioClip">Audio clip</param>
        /// <param name="language">Language</param>
        public TranslatedAudioData(AudioClip audioClip, SystemLanguage language)
        {
            this.audioClip = audioClip;
            this.language = language;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return ((audioClip == null) ? string.Empty : audioClip.name);
        }
    }
}
