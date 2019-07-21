using UnityEngine;
using UnityEngine.UI;
using UnityTranslator.Objects;

/// <summary>
/// Unity translator triggers namespace
/// </summary>
namespace UnityTranslator.Triggers
{
    /// <summary>
    /// Translator trigger script class
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class TranslatorTriggerScript : MonoBehaviour
    {
        /// <summary>
        /// Translation object
        /// </summary>
        [SerializeField]
        private TranslationObjectScript translationObject;

        /// <summary>
        /// Translation
        /// </summary>
        public string Translation
        {
            get
            {
                return ((translationObject == null) ? string.Empty : translationObject.ToString());
            }
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            Text text = GetComponent<Text>();
            if (text != null)
            {
                text.text = Translation;
            }
            Destroy(this);
        }
    }
}
