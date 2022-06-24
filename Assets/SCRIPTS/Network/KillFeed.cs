using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFeed : MonoBehaviour
{
    public static KillFeed instance;
    [SerializeField] GameObject KillListingPrefab;

    private void Start()
    {
        instance = this;
    }

    public void AddNewKillListing(string Killer, string Killed)
    {
        GameObject temp = Instantiate(KillListingPrefab, transform);
        temp.transform.SetSiblingIndex(0);
        KillListing tempListing = temp.GetComponent<KillListing>();
        tempListing.SetNames(Killer, Killed);
    }
}
