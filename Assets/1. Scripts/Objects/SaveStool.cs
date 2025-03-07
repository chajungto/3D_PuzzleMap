using UnityEngine;
using UnityEngine.UI;

public class SaveStool : Stool
{
    public GameObject spawnText;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SavePosition();
            spawnText.SetActive(true);
            Invoke("RemoveSpawnText", 3f);
        }
    }

    void RemoveSpawnText()
    {
        spawnText.SetActive(false);
    }

    void SavePosition()
    {
        GameManager.Instance.spawnPoint = transform.position;
    }
}
