using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Unity translator editor editor windows namespace
/// </summary>
namespace UnityTranslatorEditor.EditorWindows
{
    /// <summary>
    /// A class that describes a XLIFF importer editor window script
    /// </summary>
    public class XLIFFImporterEditorWindowScript : EditorWindow
    {
        /// <summary>
        /// String translations
        /// </summary>
        private readonly Dictionary<string, StringTranslationObjectScript> stringTranslations = new Dictionary<string, StringTranslationObjectScript>();

        /// <summary>
        /// Language import order
        /// </summary>
        private readonly List<SystemLanguage> languageImportOrder = new List<SystemLanguage>();

        /// <summary>
        /// Modified languages
        /// </summary>
        private readonly Dictionary<SystemLanguage, Dictionary<string, (bool IsSelected, string ModifiedTranslation, string ModifiedComment, SystemLanguage PreviewLanguage)>> modifiedLanguages = new Dictionary<SystemLanguage, Dictionary<string, (bool IsSelected, string ModifiedTranslation, string ModifiedComment, SystemLanguage PreviewLanguage)>>();

        /// <summary>
        /// XLIFF
        /// </summary>
        private IXLIFF xliff;

        /// <summary>
        /// Translations tokenized search field
        /// </summary>
        private TokenizedSearchField translationsTokenizedSearchField;

        /// <summary>
        /// Scroll position
        /// </summary>
        private Vector2 scrollPosition = Vector2.zero;

        /// <summary>
        /// XLIFF
        /// </summary>
        public IXLIFF XLIFF
        {
            get => xliff;
            set
            {
                if (xliff != value)
                {
                    stringTranslations.Clear();
                    foreach (Dictionary<string, (bool IsSelected, string ModifiedTranslation, string ModifiedComment, SystemLanguage PreviewLanguage)> modified_translations in modifiedLanguages.Values)
                    {
                        modified_translations.Clear();
                    }
                    languageImportOrder.Clear();
                    modifiedLanguages.Clear();
                    xliff = value;
                }
            }
        }

        /// <summary>
        /// Tries to get string translation
        /// </summary>
        /// <param name="assetPath">Asset path</param>
        /// <param name="stringTranslation">String translation</param>
        /// <returns>"true" if string translation is available, otherwise "false"</returns>
        private bool TryGetStringTranslation(string assetPath, out StringTranslationObjectScript stringTranslation)
        {
            if (!stringTranslations.TryGetValue(assetPath, out stringTranslation))
            {
                stringTranslation = AssetDatabase.LoadAssetAtPath<StringTranslationObjectScript>(assetPath);
                if (stringTranslation)
                {
                    stringTranslations.Add(assetPath, stringTranslation);
                }
            }
            return stringTranslation;
        }

        /// <summary>
        /// Gets invoked when editor window gets enabled
        /// </summary>
        protected virtual void OnEnable() => translationsTokenizedSearchField = new TokenizedSearchField();

