using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityTranslator;
using UnityTranslator.Data;
using UnityTranslator.Objects;

/// <summary>
/// Unity translator editor editor windows namespace
/// </summary>
namespace UnityTranslatorEditor.EditorWindows
{
    /// <summary>
    /// Translator editor window script class
    /// </summary>
    public class TranslatorEditorWindowScript : EditorWindow
    {
        /// <summary>
        /// Table margin
        /// </summary>
        private static readonly float tableMargin = 30.0f;

        /// <summary>
        /// XLIFF versions
        /// </summary>
        private static readonly string[] xliffSpeficitations = new string[]
        {
            "1.0",
            "1.1",
            "1.2",
            "2.0"
        };

        /// <summary>
        /// Translation search tokens
        /// </summary>
        private readonly List<string> translationSearchTokens = new List<string>();

        /// <summary>
        /// Edit audio clip translation dictionary
        /// </summary>
        private readonly Dictionary<int, (AudioClip Value, string Comment)> editAudioClipTranslationDictionary = new Dictionary<int, (AudioClip Value, string Comment)>();

        /// <summary>
        /// Edit material translation dictionary
        /// </summary>
        private readonly Dictionary<int, (Material Value, string Comment)> editMaterialTranslationDictionary = new Dictionary<int, (Material Value, string Comment)>();

        /// <summary>
        /// Edit mesh translation dictionary
        /// </summary>
        private readonly Dictionary<int, (Mesh Value, string Comment)> editMeshTranslationDictionary = new Dictionary<int, (Mesh Value, string Comment)>();

        /// <summary>
        /// Edit sprite translation dictionary
        /// </summary>
        private readonly Dictionary<int, (Sprite Value, string Comment)> editSpriteTranslationDictionary = new Dictionary<int, (Sprite Value, string Comment)>();

        /// <summary>
        /// Edit string translation dictionary
        /// </summary>
        private readonly Dictionary<int, (string Value, string Comment)> editStringTranslationDictionary = new Dictionary<int, (string Value, string Comment)>();

        /// <summary>
        /// Edit texture translation dictionary
        /// </summary>
        private readonly Dictionary<int, (Texture Value, string Comment)> editTextureTranslationDictionary = new Dictionary<int, (Texture Value, string Comment)>();

        /// <summary>
        /// Translation object language preview
        /// </summary>
        private readonly Dictionary<int, SystemLanguage> translationObjectLanguagePreview = new Dictionary<int, SystemLanguage>();

        /// <summary>
        /// Translations search field
        /// </summary>
        private SearchField translationsSearchField;

        /// <summary>
        /// Target language
        /// </summary>
        private SystemLanguage targetLanguage = SystemLanguage.English;

        /// <summary>
        /// Source language
        /// </summary>
        private SystemLanguage sourceLanguage = SystemLanguage.English;

        /// <summary>
        /// Is showing missing translations only
        /// </summary>
        private bool isShowingMissingTranslationsOnly;

        /// <summary>
        /// Scroll position
        /// </summary>
        private Vector2 scrollPosition = Vector2.zero;

        /// <summary>
        /// Selected export XLIFF specification index
        /// </summary>
        private EXLIFFSpecification selectedExportXLIFFSpecification;

        /// <summary>
        /// Translations search query
        /// </summary>
        private string translationsSearchQuery = string.Empty;

        /// <summary>
        /// Audio clip translations
        /// </summary>
        private IReadOnlyList<(AudioClipTranslationObjectScript Translation, bool IsMissing)> audioClipTranslations;

        /// <summary>
        /// Material translations
        /// </summary>
        private IReadOnlyList<(MaterialTranslationObjectScript Translation, bool IsMissing)> materialTranslations;

        /// <summary>
        /// Mesh translations
        /// </summary>
        private IReadOnlyList<(MeshTranslationObjectScript Translation, bool IsMissing)> meshTranslations;

        /// <summary>
        /// Sprite translations
        /// </summary>
        private IReadOnlyList<(SpriteTranslationObjectScript Translation, bool IsMissing)> spriteTranslations;

        /// <summary>
        /// String translations
        /// </summary>
        private IReadOnlyList<(StringTranslationObjectScript Translation, bool IsMissing)> stringTranslations;

