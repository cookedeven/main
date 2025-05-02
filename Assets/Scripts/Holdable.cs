using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : MonoBehaviour
{
    public bool IsHeld = false;

    public void SetHightlight(bool onOff)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = onOff ? Color.yellow : Color.white;
        }
    }
}
