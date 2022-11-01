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
        Recipe targetRecipe = null;
        item = null;
        Count = 0;

        // Check added ingredients against each recipe
        foreach (Recipe recipe in recipeList)
        {
            // Reset checks each loop
            ingredientsMatchRecipe = false;
            effectsMatchRecipe = false;

            // Set target recipe
            targetRecipe = recipe;

            // Check ingredients if required
            if (recipe.ingredientList.Count != 0)
            {
                // Make temporary list of items
                List<Ingredient> tempIngredientList = new List<Ingredient>(recipe.ingredientList);

                // Check through each ingredient slot
                foreach (IngredientSlot inputSlots in ingredientSlots)
                {
                    // If the ingredient in the slot matches an item in list, remove item from list
                    bool removeItem = false;
                    int removeIndex = -1;
                    foreach (Ingredient tempIngredient in tempIngredientList)
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
                        tempIngredientList.RemoveAt(removeIndex);
                    }
                }

                // Once through all slots, if list is empty, all items in that recipe are in the pot
                if (tempIngredientList.Count == 0)
                {
                    // Therefore that recipe is makeable with given ingredients, check required effects
                    ingredientsMatchRecipe = true;

                }
            }
            else
            {
                // Otherwise no ingredients required
                ingredientsMatchRecipe = true;
            }
            
            // Check Effects if required
            if (recipe.requiredEffects.Count != 0)
            {
                List<Effect> tempEffectList = new List<Effect>(recipe.requiredEffects);

                // Check each ingredient slot
                foreach (IngredientSlot inputSlots in ingredientSlots)
                {
                    if (inputSlots.ingredient != null)
                    {
                        // If an effect on that item in the slot is required, subtract said effect from the required effect list
                        foreach (Effect inputEffect in inputSlots.ingredient.effectList)
                        {
                            bool reduceEffect = false;
                            int reduceAmount = 0;
                            int reduceIndex = -1;
                            foreach (Effect tempEffect in tempEffectList)
                            {
                                reduceIndex++;
                                if (inputEffect.affectedStat == tempEffect.affectedStat)
                                {
                                    reduceEffect = true;
                                    reduceAmount = inputEffect.effectStrength;
                                    break;
                                }
                            }

                            // If the required effect goes to 0 or less, remove it
                            if (reduceEffect)
                            {
                                // Workaround to reduce value of list item
                                // Clone list item
                                Effect cloneEffect = tempEffectList[reduceIndex];
                                // Modify clone
                                cloneEffect.effectStrength -= reduceAmount;
                                // Replace original with clone if required strength > 0, otherwise remove the element
                                if (cloneEffect.effectStrength > 0)
                                {
                                    tempEffectList[reduceIndex] = cloneEffect;
                                }
                                else
                                {
                                    tempEffectList.RemoveAt(reduceIndex);
                                }

                            }
                        }
                    }

                }

                // If the required effect list is empty, the effect requirement is met
                if (tempEffectList.Count == 0)
                {
                    effectsMatchRecipe = true;
                }
            }
            else
            {
                effectsMatchRecipe = true;
            }
            

            // Check if both requirements are met, if so break loop
            if (ingredientsMatchRecipe && effectsMatchRecipe)
            {
                item = targetRecipe.outputItem;
                Count = 1;
                UpdateGraphic();
                break;
            }


            // Otherwise, check next recipe (continue loop)
        }

        if (ingredientsMatchRecipe && effectsMatchRecipe)
        {
            item = targetRecipe.outputItem;
            Count = 1;
            UpdateGraphic();
            
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
