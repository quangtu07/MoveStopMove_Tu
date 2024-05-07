using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder;
    public void HideWeapon()
    {
        weaponHolder.gameObject.SetActive(false);
    }

    public void ShowWeapon()
    {
        weaponHolder.gameObject.SetActive(true);
    }
}
