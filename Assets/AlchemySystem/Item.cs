using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attribute which allows right click->Create
[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
public class Item : ScriptableObject //Extending SO allows us to have an object which exists in the project, not in the scene
{
    public Sprite icon;
    public string description = "";
    public bool isConsumable = false;

    public List<Effect> itemEffects;


    public void Use(CharacterStatus affectedCharacter)
    {
        Debug.Log("Used item: " + name + " - " + description);

        // Alter character stats based on all the effects the item applies
        foreach(Effect effect in itemEffects)
        {
            switch (effect.affectedStat)
            {
                case Stats.MAXHEALTH:
                    affectedCharacter.maxHealth += effect.effectStrength;
                    break;
                case Stats.HEALTH:
                    affectedCharacter.currentHealth += effect.effectStrength;
                    break;
                case Stats.MAXMANA:
                    affectedCharacter.maxMana += effect.effectStrength;
                    break;
                case Stats.MANA:
                    affectedCharacter.currentMana += effect.effectStrength;
                    break;
                case Stats.STRENGTH:
                    affectedCharacter.strength += effect.effectStrength;
                    break;
                case Stats.DEXTERITY:
                    affectedCharacter.dexterity += effect.effectStrength;
                    break;
                case Stats.INTELLIGENCE:
                    affectedCharacter.intellegence += effect.effectStrength;
                    break;
                case Stats.DEFENCE:
                    affectedCharacter.defence += effect.effectStrength;
                    break;
                case Stats.STAMINA:
                    affectedCharacter.stamina += effect.effectStrength;
                    break;
            }
        }

        // Update the player's status
        affectedCharacter.UpdateText();
    }
}
