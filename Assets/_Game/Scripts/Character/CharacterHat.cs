using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class CharacterHat : MonoBehaviour
{
    public HatDataSO hatSO;
    public Transform hatHolder;
    public List<HatItemData> userHatData = new List<HatItemData>();

    public void ChangeHat(HatType type)
    {
        SetDeActiveCurrentHat(type);

        foreach (HatItemData item in userHatData)
        {
            if (item.type == type)
            {
                item.hatPrefabScript.gameObject.SetActive(true);
            }
        }
    }

    public BaseHat GetHat(HatType type)
    {
        foreach (HatItemData item in userHatData)
        {
            if (item.type == type)
            {
                return item.hatPrefabScript;
            }
        }
        return userHatData[0].hatPrefabScript;
    }

    public void AddHat(HatType type)
    {
        foreach (HatItemData item in hatSO.listHatItemData)
        {
            if (item.type == type)
            {
                BaseHat prefab = Instantiate(item.hatPrefabScript, hatHolder.position, hatHolder.rotation);
                prefab.Tf.SetParent(hatHolder);
                prefab.gameObject.SetActive(false);
                HatItemData newItemData = new HatItemData(item.type, prefab, item.price);
                userHatData.Add(newItemData);

                //BaseHat prefab = LeanPool.Spawn(item.hatPrefabScript, hatHolder.position, hatHolder.rotation);
            }
        }
    }

    public void SetDeActiveCurrentHat(HatType currentHatType)
    {
        if (userHatData.Count > 0)
        {
            foreach (HatItemData item in userHatData)
            {
                if (item.type == currentHatType)
                {
                    item.hatPrefabScript.gameObject.SetActive(false);
                }
            }
        }
    }

}
