using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    public bool AliveVisable = true;
    public PlayerMovementController PlayerMovementController;
    public Light childLight;

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovementController.IsLiving && AliveVisable)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider>().enabled = true;
            childLight.enabled = true;
        }
        else if(!PlayerMovementController.IsLiving && !AliveVisable)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider>().enabled = true;
            childLight.enabled = true;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            childLight.enabled = false;
        }
    }
}