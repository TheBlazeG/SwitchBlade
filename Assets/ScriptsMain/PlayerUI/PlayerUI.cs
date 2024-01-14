using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider healthSlider;
    public Slider ammoSlider;
    public Image skillImage;

    public Color skillImageActiveColor, skillImageDeactiveColor;
    public bool skillImageState = false;

    private void Update()
    {
        ChangeSkillImageState();
    }

    //Sets Health
    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(float health)
    {
        healthSlider.value -= health;
    }

    //Sets Ammo
    public void SetMaxAmmo(float ammo)
    {
        ammoSlider.maxValue = ammo;
        ammoSlider.value -= ammo;
    }

    public void SetAmmo(float ammo)
    {
        ammoSlider.value += ammo;
    }

    //Changes color
    public void ChangeSkillImageState()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (skillImageState)
            {
                skillImageState = false;
                skillImage.color = skillImageDeactiveColor;
            }
            else
            {
                skillImageState = true;
                skillImage.color = skillImageActiveColor;
            }
        }
    }

    public bool GetSkillImageState()
    {
        return skillImageState;
    }
}
