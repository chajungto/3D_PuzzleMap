using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData", order = int.MaxValue)]

public class ItemData : ScriptableObject
{
    //������ �̸�
    [SerializeField]
    private string itemName;
    public string ItemName { get { return itemName; } }

    //������ ����
    [SerializeField]
    private string description;
    public string Description { get { return description; } }

    //�߰� ȸ����
    [SerializeField]
    private int extraHealth;
    public int ExtraHealth { get { return extraHealth; } }

    //�߰� 
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