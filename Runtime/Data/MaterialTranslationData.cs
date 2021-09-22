using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// A class that describes material translation data
    /// </summary>
    [Serializable]
    public class MaterialTranslationData : IMaterialTranslationData<MaterialTranslationData, TranslatedMaterialData>
    {
        /// <summary>
        /// Translated materials
        /// </summary>
        [SerializeField]
        private TranslatedMaterialData[] materials = new TranslatedMaterialData[] { TranslatedMaterialData.defaultTranslatedMaterial };

#if !UNITY_EDITOR
        /// <summary>
        /// System language to material lookup
        /// </summary>
        private Dictionary<SystemLanguage, Material> systemLanguageToMaterialLookup;
#endif

        /// <summary>
        /// Translated values
        /// </summary>
        public IReadOnlyList<TranslatedMaterialData> Values => materials ??= new TranslatedMaterialData[] { TranslatedMaterialData.defaultTranslatedMaterial };

        /// <summary>
        /// Translated value
        /// </summary>
        public Material Value
        {
            get
            {
#if UNITY_EDITOR
                Material ret = null;
                bool is_found = false;
                foreach (TranslatedMaterialData translated_material in Values)
                {
                    if (translated_material.Language == Translator.CurrentLanguage)
                    {
                        ret = translated_material.Value;
                        is_found = true;
                        break;
                    }
                }
                return is_found ? ret : FallbackValue;
#else
                UpdateSystemLanguageToMaterialLookup();
                return systemLanguageToMaterialLookup.TryGetValue(Translator.CurrentLanguage, out Material ret) ? ret : FallbackValue;
#endif
            }
        }

        /// <summary>
        /// Fallback value
        /// </summary>
        public Material FallbackValue => (Values.Count > 0) ? Values[0].Value : null;

#if !UNITY_EDITOR
        /// <summary>
        /// Updates system language to material lookup
        /// </summary>
        private void UpdateSystemLanguageToMaterialLookup()
        {
            if (systemLanguageToMaterialLookup == null)
            {
                systemLanguageToMaterialLookup = new Dictionary<SystemLanguage, Material>();
                foreach (TranslatedMaterialData translated_material in Values)
                {
                    if (systemLanguageToMaterialLookup.ContainsKey(translated_material.Language))
                    {
                        systemLanguageToMaterialLookup[translated_material.Language] = translated_material.Value;
                    }
                    else
                    {
                        systemLanguageToMaterialLookup.Add(translated_material.Language, translated_material.Value);
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
        public void Insert(Material value, SystemLanguage language)
        {
            bool is_appending = true;
            for (int i = 0; i < Values.Count; i++)
            {
                ref TranslatedMaterialData translated_material = ref materials[i];
                if (translated_material.Language == language)
                {
                    translated_material = new TranslatedMaterialData(value, language);
                    is_appending = false;
                    break;
                }
            }
            if (is_appending)
            {
                TranslatedMaterialData[] materials = new TranslatedMaterialData[Values.Count + 1];
                Array.Copy(this.materials, 0, materials, 0, this.materials.Length);
                materials[this.materials.Length] = new TranslatedMaterialData(value, language);
                this.materials = materials;
            }
#if !UNITY_EDITOR
            systemLanguageToMaterialLookup?.Clear();
            systemLanguageToMaterialLookup = null;
#endif
        }

        /// <summary>
        /// Removes translated value with the specified language
        /// </summary>
        /// <param name="language">Language</param>
        public void Remove(SystemLanguage language)
        {
            if (materials != null)
            {
                int found_index = Array.FindIndex(materials, (element) => element.Language == language);
                if (found_index >= 0)
                {
                    for (int index = found_index + 1; index < materials.Length; index++)
                    {
                        materials[index - 1] = materials[index];
                    }
                    Array.Resize(ref materials, materials.Length - 1);
#if !UNITY_EDITOR
                    systemLanguageToMaterialLookup?.Remove(language);
#endif
                }
            }
        }

        /// <summary>
        /// Clears this translation data
        /// </summary>
        public void Clear()
        {
            if (materials != null)
            {
                materials = Array.Empty<TranslatedMaterialData>();
#if !UNITY_EDITOR
                systemLanguageToMaterialLookup?.Clear();
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
            foreach (TranslatedMaterialData translated_material in Values)
            {
                if (translated_material.Language == language)
                {
                    ret = translated_material.Value;
                    break;
                }
            }
            return ret;
#else
            UpdateSystemLanguageToMaterialLookup();
            return systemLanguageToMaterialLookup.ContainsKey(language);
#endif
        }

        /// <summary>
        /// Tries to get translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="result">Result</param>
        /// <returns>"true" if translated value is available, otherwise "false"</returns>
        public bool TryGetValue(SystemLanguage language, out Material result)
        {
#if UNITY_EDITOR
            bool ret = false;
            result = null;
            foreach (TranslatedMaterialData translated_material in Values)
            {
                if (translated_material.Language == language)
                {
                    result = translated_material.Value;
                    ret = true;
                    break;
                }
            }
            return ret;
#else
            UpdateSystemLanguageToMaterialLookup();
            return systemLanguageToMaterialLookup.TryGetValue(language, out result);
#endif
        }

        /// <summary>
        /// Gets translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Translated value</returns>
        public Material GetValue(SystemLanguage language) => TryGetValue(language, out Material ret) ? ret : null;

        /// <summary>
        /// Compares this material translation data to another material translation data
        /// </summary>
        /// <param name="other">Other material translation data</param>
        /// <returns>Comparison result</returns>
        public int CompareTo(MaterialTranslationData other) => (other == null) ? 1 : (Value ? Value.name : string.Empty).CompareTo(other.Value ? other.Value.name : string.Empty);
    }
}
