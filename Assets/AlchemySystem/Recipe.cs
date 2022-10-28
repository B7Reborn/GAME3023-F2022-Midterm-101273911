using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attribute which allows right click->Create
[CreateAssetMenu(fileName = "New Recipe", menuName = "Alchemy/New Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    public bool isUnique = false; // Unique recipes have specific effects, can be different/stronger than effects of generic recipes

    // The recipe can require specific named ingredients
    public bool requiresIngredients;
    public Ingredient[] ingredientList;

    // The recipe can also require a specific effect strength
    public bool requiresEffect;
    public Effect requiredEffect;
    public int effectStrength;

    
    

}
