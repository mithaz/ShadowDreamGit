using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotating : MonoBehaviour
{
    [SerializeField] private float rotatingSpeed = 10f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotatingSpeed * Time.deltaTime);
    }
}
