using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;
using UnityEngine.UIElements;

public class CanvasShopWeapon : UICanvas
{
    [SerializeField] private List<UnityEngine.UI.Button> listButton;
    [SerializeField] private WeaponDataSO weaponDataSO;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private RawImage weaponImage;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI price;

    private BaseWeapon currentWeapon;
    private WeaponType currentWeaponType;
    List<int> userWeaponData;

    public void SetUp()
    {
        userWeaponData = LoadUserData.Instance.data.userWeaponData;
        currentWeaponType = (WeaponType) LoadUserData.Instance.data.currentWeapon;

        if (userWeaponData.Count <= 0 )
        {
            userWeaponData.Add(2);
            for ( int i = 0; i < weaponDataSO.listWeaponItemData.Count; i++)
            {
                userWeaponData.Add(0);
            }
        }

        if (userWeaponData.Count > 0 )
        {
            //UnEquiqWeapon();
            //EquipButton();
            ChangeWeapon(currentWeaponType);
            LoadUserData.Instance.data.currentWeapon = (int) currentWeaponType;
        }
    }

    public void SetDeactiveButtons()
    {
        for (int i = 0; i < listButton.Count; i++)
        {
            listButton[i].gameObject.SetActive(false);
        }
    }

    public void NextButton()
    {
        currentWeaponType = weaponDataSO.NextType(currentWeaponType);
        ChangeWeapon(currentWeaponType);
    }

    public void PrevButton()
    {
        currentWeaponType = weaponDataSO.PrevType(currentWeaponType);
        ChangeWeapon(currentWeaponType);
    }

    public void BuyButton()
    {
        //UnEquiqWeapon();
        int index = weaponDataSO.GetIndexWeaponItem(currentWeaponType);
        userWeaponData[index] = 1;
        //LoadUserData.Instance.data.currentWeapon = (int)weaponDataSO.listWeaponItemData[index].type;
        ChangeWeapon(currentWeaponType);
    }

    // candy = 2, sword = 0, uzi = 0
    // sword = 1
    // candy = 1, 
    public void UnEquiqWeapon()
    {
        for (int i = 0; i < userWeaponData.Count; i++)
        {
            if (userWeaponData[i] == 2)
            {
                userWeaponData[i] = 1;
            }
        }
    }

    public void EquipButton()
    {
        int index = weaponDataSO.GetIndexWeaponItem(currentWeaponType);
        UnEquiqWeapon();
        userWeaponData[index] = 2;
        LoadUserData.Instance.data.currentWeapon = (int) currentWeaponType;
        ChangeWeapon(currentWeaponType);
    }

    public void CloseButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }

    public void ChangeWeapon(WeaponType weaponType)
    {
        if (currentWeapon != null)
        {
            LeanPool.Despawn(currentWeapon);
        }

        List<WeaponItemData> listWeaponData = weaponDataSO.listWeaponItemData;

        for (int i = 0; i < listWeaponData.Count; i++)
        {
            if (listWeaponData[i].type == weaponType)
            {
                weaponName.text = listWeaponData[i].name;
                //currentWeaponType = weaponType;
                WeaponItemData weaponItemData = new WeaponItemData(weaponDataSO.listWeaponItemData[i]);
                currentWeapon = LeanPool.Spawn(weaponItemData.weaponPrefabScript, weaponHolder);
                currentWeapon.Tf.localPosition = Vector3.zero;
                //currentWeapon.Tf.localRotation = Quaternion.identity;
                if (userWeaponData[i] == 2)
                {
                    // using equiped button
                    SetDeactiveButtons();
                    listButton[2].gameObject.SetActive(true);
                } else if (userWeaponData[i] == 0)
                {
                    // using buy button
                    SetDeactiveButtons();
                    listButton[0].gameObject.SetActive(true);
                    price.text = listWeaponData[i].price.ToString();
                } else if (userWeaponData[i] == 1)
                {
                    // using equip button
                    SetDeactiveButtons();
                    listButton[1].gameObject.SetActive(true);
                }
            }
        }
    }
}
