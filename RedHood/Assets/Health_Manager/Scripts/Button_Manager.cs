using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Button_Manager : MonoBehaviour
{
    #region Public Variables
    public Health_Manager HealthManager;            //Reference to the Health_Manager Script (Set in Hierarchy)
    public InputField DamageAmount_InputBox;             //Reference to the DamageAmount_InputBox (Set in Heirarchy)
    public InputField HealAmount_Inputbox;               //Reference to the HealAmount_Inputbox (Set in Heirarchy)
    public InputField SetMaxHealth_InputBox;             //Reference to the SetMaxHealth_InputBox (Set in Heirarchy)
    public InputField SetCurrent_InputBox;               //Reference to the SetCurrent_InputBox (Set in Heirarchy)
    public InputField HOTAmount_InputBox;                //Reference to the HOTAmount_InputBox (Set in Heirarchy)
    public InputField HOTTime_InputBox;                  //Reference to the HOTTime_InputBox (Set in Heirarchy)
    public InputField DOTAmount_InputBox;                //Reference to the DOTAmount_InputBox (Set in Heirarchy)
    public InputField DOTTime_InputBox;                  //Reference to the DOTTime_InputBox (Set in Heirarchy)

    #endregion //Public Variables

    #region Private Variables

    #endregion //Private Variables

    // (Unity Named Methods)
    #region Main Methods
    //Update is called by Unity every frame
    void Update()
    {

    }
    #endregion //Main Methods

    //(Custom Named Methods)
    #region Utility Methods 

    //What happens when you click the damage button
    public void DamageButton()
    {
        try
        {
            HealthManager.TakeDamage(Convert.ToInt32(DamageAmount_InputBox.textComponent.text));
        }
        catch
        {
            Debug.Log("Could not execute HealthManager.TakeDamage, ensure supplied value is an integer");
        }
    }

    //What happens when you click the Set Max Health Button
    public void HealButton()
    {
        try
        {
            HealthManager.Heal(Convert.ToInt32(HealAmount_Inputbox.textComponent.text));
        }
        catch
        {
            Debug.Log("Could not execute HealthManager.Heal, ensure supplied value is an integer");
        }
    }

    //What happens when you click the Set Max Health Button
    public void SetMaxHealthButton()
    {
        try
        {
            HealthManager.SetMaxHealth(Convert.ToInt32(SetMaxHealth_InputBox.textComponent.text));
        }
        catch
        {
            Debug.Log("Could not execute HealthManager.SetMaxHealth, ensure supplied value is an integer");
        }
    }

    //What Happens when you click the Set Current Health Button
    public void SetCurrentHealthButton()
    {
        try
        {
            HealthManager.SetCurrentHealth(Convert.ToInt32(SetCurrent_InputBox.textComponent.text));
        }
        catch
        {
            Debug.Log("Could not execute HealthManager.SetCurrentHealth, ensure supplied value is an integer");
        }
    }

    //What Happens when you click the Heal Over Time Button
    public void HealOverTimeButton()
    {
        try
        {
            HealthManager.HealOverTime(Convert.ToInt32(HOTAmount_InputBox.textComponent.text),
                                    Convert.ToInt32(HOTTime_InputBox.textComponent.text));
        }
        catch
        {
            Debug.Log("Could not execute HealthManager.HealOverTime, ensure supplied value is an integer");
        }
    }

    //What Happens when you click the Damage Over Time Button
    public void DamageOverTimeButton()
    {
        try
        {
            HealthManager.DamageOverTime(Convert.ToInt32(DOTAmount_InputBox.textComponent.text),
                                        Convert.ToInt32(DOTTime_InputBox.textComponent.text));
        }
        catch
        {
            Debug.Log("Could not execute HealthManager.DamageOverTime, ensure supplied value is an integer");
        }
    }

    #endregion //Utility Methods

    //Coroutines run parallel to other fucntions
    #region Coroutines

    #endregion //Coroutines
}
