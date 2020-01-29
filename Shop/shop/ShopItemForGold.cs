using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using system;

[CreateAssetMenu(menuName = "Shop/Shop Item For Gold")]
public class ShopItemForGold : BaseShopItem, IItemForGold
{
    [Header("Item Params")]
    [SerializeField] private string _name;
    [SerializeField] private int _itemID;
    [SerializeField] private GameObject _hatPrefab;
    [SerializeField] private Sprite _sprite;
    [Header("Item By Gold params")]
    [SerializeField] private int _cost;


    // public override int Cost { get => _cost; set => _cost = value; }
    public override GameObject Prefab { get => _hatPrefab; set => _hatPrefab = value; }
    public override int ID { get => _itemID; set => _itemID = value; }
    public override Sprite Sprite { get => _sprite; set => _sprite = value; }
    public override string Name { get => _name; set => _name = value; }
    public int Cost { get => _cost; set => _cost = value; }

    public override void Init(ShopItemPrefabArgumentWrapper reffer)
    {
        if (ID == LocalStorage.LastChoosedHatID)
            reffer.ChoosedItemFrame.SetActive(true);

        reffer.Price.SetActive(true);
        reffer.GoldText.text = Cost.ToString("#,#", CultureInfo.InvariantCulture);
        if (LocalStorage.SoldItems[ID] == true)
        {
            OpenItem(reffer.Image, reffer.Button, reffer.Price, this);
        }
        else
        {
            reffer.Image.color = new Color(1, 1, 1, 0.55f);
            reffer.Button.onClick.AddListener(() => TryToBuy(this));
            //if we can buy item , we must buy and y
            if (LocalStorage.Gold >= Cost)
                reffer.Button.onClick.AddListener(() => ChooseItem(this));

        }
    }
    public override int OrderInShopPriority()
    {
        return int.MaxValue - Cost * 10;
    }
}
