using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PantItemData
{
    public PantType type;
    public string name;
    public Material pantMaterial;
    public Texture2D pantTexture;
    public int price;

    public PantItemData(PantType type, Material pantMaterial, Texture2D pantTexture, int price)
    {
        this.type = type;
        this.pantMaterial = pantMaterial;
        this.pantTexture = pantTexture;
        this.price = price;
    }

    public PantItemData(PantItemData item)
    {
        this.type = item.type;
        this.pantMaterial = item.pantMaterial;
        this.pantTexture = item.pantTexture;
        this.price = item.price;
    }
}
