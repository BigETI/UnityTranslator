using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity translator triggers namespace
/// </summary>
namespace UnityTranslator.Triggers
{
    /// <summary>
    /// A class that describes a mesh renderer translator trigger script
    /// </summary>
    public class MeshRendererTranslatorTriggerScript : AMeshRendererTranslatorTriggerScript, IMeshRendererTranslatorTrigger
    {
        /// <summary>
        /// Updates materials
        /// </summary>
        /// <param name="materials">Materials</param>
        protected override void UpdateMaterials(IReadOnlyList<Material> materials)
        {
            if (TryGetComponent(out MeshRenderer mesh_renderer))
            {
                Material[] material_array = new Material[materials.Count];
                for (int index = 0; index < material_array.Length; index++)
                {
                    material_array[index] = materials[index];
                }
                mesh_renderer.materials = material_array;
            }
        }
    }
}
