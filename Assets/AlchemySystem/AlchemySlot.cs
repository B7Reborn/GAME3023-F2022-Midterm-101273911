using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlchemySlot : MonoBehaviour
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
    private Item genericPotion;


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
        // Create the made potion
        Item newPotion = genericPotion;
        foreach (IngredientSlot slot in ingredientSlots)
        {
            if (slot.ingredient != null)
            {
                foreach (Effect effect in slot.ingredient.effectList)
                {
                    newPotion.itemEffects.Add(effect);
                }
            }
        }

        item = newPotion;
        Count = 1;
        // asdf
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
