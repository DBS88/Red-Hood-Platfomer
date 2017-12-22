using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour {

    public float minValue;
    public float maxValue;

    public Image HealthBar;
    public float max_health = 100f;
    public float cur_health = 0f;

    //public Color32 startcolor;
    //public Color32 endcolor;

    // Use this for initialization
    void Start()
    {
        cur_health = max_health;
       // InvokeRepeating("decreaseHealth", 1f, 1f); //This will decrese the health by one over one second
        SetHealth (); //on respawn we can set health
    }

    /*void decreaseHealth()
    {
        if (cur_health <= 0) //If the current health is less or equal to zero
        {
            cur_health = 0; // Set to zero to stay where it is
        }
       else //If this is not the case then continue to decrease
        {
            cur_health -= 5f;
            SetHealth();
        }
        
    }*/

    public void TakeDamage (float AmountDamage)
    {
        cur_health -= AmountDamage;
        SetHealth(); //Update the current health
    }


    void SetHealth()
    {
        float calc_health = cur_health / max_health; //Calculate the current health divided by the max health
        float output_health = calc_health * (maxValue - minValue) + minValue;

        HealthBar.fillAmount = Mathf.Clamp(output_health, minValue, maxValue); //Sets a boundary that the values cannot exceed which are located in the HUD script of unity's inspector tab
        //HealthBar.color = Color.Lerp(endcolor, startcolor, calc_health); //The color will change (lerp) between end and start from the amount calculated between 0-1 in our calc_health
    } 

}
