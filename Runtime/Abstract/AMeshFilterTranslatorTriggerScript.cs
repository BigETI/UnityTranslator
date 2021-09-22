using System;
using UnityEngine;
using UnityTranslator.Objects;

/// <summary>
/// Unity translator triggers namespace
/// </summary>
namespace UnityTranslator.Triggers
{
    /// <summary>
    /// An abstract class that describes a mesh filter translator trigger script
    /// </summary>
    [Serializable]
    public abstract class AMeshFilterTranslatorTriggerScript : MonoBehaviour, IBaseMeshFilterTranslatorTrigger
    {
        /// <summary>
        /// Mesh translation object
        /// </summary>
        [SerializeField]
        private MeshTranslationObjectScript meshTranslationObject = default;

        /// <summary>
        /// Mesh translation
        /// </summary>
        public Mesh MeshTranslation => meshTranslationObject ? meshTranslationObject.Value : null;

        /// <summary>
        /// Updates mesh
        /// </summary>
        /// <param name="mesh">Mesh</param>
        protected abstract void UpdateMesh(Mesh mesh);

        /// <summary>
        /// Gets invoked when script gets started
        /// </summary>
        protected virtual void Start()
        {
            UpdateMesh(MeshTranslation);
            Destroy(this);
        }
    }
}
