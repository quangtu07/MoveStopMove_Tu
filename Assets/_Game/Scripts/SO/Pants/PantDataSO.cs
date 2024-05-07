using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Pant")]
public class PantDataSO : ScriptableObject
{
    public List<PantItemData> listPantItemData;

    public PantItemData GetPantItemData(PantType type)
    {
        return listPantItemData[(int)type];
    }

    public PantType NextType(PantType pantType)
    {
        int index = listPantItemData.FindIndex(q => q.type == pantType);
        index = index + 1 >= listPantItemData.Count ? 0 : index + 1;
        return listPantItemData[index].type;
    }

    public PantType PrevType(PantType pantType)
    {
        int index = listPantItemData.FindIndex(q => q.type == pantType);
        index = index - 1 < 0 ? listPantItemData.Count - 1 : index - 1;
        return listPantItemData[index].type;
    }

    public int GetIndexPantItem(PantType pantType)
    {
        return listPantItemData.FindIndex(q => q.type == pantType);
    }
}
