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
    public class TranslatedAudioData
    {
        /// <summary>
        /// Audio clip
        /// </summary>
        [SerializeField]
        private AudioClip audioClip = default;

        /// <summary>
        /// Language
        /// </summary>
        [SerializeField]
        private SystemLanguage language = SystemLanguage.English;

        /// <summary>
        /// Text
        /// </summary>
        public AudioClip AudioClip
        {
            get
            {
                return audioClip;
            }
        }

        /// <summary>
        /// Language
        /// </summary>
        public SystemLanguage Language
        {
            get
            {
                return language;
            }
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
