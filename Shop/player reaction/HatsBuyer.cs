using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using system;

public class HatsBuyer : MonoBehaviour
{
    [SerializeField] private Mediator _mediator;
    void Start()
    {
        _mediator.Subscribe<TryTobuyItemCommand>(TryToBuyHat);
        PlayerGOLDTest();
    }

    private void TryToBuyHat(TryTobuyItemCommand choosedItem)
    {

        //can buy
        if(LocalStorage.Gold > choosedItem.ShopHat.cost)
        {
            Debug.Log("Hat Cost: " + choosedItem.ShopHat.cost);
            Debug.Log("Gold PrePurchase: " + LocalStorage.Gold);

            LocalStorage.Gold -= choosedItem.ShopHat.cost;
            LocalStorage.SoldHats[choosedItem.ShopHat.hatIDinLocalStorage] = true;


            Debug.Log("Gold AfterPurchase: " + LocalStorage.Gold);
            Shop.Instance.ReInitShopItems();
        }
        else
        {
            Debug.Log("Hat Cost: " + choosedItem.ShopHat.cost);
            Debug.Log("Not enought gold! player has: " + LocalStorage.Gold);

        }
    }
    

    private void PlayerGOLDTest()
    {
        LocalStorage.Gold = 20000;
        Debug.Log(LocalStorage.Gold);
    }

}
