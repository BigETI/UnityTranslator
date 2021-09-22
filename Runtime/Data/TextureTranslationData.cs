using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// A class that describes texture translation data
    /// </summary>
    [Serializable]
    public class TextureTranslationData : ITextureTranslationData<TextureTranslationData, TranslatedTextureData>
    {
        /// <summary>
        /// Translated textures
        /// </summary>
        [SerializeField]
        private TranslatedTextureData[] textures = new TranslatedTextureData[] { TranslatedTextureData.defaultTranslatedTexture };

#if !UNITY_EDITOR
        /// <summary>
        /// System language to texture lookup
        /// </summary>
        private Dictionary<SystemLanguage, Texture> systemLanguageToTextureLookup;
#endif

        /// <summary>
        /// Translated values
        /// </summary>
        public IReadOnlyList<TranslatedTextureData> Values => textures ??= new TranslatedTextureData[] { TranslatedTextureData.defaultTranslatedTexture };

        /// <summary>
        /// Translated value
        /// </summary>
        public Texture Value
        {
            get
            {
#if UNITY_EDITOR
                Texture ret = null;
                bool is_found = false;
                foreach (TranslatedTextureData translated_texture in Values)
                {
                    if (translated_texture.Language == Translator.CurrentLanguage)
                    {
                        ret = translated_texture.Value;
                        is_found = true;
                        break;
                    }
                }
                return is_found ? ret : FallbackValue;
#else
                if (systemLanguageToTextureLookup == null)
                {
                    systemLanguageToTextureLookup = new Dictionary<SystemLanguage, Texture>();
                    foreach (TranslatedTextureData translated_texture in Values)
                    {
                        if (systemLanguageToTextureLookup.ContainsKey(translated_texture.Language))
                        {
                            systemLanguageToTextureLookup[translated_texture.Language] = translated_texture.Value;
                        }
                        else
                        {
                            systemLanguageToTextureLookup.Add(translated_texture.Language, translated_texture.Value);
                        }
                    }
                }
                return systemLanguageToTextureLookup.ContainsKey(Translator.CurrentLanguage) ? systemLanguageToTextureLookup[Translator.CurrentLanguage] : FallbackValue;
#endif
            }
        }

        /// <summary>
        /// Fallback value
        /// </summary>
        public Texture FallbackValue => (Values.Count > 0) ? Values[0].Value : null;

#if !UNITY_EDITOR
        /// <summary>
        /// Updates system language to sprite lookup
        /// </summary>
        private void UpdateSystemLanguageToTextureLookup()
        {
            if (systemLanguageToTextureLookup == null)
            {
                systemLanguageToTextureLookup = new Dictionary<SystemLanguage, Texture>();
                foreach (TranslatedTextureData translated_texture in Values)
                {
                    if (systemLanguageToTextureLookup.ContainsKey(translated_texture.Language))
                    {
                        systemLanguageToTextureLookup[translated_texture.Language] = translated_texture.Value;
                    }
                    else
                    {
                        systemLanguageToTextureLookup.Add(translated_texture.Language, translated_texture.Value);
                    }
                }
            }
        }
#endif

        /// <summary>
        /// Insert translated value
        /// </summary>
        /// <param name="value">Translated value</param>
        /// <param name="language">Language</param>
        public void Insert(Texture value, SystemLanguage language)
        {
            bool is_appending = true;
            for (int i = 0; i < Values.Count; i++)
            {
                ref TranslatedTextureData translated_texture = ref textures[i];
                if (translated_texture.Language == language)
                {
                    translated_texture = new TranslatedTextureData(value, language);
                    is_appending = false;
                    break;
                }
            }
            if (is_appending)
            {
                TranslatedTextureData[] textures = new TranslatedTextureData[Values.Count + 1];
                Array.Copy(this.textures, 0, textures, 0, this.textures.Length);
                textures[this.textures.Length] = new TranslatedTextureData(value, language);
                this.textures = textures;
            }
#if !UNITY_EDITOR
            systemLanguageToTextureLookup?.Clear();
            systemLanguageToTextureLookup = null;
#endif
        }

        /// <summary>
        /// Removes translated value with the specified language
        /// </summary>
        /// <param name="language">Language</param>
        public void Remove(SystemLanguage language)
        {
            if (textures != null)
            {
                int found_index = Array.FindIndex(textures, (element) => element.Language == language);
                if (found_index >= 0)
                {
                    for (int index = found_index + 1; index < textures.Length; index++)
                    {
                        textures[index - 1] = textures[index];
                    }
                    Array.Resize(ref textures, textures.Length - 1);
#if !UNITY_EDITOR
                    systemLanguageToTextureLookup?.Remove(language);
#endif
                }
            }
        }

        /// <summary>
        /// Clears this translation
        /// </summary>
        public void Clear()
        {
            if (textures != null)
            {
                textures = Array.Empty<TranslatedTextureData>();
#if !UNITY_EDITOR
                systemLanguageToTextureLookup?.Clear();
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
            foreach (TranslatedTextureData translated_texture in Values)
            {
                if (translated_texture.Language == language)
                {
                    ret = translated_texture.Value;
                    break;
                }
            }
            return ret;
#else
            UpdateSystemLanguageToTextureLookup();
            return systemLanguageToTextureLookup.ContainsKey(language);
#endif
        }

        /// <summary>
        /// Tries to get translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="result">Result</param>
        /// <returns>"true" if translated value is available, otherwise "false"</returns>
        public bool TryGetValue(SystemLanguage language, out Texture result)
        {
#if UNITY_EDITOR
            bool ret = false;
            result = null;
            foreach (TranslatedTextureData translated_sprite in Values)
            {
                if (translated_sprite.Language == language)
                {
                    result = translated_sprite.Value;
                    ret = true;
                    break;
                }
            }
            return ret;
#else
            UpdateSystemLanguageToTextureLookup();
            return systemLanguageToTextureLookup.TryGetValue(language, out result);
#endif
        }

        /// <summary>
        /// Gets translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Translated value</returns>
        public Texture GetValue(SystemLanguage language) => TryGetValue(language, out Texture ret) ? ret : null;

        /// <summary>
        /// Compares this texture translation data to another texture translation data
        /// </summary>
        /// <param name="other">Other texture translation data</param>
        /// <returns>Comparison result</returns>
        public int CompareTo(TextureTranslationData other) => (other == null) ? 1 : (Value ? Value.name : string.Empty).CompareTo(other.Value ? other.Value.name : string.Empty);
    }
}
