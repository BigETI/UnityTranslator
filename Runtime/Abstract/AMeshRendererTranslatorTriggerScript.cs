using System;
using System.Collections.Generic;
using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Unity translator triggers namespace
/// </summary>
namespace UnityTranslator.Triggers
{
    /// <summary>
    /// An abstract class that describes a mesh renderer translator trigger script
    /// </summary>
    [Serializable]
    public abstract class AMeshRendererTranslatorTriggerScript : MonoBehaviour, IBaseMeshRendererTranslatorTrigger
    {
        /// <summary>
        /// Material translation objects
        /// </summary>
        [SerializeField]
        private MaterialTranslationObjectScript[] meshTranslationObjects = default;

        /// <summary>
        /// Material translations
        /// </summary>
        private Material[] materialTranslations;

        /// <summary>
        /// Material translations
        /// </summary>
        public IReadOnlyList<Material> MaterialTranslations
        {
            get
            {
                MaterialTranslationObjectScript[] mesh_translation_objects = meshTranslationObjects ?? Array.Empty<MaterialTranslationObjectScript>();
                if (materialTranslations == null)
                {
                    materialTranslations = new Material[mesh_translation_objects.Length];
                    for (int index = 0; index < mesh_translation_objects.Length; index++)
                    {
                        MaterialTranslationObjectScript mesh_translation_object = mesh_translation_objects[index];
                        materialTranslations[index] = mesh_translation_object ? mesh_translation_object.Value : null;
                    }
                }
                return materialTranslations;
            }
        }

        /// <summary>
        /// Updates materials
        /// </summary>
        /// <param name="materials">Materials</param>
        protected abstract void UpdateMaterials(IReadOnlyList<Material> materials);

        /// <summary>
        /// Gets invoked when script gets started
        /// </summary>
        protected virtual void Start()
        {
            UpdateMaterials(MaterialTranslations);
            Destroy(this);
        }
    }
}
