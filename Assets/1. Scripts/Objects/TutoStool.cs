using UnityEngine;
using UnityEngine.UI;

public class TutoStool : Stool
{
    public GameObject tutoPanel;
    public TutoInfo tutoInfo;
    public Text tutoInfoText;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OpenPanel();
        }
    }

    public void OpenPanel()
    {
        tutoInfoText.text = tutoInfo.TutoText;
        Time.timeScale = 0f;
        tutoPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        Time.timeScale = 1f;
        tutoPanel.SetActive(false);
    }
}
