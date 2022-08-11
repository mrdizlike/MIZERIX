using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZiplaneActivator : MonoBehaviour
{
    public Transform EndZone;
    public bool ZiplaneActive;
    private Zipline ziplineScript;
    private InputManager IM;
    private Player_MAIN P_M;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && ZiplaneActive)
            {
                ziplineScript.endPOS = EndZone;

                P_M.CanZIP = true;
                P_M.MainAux.PlayOneShot(P_M.ZipLine_Sound);
            }
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "LightTeam" || other.tag == "DarkTeam")
        {
            ZiplaneActive = true;

            ziplineScript = other.GetComponent<Zipline>();
            P_M = other.GetComponent<Player_MAIN>();
            IM = other.GetComponent<InputManager>();
            ziplineScript.TextForUse.GetComponent<Text>().text = "Press F to use zipline";
            ziplineScript.TextForUse.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "LightTeam" || other.tag == "DarkTeam")
        {
            ZiplaneActive = false;
            P_M.CanZIP = false;
            ziplineScript.TextForUse.SetActive(false);
        }
    }
}
