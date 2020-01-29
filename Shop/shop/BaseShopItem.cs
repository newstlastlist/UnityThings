
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseShopItem : ScriptableObject
{
    public abstract string Name {get; set;}
    public abstract int ID {get; set;}
    public abstract GameObject Prefab {get; set;}
    public abstract Sprite Sprite {get; set;}
    public abstract int OrderInShopPriority();
    

    public abstract void Init(ShopItemPrefabArgumentWrapper arguments);
    public virtual void OpenItem(Image image, Button button, GameObject price, BaseShopItem shopItem)
    {
        image.sprite = Sprite;
        image.color = new Color(1,1,1,1);

        price.SetActive(false);

        button.onClick.AddListener(() => ChooseItem(shopItem));
    }
    public virtual void ChooseItem(BaseShopItem shopItem)
    {
        Mediator.Publish(new ChooseItemCommand(shopItem));
    }
    public virtual void TryToBuy(BaseShopItem shopItemOnClick)
    {
        Mediator.Publish( new TryTobuyItemCommand((ShopItemForGold)shopItemOnClick));
    }
    
}


public interface IItemForGold
{
    int Cost {get; set;}   

}
public interface IItemForAchievement
{
    int AchievementValueNeeded {get; set;}   
    
}
internal class TryTobuyItemCommand : ICommand
{
    private ShopItemForGold _shopItem;
    public ShopItemForGold ShopItem { get => _shopItem; private set => _shopItem = value; }
    public TryTobuyItemCommand(ShopItemForGold shopItem)
    {
        _shopItem = shopItem;
    }
}

internal class ChooseItemCommand : ICommand 
{
    private BaseShopItem _shopItem;
    public BaseShopItem ShopItem { get => _shopItem; private set => _shopItem = value; }
    public ChooseItemCommand(BaseShopItem shopItem)
    {
        _shopItem = shopItem;
    }

}
