using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CacheCollider<T> where T : Component
{
    private static Dictionary<Collider, T> listColiderComponents = new Dictionary<Collider, T>();

    public static T GetColliderComponent(Collider collider)
    {
        if (!listColiderComponents.ContainsKey(collider))
        {
            listColiderComponents.Add(collider, collider.GetComponent<T>());
        }

        return listColiderComponents[collider];
    }

    public static void ClearAll()
    {
        listColiderComponents.Clear();
    }
}
