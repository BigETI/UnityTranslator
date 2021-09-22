using System;
using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Unity translator triggers namespace
/// </summary>
namespace UnityTranslator.Triggers
{
    /// <summary>
    ///  An abstract class that describes a text translator trigger script
    /// </summary>
    [Serializable]
    public abstract class ATextTranslatorTriggerScript : MonoBehaviour, IBaseTextTranslatorTrigger
    {
        /// <summary>
        /// String translation object
        /// </summary>
        [SerializeField]
        private StringTranslationObjectScript stringTranslationObject = default;

        /// <summary>
        /// String translation
        /// </summary>
        public string StringTranslation => stringTranslationObject ? stringTranslationObject.ToString() : string.Empty;

        /// <summary>
        /// Updates string
        /// </summary>
        /// <param name="stringValue">String</param>
        protected abstract void UpdateString(string stringValue);

        /// <summary>
        /// Gets invoked when script gets started
        /// </summary>
        protected virtual void Start()
        {
            UpdateString(StringTranslation);
            Destroy(this);
        }
    }
}
