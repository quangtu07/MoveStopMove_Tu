using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HatItemData
{
    public HatType type;
    public string name;
    public BaseHat hatPrefabScript;
    public int price;

    public HatItemData(HatType type, BaseHat script, int price)
    {
        this.type = type;
        this.hatPrefabScript = script;
        this.price = price;
    }

    public HatItemData(HatItemData item)
    {
        this.type = item.type;
        this.hatPrefabScript = item.hatPrefabScript;
        this.price = item.price;
    }
}
