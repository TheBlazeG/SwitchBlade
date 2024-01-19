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
                Time.timeScale = 0f;
                CursorState(true);
            }
            else
            {
                pausemenu.SetActive(false);
                Time.timeScale = 1f;
                CursorState(false);
            }
        }
    }
    public void CursorState(bool _state)
    {
        Cursor.visible = _state;
        if (_state)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        { Cursor.lockState = CursorLockMode.Locked; }
    }
    public void timeFlow()
    {
        Time.timeScale = 1f;
    }
}
