using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWin : MonoBehaviour
{
    public GameObject winPanel;

    public void TriggerWinPanel() 
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }
}
