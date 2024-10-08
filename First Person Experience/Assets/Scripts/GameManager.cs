using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text infoText;
    public Image keyIcon;
    public bool hasKey;


    // Update is called once per frame
    void Update()
    {
        if (hasKey)
        {
            keyIcon.enabled = true;
        }
        else
        {
            keyIcon.enabled = false;
        }
    }
}
