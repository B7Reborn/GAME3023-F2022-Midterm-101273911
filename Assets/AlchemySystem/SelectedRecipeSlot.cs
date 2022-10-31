using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class SelectedRecipeSlot : MonoBehaviour
{
    public Recipe selectedRecipe;

    [SerializeField]
    private TMPro.TextMeshProUGUI descriptionText;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameText;

    [SerializeField]
    private TMPro.TextMeshProUGUI ingredientsText;
    [SerializeField]
    private TMPro.TextMeshProUGUI effectsText;

    [SerializeField]
    Image itemIcon;

    [SerializeField] private List<IngredientSlot> requiredIngredientSlots;

    public void UpdateDisplay()
    {
        ClearDisplay();
        nameText.text = selectedRecipe.name;

        foreach (Ingredient ingredient in selectedRecipe.ingredientList)
        {
            ingredientsText.text += $"{ingredient.name}\n";
            foreach (IngredientSlot inputSlot in requiredIngredientSlots)
            {
                if (inputSlot.ingredient == null)
                {
                    inputSlot.ingredient = ingredient;
                    inputSlot.Count = 1;
                    inputSlot.UpdateGraphic();
                    break;
                }
            }
        }

        foreach (Effect effect in selectedRecipe.requiredEffects)
        {
            effectsText.text += $"{effect.affectedStat}";
            if (effect.effectStrength > -1)
            {
                effectsText.text += $" +{effect.effectStrength}\n";
            }
            else
            {
                effectsText.text += $" {effect.effectStrength}\n";
            }
        }

        foreach (Effect itemEffect in selectedRecipe.outputItem.itemEffects)
        {
            descriptionText.text += $"{itemEffect.affectedStat}";
            if (itemEffect.effectStrength > -1)
            {
                descriptionText.text += $" +{itemEffect.effectStrength}\n";
            }
            else
            {
                descriptionText.text += $" {itemEffect.effectStrength}\n";
            }
        }

        itemIcon.sprite = selectedRecipe.outputItem.icon;
        itemIcon.gameObject.SetActive(true);


    }

    private void ClearDisplay()
    {
        nameText.text = "";
        descriptionText.text = "";
        ingredientsText.text = "";
        effectsText.text = "";

        foreach (IngredientSlot inputSlot in requiredIngredientSlots)
        {
            inputSlot.ingredient = null;
            inputSlot.Count = 0;
            inputSlot.UpdateGraphic();
        }
    }
}
