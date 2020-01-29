using UnityEngine;
using system;

[CreateAssetMenu(menuName = "Shop/Shop Item For Achievement")]
public class ShopItemForAchiv :  BaseShopItem, IItemForAchievement
{
    [Header("Item Params")]
    [SerializeField] private string _name;
    [SerializeField] private int _itemID;
    [SerializeField] private GameObject _hatPrefab;
    [SerializeField] private Sprite _sprite;
    [Header("Item By Achivement")]
    [SerializeField] private AchievementType _achievementType;
    [SerializeField] private int _AchievementValueNeeded;
    

    // public override int Cost { get => _cost; set => _cost = value; }
    public override GameObject Prefab { get => _hatPrefab; set => _hatPrefab = value; }
    public override int ID { get => _itemID; set => _itemID = value; }
    public override Sprite Sprite { get => _sprite; set => _sprite = value; }
    public override string Name { get => _name; set => _name = value; }
    public int AchievementValueNeeded { get => _AchievementValueNeeded; set => _AchievementValueNeeded = value; }

    public override void Init(ShopItemPrefabArgumentWrapper reffer)
    {
        if(ID == LocalStorage.LastChoosedHatID)
            reffer.ChoosedItemFrame.SetActive(true);
        
        reffer.Price.SetActive(false);
        reffer.AchivText.gameObject.SetActive(true);
        reffer.AchivText.text = _achievementType.ToString()+ " " + _AchievementValueNeeded;
        reffer.Image.sprite = Sprite;
        reffer.Image.color = new Color(1,1,1,0.4f);
        if(GetAchiveValue(_achievementType) == null)
            Debug.Log("checked achievements type");

        if(GetAchiveValue(_achievementType) >= _AchievementValueNeeded)
        {
            OpenItem(reffer.Image, reffer.Button, reffer.Price, this);
        }

    }

    private enum AchievementType
    {
        LVL
    } 

    private int? GetAchiveValue(AchievementType achivType)
    {
        switch(achivType)
        {
            case AchievementType.LVL:
                return system.LocalStorage.Level;
        }
        return null;
    }
    public override int OrderInShopPriority()
    {
        switch(_achievementType)
        {
            case AchievementType.LVL:
                return int.MaxValue - 100000 * AchievementValueNeeded;
            default:
                return 1; 
                
        }
    }

    
}
