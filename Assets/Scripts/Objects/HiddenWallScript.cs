using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenWallScript : MonoBehaviour
{
    [SerializeField] private bool isHiddenAlivePlane = false;
    public bool IsHiddenAlivePlane => isHiddenAlivePlane;
}