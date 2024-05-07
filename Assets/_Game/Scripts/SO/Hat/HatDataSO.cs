using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Hat")]
public class HatDataSO : ScriptableObject
{
    public List<HatItemData> listHatItemData;

    public HatItemData GetHatItemData(HatType type)
    {
        return listHatItemData[(int)type];
    }

    public HatType NextType(HatType hatType)
    {
        int index = listHatItemData.FindIndex(q => q.type == hatType);
        index = index + 1 >= listHatItemData.Count ? 0 : index + 1;
        return listHatItemData[index].type;
    }

    public HatType PrevType(HatType hatType)
    {
        int index = listHatItemData.FindIndex(q => q.type == hatType);
        index = index - 1 < 0 ? listHatItemData.Count - 1 : index - 1;
        return listHatItemData[index].type;
    }

    public int GetIndexHatItem(HatType hatType)
    {
        return listHatItemData.FindIndex(q => q.type == hatType);
    }
}
