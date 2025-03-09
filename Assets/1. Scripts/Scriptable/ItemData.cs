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

    //�߰� �ӵ�
    [SerializeField]
    private int extraSpeed;
    public int ExtraSpeed { get { return extraSpeed; } }

    //������ Ÿ��
    [SerializeField]
    private ItemType type;
    public ItemType Type { get { return type; } }

    //������ Ÿ��
    [SerializeField]
    private bool isCorrect;
    public bool IsCorrect { get { return isCorrect; } }
}

public enum ItemType
{
    Key,
    Map,
    Buff
}
