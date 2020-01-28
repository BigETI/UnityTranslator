using System;
using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// Text translator trigger script abstract class
    /// </summary>
    [Serializable]
    public abstract class ATextTranslatorTriggerScript : MonoBehaviour, ITextTranslatorTrigger
    {
        /// <summary>
        /// String translation object
        /// </summary>
        [SerializeField]
        private StringTranslationObjectScript stringTranslationObject = default;

        /// <summary>
        /// String translation
        /// </summary>
        public string StringTranslation => ((stringTranslationObject == null) ? string.Empty : stringTranslationObject.ToString());

        /// <summary>
        /// Update string
        /// </summary>
        /// <param name="text">Text</param>
        protected abstract void UpdateString(string stringValue);

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            UpdateString(StringTranslation);
            Destroy(this);
        }
    }
}
