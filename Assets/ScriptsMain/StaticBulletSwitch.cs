using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBulletSwitch : MonoBehaviour
{
    [SerializeField] private GameObject Door;
    public Color imageActiveColor, imageDeactiveColor;
    public bool imageState = true;
    [SerializeField] private GameObject Switch;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Door.SetActive(false);
            ChangeimageState();
        }
    }

    public void ChangeimageState()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    if (imageState)
        //    {
        //        imageState = false;
        //        Switch. = imageDeactiveColor;
        //    }
        //    else
        //    {
        //        imageState = true;
        //        skillImage.color = imageActiveColor;
        //    }
        //}
    }
}
