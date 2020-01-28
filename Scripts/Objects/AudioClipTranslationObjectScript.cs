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
    [CreateAssetMenu(fileName = "AudioClipTranslation", menuName = "Translator/Audio clip translation")]
    public class AudioClipTranslationObjectScript : ScriptableObject, IAudioClipTranslationObject
    {
        /// <summary>
        /// Audio clip translation
        /// </summary>
        [SerializeField]
        private AudioClipTranslationData audioClipTranslation = default;

        /// <summary>
        /// Comment
        /// </summary>
        [TextArea]
        [SerializeField]
        private string comment = default;

        /// <summary>
        /// Audio clip translation
        /// </summary>
        public AudioClipTranslationData AudioClipTranslation
        {
            get
            {
                if (audioClipTranslation == null)
                {
                    audioClipTranslation = new AudioClipTranslationData();
                }
                return audioClipTranslation;
            }
        }

        /// <summary>
        /// Translated audio clip
        /// </summary>
        public AudioClip AudioClip => AudioClipTranslation.AudioClip;

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
        /// Contains language
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>"true" if contains language, otherwise "false"</returns>
        public bool ContainsLanguage(SystemLanguage language)
        {
            bool ret = false;
            foreach (TranslatedAudioClipData translated_audio_clip in AudioClipTranslation.AudioClips)
            {
                if (translated_audio_clip.Language == language)
                {
                    if (translated_audio_clip.AudioClip != null)
                    {
                        ret = true;
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Compare to
        /// </summary>
        /// <param name="other">Other</param>
        /// <returns>Result</returns>
        public int CompareTo(IAudioClipTranslationObject other)
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
        public override string ToString() => AudioClipTranslation.ToString();
    }
}
