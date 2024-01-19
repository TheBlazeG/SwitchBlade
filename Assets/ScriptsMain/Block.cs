using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool isOn;
    private Animator redswitch;
    public PlayerUI switchController;



    // Start is called before the first frame update
    void Start()
    {
        redswitch = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isOn = switchController.skillImageState;
        if (isOn)
        {
            redswitch.SetBool("OnOff", true);
        }
        else if (!isOn)
        {
            redswitch.SetBool("OnOff", false);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Bullet")
    //    {
    //        if (isOn)
    //        {
    //            SwitchOn();
    //        }
    //        else if (!isOn)
    //        {
    //            SwitchOff();
    //        }
    //        Destroy(gameObject);
    //    }
    //}
    //public void SwitchOn()
    //{
    //    redswitch.SetBool("OnOff", true);
    //}
    //public void SwitchOff()
    //{
    //    redswitch.SetBool("OnOff", false);
    //}
}
