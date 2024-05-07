using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasShopPant : UICanvas
{
    [SerializeField] private List<UnityEngine.UI.Button> listButton;
    [SerializeField] private PantDataSO pantDataSO;
    [SerializeField] private RawImage pantImage;
    [SerializeField] private TextMeshProUGUI pantName;
    [SerializeField] private TextMeshProUGUI price;

    private PantType currentPantType;
    List<int> userPantData;

    public void SetUp()
    {
        userPantData = LoadUserData.Instance.data.userPantData;
        currentPantType = (PantType)LoadUserData.Instance.data.currentPant;

        if (userPantData.Count <= 0)
        {
            userPantData.Add(2);
            for (int i = 1; i < pantDataSO.listPantItemData.Count; i++)
            {
                userPantData.Add(0);
            }
        }

        if (userPantData.Count > 0)
        {
            ChangePant(currentPantType);
        }
    }

    public void CloseButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }

    public void NextButton()
    {
        ChangePant(pantDataSO.NextType(currentPantType));
    }

    public void PrevButton()
    {
        ChangePant(pantDataSO.PrevType(currentPantType));
    }

    public void BuyButton()
    {
        UnEquiqHat();
        int index = pantDataSO.GetIndexPantItem(currentPantType);
        userPantData[index] = 1;
        LoadUserData.Instance.data.currentPant = (int)pantDataSO.listPantItemData[index].type;
        ChangePant(currentPantType);
    }

    public void UnEquiqHat()
    {
        for (int i = 0; i < userPantData.Count; i++)
        {
            if (userPantData[i] == 2)
            {
                userPantData[i] = 1;
            }
        }
    }

    public void EquipButton()
    {
        int index = pantDataSO.GetIndexPantItem(currentPantType);
        userPantData[index] = 2;
        ChangePant(currentPantType);
    }


    public void ChangePant(PantType pantType)
    {

        List<PantItemData> listPantData = pantDataSO.listPantItemData;

        for (int i = 0; i < listPantData.Count; i++)
        {
            if (listPantData[i].type == pantType)
            {
                pantName.text = listPantData[i].name;
                currentPantType = pantType;
                pantImage.texture = pantDataSO.listPantItemData[i].pantTexture;
                //currentWeapon.Tf.localRotation = Quaternion.identity;
                if (userPantData[i] == 2)
                {
                    // using equiped button
                    SetDeactiveButtons();
                    listButton[2].gameObject.SetActive(true);
                }
                else if (userPantData[i] == 0)
                {
                    // using buy button
                    SetDeactiveButtons();
                    listButton[0].gameObject.SetActive(true);
                    price.text = listPantData[i].price.ToString();
                }
                else if (userPantData[i] == 1)
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
