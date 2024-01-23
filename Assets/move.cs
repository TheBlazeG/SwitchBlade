using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    
    void Update()
    {
        if(transform.localPosition.y <= 102)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + .2f);
        }
    }
}
