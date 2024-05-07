using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    public int currentLevel;
    public int currentWeapon;
    public int currentPant;
    public int currentHat;
    public List<int> userWeaponData;
    public List<int> userPantData;
    public List<int> userHatData;

    public UserData()
    {

    }

    public UserData(int currentLevel, int currentWeapon, int currentPant, int currentHat, List<int> userWeaponData, List<int> userPantData, List<int> userHatData)
    {
        this.currentLevel = currentLevel;
        this.currentWeapon = currentWeapon;
        this.currentPant = currentPant;
        this.currentHat = currentHat;
        this.userWeaponData = userWeaponData;
        this.userPantData = userPantData;
        this.userHatData = userHatData;
    }

    public UserData(UserData userData)
    {
        this.currentLevel = userData.currentLevel;
        this.currentWeapon = userData.currentWeapon;
        this.currentPant = userData.currentPant;
        this.currentHat = userData.currentHat;
        this.userWeaponData = userData.userWeaponData;
        this.userPantData = userData.userPantData;
        this.userHatData = userData.userHatData;
    }
}
