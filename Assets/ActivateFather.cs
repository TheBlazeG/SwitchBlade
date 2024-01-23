using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFather : MonoBehaviour
{
    [SerializeField] GameObject activate;
    public MovimientoPlayer player;
    public activateBoss son;
    public int can = 1;
    public bool activeTrigger = true;
    void Update()
    {
        Debug.Log(activeTrigger);

        if (!player.DeathPlayer && can > 0)
        {

            activeTrigger = true;
        }


        

        if(activeTrigger)
        {
            activate.SetActive(true);
        }
        else
        {
            activate.SetActive(false);
        }
    }
}
