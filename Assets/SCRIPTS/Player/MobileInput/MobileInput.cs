using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MobileInput : MonoBehaviour
{
    public Player_MAIN PM;
    public Look L;
    public JoyStick joyStick;
    public JoyStick TouchField;

    void Update()
    {
        PM.RunAxis = joyStick.InputDirection;
        L.LookAxis = TouchField.InputDirection;

    }
}
