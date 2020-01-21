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
    private BaseItem _shopItem;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _choosedItemFrame;
    [SerializeField] private GameObject _price;

    public BaseItem ShopItem { get => _shopItem; set => _shopItem = value; }

    
    public void Initialize()
    {
        var btn = GetComponent<Button>();
        var img = GetComponent<Image>();
        _choosedItemFrame.SetActive(false);
        _price.SetActive(false);
        _levelText.gameObject.SetActive(false);

        btn.onClick.RemoveAllListeners();

        //check for availability of hat in local storage. If Hat with this ID does not exist - we add this hat to localstorage
        if(LocalStorage.SoldItems.ContainsKey(_shopItem.ID) == false)
            LocalStorage.SoldItems[_shopItem.ID] = false;

        //chek if item is Hat (Hats have specific params)
        if(_shopItem is ShopHat )
        {
            var hat = (ShopHat)_shopItem;
            if(hat.ID == LocalStorage.LastChoosedHatID)
                _choosedItemFrame.SetActive(true);

            if(hat.isThisHatByLevel)
            {
                _price.SetActive(false);
                _levelText.gameObject.SetActive(true);
                _levelText.text = "LVL " + hat.levelNeeded;
                if(LocalStorage.Level >= hat.levelNeeded)
                {
                    OpenItem(img,btn);
                    return;
                }
                else return;
            }
        }

        //set item cost
        _price.SetActive(true);
        _goldText.text = _shopItem.Cost.ToString("#,#", CultureInfo.InvariantCulture);;
        //if item is sold
        if(LocalStorage.SoldItems[_shopItem.ID] == true)
        {
            OpenItem(img,btn);
        }
        else
            btn.onClick.AddListener(() => TryToBuy(_shopItem));
    }

    private void TryToBuy(BaseItem shopItemOnClick)
    {
        Mediator.Publish( new TryTobuyItemCommand(shopItemOnClick));
    }
    private void ChooseItem(BaseItem shopItemOnClick)
    {
        Mediator.Publish( new ChooseHatCommand(shopItemOnClick));
    }
    
    private void OpenItem(Image img, Button btn)
    {
        img.sprite = _shopItem.Sprite;
        _price.SetActive(false);

        btn.onClick.AddListener(() => ChooseItem(_shopItem));
    }
}

internal class TryTobuyItemCommand : ICommand
{
    private BaseItem _shopItem;
    public BaseItem ShopItem { get => _shopItem; private set => _shopItem = value; }
    public TryTobuyItemCommand(BaseItem shopItem)
    {
        _shopItem = shopItem;
    }
}
public class UpdateShopGoldCommand : ICommand
{

}
internal class ChooseHatCommand : TryTobuyItemCommand 
{
    public ChooseHatCommand(BaseItem shopItem) : base(shopItem) {}

}
