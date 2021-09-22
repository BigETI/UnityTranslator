using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Unity translator triggers namespace
/// </summary>
namespace UnityTranslator.Triggers
{
    /// <summary>
    /// A class that describes a text translator trigger script
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class TextTranslatorTriggerScript : ATextTranslatorTriggerScript, ITextTranslatorTrigger
    {
        /// <summary>
        /// Updates string
        /// </summary>
        /// <param name="stringValue">String</param>
        protected override void UpdateString(string stringValue)
        {
            if (TryGetComponent(out Text text))
            {
                text.text = StringTranslation;
            }
        }
    }
}
