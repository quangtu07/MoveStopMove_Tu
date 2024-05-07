using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUserData : Singleton<LoadUserData>
{
    public UserData data;

    [ContextMenu("Save")]
    public void SaveData()
    {
        string dataString = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(KeyPlayerpef.KEY_USER_DATA, dataString);
        PlayerPrefs.Save();
    }

    [ContextMenu("Load")]
    public void LoadData()
    {
        string dataJson = PlayerPrefs.GetString(KeyPlayerpef.KEY_USER_DATA);

        if (string.IsNullOrEmpty(dataJson))
        {
            List<int> listWeapon = new List<int>();
            List<int> listPant = new List<int>();
            List<int> listHat = new List<int>();
            data = new UserData(1, 0, 0, 0, listWeapon, listPant, listHat);
        } else
        {
            data = new UserData(JsonUtility.FromJson<UserData>(dataJson));

        }

        //if (!File.Exists(destination))
        //{
        //    Debug.LogError("File not found");
        //    return;
        //}

        //string dataJson = File.ReadAllText(destination);
        //UnitData data = JsonUtility.FromJson<UnitData>(dataJson);

        //Debug.Log(data.hp);
        //Debug.Log(data.name);
        //Debug.Log(data.level);
    }
}
