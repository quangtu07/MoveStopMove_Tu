using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimplePool
{
    private static Dictionary<PoolType, Pool> poolInstance = new Dictionary<PoolType, Pool>();

    // khoi tao pool moi
    public static void Preload(GameUnit prefab, int amount, Transform parent)
    {
        if (prefab == null)
        {
            Debug.LogError("PREFAB IS EMPTY !!! ");
            return;
        }

        if (!poolInstance.ContainsKey(prefab.poolType) || poolInstance[prefab.poolType] == null)
        {
            Pool pool = new Pool();
            pool.Preload(prefab, amount, parent);
            poolInstance[prefab.poolType] = pool;
        }
    }

    // lay phan tu ra
    public static T Spawn<T>(PoolType poolType, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD !!!");
            return null;
        }

        return poolInstance[poolType].Spawn(pos, rot) as T;
    }

    // tra phan tu vao
    public static void Despawn(GameUnit unit)
    {
        if (!poolInstance.ContainsKey(unit.poolType))
        {
            Debug.LogError(unit.poolType + "IS NOT PRELOAD !!!");
        }
        poolInstance[unit.poolType].Despawn(unit);
    }

    // thu thap phan tu
    public static void Collect(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD !!!");
        }
        poolInstance[poolType].Collect();
    }

    // thu thap tat ca
    public static void CollectAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Collect();
        }
    }

    // destroy 1 pool
    public static void Release(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "IS NOT PRELOAD !!!");
        }
        poolInstance[poolType].Release();
    }

    // destroy tat ca pool
    public static void ReleaseAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Release();
        }
    }
}

public class Pool
{
    Transform parent;
    GameUnit prefab;
    // list chua cac unit dang o trong pool
    Queue<GameUnit> inactives = new Queue<GameUnit>();
    // list chua cac unit dang duoc su dung
    List<GameUnit> actives = new List<GameUnit>();

    // khoi tao pool
    public void Preload(GameUnit prefab, int amount, Transform parent)
    {
        this.parent = parent;
        this.prefab = prefab;

        for (int i = 0; i < amount; i++)
        {
            Despawn(GameObject.Instantiate(prefab, parent));
        }
    }

    //lay phan tu ra tu pool
    public GameUnit Spawn(Vector3 pos, Quaternion rot)
    {
        GameUnit unit;

        if (inactives.Count <= 0)
        {
            unit = GameObject.Instantiate(prefab, parent);
        }
        else
        {
            unit = inactives.Dequeue();
        }

        unit.Tf.SetPositionAndRotation(pos, rot);
        actives.Add(unit);
        unit.gameObject.SetActive(true);

        return unit;
    }

    // tra phan tu vao trong pool
    public void Despawn(GameUnit unit)
    {
        if (unit != null && unit.gameObject.activeSelf)
        {
            actives.Remove(unit);
            inactives.Enqueue(unit);
            unit.gameObject.SetActive(false);
        }
    }

    // thu thap tat ca phan tu dang dung ve pool
    public void Collect()
    {
        while (actives.Count > 0)
        {
            Despawn(actives[0]);
        }
    }

    // destroy tat ca phan tu
    public void Release()
    {
        Collect();
        while (inactives.Count > 0)
        {
            GameObject.Destroy(inactives.Dequeue().gameObject);
        }

        inactives.Clear();
    }
}
