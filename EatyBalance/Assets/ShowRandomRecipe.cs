using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRandomRecipe : MonoBehaviour
{
    [SerializeField] private GameObject[] recipes;

    private int currentIndex = 0;

    private void OnEnable()
    {
        if (recipes.Length == 0) return;

        foreach (var recipe in recipes)
        {
            recipe.SetActive(false);
        }

        recipes[currentIndex].SetActive(true);

        currentIndex++;
        if (currentIndex >= recipes.Length)
        {
            currentIndex = 0;
        }
    }
}
