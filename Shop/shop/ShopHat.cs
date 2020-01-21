using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Shop Hat")]
public class ShopHat :  BaseItem
{
    [Header("Hat Params")]
    [SerializeField] private int _itemID;
    [SerializeField] private GameObject _hatPrefab;
    [SerializeField] private Sprite _sprite;
    [Header("Hat By Gold params")]
    [SerializeField] private int _cost;
    [Header("Hat By Level progress")]
    public bool isThisHatByLevel;
    public int levelNeeded;

    public override int Cost { get => _cost; set => _cost = value; }
    public override GameObject Prefab { get => _hatPrefab; set => _hatPrefab = value; }
    public override int ID { get => _itemID; set => _itemID = value; }
    public override Sprite Sprite { get => _sprite; set => _sprite = value; }
}
