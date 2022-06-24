using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillListing : MonoBehaviour
{
    [SerializeField] Text KillerDisplay;
    [SerializeField] Text KilledDisplay;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void SetNames(string Killer, string Killed)
    {
        KillerDisplay.text = Killer;
        KilledDisplay.text = Killed;
    }


}
