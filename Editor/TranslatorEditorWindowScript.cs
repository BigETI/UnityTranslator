using System;
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
        /// Edit dictionary
        /// </summary>
        private Dictionary<int, string> editDictionary = new Dictionary<int, string>();

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
                    ret = false;
                    break;
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
        /// Mmissing translations
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
        /// On GUI
        /// </summary>
        private void OnGUI()
        {
            SystemLanguage to_system_language = (SystemLanguage)(EditorGUILayout.EnumPopup("Translate to system language", toSystemLanguage));
            if (toSystemLanguage != to_system_language)
            {
                toSystemLanguage = to_system_language;
                missingTranslations = MissingTranslations;
            }
            GUILayout.Space(21.0f);
            if (GUILayout.Button("Copy translation form to clipboard"))
            {
                StringBuilder sb = new StringBuilder("# Translation form\r\n\r\n## Description\r\nThis form is used to translate words into ");
                sb.Append(toSystemLanguage.ToString());
                sb.AppendLine(".");
                sb.AppendLine("\r\n\r\n## Words\r\n");
                missingTranslations = MissingTranslations;
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
                missingTranslations = MissingTranslations;
            }
            if (GUILayout.Button("Apply changes"))
            {
                missingTranslations = MissingTranslations;
                foreach (TranslationObjectScript missing_translation in missingTranslations)
                {
                    int key = missing_translation.GetInstanceID();
                    if (editDictionary.ContainsKey(key))
                    {
                        missing_translation.Translation.AddText(new TranslatedTextData(editDictionary[key], toSystemLanguage));
                    }
                }
                editDictionary.Clear();
                missingTranslations = MissingTranslations;
            }
            GUILayout.Space(21.0f);
            if (missingTranslations == null)
            {
                missingTranslations = MissingTranslations;
            }
            foreach (TranslationObjectScript missing_translation in missingTranslations)
            {
                EditorGUILayout.ObjectField(missing_translation, typeof(TranslationObjectScript), true);
                int key = missing_translation.GetInstanceID();
                string value = (editDictionary.ContainsKey(key) ? editDictionary[key] : string.Empty);
                string input = EditorGUILayout.TextArea(value);
                if (input.Length > 0)
                {
                    if (editDictionary.ContainsKey(key))
                    {
                        editDictionary[key] = input;
                    }
                    else
                    {
                        editDictionary.Add(key, input);
                    }
                }
                else
                {
                    editDictionary.Remove(key);
                }
            }
        }
    }
}
