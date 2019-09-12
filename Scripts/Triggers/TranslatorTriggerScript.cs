using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Unity translator triggers namespace
/// </summary>
namespace UnityTranslator.Triggers
{
    /// <summary>
    /// Translator trigger script class
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class TranslatorTriggerScript : ATranslatorTriggerScript
    {
        /// <summary>
        /// Update text
        /// </summary>
        /// <param name="text">Text</param>
        protected override void UpdateText(string text)
        {
            Text text_component = GetComponent<Text>();
            if (text_component != null)
            {
                text_component.text = Translation;
            }
        }
    }
}
