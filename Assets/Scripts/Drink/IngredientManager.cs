using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class IngredientManager : MonoBehaviour
{
    // Ingredient Manager Getter Setter for easier usability of methods in other scripts
    public static IngredientManager ingredientManager { get; set; }
    
    
    [SerializeField] private Image ingredientMeter;
    [SerializeField] private Ingredient ingredient;
    
    
    private bool _isPressed;
    private bool _isPoured = false;
    
    
    private float _ingredientAmount;
    private float _ingredientMultiplier = 1f;
    
    private void Start()
    {
        // Set instance of IngredientManager script
        ingredientManager = this;
    }
    
    private void OnMouseDown()
    {
        _isPressed = true;
    }
    
    private void OnMouseUp()
    {
        _isPressed = false;
    }

    private void Update()
    {
        if (CanPourIngredient())
        {
            PourIngredient();
        }
    }
    
    public void ResetShakerIfEmptied()
    {
        ingredientMeter.fillAmount = 0f;
        _ingredientAmount = 0f;
        Shaker.isBeingEmptied = false;
        _isPoured = false;
    }

    private bool CanPourIngredient()
    {
        return _isPressed && !_isPoured;
    }
    
    private void PourIngredient()
    {
        _ingredientAmount += _ingredientMultiplier * Time.deltaTime;
        ingredientMeter.fillAmount = _ingredientAmount;
        if (Mathf.Approximately(ingredientMeter.fillAmount, 1))
        {
            AddIngredientToList();
            Debug.Log(ingredientMeter.fillAmount);
        }
    }

    private void AddIngredientToList()
    {
        if (!Mathf.Approximately(ingredientMeter.fillAmount, 1)) return;
        Shaker.ShakerManager.AddIngredient(ingredient);
        _isPoured = true;
    }
}
