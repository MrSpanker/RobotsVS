using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Material[] materials;
    private int materialIndex;
    private int materialChanges;

    public bool nextLvl = false;
    private void Update()
    {
        if (nextLvl)
        {
            ChangeMaterial();
            nextLvl = false;
        }

    }

    void ChangeMaterial()
    {
        if (materials.Length > 0)
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material = materials[materialIndex];
            materialChanges++;
            materialIndex = (materialIndex + 1) % materials.Length;
        }
    }
    public int GetMaterialChangesCount()
    {
        return materialChanges;
    }
}