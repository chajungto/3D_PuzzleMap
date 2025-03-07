using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    public GameObject tutoPanel;

    public void Close()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        tutoPanel.SetActive(false);
    }

}
