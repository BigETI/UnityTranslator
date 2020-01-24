using System;
using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Unity translator namespace
/// </summary>
namespace UnityTranslator
{
    /// <summary>
    /// Translator trigger script abstract class
    /// </summary>
    [Serializable]
    public abstract class ATranslatorTriggerScript : MonoBehaviour
    {
        /// <summary>
        /// Translation object
        /// </summary>
        [SerializeField]
        private TranslationObjectScript translationObject = default;

        /// <summary>
        /// Translation
        /// </summary>
        public string Translation => ((translationObject == null) ? string.Empty : translationObject.ToString());

        /// <summary>
        /// Update text
        /// </summary>
        /// <param name="text">Text</param>
        protected abstract void UpdateText(string text);

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            UpdateText(Translation);
            Destroy(this);
        }
    }
}
