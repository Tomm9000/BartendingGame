using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DrinkChecker : MonoBehaviour
{
    [SerializeField] private List<Drink> _openOrders;
    [SerializeField] private GameObject satisfiedCustomerText;
    [SerializeField] private Transform satisfiedCustomerTextParent;
    
    [SerializeField] private GameObject dissatisfiedCustomerText;
    [SerializeField] private Transform dissatisfiedCustomerTextParent;

    [SerializeField] public CustomerManager _CustomerManager;
    [SerializeField] public DialogueManager _DialogueManager;

    public static bool isSatisfied { get; set; }
    public static string orderRecievedText { get; set; }
    public static CustomerBase customer { get; set; }

    private void Update()
    {
        _openOrders = CustomerManager.GetOpenOrders();
    }

    public void IsCorrectDrink()
    {
        print(Shaker.MixedDrink + "Test");
        if (_openOrders.Contains(Shaker.MixedDrink))
        {
            _DialogueManager.ServeDrink(_CustomerManager);
            if (isSatisfied)
                InstantiateFeedbackText(customer, satisfiedCustomerText, satisfiedCustomerTextParent);

            else
                InstantiateFeedbackText(customer, dissatisfiedCustomerText, dissatisfiedCustomerTextParent);
        }
    }
    
    private void InstantiateFeedbackText(CustomerBase patron, GameObject textPrefab, Transform parent)
    {
        var feedbackTextObject = Instantiate(textPrefab, parent);
        feedbackTextObject.GetComponent<TextMeshProUGUI>().text = patron.name;
    }
}
