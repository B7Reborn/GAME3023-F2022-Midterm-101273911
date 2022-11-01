using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attribute which allows right click->Create
[CreateAssetMenu(fileName = "New Ingredient", menuName = "Alchemy/New Ingredient", order = 2)]
public class Ingredient : ScriptableObject
{
    public Sprite icon;
    public string description = "";
    public List<Effect> effectList;


}
