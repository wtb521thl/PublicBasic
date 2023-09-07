using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AddMeshColliders : Editor
{
    [MenuItem("Tools/AddMeshColliders")]
    public static void AddMeshCoilliders()
    {
        MeshRenderer[] meshRenderers = Selection.activeGameObject.GetComponentsInChildren<MeshRenderer>(true);
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            MeshCollider meshCollider = meshRenderers[i].gameObject.GetComponent<MeshCollider>();
            if (meshCollider == null)
            {
                meshCollider = meshRenderers[i].gameObject.AddComponent<MeshCollider>();
            }
            meshCollider.convex = true;
        }
    }
}
