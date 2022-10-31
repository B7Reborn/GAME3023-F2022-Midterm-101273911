using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlchemySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item = null;

    [SerializeField]
    private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;

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
    Image itemIcon;

    [SerializeField]
    TextMeshProUGUI itemCountText;

    [Header("Alchemy Slot Settings")]
    [SerializeField]
    private IngredientSlot[] ingredientSlots;
    [SerializeField]
    private ItemSlot[] itemSlots;
    [SerializeField]
    public List<Recipe> recipeList;

    private bool ingredientsMatchRecipe = false;
    private bool effectsMatchRecipe = false;


    // Start is called before the first frame update
    void Start()
    {
        UpdateGraphic();
    }

    //Change Icon and count
    void UpdateGraphic()
    {
        if (count < 1)
        {
            item = null;
            itemIcon.gameObject.SetActive(false);
            itemCountText.gameObject.SetActive(false);
        }
        else
        {
            //set sprite to the one from the item
            itemIcon.sprite = item.icon;
            itemIcon.gameObject.SetActive(true);
            itemCountText.gameObject.SetActive(true);
            itemCountText.text = count.ToString();
        }
    }

    public void GeneratePotion()
    {
        // Reset potion making checks
        ingredientsMatchRecipe = false;
        effectsMatchRecipe = true;
        Recipe targetRecipe = null;
        item = null;
        Count = 0;

        // Check added ingredients against each recipe
        foreach (Recipe recipe in recipeList)
        {
            // Set target recipe
            targetRecipe = recipe;
            // Make temporary list of items
            List<Ingredient> tempList = new List<Ingredient>(recipe.ingredientList);

            // Check through each ingredient slot
            foreach (IngredientSlot inputSlots in ingredientSlots)
            {
                // If the ingredient in the slot matches an item in list, remove item from list
                bool removeItem = false;
                int removeIndex = -1;
                foreach (Ingredient tempIngredient in tempList)
                {
                    removeIndex++;
                    if (inputSlots.ingredient == tempIngredient)
                    {
                        removeItem = true;
                        break;
                    }
                    
                }

                if (removeItem)
                {
                    tempList.RemoveAt(removeIndex);
                }
            }

            // Once through all slots, if list is empty, all items in that recipe are in the pot
            if (tempList.Count == 0)
            {
                // Therefore that recipe is makeable with given ingredients, break loop
                ingredientsMatchRecipe = true;
                break;
            }

            // Otherwise, check next recipe (continue loop)
        }

        

        if (ingredientsMatchRecipe && effectsMatchRecipe)
        {
            // Set to appropriate potion and display it
            item = targetRecipe.outputItem;
            Count = 1;
            
        }


        UpdateGraphic();
    }

    public void UseItemInSlot()
    {

        // When clicking the alchemy slot 
        foreach (ItemSlot slot in itemSlots)
        {
            if (slot.item == null && this.item != null)
            {
                slot.item = this.item;
                slot.Count += 1;
                break;
            }
            else if (slot.item == this.item)
            {
                slot.Count += 1;
                break;
            }
        }

    }

    private bool CanUseItem()
    {
        return (item != null && count > 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            nameText.text = item.name;
            descriptionText.text = item.description;
            foreach (Effect effect in item.itemEffects)
            {
                descriptionText.text += $"\n {effect.affectedStat}";
                if (effect.effectStrength > -1)
                {
                    descriptionText.text += $" +{effect.effectStrength}";
                }
                else
                {
                    descriptionText.text += $" -{effect.effectStrength}";
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null)
        {
            descriptionText.text = "";
            nameText.text = "";
        }
    }
}
