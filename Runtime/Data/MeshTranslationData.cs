using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator data namespace
/// </summary>
namespace UnityTranslator.Data
{
    /// <summary>
    /// A class that describes mesh translation data
    /// </summary>
    [Serializable]
    public class MeshTranslationData : IMeshTranslationData<MeshTranslationData, TranslatedMeshData>
    {
        /// <summary>
        /// Translated meshes
        /// </summary>
        [SerializeField]
        private TranslatedMeshData[] meshes = new TranslatedMeshData[] { TranslatedMeshData.defaultTranslatedMesh };

#if !UNITY_EDITOR
        /// <summary>
        /// System language to mesh lookup
        /// </summary>
        private Dictionary<SystemLanguage, Mesh> systemLanguageToMeshLookup;
#endif

        /// <summary>
        /// Translated values
        /// </summary>
        public IReadOnlyList<TranslatedMeshData> Values => meshes ??= new TranslatedMeshData[] { TranslatedMeshData.defaultTranslatedMesh };

        /// <summary>
        /// Translated value
        /// </summary>
        public Mesh Value
        {
            get
            {
#if UNITY_EDITOR
                Mesh ret = null;
                bool is_found = false;
                foreach (TranslatedMeshData translated_mesh in Values)
                {
                    if (translated_mesh.Language == Translator.CurrentLanguage)
                    {
                        ret = translated_mesh.Value;
                        is_found = true;
                        break;
                    }
                }
                return is_found ? ret : FallbackValue;
#else
                UpdateSystemLanguageToMeshLookup();
                return systemLanguageToMeshLookup.TryGetValue(Translator.CurrentLanguage, out Mesh ret) ? ret : FallbackValue;
#endif
            }
        }

        /// <summary>
        /// Fallback value
        /// </summary>
        public Mesh FallbackValue => (Values.Count > 0) ? Values[0].Value : null;

#if !UNITY_EDITOR
        /// <summary>
        /// Updates system language to mesh lookup
        /// </summary>
        private void UpdateSystemLanguageToMeshLookup()
        {
            if (systemLanguageToMeshLookup == null)
            {
                systemLanguageToMeshLookup = new Dictionary<SystemLanguage, Mesh>();
                foreach (TranslatedMeshData translated_mesh in Values)
                {
                    if (systemLanguageToMeshLookup.ContainsKey(translated_mesh.Language))
                    {
                        systemLanguageToMeshLookup[translated_mesh.Language] = translated_mesh.Value;
                    }
                    else
                    {
                        systemLanguageToMeshLookup.Add(translated_mesh.Language, translated_mesh.Value);
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
        public void Insert(Mesh value, SystemLanguage language)
        {
            bool is_appending = true;
            for (int i = 0; i < Values.Count; i++)
            {
                ref TranslatedMeshData translated_mesh = ref meshes[i];
                if (translated_mesh.Language == language)
                {
                    translated_mesh = new TranslatedMeshData(value, language);
                    is_appending = false;
                    break;
                }
            }
            if (is_appending)
            {
                TranslatedMeshData[] meshes = new TranslatedMeshData[Values.Count + 1];
                Array.Copy(this.meshes, 0, meshes, 0, this.meshes.Length);
                meshes[this.meshes.Length] = new TranslatedMeshData(value, language);
                this.meshes = meshes;
            }
#if !UNITY_EDITOR
            systemLanguageToMeshLookup?.Clear();
            systemLanguageToMeshLookup = null;
#endif
        }

        /// <summary>
        /// Removes translated value with the specified language
        /// </summary>
        /// <param name="language">Language</param>
        public void Remove(SystemLanguage language)
        {
            if (meshes != null)
            {
                int found_index = Array.FindIndex(meshes, (element) => element.Language == language);
                if (found_index >= 0)
                {
                    for (int index = found_index + 1; index < meshes.Length; index++)
                    {
                        meshes[index - 1] = meshes[index];
                    }
                    Array.Resize(ref meshes, meshes.Length - 1);
#if !UNITY_EDITOR
                    systemLanguageToMeshLookup?.Remove(language);
#endif
                }
            }
        }

        /// <summary>
        /// Clears this translation data
        /// </summary>
        public void Clear()
        {
            if (meshes != null)
            {
                meshes = Array.Empty<TranslatedMeshData>();
#if !UNITY_EDITOR
                systemLanguageToMeshLookup?.Clear();
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
            foreach (TranslatedMeshData translated_mesh in Values)
            {
                if (translated_mesh.Language == language)
                {
                    ret = translated_mesh.Value;
                    break;
                }
            }
            return ret;
#else
            UpdateSystemLanguageToMeshLookup();
            return systemLanguageToMeshLookup.ContainsKey(language);
#endif
        }

        /// <summary>
        /// Tries to get translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <param name="result">Result</param>
        /// <returns>"true" if translated value is available, otherwise "false"</returns>
        public bool TryGetValue(SystemLanguage language, out Mesh result)
        {
#if UNITY_EDITOR
            bool ret = false;
            result = null;
            foreach (TranslatedMeshData translated_mesh in Values)
            {
                if (translated_mesh.Language == language)
                {
                    result = translated_mesh.Value;
                    ret = true;
                    break;
                }
            }
            return ret;
#else
            UpdateSystemLanguageToMeshLookup();
            return systemLanguageToMeshLookup.TryGetValue(language, out result);
#endif
        }

        /// <summary>
        /// Gets translated value from the specified language
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Translated value</returns>
        public Mesh GetValue(SystemLanguage language) => TryGetValue(language, out Mesh ret) ? ret : null;

        /// <summary>
        /// Compares this mesh translation data to another mesh translation data
        /// </summary>
        /// <param name="other">Other mesh translation data</param>
        /// <returns>Comparison result</returns>
        public int CompareTo(MeshTranslationData other) => (other == null) ? 1 : (Value ? Value.name : string.Empty).CompareTo(other.Value ? other.Value.name : string.Empty);
    }
}
