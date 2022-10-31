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

    public bool alchemyEnabled = false;




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
        //empts
    }
}
