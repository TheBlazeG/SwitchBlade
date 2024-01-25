using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSwitch : MonoBehaviour
{
    public bool originalIsOn;
    public PlayerUI block;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!block.skillImageState)
        {
            block.skillImageState = true;
        }
    }
}