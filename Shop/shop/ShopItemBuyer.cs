using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using system;

public class ShopItemBuyer : MonoBehaviour
{
    void Start()
    {
        Mediator.Subscribe<TryTobuyItemCommand>(TryToBuyItem);
        PlayerGOLDTest();
    }

    private void TryToBuyItem(TryTobuyItemCommand choosedItem)
    {

        //can buy
        if(LocalStorage.Gold > choosedItem.ShopItem.Cost)
        {
            Debug.Log("Item Cost: " + choosedItem.ShopItem.Cost);
            Debug.Log("Gold PrePurchase: " + LocalStorage.Gold);

            LocalStorage.Gold -= choosedItem.ShopItem.Cost;
            LocalStorage.SoldItems[choosedItem.ShopItem.ID] = true;


            Debug.Log("Gold AfterPurchase: " + LocalStorage.Gold);
            Shop.Instance.ReInitShopItems();
            UpdateShopGold();
        }
        else
        {
            Debug.Log("Item Cost: " + choosedItem.ShopItem.Cost);
            Debug.Log("Not enought gold! player has: " + LocalStorage.Gold);

        }
    }
    
    private void UpdateShopGold()
    {
        Mediator.Publish(new UpdateShopGoldCommand());
    }
    private void PlayerGOLDTest()
    {
        LocalStorage.Gold = 20000;
        Debug.Log(LocalStorage.Gold);
    }

}
