using System;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// A structure that describes translated audio clip data
    /// </summary>
    [Serializable]
    public struct TranslatedAudioClipData : ITranslatedAudioClipData
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
        /// Translated value
        /// </summary>
        public AudioClip Value => audioClip;

        /// <summary>
        /// Language
        /// </summary>
        public SystemLanguage Language => language;

        /// <summary>
        /// Constructs new translated audio clip data
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
        public override string ToString() => audioClip ? audioClip.name : string.Empty;
    }
}
