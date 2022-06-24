using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimSync : MonoBehaviour
{
    [SerializeField]
    private GunScript GS;

    public void StopInspection()
    {
        GS.IsInspect = false;
    }
}
