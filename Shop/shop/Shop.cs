using System.Linq;
using System.Diagnostics.Tracing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("List of items to sold")]
    [SerializeField] private BaseShopItem[] _shopItem;

    [Header("References")]
    [SerializeField] private Transform _shopContainerTrans;
    [SerializeField] private GameObject _shopItemPrefab;
    [SerializeField] private GameObject _notEnoughCoins;
    [Space]
    private ShopItemPrefab[] _shopItemsBehList;

    private static Shop _instance;

    public static Shop Instance { get { return _instance; } }

    public BaseShopItem[] ShopItem { get => _shopItem; private set => _shopItem = value; }
    public GameObject NotEnoughCoins { get => _notEnoughCoins; set => _notEnoughCoins = value; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void Start() {
        _notEnoughCoins.SetActive(false);
        if(_shopItem.Length == 0)
        {
            Debug.Log("Assign Items In the Inspector!!!");
            return;
        }
        ShopItem = ShopItem.OrderByDescending(item => item.OrderInShopPriority()).ToArray();
        
        PopulateShop();
    }

    private void PopulateShop()
    {
        _shopItemsBehList = new ShopItemPrefab[_shopItem.Length];
        for (int i = 0; i < _shopItem.Length; i++)
        {
            GameObject item = Instantiate(_shopItemPrefab, _shopContainerTrans);
            ShopItemPrefab itemBeh = item.GetComponent<ShopItemPrefab>();
            _shopItemsBehList[i] = itemBeh;
            itemBeh.ShopItem = _shopItem[i];
            itemBeh.Initialize();
            
        }
    }

    public void ReInitShopItems()
    {
        foreach(var beh in _shopItemsBehList)
        {
            beh.Initialize();
        }
    }

   
    
}




