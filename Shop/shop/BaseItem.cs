
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItem : ScriptableObject
{
    public abstract int Cost {get; set;}
    public abstract int ID {get; set;}
    public abstract GameObject Prefab {get; set;}
    public abstract Sprite Sprite {get; set;}
    
}
