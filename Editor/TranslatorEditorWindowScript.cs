using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityTranslator.Data;
using UnityTranslator.Objects;

/// <summary>
/// Unity translator editor namespace
/// </summary>
namespace UnityTranslator.Editor
{
    /// <summary>
    /// Translator editor window script class
    /// </summary>
    public class TranslatorEditorWindowScript : EditorWindow
    {
        /// <summary>
        /// To system language
        /// </summary>
        private SystemLanguage toSystemLanguage = SystemLanguage.English;

        /// <summary>
        /// Scroll position
        /// </summary>
        private Vector2 scrollPosition = Vector2.zero;

        /// <summary>
        /// Missing string translations
        /// </summary>
        private IReadOnlyList<StringTranslationObjectScript> missingStringTranslations;

        /// <summary>
        /// Missing audio clip translations
        /// </summary>
        private IReadOnlyList<AudioClipTranslationObjectScript> missingAudioClipTranslations;

        /// <summary>
        /// Missing texture translations
        /// </summary>
        private IReadOnlyList<TextureTranslationObjectScript> missingTextureTranslations;

        /// <summary>
        /// Missing sprite translations
        /// </summary>
        private IReadOnlyList<SpriteTranslationObjectScript> missingSpriteTranslations;

        /// <summary>
        /// Edit string translation dictionary
        /// </summary>
        private Dictionary<int, string> editStringTranslationDictionary = new Dictionary<int, string>();

        /// <summary>
        /// Edit audio clip translation dictionary
        /// </summary>
        private Dictionary<int, AudioClip> editAudioClipTranslationDictionary = new Dictionary<int, AudioClip>();

        /// <summary>
        /// Edit texture translation dictionary
        /// </summary>
        private Dictionary<int, Texture> editTextureTranslationDictionary = new Dictionary<int, Texture>();

        /// <summary>
        /// Edit sprite translation dictionary
        /// </summary>
        private Dictionary<int, Sprite> editSpriteTranslationDictionary = new Dictionary<int, Sprite>();

        /// <summary>
        /// Show window
        /// </summary>
        [MenuItem("Window/Translator")]
        public static void ShowWindow()
        {
            GetWindow<TranslatorEditorWindowScript>("Translator");
        }

        /// <summary>
        /// Get missing translations
        /// </summary>
        /// <typeparam name="T">Translation object type</typeparam>
        /// <returns>Missing translations</returns>
        private IReadOnlyList<T> GetMissingTranslations<T>() where T : Object, ITranslationObject
        {
            List<T> missing_translations = new List<T>();
            string[] translation_guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            if (translation_guids != null)
            {
                foreach (string translation_guid in translation_guids)
                {
                    if (translation_guid != null)
                    {
                        T translation = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(translation_guid));
                        if (translation != null)
                        {
                            if (!(translation.ContainsLanguage(toSystemLanguage)))
                            {
                                missing_translations.Add(translation);
                            }
                        }
                    }
                }
            }
            T[] ret = missing_translations.ToArray();
            missing_translations.Clear();
            return ret;
        }

        /// <summary>
        /// Update missing translations
        /// </summary>
        private void UpdateMissingTranslations()
        {
            missingStringTranslations = GetMissingTranslations<StringTranslationObjectScript>();
            missingAudioClipTranslations = GetMissingTranslations<AudioClipTranslationObjectScript>();
            missingTextureTranslations = GetMissingTranslations<TextureTranslationObjectScript>();
            missingSpriteTranslations = GetMissingTranslations<SpriteTranslationObjectScript>();
        }

