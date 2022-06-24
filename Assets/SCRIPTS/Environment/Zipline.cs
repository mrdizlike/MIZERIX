using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zipline : MonoBehaviour
{
    public Transform endPOS;
    public GameObject TextForUse;

    public bool Ziplined;

    public IEnumerator StartZipLine()
    {
        float time = 2;
        float elapsedTime = 0;
        Vector3 startingPOS = transform.position;
        Ziplined = true;
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startingPOS, endPOS.position, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Ziplined = false;
    }
}
