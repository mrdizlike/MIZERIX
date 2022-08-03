using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZiplaneActivator : MonoBehaviour
{
    public Transform EndZone;
    private Zipline ziplineScript;
    private InputManager IM;
    private Player_MAIN P_M;

    // Start is called before the first frame update
    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "LightTeam" || other.tag == "DarkTeam")
        {
            if (Input.GetKey(KeyCode.F))
            {
                ziplineScript.endPOS = EndZone;

                P_M.CanZIP = true;
            }

            Debug.Log("I'am here!");

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
            P_M.CanZIP = false;
            ziplineScript.TextForUse.SetActive(false);
        }

        Debug.Log("I'am exit!");
    }
}