        /// <summary>
        /// On GUI
        /// </summary>
        private void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            SystemLanguage to_system_language = (SystemLanguage)(EditorGUILayout.EnumPopup("Translate to system language", toSystemLanguage));
            if (toSystemLanguage != to_system_language)
            {
                toSystemLanguage = to_system_language;
                UpdateMissingTranslations();
            }
            GUILayout.Space(21.0f);
            if (GUILayout.Button("Copy translation form to clipboard"))
            {
                StringBuilder sb = new StringBuilder("# Translation form\r\n\r\n## Description\r\nThis form is used to translate words into ");
                sb.Append(toSystemLanguage.ToString());
                sb.AppendLine(".");
                sb.AppendLine("\r\n\r\n## Words\r\n");
                UpdateMissingTranslations();
                foreach (StringTranslationObjectScript missing_translation in missingStringTranslations)
                {
                    if (!(missing_translation.ContainsLanguage(toSystemLanguage)))
                    {
                        sb.Append("### `");
                        sb.Append(missing_translation.name);
                        sb.AppendLine("`");
                        if (missing_translation.Comment != null)
                        {
                            if (missing_translation.Comment.Trim().Length > 0)
                            {
                                sb.Append("Comment: ");
                                sb.AppendLine(missing_translation.Comment);
                            }
                        }
                        foreach (TranslatedStringData translated_string in missing_translation.StringTranslation.Strings)
                        {
                            sb.Append("- In ");
                            sb.Append(translated_string.Language.ToString());
                            sb.Append(": \"");
                            sb.Append(translated_string.String);
                            sb.AppendLine("\"");
                        }
                        sb.Append("\r\nWhat is it called in ");
                        sb.Append(toSystemLanguage.ToString());
                        sb.AppendLine("?: \r\n\r\n");
                    }
                }
                sb.Append("Thank your for translating into ");
                sb.Append(toSystemLanguage.ToString());
                sb.AppendLine("!");
                GUIUtility.systemCopyBuffer = sb.ToString();
            }
            if (GUILayout.Button("Update view"))
            {
                UpdateMissingTranslations();
            }
            if (GUILayout.Button("Apply changes"))
            {
                UpdateMissingTranslations();
                foreach (StringTranslationObjectScript missing_translation in missingStringTranslations)
                {
                    int key = missing_translation.GetInstanceID();
                    if (editStringTranslationDictionary.ContainsKey(key))
                    {
                        missing_translation.StringTranslation.AddString(new TranslatedStringData(editStringTranslationDictionary[key], toSystemLanguage));
                    }
                }
                foreach (AudioClipTranslationObjectScript missing_audio_clip_translation in missingAudioClipTranslations)
                {
                    int key = missing_audio_clip_translation.GetInstanceID();
                    if (editAudioClipTranslationDictionary.ContainsKey(key))
                    {
                        missing_audio_clip_translation.AudioClipTranslation.AddAudioClip(new TranslatedAudioClipData(editAudioClipTranslationDictionary[key], toSystemLanguage));
                    }
                }
                foreach (TextureTranslationObjectScript missing_texture_translation in missingTextureTranslations)
                {
                    int key = missing_texture_translation.GetInstanceID();
                    if (editTextureTranslationDictionary.ContainsKey(key))
                    {
                        missing_texture_translation.TextureTranslation.AddTexture(new TranslatedTextureData(editTextureTranslationDictionary[key], toSystemLanguage));
                    }
                }
                foreach (SpriteTranslationObjectScript missing_sprite_translation in missingSpriteTranslations)
                {
                    int key = missing_sprite_translation.GetInstanceID();
                    if (editSpriteTranslationDictionary.ContainsKey(key))
                    {
                        missing_sprite_translation.SpriteTranslation.AddSprite(new TranslatedSpriteData(editSpriteTranslationDictionary[key], toSystemLanguage));
                    }
                }
                editStringTranslationDictionary.Clear();
                editAudioClipTranslationDictionary.Clear();
                editTextureTranslationDictionary.Clear();
                editSpriteTranslationDictionary.Clear();
                UpdateMissingTranslations();
            }
            if (missingStringTranslations == null)
            {
                UpdateMissingTranslations();
            }
            foreach (StringTranslationObjectScript missing_translation in missingStringTranslations)
            {
                GUILayout.Space(21.0f);
                EditorGUILayout.ObjectField(missing_translation, typeof(StringTranslationObjectScript), true);
                int key = missing_translation.GetInstanceID();
                string value = (editStringTranslationDictionary.ContainsKey(key) ? editStringTranslationDictionary[key] : string.Empty);
                string input = EditorGUILayout.TextArea(value);
                if (input.Length > 0)
                {
                    if (editStringTranslationDictionary.ContainsKey(key))
                    {
                        editStringTranslationDictionary[key] = input;
                    }
                    else
                    {
                        editStringTranslationDictionary.Add(key, input);
                    }
                }
                else
                {
                    editStringTranslationDictionary.Remove(key);
                }
            }
            foreach (AudioClipTranslationObjectScript missing_audio_clip_translation in missingAudioClipTranslations)
            {
                GUILayout.Space(21.0f);
                EditorGUILayout.ObjectField(missing_audio_clip_translation, typeof(AudioClipTranslationObjectScript), true);
                int key = missing_audio_clip_translation.GetInstanceID();
                AudioClip value = (editAudioClipTranslationDictionary.ContainsKey(key) ? editAudioClipTranslationDictionary[key] : null);
                AudioClip input = (AudioClip)(EditorGUILayout.ObjectField(value, typeof(AudioClip), true));
                if (input != null)
                {
                    if (editAudioClipTranslationDictionary.ContainsKey(key))
                    {
                        editAudioClipTranslationDictionary[key] = input;
                    }
                    else
                    {
                        editAudioClipTranslationDictionary.Add(key, input);
                    }
                }
                else
                {
                    editAudioClipTranslationDictionary.Remove(key);
                }
            }
            foreach (TextureTranslationObjectScript missing_texture_translation in missingTextureTranslations)
            {
                GUILayout.Space(21.0f);
                EditorGUILayout.ObjectField(missing_texture_translation, typeof(TextureTranslationObjectScript), true);
                int key = missing_texture_translation.GetInstanceID();
                Texture value = (editTextureTranslationDictionary.ContainsKey(key) ? editTextureTranslationDictionary[key] : null);
                Texture input = (Texture)(EditorGUILayout.ObjectField(value, typeof(Texture), true));
                if (input != null)
                {
                    if (editTextureTranslationDictionary.ContainsKey(key))
                    {
                        editTextureTranslationDictionary[key] = input;
                    }
                    else
                    {
                        editTextureTranslationDictionary.Add(key, input);
                    }
                }
                else
                {
                    editTextureTranslationDictionary.Remove(key);
                }
            }
            foreach (SpriteTranslationObjectScript missing_sprite_translation in missingSpriteTranslations)
            {
                GUILayout.Space(21.0f);
                EditorGUILayout.ObjectField(missing_sprite_translation, typeof(SpriteTranslationObjectScript), true);
                int key = missing_sprite_translation.GetInstanceID();
                Sprite value = (editSpriteTranslationDictionary.ContainsKey(key) ? editSpriteTranslationDictionary[key] : null);
                Sprite input = (Sprite)(EditorGUILayout.ObjectField(value, typeof(Sprite), true));
                if (input != null)
                {
                    if (editSpriteTranslationDictionary.ContainsKey(key))
                    {
                        editSpriteTranslationDictionary[key] = input;
                    }
                    else
                    {
                        editSpriteTranslationDictionary.Add(key, input);
                    }
                }
                else
                {
                    editSpriteTranslationDictionary.Remove(key);
                }
            }
            GUILayout.EndScrollView();
        }
    }
}
