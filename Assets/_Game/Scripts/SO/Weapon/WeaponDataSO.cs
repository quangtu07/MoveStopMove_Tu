using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon")]
public class WeaponDataSO : ScriptableObject
{
    public List<WeaponItemData> listWeaponItemData;

    public WeaponItemData GetWeaponItemData(WeaponType weaponType)
    {
        return listWeaponItemData[(int) weaponType];
    }

    public WeaponType NextType(WeaponType weaponType)
    {
        int index = listWeaponItemData.FindIndex(q => q.type == weaponType);
        index = index + 1 >= listWeaponItemData.Count ? 0 : index + 1;
        return listWeaponItemData[index].type;
    }

    public WeaponType PrevType(WeaponType weaponType)
    {
        int index = listWeaponItemData.FindIndex(q => q.type == weaponType);
        index = index - 1 < 0 ? listWeaponItemData.Count - 1 : index - 1;
        return listWeaponItemData[index].type;
    }

    public int GetIndexWeaponItem(WeaponType weaponType)
    {
        return listWeaponItemData.FindIndex(q => q.type == weaponType);
    }
}
