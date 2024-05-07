using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShopHat : UICanvas
{
    [SerializeField] private List<UnityEngine.UI.Button> listButton;
    [SerializeField] private HatDataSO hatDataSO;
    [SerializeField] private Transform hatHolder;
    [SerializeField] private RawImage hatImage;
    [SerializeField] private TextMeshProUGUI hatName;
    [SerializeField] private TextMeshProUGUI price;

    private BaseHat currentHat;
    private HatType currentHatType;
    List<int> userHatData;

    public void CloseButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }

    public void SetUp()
    {
        userHatData = LoadUserData.Instance.data.userHatData;
        currentHatType = (HatType)LoadUserData.Instance.data.currentHat;

        if (userHatData.Count <= 0)
        {
            userHatData.Add(2);
            for (int i = 1; i < hatDataSO.listHatItemData.Count; i++)
            {
                userHatData.Add(0);
            }
        }

        if (userHatData.Count > 0)
        {
            ChangeHat(currentHatType);
        }
    }

    public void NextButton()
    {
        ChangeHat(hatDataSO.NextType(currentHatType));
    }

    public void PrevButton()
    {
        ChangeHat(hatDataSO.PrevType(currentHatType));
    }

    public void BuyButton()
    {
        UnEquiqHat();
        int index = hatDataSO.GetIndexHatItem(currentHatType);
        userHatData[index] = 1;
        LoadUserData.Instance.data.currentHat = (int)hatDataSO.listHatItemData[index].type;
        ChangeHat(currentHatType);
    }

    public void UnEquiqHat()
    {
        for (int i = 0; i < userHatData.Count; i++)
        {
            if (userHatData[i] == 2)
            {
                userHatData[i] = 1;
            }
        }
    }

    public void EquipButton()
    {
        int index = hatDataSO.GetIndexHatItem(currentHatType);
        userHatData[index] = 2;
        ChangeHat(currentHatType);
    }

    public void ChangeHat(HatType hatType)
    {
        if (currentHat != null)
        {
            LeanPool.Despawn(currentHat);
        }

        List<HatItemData> listHatData = hatDataSO.listHatItemData;

        for (int i = 0; i < listHatData.Count; i++)
        {
            if (listHatData[i].type == hatType)
            {
                hatName.text = listHatData[i].name;
                currentHatType = hatType;
                HatItemData hatItemData = new HatItemData(hatDataSO.listHatItemData[i]);
                currentHat = LeanPool.Spawn(hatItemData.hatPrefabScript, hatHolder);
                currentHat.Tf.localPosition = Vector3.zero;
                //currentWeapon.Tf.localRotation = Quaternion.identity;
                if (userHatData[i] == 2)
                {
                    // using equiped button
                    SetDeactiveButtons();
                    listButton[2].gameObject.SetActive(true);
                }
                else if (userHatData[i] == 0)
                {
                    // using buy button
                    SetDeactiveButtons();
                    listButton[0].gameObject.SetActive(true);
                    price.text = listHatData[i].price.ToString();
                }
                else if (userHatData[i] == 1)
                {
                    // using equip button
                    SetDeactiveButtons();
                    listButton[1].gameObject.SetActive(true);
                }
            }
        }
    }

    public void SetDeactiveButtons()
    {
        for (int i = 0; i < listButton.Count; i++)
        {
            listButton[i].gameObject.SetActive(false);
        }
    }
}
