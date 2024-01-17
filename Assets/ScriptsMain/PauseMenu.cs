using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pausemenu;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausemenu.activeInHierarchy)
            {
                pausemenu.SetActive(true);
            }
            else
            {
                pausemenu.SetActive(false);
            }
        }
    }
}
