using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using system;
using System.Globalization;

public class ShopItemTemplaneBeh : MonoBehaviour
{
    private BaseShopItem _shopItem;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _achivText;
    [SerializeField] private GameObject _choosedItemFrame;
    [SerializeField] private GameObject _price;

    public BaseShopItem ShopItem { get => _shopItem; set => _shopItem = value; }

    

    public void Initialize()
    {
        if(LocalStorage.ShopType == StaticThings.ShopType.Base)
        {
            LocalStorage.ShopType = Random.Range(0,2) == 0 ? StaticThings.ShopType.SecretImages : StaticThings.ShopType.OppenedImages;


            string shopType = LocalStorage.ShopType.ToString();
            YandexProfile.ShopType = shopType;

        }
        
        Debug.Log("ShopType " + LocalStorage.ShopType);
            
        var btn = GetComponent<Button>();
        var img = GetComponent<Image>();

        if(LocalStorage.ShopType == StaticThings.ShopType.OppenedImages)
            img.sprite = _shopItem.Sprite;
            
        _choosedItemFrame.SetActive(false);
        _price.SetActive(false);
        _achivText.gameObject.SetActive(false);

        btn.onClick.RemoveAllListeners();

        //check for availability of hat in local storage. If Hat with this ID does not exist - we add this hat to localstorage
        if(LocalStorage.SoldItems.ContainsKey(_shopItem.ID) == false)
            LocalStorage.SoldItems[_shopItem.ID] = false;
        
        var reffer = new ShopItemTemplateArgumentWrapper();
        reffer.AchivText = _achivText;
        reffer.ChoosedItemFrame = _choosedItemFrame;
        reffer.GoldText = _goldText;
        reffer.Image = img;
        reffer.Price = _price;
        reffer.Button = btn;

        _shopItem.Init(reffer);
    }

    
}


