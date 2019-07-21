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
        private AudioTranslationData audioTranslation;

        /// <summary>
        /// Comment
        /// </summary>
        [TextArea]
        [SerializeField]
        private string comment;

        /// <summary>
        /// Audio translation
        /// </summary>
        public AudioTranslationData AudioTranslation
        {
            get
            {
                return audioTranslation;
            }
        }

        /// <summary>
        /// Audio clip
        /// </summary>
        public AudioClip AudioClip
        {
            get
            {
                return ((audioTranslation == null) ? null : audioTranslation.AudioClip);
            }
        }

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
    }
}
