using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attribute which allows right click->Create
[CreateAssetMenu(fileName = "New Recipe", menuName = "Alchemy/New Recipe")]
public class Recipe : ScriptableObject
{
    public bool isUnique = false; // Unique recipes have specific effects, can be different/stronger than effects of generic recipes
    

}
