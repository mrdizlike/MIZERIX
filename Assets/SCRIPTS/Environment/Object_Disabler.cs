using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Disabler : MonoBehaviour
{
    public bool SoundDisabler;

    private void Update()
    {
        if (SoundDisabler)
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}


