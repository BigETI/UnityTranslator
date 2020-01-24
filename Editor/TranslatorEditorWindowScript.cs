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
        /// Missing translations
        /// </summary>
        private IReadOnlyList<TranslationObjectScript> missingTranslations;

        /// <summary>
        /// Missing audio translations
        /// </summary>
        private IReadOnlyList<AudioTranslationObjectScript> missingAudioTranslations;

        /// <summary>
        /// Edit translation dictionary
        /// </summary>
        private Dictionary<int, string> editTranslationDictionary = new Dictionary<int, string>();

        /// <summary>
        /// Edit audio translation dictionary
        /// </summary>
        private Dictionary<int, AudioClip> editAudioTranslationDictionary = new Dictionary<int, AudioClip>();

        /// <summary>
        /// Is not translated
        /// </summary>
        /// <param name="translation">Translation</param>
        /// <returns>Result</returns>
        private bool IsNotTranslated(TranslationObjectScript translation)
        {
            bool ret = true;
            foreach (TranslatedTextData text in translation.Translation.Texts)
            {
                if (text.Language == toSystemLanguage)
                {
                    if (text.Text.Trim().Length > 0)
                    {
                        ret = false;
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Is not translated
        /// </summary>
        /// <param name="audioTranslation">Audio translation</param>
        /// <returns>Result</returns>
        private bool IsNotTranslated(AudioTranslationObjectScript audioTranslation)
        {
            bool ret = true;
            foreach (TranslatedAudioData audio in audioTranslation.AudioTranslation.Audios)
            {
                if (audio.Language == toSystemLanguage)
                {
                    if (audio.AudioClip != null)
                    {
                        ret = false;
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Show window
        /// </summary>
        [MenuItem("Window/Translator")]
        public static void ShowWindow()
        {
            GetWindow<TranslatorEditorWindowScript>("Translator");
        }

        /// <summary>
        /// Missing translations
        /// </summary>
        private IReadOnlyList<TranslationObjectScript> MissingTranslations
        {
            get
            {
                List<TranslationObjectScript> missing_translations = new List<TranslationObjectScript>();
                string[] translation_guids = AssetDatabase.FindAssets("t:" + nameof(TranslationObjectScript));
                if (translation_guids != null)
                {
                    foreach (string translation_guid in translation_guids)
                    {
                        if (translation_guid != null)
                        {
                            TranslationObjectScript translation = AssetDatabase.LoadAssetAtPath<TranslationObjectScript>(AssetDatabase.GUIDToAssetPath(translation_guid));
                            if (translation != null)
                            {
                                if (IsNotTranslated(translation))
                                {
                                    missing_translations.Add(translation);
                                }
                            }
                        }
                    }
                }
                TranslationObjectScript[] ret = missing_translations.ToArray();
                missing_translations.Clear();
                return ret;
            }
        }

        /// <summary>
        /// Missing audio translations
        /// </summary>
        private IReadOnlyList<AudioTranslationObjectScript> MissingAudioTranslations
        {
            get
            {
                List<AudioTranslationObjectScript> missing_audio_translations = new List<AudioTranslationObjectScript>();
                string[] audio_translation_guids = AssetDatabase.FindAssets("t:" + nameof(AudioTranslationObjectScript));
                if (audio_translation_guids != null)
                {
                    foreach (string audio_translation_guid in audio_translation_guids)
                    {
                        if (audio_translation_guid != null)
                        {
                            AudioTranslationObjectScript audio_translation = AssetDatabase.LoadAssetAtPath<AudioTranslationObjectScript>(AssetDatabase.GUIDToAssetPath(audio_translation_guid));
                            if (audio_translation != null)
                            {
                                if (IsNotTranslated(audio_translation))
                                {
                                    missing_audio_translations.Add(audio_translation);
                                }
                            }
                        }
                    }
                }
                AudioTranslationObjectScript[] ret = missing_audio_translations.ToArray();
                missing_audio_translations.Clear();
                return ret;
            }
        }

        private void UpdateMissingTranslations()
        {
            missingTranslations = MissingTranslations;
            missingAudioTranslations = MissingAudioTranslations;
        }

        /// <summary>
        /// On GUI
        /// </summary>
        private void OnGUI()
        {
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
                foreach (TranslationObjectScript missing_translation in missingTranslations)
                {
                    if (IsNotTranslated(missing_translation))
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
                        foreach (TranslatedTextData text in missing_translation.Translation.Texts)
                        {
                            sb.Append("- In ");
                            sb.Append(text.Language.ToString());
                            sb.Append(": \"");
                            sb.Append(text.Text);
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
                foreach (TranslationObjectScript missing_translation in missingTranslations)
                {
                    int key = missing_translation.GetInstanceID();
                    if (editTranslationDictionary.ContainsKey(key))
                    {
                        missing_translation.Translation.AddText(new TranslatedTextData(editTranslationDictionary[key], toSystemLanguage));
                    }
                }
                foreach (AudioTranslationObjectScript missing_audio_translation in missingAudioTranslations)
                {
                    int key = missing_audio_translation.GetInstanceID();
                    if (editTranslationDictionary.ContainsKey(key))
                    {
                        missing_audio_translation.AudioTranslation.AddAudioClip(new TranslatedAudioData(editAudioTranslationDictionary[key], toSystemLanguage));
                    }
                }
                editTranslationDictionary.Clear();
                editAudioTranslationDictionary.Clear();
                UpdateMissingTranslations();
            }
            if (missingTranslations == null)
            {
                UpdateMissingTranslations();
            }
            foreach (TranslationObjectScript missing_translation in missingTranslations)
            {
                GUILayout.Space(21.0f);
                EditorGUILayout.ObjectField(missing_translation, typeof(TranslationObjectScript), true);
                int key = missing_translation.GetInstanceID();
                string value = (editTranslationDictionary.ContainsKey(key) ? editTranslationDictionary[key] : string.Empty);
                string input = EditorGUILayout.TextArea(value);
                if (input.Length > 0)
                {
                    if (editTranslationDictionary.ContainsKey(key))
                    {
                        editTranslationDictionary[key] = input;
                    }
                    else
                    {
                        editTranslationDictionary.Add(key, input);
                    }
                }
                else
                {
                    editTranslationDictionary.Remove(key);
                }
            }
            foreach (AudioTranslationObjectScript missing_audio_translation in missingAudioTranslations)
            {
                GUILayout.Space(21.0f);
                EditorGUILayout.ObjectField(missing_audio_translation, typeof(AudioTranslationObjectScript), true);
                int key = missing_audio_translation.GetInstanceID();
                AudioClip value = (editAudioTranslationDictionary.ContainsKey(key) ? editAudioTranslationDictionary[key] : null);
                AudioClip input = (AudioClip)(EditorGUILayout.ObjectField(value, typeof(AudioClip), true));
                if (input != null)
                {
                    if (editAudioTranslationDictionary.ContainsKey(key))
                    {
                        editAudioTranslationDictionary[key] = input;
                    }
                    else
                    {
                        editAudioTranslationDictionary.Add(key, input);
                    }
                }
                else
                {
                    editAudioTranslationDictionary.Remove(key);
                }
            }
        }
    }
}
