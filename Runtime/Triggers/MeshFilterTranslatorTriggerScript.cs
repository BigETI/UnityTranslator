using UnityEngine;

/// <summary>
/// Unity translator triggers namespace
/// </summary>
namespace UnityTranslator.Triggers
{
    /// <summary>
    /// A class that describes a mesh filter translator trigger script
    /// </summary>
    public class MeshFilterTranslatorTriggerScript : AMeshFilterTranslatorTriggerScript, IMeshFilterTranslatorTrigger
    {
        /// <summary>
        /// Updates mesh
        /// </summary>
        /// <param name="mesh">Mesh</param>
        protected override void UpdateMesh(Mesh mesh)
        {
            if (TryGetComponent(out MeshFilter mesh_filter))
            {
                mesh_filter.mesh = mesh;
            }
        }
    }
}