        /// <summary>
        /// Texture translations
        /// </summary>
        private IReadOnlyList<(TextureTranslationObjectScript Translation, bool IsMissing)> textureTranslations;

        /// <summary>
        /// Tabs
        /// </summary>
        private IReadOnlyList<(string Name, TabDrawnDelegate OnTabDrawn)> tabs;

        /// <summary>
        /// Selected tab index
        /// </summary>
        private int selectedTabIndex = 4;

        /// <summary>
        /// Shows a window
        /// </summary>
        [MenuItem("Window/Translator")]
        public static void ShowWindow() => GetWindow<TranslatorEditorWindowScript>("Translator");

        /// <summary>
        /// Gets translations
        /// </summary>
        /// <typeparam name="T">Translation object type</typeparam>
        /// <param name="isShowingMissingTranslationsOnly">Is showing missing translations only</param>
        /// <returns>Translations</returns>
        private IReadOnlyList<(T Translation, bool IsMissing)> GetTranslations<T>(bool isShowingMissingTranslationsOnly) where T : UnityEngine.Object, IBaseTranslationObject
        {
            List<(T Translation, bool IsMissing)> translations = new List<(T Translation, bool IsMissing)>();
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
                            bool is_missing = !translation.IsLanguageContained(targetLanguage);
                            if (!isShowingMissingTranslationsOnly || is_missing)
                            {
                                translations.Add((translation, is_missing));
                            }
                        }
                    }
                }
            }
            (T Translation, bool IsMissing)[] ret = translations.ToArray();
            translations.Clear();
            return ret;
        }

        /// <summary>
        /// Draws an object translation table
        /// </summary>
        /// <typeparam name="TTranslationObject">Translation object type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <typeparam name="TTranslationData">Translation data type</typeparam>
        /// <typeparam name="TTranslatedData">Translated data type</typeparam>
        /// <param name="translations">Translations</param>
        /// <param name="editTranslationDictionary">Edit translation dictionary</param>
        private void DrawObjectTranslationTable<TTranslationObject, TValue, TTranslationData, TTranslatedData>(IReadOnlyList<(TTranslationObject Translation, bool IsMissing)> translations, Dictionary<int, (TValue Value, string Comment)> editTranslationDictionary) where TTranslationObject : UnityEngine.Object, IBaseTranslationObject, IReadOnlyTranslationData<TValue, TTranslatedData>, ITranslationDataWrapper<TValue, TTranslationData, TTranslatedData>, IComparable<TTranslationObject> where TValue : UnityEngine.Object where TTranslationData : ITranslationData<TValue, TTranslatedData> where TTranslatedData : ITranslatedData<TValue>
        {
            float table_width = Screen.width - tableMargin;
            Color default_background_color = GUI.backgroundColor;
            Color light_background_color = new Color(default_background_color.r, default_background_color.g, default_background_color.b * 1.25f, default_background_color.a);
            GUILayoutOption[] no_apply_or_revert_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width * 0.1f), GUILayout.Height(0.0f) };
            GUILayoutOption[] apply_or_revert_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width * 0.1f), GUILayout.Height(53.0f) };
            GUILayoutOption[] asset_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width * 0.65f), GUILayout.Height(20.0f) };
            GUILayoutOption[] preview_translation_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width * 0.65f), GUILayout.Height(20.0f) };
            GUILayoutOption[] translation_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width * 0.65f), GUILayout.Height(32.0f) };
            GUILayoutOption[] comment_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width * 0.25f), GUILayout.Height(118.0f) };
            GUILayoutOption[] entry_divider_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width), GUILayout.Height(3.0f) };
            GUILayoutOption[] preview_translation_divider_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width * 0.65f), GUILayout.Height(2.0f) };
            GUILayout.BeginHorizontal();
            GUILayout.Label("Apply/Revert", new GUILayoutOption[] { GUILayout.Width(table_width * 0.1f), GUILayout.Height(20.0f) });
            GUILayout.Label("Translation", new GUILayoutOption[] { GUILayout.Width(table_width * 0.65f), GUILayout.Height(20.0f) });
            GUILayout.Label("Comment", new GUILayoutOption[] { GUILayout.Width(table_width * 0.25f), GUILayout.Height(20.0f) });
            GUILayout.EndHorizontal();
            bool is_even_entry = true;
            bool is_updating_translations = false;
            foreach ((TTranslationObject Translation, bool IsMissing) in translations)
            {
                if (SearchUtilities.IsContainedInSearch(Translation.name, translationSearchTokens))
                {
                    int key = Translation.GetInstanceID();
                    is_even_entry = !is_even_entry;
                    GUI.backgroundColor = default_background_color;
                    GUILayout.Box(GUIContent.none, entry_divider_gui_layout_options);
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginVertical();
                    bool is_edited = editTranslationDictionary.TryGetValue(key, out (TValue Value, string Comment) translation);
                    if (is_edited)
                    {
                        GUI.backgroundColor = Color.green;
                        if (GUILayout.Button("Apply", apply_or_revert_gui_layout_options))
                        {
                            Translation.Translation.Insert(translation.Value, targetLanguage);
                            Translation.SetComment(translation.Comment);
                            editTranslationDictionary.Remove(key);
                            is_edited = false;
                            is_updating_translations = true;
                            EditorUtility.SetDirty(Translation);
                            GUI.FocusControl(null);
                        }
                        GUI.backgroundColor = Color.red;
                        if (GUILayout.Button("Revert", apply_or_revert_gui_layout_options))
                        {
                            editTranslationDictionary.Remove(key);
                            is_edited = false;
                            translation = (Translation.Translation.GetValue(targetLanguage), Translation.Comment);
                            is_updating_translations = true;
                            GUI.FocusControl(null);
                        }
                        GUI.backgroundColor = default_background_color;
                    }
                    else
                    {
                        GUILayout.Button(string.Empty, no_apply_or_revert_gui_layout_options);
                        GUILayout.Button(string.Empty, no_apply_or_revert_gui_layout_options);
                    }
                    GUILayout.EndVertical();
                    TValue original_value = Translation.Translation.GetValue(targetLanguage);
                    TValue value = is_edited ? translation.Value : original_value;
                    GUI.backgroundColor = is_edited ? Color.yellow : (IsMissing ? Color.red : (is_even_entry ? light_background_color : default_background_color));
                    GUILayout.BeginVertical();
                    EditorGUILayout.ObjectField(Translation, typeof(TTranslationObject), true, asset_gui_layout_options);
                    TValue input = (TValue)EditorGUILayout.ObjectField(value, typeof(TValue), true, translation_gui_layout_options);
                    GUI.backgroundColor = default_background_color;
                    GUILayout.Box(GUIContent.none, preview_translation_divider_gui_layout_options);
                    SystemLanguage preview_language = translationObjectLanguagePreview.TryGetValue(key, out SystemLanguage selected_preview_language) ? selected_preview_language : sourceLanguage;
                    GUI.backgroundColor = is_even_entry ? light_background_color : default_background_color;
                    selected_preview_language = (SystemLanguage)EditorGUILayout.EnumPopup("Preview", preview_language, preview_translation_gui_layout_options);
                    if (preview_language != selected_preview_language)
                    {
                        preview_language = selected_preview_language;
                        if (translationObjectLanguagePreview.ContainsKey(key))
                        {
                            translationObjectLanguagePreview[key] = preview_language;
                        }
                        else
                        {
                            translationObjectLanguagePreview.Add(key, preview_language);
                        }
                    }
                    TValue language_preview_value = Translation.Translation.GetValue(preview_language);
                    if (EditorGUILayout.ObjectField(language_preview_value, typeof(TValue), true, translation_gui_layout_options) != language_preview_value)
                    {
                        GUI.FocusControl(null);
                    }
                    GUI.backgroundColor = default_background_color;
                    GUILayout.EndVertical();
                    string comment = is_edited ? translation.Comment : Translation.Comment;
                    GUI.backgroundColor = is_edited ? Color.yellow : (IsMissing ? Color.red : (is_even_entry ? light_background_color : default_background_color));
                    comment = GUILayout.TextArea(comment, comment_gui_layout_options);
                    GUI.backgroundColor = default_background_color;
                    if ((input != original_value) || (comment != Translation.Comment))
                    {
                        if (editTranslationDictionary.ContainsKey(key))
                        {
                            editTranslationDictionary[key] = (input, comment);
                        }
                        else
                        {
                            editTranslationDictionary.Add(key, (input, comment));
                        }
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUI.backgroundColor = default_background_color;
            if (is_updating_translations)
            {
                UpdateTranslations();
            }
        }

        /// <summary>
        /// Gets invoked when audio clips need to be drawn
        /// </summary>
        private void AudioClipsTabDrawnEvent() => DrawObjectTranslationTable<AudioClipTranslationObjectScript, AudioClip, AudioClipTranslationData, TranslatedAudioClipData>(audioClipTranslations, editAudioClipTranslationDictionary);

        /// <summary>
        /// Gets invoked when materials need to be drawn
        /// </summary>
        private void MaterialsTabDrawnEvent() => DrawObjectTranslationTable<MaterialTranslationObjectScript, Material, MaterialTranslationData, TranslatedMaterialData>(materialTranslations, editMaterialTranslationDictionary);

        /// <summary>
        /// Gets invoked when meshes need to be drawn
        /// </summary>
        private void MeshesTabDrawnEvent() => DrawObjectTranslationTable<MeshTranslationObjectScript, Mesh, MeshTranslationData, TranslatedMeshData>(meshTranslations, editMeshTranslationDictionary);

        /// <summary>
        /// Gets invoked when sprites need to be drawn
        /// </summary>
        private void SpritesTabDrawnEvent() => DrawObjectTranslationTable<SpriteTranslationObjectScript, Sprite, SpriteTranslationData, TranslatedSpriteData>(spriteTranslations, editSpriteTranslationDictionary);

        /// <summary>
        /// Gets invoked when strings need to be drawn
        /// </summary>
        private void StringsTabDrawnEvent()
        {
            float table_width = Screen.width - tableMargin;
            Color default_background_color = GUI.backgroundColor;
            Color light_background_color = new Color(default_background_color.r, default_background_color.g, default_background_color.b * 1.25f, default_background_color.a);
            GUILayoutOption[] no_apply_or_revert_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width * 0.1f), GUILayout.Height(0.0f) };
            GUILayoutOption[] apply_or_revert_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width * 0.1f), GUILayout.Height(53.0f) };
            GUILayoutOption[] asset_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width * 0.65f), GUILayout.Height(20.0f) };
            GUILayoutOption[] preview_translation_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width * 0.65f), GUILayout.Height(20.0f) };
            GUILayoutOption[] translation_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width * 0.65f), GUILayout.Height(32.0f) };
            GUILayoutOption[] comment_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width * 0.25f), GUILayout.Height(118.0f) };
            GUILayoutOption[] entry_divider_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width), GUILayout.Height(3.0f) };
            GUILayoutOption[] preview_translation_divider_gui_layout_options = new GUILayoutOption[] { GUILayout.Width(table_width * 0.65f), GUILayout.Height(2.0f) };
            GUILayout.BeginHorizontal();
            GUILayout.Label("Apply/Revert", new GUILayoutOption[] { GUILayout.Width(table_width * 0.1f), GUILayout.Height(20.0f) });
            GUILayout.Label("Translation", new GUILayoutOption[] { GUILayout.Width(table_width * 0.65f), GUILayout.Height(20.0f) });
            GUILayout.Label("Comment", new GUILayoutOption[] { GUILayout.Width(table_width * 0.25f), GUILayout.Height(20.0f) });
            GUILayout.EndHorizontal();
            bool is_even_entry = true;
            bool is_updating_translations = false;
            foreach ((StringTranslationObjectScript Translation, bool IsMissing) in stringTranslations)
            {
                string original_value = Translation.Translation.GetValue(targetLanguage);
                if
                (
                    SearchUtilities.IsContainedInSearch(Translation.name, translationSearchTokens) ||
                    SearchUtilities.IsContainedInSearch(original_value, translationSearchTokens) ||
                    SearchUtilities.IsContainedInSearch(Translation.Comment, translationSearchTokens)
                )
                {
                    int key = Translation.GetInstanceID();
                    is_even_entry = !is_even_entry;
                    GUI.backgroundColor = default_background_color;
                    GUILayout.Box(GUIContent.none, entry_divider_gui_layout_options);
                    GUILayout.BeginHorizontal();
                    GUILayout.BeginVertical();
                    bool is_edited = editStringTranslationDictionary.TryGetValue(key, out (string Value, string Comment) translation);
                    if (is_edited)
                    {
                        GUI.backgroundColor = Color.green;
                        if (GUILayout.Button("Apply", apply_or_revert_gui_layout_options))
                        {
                            Translation.Translation.Insert(translation.Value, targetLanguage);
                            Translation.SetComment(translation.Comment);
                            editStringTranslationDictionary.Remove(key);
                            is_edited = false;
                            is_updating_translations = true;
                            EditorUtility.SetDirty(Translation);
                            GUI.FocusControl(null);
                        }
                        GUI.backgroundColor = Color.red;
                        if (GUILayout.Button("Revert", apply_or_revert_gui_layout_options))
                        {
                            editStringTranslationDictionary.Remove(key);
                            is_edited = false;
                            translation = (Translation.Translation.GetValue(targetLanguage), Translation.Comment);
                            is_updating_translations = true;
                            GUI.FocusControl(null);
                        }
                        GUI.backgroundColor = default_background_color;
                    }
                    else
                    {
                        GUILayout.Button(string.Empty, no_apply_or_revert_gui_layout_options);
                        GUILayout.Button(string.Empty, no_apply_or_revert_gui_layout_options);
                    }
                    GUILayout.EndVertical();
                    string value = is_edited ? translation.Value : original_value;
                    GUI.backgroundColor = is_edited ? Color.yellow : (IsMissing ? Color.red : (is_even_entry ? light_background_color : default_background_color));
                    GUILayout.BeginVertical();
                    EditorGUILayout.ObjectField(Translation, typeof(StringTranslationObjectScript), true, asset_gui_layout_options);
                    string input = EditorGUILayout.TextArea(value, translation_gui_layout_options);
                    GUI.backgroundColor = default_background_color;
                    GUILayout.Box(GUIContent.none, preview_translation_divider_gui_layout_options);
                    SystemLanguage preview_language = translationObjectLanguagePreview.TryGetValue(key, out SystemLanguage selected_preview_language) ? selected_preview_language : sourceLanguage;
                    GUI.backgroundColor = is_even_entry ? light_background_color : default_background_color;
                    selected_preview_language = (SystemLanguage)EditorGUILayout.EnumPopup("Preview", preview_language, preview_translation_gui_layout_options);
                    if (preview_language != selected_preview_language)
                    {
                        preview_language = selected_preview_language;
                        if (translationObjectLanguagePreview.ContainsKey(key))
                        {
                            translationObjectLanguagePreview[key] = preview_language;
                        }
                        else
                        {
                            translationObjectLanguagePreview.Add(key, preview_language);
                        }
                    }
                    string language_preview_string = Translation.Translation.GetValue(preview_language);
                    if (EditorGUILayout.TextArea(language_preview_string, translation_gui_layout_options) != language_preview_string)
                    {
                        GUI.FocusControl(null);
                    }
                    GUI.backgroundColor = default_background_color;
                    GUILayout.EndVertical();
                    string comment = is_edited ? translation.Comment : Translation.Comment;
                    GUI.backgroundColor = is_edited ? Color.yellow : (IsMissing ? Color.red : (is_even_entry ? light_background_color : default_background_color));
                    comment = GUILayout.TextArea(comment, comment_gui_layout_options);
                    GUI.backgroundColor = default_background_color;
                    if ((input != original_value) || (comment != Translation.Comment))
                    {
                        if (editStringTranslationDictionary.ContainsKey(key))
                        {
                            editStringTranslationDictionary[key] = (input, comment);
                        }
                        else
                        {
                            editStringTranslationDictionary.Add(key, (input, comment));
                        }
                    }
                    else
                    {
                        editStringTranslationDictionary.Remove(key);
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUI.backgroundColor = default_background_color;
            if (is_updating_translations)
            {
                UpdateTranslations();
            }
        }

        /// <summary>
        /// Gets invoked when textures need to be drawn
        /// </summary>
        private void TexturesTabDrawnEvent() => DrawObjectTranslationTable<TextureTranslationObjectScript, Texture, TextureTranslationData, TranslatedTextureData>(textureTranslations, editTextureTranslationDictionary);

        /// <summary>
        /// Update translations
        /// </summary>
        public void UpdateTranslations()
        {
            audioClipTranslations = GetTranslations<AudioClipTranslationObjectScript>(isShowingMissingTranslationsOnly);
            materialTranslations = GetTranslations<MaterialTranslationObjectScript>(isShowingMissingTranslationsOnly);
            meshTranslations = GetTranslations<MeshTranslationObjectScript>(isShowingMissingTranslationsOnly);
            spriteTranslations = GetTranslations<SpriteTranslationObjectScript>(isShowingMissingTranslationsOnly);
            stringTranslations = GetTranslations<StringTranslationObjectScript>(isShowingMissingTranslationsOnly);
            textureTranslations = GetTranslations<TextureTranslationObjectScript>(isShowingMissingTranslationsOnly);
        }

        /// <summary>
        /// Gets invoked when editor window gets enabled
        /// </summary>
        private void OnEnable() => translationsSearchField = new SearchField();

        /// <summary>
        /// Gets invoked when GUI needs to be drawn
        /// </summary>
        private void OnGUI()
        {
            tabs ??= new (string Name, TabDrawnDelegate OnTabDrawn)[]
            {
                ("Audio clips", AudioClipsTabDrawnEvent),
                ("Materials", MaterialsTabDrawnEvent),
                ("Meshes", MeshesTabDrawnEvent),
                ("Sprites", SpritesTabDrawnEvent),
                ("Strings", StringsTabDrawnEvent),
                ("Textures", TexturesTabDrawnEvent)
            };
            GUILayout.Label("Translation options");
            SystemLanguage to_language = (SystemLanguage)EditorGUILayout.EnumPopup("Translate to language", targetLanguage);
            SystemLanguage source_language = (SystemLanguage)EditorGUILayout.EnumPopup("Source language", sourceLanguage);
            bool is_showing_missing_translations = GUILayout.Toggle(isShowingMissingTranslationsOnly, "Is showing missing translations only");
            if ((targetLanguage != to_language) || (sourceLanguage != source_language) || (isShowingMissingTranslationsOnly != is_showing_missing_translations))
            {
                targetLanguage = to_language;
                sourceLanguage = source_language;
                isShowingMissingTranslationsOnly = is_showing_missing_translations;
                UpdateTranslations();
                translationObjectLanguagePreview.Clear();
            }
            GUILayout.Space(21.0f);
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            selectedExportXLIFFSpecification = (EXLIFFSpecification)EditorGUILayout.Popup("Export XLIFF version", (int)selectedExportXLIFFSpecification, xliffSpeficitations);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Import from XLIFF...", GUILayout.Width((Screen.width - tableMargin) / 3.0f)))
            {
                string file_path = EditorUtility.OpenFilePanel("Open XLIFF file", null, "xml");
                if (!string.IsNullOrWhiteSpace(file_path))
                {
                    IXLIFF xliff = XLIFFImporter.ImportFromFile(file_path);
                    if (xliff != null)
                    {
                        XLIFFImporterEditorWindowScript preview_translation_editor_window = GetWindow<XLIFFImporterEditorWindowScript>($"Importing XLIFF \"{ file_path }\"...", true, typeof(TranslatorEditorWindowScript));
                        if (preview_translation_editor_window)
                        {
                            preview_translation_editor_window.XLIFF = xliff;
                        }
                    }
                }
            }
            if (GUILayout.Button("Export to XLIFF", GUILayout.Width((Screen.width - tableMargin) / 3.0f)))
            {
                if (sourceLanguage == targetLanguage)
                {
                    EditorUtility.DisplayDialog("Source and target languages are same", "Please specify atleast a different target language to export to XLIFF.", "OK");
                }
                else
                {
                    string file_path = EditorUtility.SaveFilePanel("Save XLIFF file to...", null, "translations", "xml");
                    if (!string.IsNullOrWhiteSpace(file_path))
                    {
                        IReadOnlyList<(StringTranslationObjectScript Translation, bool IsMissing)> string_translations = GetTranslations<StringTranslationObjectScript>(false);
                        Dictionary<string, string> comments = new Dictionary<string, string>();
                        Dictionary<string, string> source_translations = new Dictionary<string, string>();
                        Dictionary<string, string> target_translations = new Dictionary<string, string>();
                        Dictionary<SystemLanguage, Dictionary<string, string>> languages = new Dictionary<SystemLanguage, Dictionary<string, string>>
                        {
                            { sourceLanguage, source_translations },
                            { targetLanguage, target_translations }
                        };
                        foreach ((StringTranslationObjectScript Translation, bool IsMissing) in string_translations)
                        {
                            string asset_path = AssetDatabase.GetAssetPath(Translation);
                            if (!source_translations.ContainsKey(asset_path))
                            {
                                source_translations.Add(asset_path, Translation.TryGetValue(sourceLanguage, out string source_translation) ? source_translation : string.Empty);
                                target_translations.Add(asset_path, Translation.TryGetValue(targetLanguage, out string target_translation) ? target_translation : string.Empty);
                            }
                            if (!string.IsNullOrWhiteSpace(Translation.Comment))
                            {
                                if (!comments.ContainsKey(asset_path))
                                {
                                    comments.Add(asset_path, Translation.Comment);
                                }
                            }
                        }
                        IReadOnlyList<IXLIFFDocument> xliff_documents = new XLIFF(selectedExportXLIFFSpecification, sourceLanguage, languages, comments).XLIFFDocuments;
                        if (xliff_documents.Count == 1)
                        {
                            XLIFFExporter.ExportXLIFFDocumetToFile(xliff_documents[0], file_path);
                        }
                        else
                        {
                            foreach (IXLIFFDocument xliff_document in xliff_documents)
                            {
                                StringBuilder extended_file_path = new StringBuilder();
                                extended_file_path.Append(Path.GetFileNameWithoutExtension(file_path));
                                extended_file_path.Append('_');
                                extended_file_path.Append(ISO639.GetLanguageCodeFromLanguage(xliff_document.SourceLanguage).ToUpper());
                                extended_file_path.Append("To");
                                foreach (SystemLanguage target_language in xliff_document.TargetLanguages)
                                {
                                    extended_file_path.Append(ISO639.GetLanguageCodeFromLanguage(target_language).ToUpper());
                                }
                                extended_file_path.Append(Path.GetExtension(file_path));
                                XLIFFExporter.ExportXLIFFDocumetToFile(xliff_document, extended_file_path.ToString());
                                extended_file_path.Clear();
                            }
                        }
                    }
                }
            }
            if (GUILayout.Button("Copy translation form to clipboard", GUILayout.Width((Screen.width - tableMargin) / 3.0f)))
            {
                StringBuilder sb = new StringBuilder("# Translation form\r\n\r\n## Description\r\nThis form is used to translate words into ");
                sb.Append(targetLanguage.ToString());
                sb.AppendLine(".");
                sb.AppendLine("\r\n\r\n## Words\r\n");
                UpdateTranslations();
                foreach ((StringTranslationObjectScript Translation, _) in stringTranslations)
                {
                    if (!Translation.IsLanguageContained(targetLanguage))
                    {
                        sb.Append("### `");
                        sb.Append(Translation.name);
                        sb.AppendLine("`");
                        sb.AppendLine();
                        if (Translation.Comment != null)
                        {
                            if (Translation.Comment.Trim().Length > 0)
                            {
                                sb.Append("Comment: ");
                                sb.AppendLine(Translation.Comment);
                            }
                        }
                        foreach (TranslatedStringData translated_string in Translation.Translation.Values)
                        {
                            sb.Append("- In ");
                            sb.Append(translated_string.Language.ToString());
                            sb.Append(": \"");
                            sb.Append(translated_string.Value);
                            sb.AppendLine("\"");
                        }
                        sb.Append("\r\nWhat is it called in ");
                        sb.Append(targetLanguage.ToString());
                        sb.AppendLine("?: \r\n\r\n");
                    }
                }
                sb.Append("Thank you for translating into ");
                sb.Append(targetLanguage.ToString());
                sb.AppendLine("!");
                GUIUtility.systemCopyBuffer = sb.ToString();
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(21.0f);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Update view", GUILayout.Width((Screen.width - tableMargin) / 3.0f)))
            {
                UpdateTranslations();
                translationObjectLanguagePreview.Clear();
                GUI.FocusControl(null);
            }
            if (GUILayout.Button("Apply changes", GUILayout.Width((Screen.width - tableMargin) / 3.0f)))
            {
                UpdateTranslations();
                foreach ((StringTranslationObjectScript Translation, _) in stringTranslations)
                {
                    int key = Translation.GetInstanceID();
                    if (editStringTranslationDictionary.TryGetValue(key, out (string Value, string Comment) translation))
                    {
                        Translation.Translation.Insert(translation.Value, targetLanguage);
                        Translation.SetComment(translation.Comment);
                        EditorUtility.SetDirty(Translation);
                    }
                }
                foreach ((AudioClipTranslationObjectScript Translation, _) in audioClipTranslations)
                {
                    int key = Translation.GetInstanceID();
                    if (editAudioClipTranslationDictionary.TryGetValue(key, out (AudioClip Value, string Comment) translation))
                    {
                        Translation.Translation.Insert(translation.Value, targetLanguage);
                        Translation.SetComment(translation.Comment);
                        EditorUtility.SetDirty(Translation);
                    }
                }
                foreach ((TextureTranslationObjectScript Translation, _) in textureTranslations)
                {
                    int key = Translation.GetInstanceID();
                    if (editTextureTranslationDictionary.TryGetValue(key, out (Texture Value, string Comment) translation))
                    {
                        Translation.Translation.Insert(translation.Value, targetLanguage);
                        Translation.SetComment(translation.Comment);
                        EditorUtility.SetDirty(Translation);
                    }
                }
                foreach ((SpriteTranslationObjectScript Translation, _) in spriteTranslations)
                {
                    int key = Translation.GetInstanceID();
                    if (editSpriteTranslationDictionary.TryGetValue(key, out (Sprite Value, string Comment) translation))
                    {
                        Translation.Translation.Insert(translation.Value, targetLanguage);
                        Translation.SetComment(translation.Comment);
                        EditorUtility.SetDirty(Translation);
                    }
                }
                editStringTranslationDictionary.Clear();
                editAudioClipTranslationDictionary.Clear();
                editTextureTranslationDictionary.Clear();
                editSpriteTranslationDictionary.Clear();
                UpdateTranslations();
                GUI.FocusControl(null);
            }
            if (GUILayout.Button("Revert changes", GUILayout.Width((Screen.width - tableMargin) / 3.0f)))
            {
                editStringTranslationDictionary.Clear();
                editAudioClipTranslationDictionary.Clear();
                editTextureTranslationDictionary.Clear();
                editSpriteTranslationDictionary.Clear();
                UpdateTranslations();
                GUI.FocusControl(null);
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(21.0f);
            if (stringTranslations == null)
            {
                UpdateTranslations();
            }
            GUILayoutOption[] tab_button_gui_layout_options = new GUILayoutOption[] { GUILayout.Width((Screen.width - tableMargin) / tabs.Count), GUILayout.Height(20.0f) };
            Color default_background_color = GUI.backgroundColor;
            Color light_background_color = new Color(default_background_color.r, default_background_color.g, default_background_color.b * 1.25f, default_background_color.a);
            GUILayout.BeginHorizontal();
            for (int tab_index = 0; tab_index < tabs.Count; tab_index++)
            {
                GUI.backgroundColor = (tab_index == selectedTabIndex) ? light_background_color : default_background_color;
                if (GUILayout.Button(tabs[tab_index].Name, tab_button_gui_layout_options))
                {
                    selectedTabIndex = tab_index;
                    GUI.FocusControl(null);
                }
            }
            GUI.backgroundColor = default_background_color;
            GUILayout.EndHorizontal();
            SearchUtilities.DrawSearchField(translationsSearchField, ref translationsSearchQuery, translationSearchTokens);
            if ((selectedTabIndex >= 0) && (selectedTabIndex < tabs.Count))
            {
                tabs[selectedTabIndex].OnTabDrawn();
            }
            GUILayout.EndScrollView();
        }
    }
}
