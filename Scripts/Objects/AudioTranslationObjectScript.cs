using System;
using UnityEngine;
using UnityTranslator.Data;

/// <summary>
/// Unity translator objects namespace
/// </summary>
namespace UnityTranslator.Objects
{
    /// <summary>
    /// Audio translation object script class
    /// </summary>
    [CreateAssetMenu(fileName = "AudioTranslation", menuName = "Translator/Audio translation")]
    public class AudioTranslationObjectScript : ScriptableObject, IComparable<AudioTranslationObjectScript>
    {
        /// <summary>
        /// Audio translation
        /// </summary>
        [SerializeField]
        private AudioTranslationData audioTranslation = default;

        /// <summary>
        /// Comment
        /// </summary>
        [TextArea]
        [SerializeField]
        private string comment = default;

        /// <summary>
        /// Audio translation
        /// </summary>
        public AudioTranslationData AudioTranslation
        {
            get
            {
                if (audioTranslation == null)
                {
                    audioTranslation = new AudioTranslationData();
                }
                return audioTranslation;
            }
        }

        /// <summary>
        /// Translated audio clip
        /// </summary>
        public AudioClip AudioClip => AudioTranslation.AudioClip;

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment
        {
            get
            {
                if (comment == null)
                {
                    comment = string.Empty;
                }
                return comment;
            }
        }

        /// <summary>
        /// Compare to
        /// </summary>
        /// <param name="other">Other</param>
        /// <returns>Result</returns>
        public int CompareTo(AudioTranslationObjectScript other)
        {
            int ret = 1;
            if (other != null)
            {
                ret = name.CompareTo(other.name);
            }
            return ret;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString() => AudioTranslation.ToString();
    }
}
