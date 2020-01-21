using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using system;

public class ShopItemTemplaneBeh : MonoBehaviour
{
    private ShopHat _shopHat;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private Image _image;

    public ShopHat ShopHat { get => _shopHat; set => _shopHat = value; }

    
    public void Initialize()
    {
        var btn = GetComponent<Button>();
        var img = GetComponent<Image>();

        btn.onClick.RemoveAllListeners();

        //check for availability of hat in local storage. If Hat with this ID does not exist - we add this hat to localstorage
        if(LocalStorage.SoldHats.ContainsKey(_shopHat.hatIDinLocalStorage) == false)
            LocalStorage.SoldHats[_shopHat.hatIDinLocalStorage] = false;

        //hat by level
        if(_shopHat.isThisHatByLevel)
        {
            _goldText.text = _shopHat.levelNeeded.ToString() + " LVL";
            if(LocalStorage.Level >= _shopHat.levelNeeded)
            {
                OpenHat(img,btn);
                return;
            }
            else return;
        }
        //hat by gold
        _goldText.text = _shopHat.cost.ToString();
        //if hat is sold
        if(LocalStorage.SoldHats[_shopHat.hatIDinLocalStorage] == true)
        {
            OpenHat(img,btn);
        }
        else
            btn.onClick.AddListener(() => TryToBuy(_shopHat));
    }

    private void TryToBuy(ShopHat shopItemOnClick)
    {
        Mediator.Publish( new TryTobuyItemCommand(shopItemOnClick));
    }
    private void ChooseItem(ShopHat shopItemOnClick)
    {
        Mediator.Publish( new ChooseHatCommand(shopItemOnClick));
    }
    private void OpenHat(Image img, Button btn)
    {
        img.sprite = _shopHat.sprite;
        _goldText.text = "";
        btn.onClick.AddListener(() => ChooseItem(_shopHat));
    }
}

internal class TryTobuyItemCommand : ICommand
{
    private ShopHat _shopItem;
    public ShopHat ShopHat { get => _shopItem; private set => _shopItem = value; }
    public TryTobuyItemCommand(ShopHat shopItem)
    {
        _shopItem = shopItem;
    }

}
internal class ChooseHatCommand : TryTobuyItemCommand 
{
    public ChooseHatCommand(ShopHat shopItem) : base(shopItem) {}

}
