using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData", order = int.MaxValue)]

public class ItemData : ScriptableObject
{
    //아이템 이름
    [SerializeField]
    private string itemName;
    public string ItemName { get { return itemName; } }

    //아이템 설명
    [SerializeField]
    private string description;
    public string Description { get { return description; } }

    //추가 회복량
    [SerializeField]
    private int extraHealth;
    public int ExtraHealth { get { return extraHealth; } }

    //추가 
    [SerializeField]
    private int extraSpeed;
    public int ExtraSpeed { get { return extraSpeed; } }

}

public enum ItemType
{
    Interact,
    Heal,
    Buff,
    DeBuff
}