        /// <summary>
        /// Gets invoked when GUI needs to be drawn
        /// </summary>
        protected virtual void OnGUI()
        {
            float input_width = Screen.width * 0.5f;
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            if (xliff == null)
            {
                GUILayout.Label("Nothing to see here.");
                if (GUILayout.Button("Close window"))
                {
                    Close();
                }
            }
            else
            {
                foreach (SystemLanguage language in xliff.SupportedLanguages)
                {
                    if (!languageImportOrder.Contains(language))
                    {
                        if (language == SystemLanguage.English)
                        {
                            languageImportOrder.Insert(0, language);
                        }
                        else
                        {
                            languageImportOrder.Add(language);
                        }
                    }
                }
                GUILayout.Box(string.Empty, GUILayout.Width(Screen.width), GUILayout.Height(3.0f));
                if (languageImportOrder.Count > 1)
                {
                    GUILayout.Label("Language import order:");
                    for (int language_index = 0; language_index < languageImportOrder.Count; language_index++)
                    {
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button((language_index > 0) ? "↑" : string.Empty, GUILayout.Width(20.0f)) && (language_index > 0))
                        {
                            SystemLanguage language = languageImportOrder[language_index];
                            languageImportOrder[language_index] = languageImportOrder[language_index - 1];
                            languageImportOrder[language_index - 1] = language;
                        }
                        if (GUILayout.Button((language_index < (languageImportOrder.Count - 1)) ? "↓" : string.Empty, GUILayout.Width(20.0f)) && (language_index < (languageImportOrder.Count - 1)))
                        {
                            SystemLanguage language = languageImportOrder[language_index];
                            languageImportOrder[language_index] = languageImportOrder[language_index + 1];
                            languageImportOrder[language_index + 1] = language;
                        }
                        GUILayout.Label($"{ language_index + 1 }. { languageImportOrder[language_index] }");
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.Box(string.Empty, GUILayout.Width(Screen.width), GUILayout.Height(3.0f));
                }
                if (GUILayout.Button("Select all translations"))
                {
                    foreach (Dictionary<string, (bool IsSelected, string ModifiedTranslation, string ModifiedComment, SystemLanguage PreviewLanguage)> modified_translations in modifiedLanguages.Values)
                    {
                        List<string> keys = new List<string>(modified_translations.Keys);
                        foreach (string key in keys)
                        {
                            (_, string ModifiedTranslation, string ModifiedComment, SystemLanguage PreviewLanguage) = modified_translations[key];
                            modified_translations[key] = (true, ModifiedTranslation, ModifiedComment, PreviewLanguage);
                        }
                        keys.Clear();
                    }
                }
                if (GUILayout.Button("Deselect all translations"))
                {
                    foreach (Dictionary<string, (bool IsSelected, string ModifiedTranslation, string ModifiedComment, SystemLanguage PreviewLanguage)> modified_translations in modifiedLanguages.Values)
                    {
                        List<string> keys = new List<string>(modified_translations.Keys);
                        foreach (string key in keys)
                        {
                            (_, string ModifiedTranslation, string ModifiedComment, SystemLanguage PreviewLanguage) = modified_translations[key];
                            modified_translations[key] = (false, ModifiedTranslation, ModifiedComment, PreviewLanguage);
                        }
                        keys.Clear();
                    }
                }
                translationsTokenizedSearchField.Draw();
                foreach (SystemLanguage language in languageImportOrder)
                {
                    GUILayout.Box(string.Empty, GUILayout.Width(Screen.width), GUILayout.Height(3.0f));
                    GUILayout.Label($"Language: { language }");
                    if (xliff.TryGetTranslations(language, out IReadOnlyDictionary<string, string> translations))
                    {
                        if (!modifiedLanguages.TryGetValue(language, out Dictionary<string, (bool IsSelected, string ModifiedTranslation, string ModifiedComment, SystemLanguage PreviewLanguage)> modified_translations))
                        {
                            modified_translations = new Dictionary<string, (bool IsSelected, string ModifiedTranslation, string ModifiedComment, SystemLanguage PreviewLanguage)>();
                            modifiedLanguages.Add(language, modified_translations);
                        }
                        foreach (KeyValuePair<string, string> translation in translations)
                        {
                            if (!xliff.Comments.TryGetValue(translation.Key, out string comment))
                            {
                                comment = string.Empty;
                            }
                            if (TryGetStringTranslation(translation.Key, out StringTranslationObjectScript string_translation))
                            {
                                string original_text = string_translation.Translation.GetValue(language);
                                if
                                (
                                    (
                                        (original_text != translation.Value) ||
                                        (string_translation.Comment != comment)
                                    ) &&
                                    (
                                        translationsTokenizedSearchField.IsContainedInSearch(string_translation.name) ||
                                        translationsTokenizedSearchField.IsContainedInSearch(translation.Value) ||
                                        translationsTokenizedSearchField.IsContainedInSearch(comment) ||
                                        translationsTokenizedSearchField.IsContainedInSearch(original_text) ||
                                        translationsTokenizedSearchField.IsContainedInSearch(string_translation.Comment)
                                    )
                                )
                                {
                                    if (!modified_translations.TryGetValue(translation.Key, out (bool IsSelected, string ModifiedTranslation, string ModifiedComment, SystemLanguage PreviewLanguage) modified_translation))
                                    {
                                        modified_translation = (true, translation.Value, comment, language);
                                        modified_translations.Add(translation.Key, modified_translation);
                                    }
                                    GUILayout.Box(string.Empty, GUILayout.Width(Screen.width), GUILayout.Height(2.0f));
                                    Color default_background_color = GUI.backgroundColor;
                                    bool is_translation_modified = (translation.Value != modified_translation.ModifiedTranslation) || (comment != modified_translation.ModifiedComment);
                                    GUI.backgroundColor = (!modified_translation.IsSelected) ? Color.red : (is_translation_modified ? Color.yellow : default_background_color);
                                    GUILayout.BeginHorizontal();
                                    if (GUILayout.Button(is_translation_modified ? "Revert" : string.Empty, GUILayout.Width(60.0f), GUILayout.Height(is_translation_modified ? 97.0f : 0.0f)))
                                    {
                                        modified_translation = (modified_translation.IsSelected, translation.Value, comment, modified_translation.PreviewLanguage);
                                        modified_translations[translation.Key] = modified_translation;
                                        GUI.FocusControl(null);
                                    }
                                    GUILayout.BeginVertical();
                                    bool is_selected = GUILayout.Toggle(modified_translation.IsSelected, string.Empty);
                                    EditorGUI.BeginDisabledGroup(true);
                                    EditorGUILayout.ObjectField(string_translation, typeof(StringTranslationObjectScript), true);
                                    EditorGUI.EndDisabledGroup();
                                    GUILayout.BeginHorizontal();
                                    string input = EditorGUILayout.TextArea(modified_translation.ModifiedTranslation, GUILayout.Width(input_width), GUILayout.Height(60.0f));
                                    string comment_input = EditorGUILayout.TextArea(modified_translation.ModifiedComment, GUILayout.Height(60.0f));
                                    GUI.backgroundColor = modified_translation.IsSelected ? default_background_color : Color.red;
                                    GUILayout.EndHorizontal();
                                    SystemLanguage preview_language = (SystemLanguage)EditorGUILayout.EnumPopup("↓ Replaces", modified_translation.PreviewLanguage);
                                    string original_translation = string_translation.Translation.GetValue(preview_language);
                                    GUILayout.BeginHorizontal();
                                    EditorGUI.BeginDisabledGroup(true);
                                    EditorGUILayout.TextArea(original_translation, GUILayout.Width(input_width), GUILayout.Height(60.0f));
                                    EditorGUILayout.TextArea(string_translation.Comment, GUILayout.Height(60.0f));
                                    EditorGUI.EndDisabledGroup();
                                    GUI.backgroundColor = default_background_color;
                                    GUILayout.EndHorizontal();
                                    GUILayout.EndVertical();
                                    GUILayout.EndHorizontal();
                                    if ((is_selected != modified_translation.IsSelected) || (input != modified_translation.ModifiedTranslation) || (comment_input != modified_translation.ModifiedComment) || (preview_language != modified_translation.PreviewLanguage))
                                    {
                                        modified_translations[translation.Key] = (is_selected, input, comment_input, preview_language);
                                    }
                                }
                            }
                            else if
                            (
                                translationsTokenizedSearchField.IsContainedInSearch(Path.GetFileNameWithoutExtension(translation.Key)) ||
                                translationsTokenizedSearchField.IsContainedInSearch(translation.Value) ||
                                translationsTokenizedSearchField.IsContainedInSearch(comment)
                            )
                            {
                                if (!modified_translations.TryGetValue(translation.Key, out (bool IsSelected, string ModifiedTranslation, string ModifiedComment, SystemLanguage PreviewLanguage) modified_translation))
                                {
                                    modified_translation = (true, translation.Value, comment, language);
                                    modified_translations.Add(translation.Key, modified_translation);
                                }
                                GUILayout.Box(string.Empty, GUILayout.Width(Screen.width), GUILayout.Height(2.0f));
                                Color default_background_color = GUI.backgroundColor;
                                bool is_translation_modified = (translation.Value != modified_translation.ModifiedTranslation) || (comment != modified_translation.ModifiedComment);
                                GUI.backgroundColor = (!modified_translation.IsSelected) ? Color.red : (is_translation_modified ? Color.yellow : default_background_color);
                                GUILayout.BeginHorizontal();
                                if (GUILayout.Button(is_translation_modified ? "Revert" : string.Empty, GUILayout.Width(60.0f), GUILayout.Height(is_translation_modified ? 96.0f : 0.0f)))
                                {
                                    modified_translation = (modified_translation.IsSelected, translation.Value, comment, modified_translation.PreviewLanguage);
                                    modified_translations[translation.Key] = modified_translation;
                                    GUI.FocusControl(null);
                                }
                                GUILayout.BeginVertical();
                                bool is_selected = GUILayout.Toggle(modified_translation.IsSelected, string.Empty);
                                Color default_content_color = GUI.contentColor;
                                GUI.contentColor = Color.green;
                                GUILayout.Label($"+ { translation.Key }");
                                GUI.contentColor = default_content_color;
                                GUILayout.BeginHorizontal();
                                string input = EditorGUILayout.TextArea(modified_translation.ModifiedTranslation, GUILayout.Width(input_width), GUILayout.Height(60.0f));
                                string comment_input = EditorGUILayout.TextArea(modified_translation.ModifiedComment, GUILayout.Height(60.0f));
                                GUILayout.EndHorizontal();
                                GUI.backgroundColor = default_background_color;
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                                if ((is_selected != modified_translation.IsSelected) || (input != modified_translation.ModifiedTranslation) || (comment_input != modified_translation.ModifiedComment))
                                {
                                    modified_translations[translation.Key] = (is_selected, input, comment_input, language);
                                };
                            }
                        }
                    }
                }
                GUILayout.Box(string.Empty, GUILayout.Width(Screen.width), GUILayout.Height(3.0f));
                if (GUILayout.Button("Import translations now!"))
                {
                    foreach (SystemLanguage language in languageImportOrder)
                    {
                        if (modifiedLanguages.TryGetValue(language, out Dictionary<string, (bool IsSelected, string ModifiedTranslation, string ModifiedComment, SystemLanguage PreviewLanguage)> modified_translations))
                        {
                            foreach (KeyValuePair<string, (bool IsSelected, string ModifiedTranslation, string ModifiedComment, SystemLanguage PreviewLanguage)> modified_translation in modified_translations)
                            {
                                if (modified_translation.Value.IsSelected)
                                {
                                    if (stringTranslations.TryGetValue(modified_translation.Key, out StringTranslationObjectScript string_translation))
                                    {
                                        string_translation.Translation.Insert(modified_translation.Value.ModifiedTranslation, language);
                                        string_translation.SetComment(modified_translation.Value.ModifiedComment);
                                    }
                                    else
                                    {
                                        string_translation = CreateInstance<StringTranslationObjectScript>();
                                        string_translation.name = Path.GetFileNameWithoutExtension(modified_translation.Key);
                                        string_translation.Translation.Insert(modified_translation.Value.ModifiedTranslation, language);
                                        string_translation.SetComment(modified_translation.Value.ModifiedComment);
                                        AssetDatabase.CreateAsset(string_translation, modified_translation.Key);
                                        stringTranslations.Add(modified_translation.Key, string_translation);
                                    }
                                    EditorUtility.SetDirty(string_translation);
                                }
                            }
                        }
                    }
                    GUI.FocusControl(null);
                    Close();
                    TranslatorEditorWindowScript translator_editor_window = GetWindow<TranslatorEditorWindowScript>("Translator");
                    if (translator_editor_window)
                    {
                        translator_editor_window.UpdateTranslations();
                    }
                }
            }
            GUILayout.EndScrollView();
        }
    }
}
