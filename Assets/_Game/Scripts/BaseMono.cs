using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMono : MonoBehaviour
{
    private Transform tf;
    private GameObject gameObj;

    public Transform Tf
    {
        get
        {
            if (tf == null)
            {
                tf = transform;
            }
            return tf;
        }
    }

    public GameObject GameObj
    {
        get
        {
            if (gameObj == null)
            {
                gameObj = gameObject;
            }
            return gameObj;
        }
    }
}
