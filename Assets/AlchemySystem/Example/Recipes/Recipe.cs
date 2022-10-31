using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attribute which allows right click->Create
[CreateAssetMenu(fileName = "New Recipe", menuName = "Alchemy/New Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    public Sprite icon;

    // The recipe can require specific named ingredients
    public List<Ingredient> ingredientList;

    // The recipe can also require specific effects/strengths
    public List<Effect> requiredEffects;

    // When crafted the recipe makes an item
    public Item outputItem;



}
