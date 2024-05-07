using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponItemData
{
    public WeaponType type;
    public string name;
    public BaseWeapon weaponPrefabScript;
    public int price;
    public int speed;
    public int damage;
    public bool isBuy;

    public WeaponItemData(WeaponType type, BaseWeapon weaponPrefabScript, int price, int speed, int damage)
    {
        this.type = type;
        this.weaponPrefabScript = weaponPrefabScript;
        this.price = price;
        this.speed = speed;
        this.damage = damage;
    }

    public WeaponItemData(WeaponItemData item)
    {
        this.type = item.type;
        this.weaponPrefabScript = item.weaponPrefabScript;
        this.price = item.price;
        this.speed = item.speed;
        this.damage = item.damage;
    }
}
