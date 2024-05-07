using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPant : MonoBehaviour
{
    public PantDataSO pantSO;
    public SkinnedMeshRenderer pantMeshRenderer;
    public List<PantItemData> userPantData = new List<PantItemData>();

    public Material GetPantMaterial(PantType type)
    {
        foreach (PantItemData item in userPantData)
        {
            if (item.type == type)
            {
                return item.pantMaterial;
            }
        }
        return userPantData[0].pantMaterial;
    }

    public void AddPant(PantType type)
    {
        foreach (PantItemData item in pantSO.listPantItemData)
        {
            if (item.type == type)
            {
                PantItemData newItemData = new PantItemData(item.type, item.pantMaterial, item.pantTexture, item.price);
                userPantData.Add(newItemData);
            }
        }
    }

    public void ChangePant(PantType type)
    {

        foreach (PantItemData item in userPantData)
        {
            if (item.type == type)
            {
                pantMeshRenderer.material = item.pantMaterial;
            }
        }
    }

}
