using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDrink", menuName = "Bar/Drink")]
public class Drink : ScriptableObject
{
    public string drinkName;
    
    public Ingredient[] Ingredients;
    
    public float Price;
}
