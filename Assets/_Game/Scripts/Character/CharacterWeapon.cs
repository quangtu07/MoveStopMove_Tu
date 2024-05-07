using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class CharacterWeapon : MonoBehaviour
{
    public WeaponDataSO weaponSO;
    public Transform weaponHolder;
    public List<WeaponItemData> userWeaponData = new List<WeaponItemData>();

    public void ChangeWeapon(WeaponType type)
    {
        SetDeActiveCurrentWeapon(type);
        foreach (WeaponItemData item in userWeaponData)
        {
            if (item.type == type)
            {
                item.weaponPrefabScript.gameObject.SetActive(true);
            }
        }
    }

    public BaseWeapon GetWeapon(WeaponType type)
    {

        foreach (WeaponItemData item in weaponSO.listWeaponItemData)
        {
            if (item.type == type)
            {

                BaseWeapon prefab = LeanPool.Spawn(item.weaponPrefabScript, weaponHolder.position, weaponHolder.rotation);
                prefab.Tf.SetParent(weaponHolder);
                return prefab;
               
            }
        }

        return userWeaponData[0].weaponPrefabScript;
    }

    public void AddWeapon(WeaponType type)
    {
        foreach (WeaponItemData item in weaponSO.listWeaponItemData)
        {
            if (item.type == type)
            {
                //BaseWeapon prefab = Instantiate(item.weaponPrefabScript, weaponHolder.position, weaponHolder.rotation);

                BaseWeapon prefab = LeanPool.Spawn(item.weaponPrefabScript, weaponHolder.position, weaponHolder.rotation);

                //prefab.Tf.SetParent(weaponHolder);
                //prefab.gameObject.SetActive(false);
                //WeaponItemData newItemData = new WeaponItemData(item.type, prefab, item.price, item.speed, item.damage);
                //userWeaponData.Add(newItemData);
            }
        } 
    }

    public void SetDeActiveCurrentWeapon(WeaponType currentWeaponType)
    {
        if (userWeaponData.Count > 0)
        {
            foreach (WeaponItemData item in userWeaponData)
            {
                if (item.type == currentWeaponType)
                {
                    item.weaponPrefabScript.gameObject.SetActive(false);
                }
            }
        }
    }

    public void ClearUserWeapon()
    {
        if (userWeaponData.Count > 0)
        {
            userWeaponData.Clear();
        }
    }

}
