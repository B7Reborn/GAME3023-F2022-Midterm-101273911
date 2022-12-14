using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Holds reference and count of items, manages their visibility in the Inventory panel
public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item = null;
    private CharacterStatus playerStatus;

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


    // Start is called before the first frame update
    void Start()
    {
        playerStatus = FindObjectOfType<CharacterStatus>();
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



    public void UseItemInSlot()
    {
        if (CanUseItem())
        {
            item.Use(playerStatus);
            if (item.isConsumable)
            {
                Count--;
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
                    descriptionText.text += $" {effect.effectStrength}";
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(item != null)
        {
            descriptionText.text = "";
            nameText.text = "";
        }
    }
}
