using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alchemy : MonoBehaviour
{
    [SerializeField] 
    private TMPro.TextMeshProUGUI screenText;
    [SerializeField] 
    private TMPro.TextMeshProUGUI toggleText;
    [SerializeField] 
    private GameObject characterScreen;
    [SerializeField] 
    private GameObject alchemyScreen;
    [SerializeField] 
    private GameObject ingredientsScreen;
    [SerializeField] 
    private GameObject recipesScreen;

    public bool alchemyEnabled = false;
    public bool recipeScreen = false;

    


    public void MenuScreenSwapper()
    {
        // Change the menu text so the player knows which screen they are on
        string leftText = screenText.text;
        string rightText = toggleText.text;
        toggleText.text = leftText;
        screenText.text = rightText;

        // Swap which menu screen is enabled
        characterScreen.SetActive(alchemyEnabled);
        alchemyScreen.SetActive(!alchemyEnabled);

        // Invert the alchemyEnabled bool so screens can be swapped back
        alchemyEnabled = !alchemyEnabled;
        
    }

    public void OpenRecipeScreen()
    {
        if (recipeScreen == false)
        {
            ingredientsScreen.SetActive(false);
            recipesScreen.SetActive(true);
            recipeScreen = !recipeScreen;
        }
    }

    public void OpenIngredientScreen()
    {
        if (recipeScreen == true)
        {
            ingredientsScreen.SetActive(true);
            recipesScreen.SetActive(false);
            recipeScreen = !recipeScreen;
        }
    }
}
