using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Health_Manager : MonoBehaviour
{
    #region Public Variables
        public Slider HealthBarSlider;                                  //Reference to the UI's health bar 
        public Text HealthText;                                         // Reference to health text box
        
        //Variables for Damage Flash
        public Image DamageImage;                                       //Reference to image asset for damage flash
        public float damageFlashSpeed = 3f;                             // The speed at which the damageImage fades away
        public Color damageFlashColor = new Color(1f, 0f, 0f, 0.1f);    // The color of the damageImage

        //Variables for Heal Flash
        public Image HealImage;                                         // Reference to image asset for heal flash
        public float healFlashSpeed = 3f;                               // The speed at which the damageImage fades away
        public Color healFlashColor = new Color(1f, 0f, 0f, 0.1f);      // The color of the damageImage
    #endregion //Public Variables

    #region Private Variables
        int maxHealth = 100;                    //Maximum health a player can have (Default 100)
        int minimumHealth = 0;                  //Minimum health a player can have (Default 0)
        float currentHealth = 100;                //Current health of player (Default 100)
        bool damaged = false;                   //Is True while damage is being taken
        bool healing = false;                   //Is True while healing is happening
    #endregion //Private Variables

    // (Unity Named Methods)
    #region Main Methods
    //Update is called by Unity every frame
    void Update()
    {
        //If there is a health slider update its values
        if (HealthBarSlider != null)
        {
        UpdateHealthBar();
        }

        //If there is a health text box update its value
        if (HealthText != null)
        {
            UpdateHealthText("Int"); //Options are Int, Float, PercentInt, and PercentFloat
        }

        DamageFlash();          //Checks if damage image should flash and if so flashes it
        HealFlash();            //Checks if heal image should flash and if so flashes it
        LockMinMaxHealth();     //Checks if the current health has exceded the health constraints and adjusts as needed
    }
    #endregion //Main Methods

    //(Custom Named Methods)
    #region Utility Methods 

        //SetMaxHealth sets the max health value to the provided value
        public void SetMaxHealth(int newMaxHealth)
        {
            maxHealth = newMaxHealth;
        }
    
        //GetMaxHealth returns the maximum possible health
        public int GetMaxHealth()
        {
            return maxHealth;
        }

        //SetCurrentHealth updates the current health value to the provided value
        public void SetCurrentHealth(int newCurrentHealth)
        {
            currentHealth = newCurrentHealth;
        }

        //GetCurrentHealth returns the current health value
        public float GetCurrentHealth()
        {
            return currentHealth;
        }

    //LockMinMaxHealth ensures the players health never goes over the maxHealth or under the minimumHealth
    public void LockMinMaxHealth()
    {
        //If health dropped below the minimum health set current health to minimum health
        if (currentHealth < minimumHealth)
        {
            currentHealth = minimumHealth;
        }

        //If healing increases current health above max health set current health to max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
        //TakeDamage subtracts the supplied damage amount from the players current health
        public void TakeDamage(int damageAmount)
        {
            //Flagged damaged as true so other functions know you just took damage
            damaged = true;
            
            //Only reduce health if you have health to reduce
            if (currentHealth > minimumHealth)
            {
                currentHealth = currentHealth - damageAmount;
            }
        }

        //Heal increases the current health by the supplied amount
        public void Heal(int healAmount)
        {
            healing = true;

            //Only increase current health if it is less than max health
            if (currentHealth < maxHealth)
            {
                currentHealth = currentHealth + healAmount;
            }
        }

        //UpdateHealthBar updates the health bar slider to match the current health value
        public void UpdateHealthBar()
        {
            HealthBarSlider.maxValue = maxHealth;
            HealthBarSlider.value = currentHealth;
        }
        
        //UpdateHealthText updates the health text box to show the current health
        //The output displayed can be chosen by 
        public void UpdateHealthText(string displayHow)
        {
            if (displayHow == "Int")
            {
                //Shows a truncated version of current health (Just the int portion)
                HealthText.text = Math.Truncate(currentHealth).ToString();
            }   
            else if (displayHow == "Float")
            {
                //Shows the actual float value of current health
                HealthText.text = currentHealth.ToString();
            }
            else if (displayHow == "PercentFloat")
            {
                //Shows the actual float value of current health
                HealthText.text = ((currentHealth/maxHealth)*100).ToString() + "%";
            }
            else if (displayHow == "PercentInt")
            {
                //Shows the actual float value of current health
                HealthText.text = Math.Truncate((currentHealth / maxHealth) * 100).ToString() + "%";
            }
            else
            {
                //Debug handling, what happens when the supplied value is not one of the actual options
                Debug.Log("The Value supplied to HealthManager.UpdateHealthText is not one of the available options");
            }
        }
        //Flash a damage image on the screen and fade it over time
        public void DamageFlash()
        {
            /*When the player takes damage the screen will flash red(a damage image)
             *on the first iteration through this function after being flagged as damaged the screen turns red
             *and damaged is set to false
             *each subsequent itteration through this function fades the red flash until it is completly gone
             *the length of the flash is controlled by damageFlashSpeed 
             *if this function is called in Update() this will happen on over time
             *if this function is called manually(outside of Update()) it will only fade durring itterations of this function call 
             */
            if (damaged)//First loop as damaged
            {
                DamageImage.color = damageFlashColor;//Apply screens flash color
            }
            else //Not the first loop as damaged
            {
                //fade out the flashColor
               DamageImage.color = Color.Lerp(DamageImage.color, Color.clear, damageFlashSpeed * Time.deltaTime);
            }

            //Reset the damaged flag
            damaged = false;
        }

        //Flash a heal image on the screen and fade it over time
        public void HealFlash()
        {
            /*When the player is healed the screen will flash green(a heal image)
             *on the first iteration through this function after being flagged as healing the screen turns green
             *and healing is set to false
             *each subsequent itteration through this function fades the green flash until it is completly gone
             *the length of the flash is controlled by healFlashSpeed 
             *if this function is called in Update() this will happen on over time
             *if this function is called manually(outside of Update()) it will only fade durring itterations of this function call 
             */
            if (healing)//First loop as healing
            {
                HealImage.color = healFlashColor;//Apply screens flash color
            }
            else //Not the first loop as damaged
            {
                //fade out the flashColor
                HealImage.color = Color.Lerp(HealImage.color, Color.clear, healFlashSpeed * Time.deltaTime);
            }

            //Reset the damaged flag
            healing = false;
        }

        //Heal Over Time increases health over time as designated in the variables passed to it
        public void HealOverTime(int healAmount, int duration)
        {
            StartCoroutine(HealOverTimeCoroutine(healAmount, duration));
        }

        //Damage Over Time decreases health over time as designated in the variables passed to it
        public void DamageOverTime(int damageAmount, int damageTime)
        {
            StartCoroutine(DamageOverTimeCoroutine(damageAmount, damageTime));
        }

    #endregion //Utility Methods

    //Coroutines run parallel to other fucntions
    #region Coroutines

    //Heal over time
    IEnumerator HealOverTimeCoroutine(float healAmount, float duration)
    {
        float amountHealed = 0;
        float healPerLoop = healAmount / duration;
        while (amountHealed < healAmount)
        {
            currentHealth += healPerLoop;
            amountHealed += healPerLoop;
            yield return new WaitForSeconds(1f);
        }
    }

    //Damage Over Time
    IEnumerator DamageOverTimeCoroutine(float damageAmount, float duration)
    {
        float amountDamaged = 0;
        float damagePerLoop = damageAmount / duration;
        while (amountDamaged < damageAmount)
        {
            currentHealth -= damagePerLoop;
            Debug.Log(currentHealth.ToString());
            amountDamaged += damagePerLoop;
            yield return new WaitForSeconds(1f);
        }
    }
    #endregion //Coroutines
}
