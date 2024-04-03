using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour, ISetColor
{
    private void SetColor(Material materialColor)
    {
        GetComponent<Renderer>().material = materialColor;
    }

    public void SetColor(Material materialColor, bool value = true)
    {
        SetColor(materialColor);
    }
}

