using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shaker : MonoBehaviour
{
    public static Shaker ShakerManager { get; set; }
    public static Drink MixedDrink { get; set; }
    
    public static bool isBeingEmptied { get; set; }
    
    [SerializeField] private Image mixMeter;
    [SerializeField] private Drink[] drinks;
    [SerializeField] private TextMeshProUGUI mixedDrinkText;

    private List<Ingredient> _ingredients = new List<Ingredient>();
    
    private bool _isButtonDown;
    private float _shakeTime;
    private readonly float _shakeMultiplier = 1f;

    private void Start()
    {
        ShakerManager = this;
        isBeingEmptied = false;
    }

    private void OnMouseDown()
    {
        _isButtonDown = true;
    }

    private void OnMouseUp()
    {
        _isButtonDown = false;
    }

    private void Update()
    {
        if (_isButtonDown)
        {
            _shakeTime += _shakeMultiplier * Time.deltaTime;
            IncreaseMixAmount(_shakeTime);
            MixedDrink = FindMatchingIngredients(_ingredients);
            mixedDrinkText.text = MixedDrink.name;
            Debug.Log(MixedDrink);
        }

        print(MixedDrink);
    }

    private void IncreaseMixAmount(float shakeTime)
    {
        mixMeter.fillAmount = shakeTime;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        _ingredients.Add(ingredient);
    }

    private Drink FindMatchingIngredients(List<Ingredient> ingredientsToCheck)
    {
        return drinks.FirstOrDefault(drink => AreIngredientsMatching(drink.Ingredients, ingredientsToCheck));
    }

    private bool AreIngredientsMatching(Ingredient[] drinkIngredients, List<Ingredient> ingredientsToCheck)
    {
        if (drinkIngredients.Length != ingredientsToCheck.Count) return false;
        return drinkIngredients.All(ingredients => ingredientsToCheck.Contains(ingredients));
    }

    public void EmptyShaker()
    {
        _ingredients.Clear();
        mixMeter.fillAmount = 0f;
        _shakeTime = 0f;
        mixedDrinkText.text = "";
        isBeingEmptied = true;
    }
}
