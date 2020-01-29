using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using system;
using DG.Tweening;

public class ShopItemBuyer : MonoBehaviour
{
    private float _fadeAlertDuration = 1f;
    private GameObject _alertRefer;
    void Start()
    {
        Mediator.Subscribe<TryTobuyItemCommand>(TryToBuyItem);
        _alertRefer = Shop.Instance.NotEnoughCoins;
        // PlayerGOLDTest();
    }

    private void TryToBuyItem(TryTobuyItemCommand choosedItem)
    {

        //can buy
        if(LocalStorage.Gold >= choosedItem.ShopItem.Cost)
        {
            Debug.Log("Item Cost: " + choosedItem.ShopItem.Cost);
            Debug.Log("Gold PrePurchase: " + LocalStorage.Gold);

            LocalStorage.Gold -= choosedItem.ShopItem.Cost;
            LocalStorage.GoldSpended += choosedItem.ShopItem.Cost;
            YandexProfile.GoldSpended = LocalStorage.GoldSpended;
            YandexProfile.HatsBuyed = LocalStorage.SoldItems.Count(soldItem => soldItem.Value == true);
            LocalStorage.SoldItems[choosedItem.ShopItem.ID] = true;


            Debug.Log("Gold AfterPurchase: " + LocalStorage.Gold);
            // Shop.Instance.ReInitShopItems();
            UpdateShopGold();
        }
        else
        {
            NotEnoughCoinsAlert();
            Debug.Log("Item Cost: " + choosedItem.ShopItem.Cost);
            Debug.Log("Not enought gold! player has: " + LocalStorage.Gold);

        }
    }
    private void OnDestroy() {
        Mediator.DeleteSubscriber<TryTobuyItemCommand>(TryToBuyItem);
    }
    private void UpdateShopGold()
    {
        Mediator.Publish(new UpdateShopGoldCommand());
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.G))
        {
            LocalStorage.Gold += 10000;
            Debug.Log("gold: " + LocalStorage.Gold);
            Mediator.Publish(new UpdateShopGoldCommand());

        }
    }
    private async void NotEnoughCoinsAlert()
    {
        _alertRefer.SetActive(true);
        RectTransform rt = _alertRefer.GetComponent<RectTransform>();
        rt.DOShakeAnchorPos(_fadeAlertDuration, 100, 15).SetUpdate(true);

        await Task.Delay((int)(_fadeAlertDuration * 1000));
        _alertRefer.SetActive(false);
        
    }

}
