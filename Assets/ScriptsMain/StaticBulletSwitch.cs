using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBulletSwitch : MonoBehaviour
{
    [SerializeField] private GameObject Door;
    public bool imageState = true;
    private Animator doorSwitch;
    public bool isOn;

    private void Start()
    {
        doorSwitch = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Door.SetActive(false);
            if(!isOn)
            {
                doorSwitch.SetBool("OnOff", true);
            }
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
