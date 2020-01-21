using System.Diagnostics.Tracing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [Header("List of items to sold")]
    [SerializeField] private ShopHat[] _shopItem;

    [Header("References")]
    [SerializeField] private Transform _shopContainerTrans;
    [SerializeField] private GameObject _shopItemPrefab;
    [Space]
    private ShopItemTemplaneBeh[] _shopItemsBehList;

    private static Shop _instance;

    public static Shop Instance { get { return _instance; } }

    public ShopHat[] ShopItem { get => _shopItem; private set => _shopItem = value; }

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
        PopulateShop();
    }

    private void PopulateShop()
    {
        _shopItemsBehList = new ShopItemTemplaneBeh[_shopItem.Length];
        for (int i = 0; i < _shopItem.Length; i++)
        {
            GameObject item = Instantiate(_shopItemPrefab, _shopContainerTrans);
            ShopItemTemplaneBeh itemBeh = item.GetComponent<ShopItemTemplaneBeh>();
            _shopItemsBehList[i] = itemBeh;
            itemBeh.ShopHat = _shopItem[i];
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




