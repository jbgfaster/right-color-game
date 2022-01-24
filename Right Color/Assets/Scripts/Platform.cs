using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public string color;
    public void SetColor(Material materialColor)
    {
        GetComponent<Renderer>().material = materialColor;
        color = materialColor.name;
    }
}

