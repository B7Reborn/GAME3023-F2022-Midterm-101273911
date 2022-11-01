using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IngredientSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Ingredient ingredient = null;
    private CharacterStatus playerStatus;

    [SerializeField]
    private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;

    [SerializeField] 
    private TMPro.TextMeshProUGUI effectsDescriptionText;
    [SerializeField] 
    private TMPro.TextMeshProUGUI effectsText;

    [SerializeField]
    private int count = 0;
    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            UpdateGraphic();
        }
    }

    [SerializeField]
    Image ingredientIcon;

    [SerializeField]
    TextMeshProUGUI ingredientCountText;

    [SerializeField] 
    private IngredientSlot[] targetSlots;

    [SerializeField]
    private AlchemySlot outputSlot;

    [SerializeField]
    private bool pouchSlot = true;

    public bool displaySlot = false;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = FindObjectOfType<CharacterStatus>();
        UpdateGraphic();
    }

    //Change Icon and count
    public void UpdateGraphic()
    {
        if (count < 1)
        {
            ResetText();
            ingredient = null;
            ingredientIcon.gameObject.SetActive(false);
            ingredientCountText.gameObject.SetActive(false);
        }
        else
        {
            //set sprite to the ingredient's sprite
            ingredientIcon.sprite = ingredient.icon;
            ingredientIcon.gameObject.SetActive(true);
            ingredientCountText.gameObject.SetActive(true);
            ingredientCountText.text = count.ToString();
        }
    }

    public void MoveIngredient()
    {
        if (CanUseIngredient() && !displaySlot)
        {
            if (pouchSlot)
            {
                foreach (IngredientSlot slot in targetSlots)
                {
                    if (slot.ingredient == null)
                    {
                        slot.ingredient = ingredient;
                        slot.Count = 1;
                        slot.UpdateGraphic();
                        break;
                    }
                }
                
            }
            else
            {
                foreach (IngredientSlot slot in targetSlots)
                {
                    if (slot.ingredient == null)
                    {
                        slot.ingredient = ingredient;
                        slot.Count = 1;
                        slot.UpdateGraphic();
                        break;
                    }
                    else if (slot.ingredient == this.ingredient)
                    {
                        slot.Count += 1;
                        slot.UpdateGraphic();
                        break;
                    }
                }
                

            }
            Count--;
            if (outputSlot != null)
            {
                outputSlot.GeneratePotion();
            }
            
            
        }
    }

    private bool CanUseIngredient()
    {
        return (ingredient != null && count > 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ingredient != null)
        {
            ResetText();
            nameText.text = ingredient.name;
            descriptionText.text = ingredient.description;
            foreach (Effect effect in ingredient.effectList)
            {
                effectsDescriptionText.text += $"{effect.affectedStat}";
                if (effect.effectStrength > -1)
                {
                    effectsDescriptionText.text += $" +{effect.effectStrength}\n";
                }
                else
                {
                    effectsDescriptionText.text += $" -{effect.effectStrength}\n";
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ingredient != null)
        {
            ResetText();
        }
    }

    public void ResetText()
    {
        descriptionText.text = "";
        nameText.text = "Ingredients";
        effectsDescriptionText.text = "";
        effectsText.text = "Effects";
    }
}
