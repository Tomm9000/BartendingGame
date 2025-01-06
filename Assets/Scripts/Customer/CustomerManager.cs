using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private CustomerBase[] customers;

    
    // Orders Variables
    [SerializeField] private GameObject orderText;
    [SerializeField] private Transform orderParent;
    [SerializeField] private RawImage portraitImage;
    
    [SerializeField] private GameObject recipeText;
    [SerializeField] private Transform recipeParent;
    
    public static CustomerBase customer { get; set; }

    public static List<Drink> openOrders = new List<Drink>();
    private GameObject orderTextObject;
    
    private GameObject currentOrderObject;
    
    private CustomerBase GetRandomPatron()
    {
        return customers[Random.Range(0, customers.Length)];
    }

    public void SpeakTo()
    {
        customer = GetRandomPatron();
        DrinkChecker.customer = customer;
        AddOrderToList(customer);
        portraitImage.texture = customer.customerPortrait;
        InstantiateOrderText(customer);
        DialogueManager.Instance.StartDialogue(customer.name, customer.availableDialogue.RootNode);
        RemoveCustomerFromArray(customer);
    }
    
    private void RemoveCustomerFromArray(CustomerBase customerToRemove)
    {
        List<CustomerBase> customerList = new List<CustomerBase>(customers);

        customerList.Remove(customerToRemove);

        customers = customerList.ToArray();
    }

    private void AddOrderToList(CustomerBase customerBase)
    {
        if (openOrders.Contains(customerBase.order)) 
            openOrders.Remove(customerBase.order);
        else 
            openOrders.Add(customerBase.order);
    }

    public static List<Drink> GetOpenOrders()
    {
        return openOrders;
    }
    
    public void InstantiateOrderText(CustomerBase patron)
    {
        orderTextObject = Instantiate(orderText, orderParent);
        orderTextObject.GetComponent<TextMeshProUGUI>().text = patron.order.name;
    }
}