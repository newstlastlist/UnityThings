using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using system;

public class ShopItemTemplaneBeh : MonoBehaviour
{
    private BaseItem _shopItem;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private Image _image;

    public BaseItem ShopItem { get => _shopItem; set => _shopItem = value; }

    
    public void Initialize()
    {
        var btn = GetComponent<Button>();
        var img = GetComponent<Image>();

        btn.onClick.RemoveAllListeners();

        //check for availability of hat in local storage. If Hat with this ID does not exist - we add this hat to localstorage
        if(LocalStorage.SoldHats.ContainsKey(_shopItem.ID) == false)
            LocalStorage.SoldHats[_shopItem.ID] = false;

        //hat by level
        if(_shopItem is ShopHat)
        {
            var hat = (ShopHat)_shopItem;
            _goldText.text = hat.levelNeeded.ToString() + " LVL";
            if(LocalStorage.Level >= hat.levelNeeded)
            {
                OpenItem(img,btn);
                return;
            }
            else return;
        }
        //hat by gold
        _goldText.text = _shopItem.Cost.ToString();
        //if hat is sold
        if(LocalStorage.SoldHats[_shopItem.ID] == true)
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
        _goldText.text = "";
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
internal class ChooseHatCommand : TryTobuyItemCommand 
{
    public ChooseHatCommand(BaseItem shopItem) : base(shopItem) {}

}
