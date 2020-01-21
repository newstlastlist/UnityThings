using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Shop Hat")]
public class ShopHat : ScriptableObject
{
    public int hatIDinLocalStorage;
    public GameObject hatPrefab;
    public Sprite sprite;
    [Header("Hat By Gold params")]
    public int cost;
    [Header("Hat By Level params")]
    public bool isThisHatByLevel;
    public int levelNeeded;
}
