using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// A class that describes audio clip translation data
    /// </summary>
    [Serializable]
    public class AudioClipTranslationData : IAudioClipTranslationData<AudioClipTranslationData, TranslatedAudioClipData>
    {
        /// <summary>
        /// Translated audio clips
        /// </summary>
        [SerializeField]
        private TranslatedAudioClipData[] audioClips = new TranslatedAudioClipData[] { TranslatedAudioClipData.defaultTranslatedAudioClip };

#if !UNITY_EDITOR
        /// <summary>
        /// System to audio clip lookup
        /// </summary>
        private Dictionary<SystemLanguage, AudioClip> systemLanguageToAudioClipLookup;
#endif

        /// <summary>
        /// Translated values
        /// </summary>
        public IReadOnlyList<TranslatedAudioClipData> Values => audioClips ??= new TranslatedAudioClipData[] { TranslatedAudioClipData.defaultTranslatedAudioClip };

        /// <summary>
        /// Translated value
        /// </summary>
        public AudioClip Value
        {
            get
            {
#if UNITY_EDITOR
                AudioClip ret = null;
                bool is_found = false;
                foreach (TranslatedAudioClipData translated_audio_clip in Values)
                {
                    if (translated_audio_clip.Language == Translator.CurrentLanguage)
                    {
                        ret = translated_audio_clip.Value;
                        is_found = true;
                        break;
                    }
                }
                return is_found ? ret : FallbackValue;
#else
                UpdateSystemLanguageToAudioClipLookup();
                return systemLanguageToAudioClipLookup.ContainsKey(Translator.CurrentLanguage) ? systemLanguageToAudioClipLookup[Translator.CurrentLanguage] : FallbackValue;
#endif
            }
        }

        /// <summary>
        /// Fallback value
        /// </summary>
        public AudioClip FallbackValue => (Values.Count > 0) ? Values[0].Value : null;

#if !UNITY_EDITOR
        /// <summary>
        /// Updates system language to string lookup
        /// </summary>
        private void UpdateSystemLanguageToAudioClipLookup()
        {
            if (systemLanguageToAudioClipLookup == null)
            {
                systemLanguageToAudioClipLookup = new Dictionary<SystemLanguage, AudioClip>();
                foreach (TranslatedAudioClipData translated_audio_clip in Values)
                {
                    if (systemLanguageToAudioClipLookup.ContainsKey(translated_audio_clip.Language))
                    {
                        systemLanguageToAudioClipLookup[translated_audio_clip.Language] = translated_audio_clip.Value;
                    }
                    else
                    {
                        systemLanguageToAudioClipLookup.Add(translated_audio_clip.Language, translated_audio_clip.Value);
                    }
                }
            }
        }
#endif

        /// <summary>
        /// Inserts translated value
        /// </summary>
        /// <param name="value">Translated value</param>
        /// <param name="language">Language</param>
        public void Insert(AudioClip value, SystemLanguage language)
        {
            bool is_appending = true;
            for (int i = 0; i < Values.Count; i++)
            {
                ref TranslatedAudioClipData translated_audio_clip = ref audioClips[i];
                if (translated_audio_clip.Language == language)
                {
                    translated_audio_clip = new TranslatedAudioClipData(value, language);
                    is_appending = false;
                    break;
                }
            }
            if (is_appending)
            {
                TranslatedAudioClipData[] audio_clips = new TranslatedAudioClipData[Values.Count + 1];
                Array.Copy(audioClips, 0, audio_clips, 0, audioClips.Length);
                audio_clips[audioClips.Length] = new TranslatedAudioClipData(value, language);
                audioClips = audio_clips;
            }
#if !UNITY_EDITOR
            systemLanguageToAudioClipLookup?.Clear();
            systemLanguageToAudioClipLookup = null;
#endif
        }

        /// <summary>
        /// Removes translated value with the specified language
        /// </summary>
        /// <param name="language">Language</param>
        public void Remove(SystemLanguage language)
        {
            if (audioClips != null)
            {
                int found_index = Array.FindIndex(audioClips, (element) => element.Language == language);
                if (found_index >= 0)
                {
                    for (int index = found_index + 1; index < audioClips.Length; index++)
                    {
                        audioClips[index - 1] = audioClips[index];
                    }
                    Array.Resize(ref audioClips, audioClips.Length - 1);
#if !UNITY_EDITOR
                    systemLanguageToAudioClipLookup?.Remove(language);
#endif
                }
            }
        }

        /// <summary>
        /// Clears this translation
        /// </summary>
        public void Clear()
        {
            if (audioClips != null)
            {
                audioClips = Array.Empty<TranslatedAudioClipData>();
#if !UNITY_EDITOR
                systemLanguageToAudioClipLookup?.Clear();
#endif
            }
        }

        /// <summary>
        /// Is language contained in this translation data
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>"true" if language is contained in this translation data, otherwise "false"</returns>
        public bool IsLanguageContained(SystemLanguage language)
        {
#if UNITY_EDITOR
            bool ret = false;
            foreach (TranslatedAudioClipData translated_audio_clip in Values)
            {
                if (translated_audio_clip.Language == language)
                {
                    ret = translated_audio_clip.Value;
                    break;
                }
            }
            return ret;
#else
            UpdateSystemLanguageToAudioClipLookup();
            return systemLanguageToAudioClipLookup.ContainsKey(language);
#endif
        }

        /// <summary>
        /// Tries to get translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="result">Result</param>
        /// <returns>"true" if translated value is available, otherwise "false"</returns>
        public bool TryGetValue(SystemLanguage language, out AudioClip result)
        {
#if UNITY_EDITOR
            bool ret = false;
            result = null;
            foreach (TranslatedAudioClipData translated_audio_clip in Values)
            {
                if (translated_audio_clip.Language == language)
                {
                    result = translated_audio_clip.Value;
                    ret = true;
                    break;
                }
            }
            return ret;
#else
            UpdateSystemLanguageToAudioClipLookup();
            return systemLanguageToAudioClipLookup.TryGetValue(language, out result);
#endif
        }

        /// <summary>
        /// Gets translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Translated value</returns>
        public AudioClip GetValue(SystemLanguage language) => TryGetValue(language, out AudioClip ret) ? ret : null;

        /// <summary>
        /// Compares this audio clip translation data to another audio clip translation data
        /// </summary>
        /// <param name="other">Other audio clip translation data</param>
        /// <returns>Comparison result</returns>
        public int CompareTo(AudioClipTranslationData other) => (other == null) ? 1 : (Value ? Value.name : string.Empty).CompareTo(other.Value ? other.Value.name : string.Empty);
    }
}
