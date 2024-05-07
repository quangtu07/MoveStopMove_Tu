using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    [SerializeField] PoolAmount[] poolAmounts;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < poolAmounts.Length; i++)
        {
            SimplePool.Preload(poolAmounts[i].prefab, poolAmounts[i].amount, poolAmounts[i].parent);
        }
    }
}

[System.Serializable]
public class PoolAmount
{
    public GameUnit prefab;
    public Transform parent;
    public int amount;
}

public enum PoolType
{
    B_Candy,
    B_Sword,
    B_Uzi
}
