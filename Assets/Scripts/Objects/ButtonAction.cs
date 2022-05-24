using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    public GameObject targetToDestroy;

    public void DestroyAndDisable()
    {
        transform.gameObject.GetComponent<BoxCollider>().enabled = false;
        Destroy(targetToDestroy);
    }
}