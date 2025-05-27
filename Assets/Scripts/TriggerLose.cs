using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLose : MonoBehaviour
{
    public GameObject losePanel;

    public void TriggerLosePanel()
    {
        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
    }
}
