using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayHUD : MonoBehaviour
{
    [SerializeField] private Look Look;

    public bool SwayEnabled;

    public float Amount = 0.1f;
    public float maxAmount = 0.1f;
    public float SmoothAmount = 2;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;

        SwayEnabled = true;
    }

    private void Update()
    {
        if (SwayEnabled)
        {
            float ordX = -Look.mouseX_ord * Amount;
            float ordY = -Look.mouseY_ord * Amount;
            ordX = Mathf.Clamp(ordX, -maxAmount, maxAmount);
            ordY = Mathf.Clamp(ordY, -maxAmount, maxAmount);

            Vector3 finalPosition = new Vector3(ordX, ordY, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * SmoothAmount);
        }
    }
}
