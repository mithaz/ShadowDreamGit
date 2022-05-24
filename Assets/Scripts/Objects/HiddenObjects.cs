using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObjects : MonoBehaviour
{
    public bool isAliveVisable = true;
    public ButtonAction action;

    void Update()
    {
        if(isAliveVisable && GameManager.Instance.playerStatus)
        {
            action.enabled = true;
        }
        else if(!isAliveVisable && !GameManager.Instance.playerStatus)
        {
            action.enabled = true;
        }
        else
        {
            action.enabled = false;
        }
    }
}