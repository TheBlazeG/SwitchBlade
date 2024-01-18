using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSwitch : MonoBehaviour
{
    public bool originalIsOn;
    public PlayerUI block;

    // Start is called before the first frame update 
    void Start()
    {
 
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!block.skillImageState)
        {
            block.skillImageState = true;
        }
        else
        {
            block.skillImageState = false;
        }
    }
}
