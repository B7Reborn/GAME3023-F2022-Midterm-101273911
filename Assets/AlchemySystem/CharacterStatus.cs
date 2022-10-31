using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Example Character Status class to show how created items can be used to affect the player
public class CharacterStatus : MonoBehaviour
{
    // Player's stats
    public int currentHealth = 10;
    public int maxHealth = 10;
    public int currentMana = 10;
    public int maxMana = 10;

    public int strength = 5;
    public int dexterity = 5;
    public int intellegence = 5;
    public int defence = 5;
    public int stamina = 5;

    // Text Mesh Pro objects to display the character's stats to the screen
    [SerializeField]
    private TMPro.TextMeshProUGUI healthText;
    [SerializeField]
    private TMPro.TextMeshProUGUI manaText;
    [SerializeField]
    private TMPro.TextMeshProUGUI strengthText;
    [SerializeField]
    private TMPro.TextMeshProUGUI dexterityText;
    [SerializeField]
    private TMPro.TextMeshProUGUI intellegenceText;
    [SerializeField]
    private TMPro.TextMeshProUGUI defenceText;
    [SerializeField]
    private TMPro.TextMeshProUGUI staminaText;

    
    void Start()
    { 
        UpdateText();
    }


    public void UpdateText()
    {
        CheckVitals();

        healthText.text = $"{currentHealth}/{maxHealth}";
        manaText.text = $"{currentMana}/{maxMana}";
        strengthText.text = $"{strength}";
        dexterityText.text = $"{dexterity}";
        intellegenceText.text = $"{intellegence}";
        defenceText.text = $"{defence}";
        staminaText.text = $"{stamina}";
    }

    // Ensure current health/mana doesn't exceed their max amount
    public void CheckVitals()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }
}
