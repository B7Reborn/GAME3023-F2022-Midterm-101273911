using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attribute which allows right click->Create
[CreateAssetMenu(fileName = "New Ingredient", menuName = "Alchemy/New Ingredient")]
public class Ingredient : ScriptableObject
{
    public Sprite icon;
    public string description = "";
    public bool isConsumable = false;
    [Range(1, 6)]
    public int numEffects = 1;
}
