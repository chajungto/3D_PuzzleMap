using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;

    public string GetInteractItemName()
    {
        string str = $"{data.ItemName}";
        return str;
    }

    public string GetInteractItemDescription()
    {
        string str = $"{data.Description}";
        return str;
    }

    public void OnInteract()
    {
        GameManager.Instance.Player.itemData = data;
        GameManager.Instance.Player.interactItem?.Invoke();
        Destroy(gameObject);
    }
}
