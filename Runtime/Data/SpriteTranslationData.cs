using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// A class that describes sprite translation data
    /// </summary>
    [Serializable]
    public class SpriteTranslationData : ISpriteTranslationData<SpriteTranslationData, TranslatedSpriteData>
    {
        /// <summary>
        /// Translated sprites
        /// </summary>
        [SerializeField]
        private TranslatedSpriteData[] sprites = new TranslatedSpriteData[] { TranslatedSpriteData.defaultTranslatedSprite };

#if !UNITY_EDITOR
        /// <summary>
        /// System language to sprite lookup
        /// </summary>
        private Dictionary<SystemLanguage, Sprite> systemLanguageToSpriteLookup;
#endif

        /// <summary>
        /// Translated values
        /// </summary>
        public IReadOnlyList<TranslatedSpriteData> Values => sprites ??= new TranslatedSpriteData[] { TranslatedSpriteData.defaultTranslatedSprite };

        /// <summary>
        /// Translated value
        /// </summary>
        public Sprite Value
        {
            get
            {
#if UNITY_EDITOR
                Sprite ret = null;
                bool is_found = false;
                foreach (TranslatedSpriteData translated_sprite in Values)
                {
                    if (translated_sprite.Language == Translator.CurrentLanguage)
                    {
                        ret = translated_sprite.Value;
                        is_found = true;
                        break;
                    }
                }
                return is_found ? ret : FallbackValue;
#else
                UpdateSystemLanguageToSpriteLookup();
                return systemLanguageToSpriteLookup.ContainsKey(Translator.CurrentLanguage) ? systemLanguageToSpriteLookup[Translator.CurrentLanguage] : FallbackValue;
#endif
            }
        }

        /// <summary>
        /// Fallback value
        /// </summary>
        public Sprite FallbackValue => (Values.Count > 0) ? Values[0].Value : null;

#if !UNITY_EDITOR
        /// <summary>
        /// Updates system language to sprite lookup
        /// </summary>
        private void UpdateSystemLanguageToSpriteLookup()
        {
            if (systemLanguageToSpriteLookup == null)
            {
                systemLanguageToSpriteLookup = new Dictionary<SystemLanguage, Sprite>();
                foreach (TranslatedSpriteData translated_sprite in Values)
                {
                    if (systemLanguageToSpriteLookup.ContainsKey(translated_sprite.Language))
                    {
                        systemLanguageToSpriteLookup[translated_sprite.Language] = translated_sprite.Value;
                    }
                    else
                    {
                        systemLanguageToSpriteLookup.Add(translated_sprite.Language, translated_sprite.Value);
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
        public void Insert(Sprite value, SystemLanguage language)
        {
            bool is_appending = true;
            for (int i = 0; i < Values.Count; i++)
            {
                ref TranslatedSpriteData translated_sprite = ref sprites[i];
                if (translated_sprite.Language == language)
                {
                    translated_sprite = new TranslatedSpriteData(value, language);
                    is_appending = false;
                    break;
                }
            }
            if (is_appending)
            {
                TranslatedSpriteData[] sprites = new TranslatedSpriteData[Values.Count + 1];
                Array.Copy(this.sprites, 0, sprites, 0, this.sprites.Length);
                sprites[this.sprites.Length] = new TranslatedSpriteData(value, language);
                this.sprites = sprites;
            }
#if !UNITY_EDITOR
            systemLanguageToSpriteLookup?.Clear();
            systemLanguageToSpriteLookup = null;
#endif
        }

        /// <summary>
        /// Removes translated value with the specified language
        /// </summary>
        /// <param name="language">Language</param>
        public void Remove(SystemLanguage language)
        {
            if (sprites != null)
            {
                int found_index = Array.FindIndex(sprites, (element) => element.Language == language);
                if (found_index >= 0)
                {
                    for (int index = found_index + 1; index < sprites.Length; index++)
                    {
                        sprites[index - 1] = sprites[index];
                    }
                    Array.Resize(ref sprites, sprites.Length - 1);
#if !UNITY_EDITOR
                    systemLanguageToSpriteLookup?.Remove(language);
#endif
                }
            }
        }

        /// <summary>
        /// Clears this translation
        /// </summary>
        public void Clear()
        {
            if (sprites != null)
            {
                sprites = Array.Empty<TranslatedSpriteData>();
#if !UNITY_EDITOR
                systemLanguageToSpriteLookup?.Clear();
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
            foreach (TranslatedSpriteData translated_sprite in Values)
            {
                if (translated_sprite.Language == language)
                {
                    ret = translated_sprite.Value;
                    break;
                }
            }
            return ret;
#else
            UpdateSystemLanguageToSpriteLookup();
            return systemLanguageToSpriteLookup.ContainsKey(language);
#endif
        }

        /// <summary>
        /// Tries to get translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="result">Result</param>
        /// <returns>"true" if translated value is available, otherwise "false"</returns>
        public bool TryGetValue(SystemLanguage language, out Sprite result)
        {
#if UNITY_EDITOR
            bool ret = false;
            result = null;
            foreach (TranslatedSpriteData translated_sprite in Values)
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
            UpdateSystemLanguageToSpriteLookup();
            return systemLanguageToSpriteLookup.TryGetValue(language, out result);
#endif
        }

        /// <summary>
        /// Gets translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Translated value</returns>
        public Sprite GetValue(SystemLanguage language) => TryGetValue(language, out Sprite ret) ? ret : null;

        /// <summary>
        /// Compares this sprite translation data to another sprite translation data
        /// </summary>
        /// <param name="other">Other sprite translation data</param>
        /// <returns>Comparison result</returns>
        public int CompareTo(SpriteTranslationData other) => (other == null) ? 1 : (Value ? Value.name : string.Empty).CompareTo(other.Value ? other.Value.name : string.Empty);
    }
}
