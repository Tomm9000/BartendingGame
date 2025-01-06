using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // DialogueManager instance getter setter
    public static DialogueManager Instance { get; private set; }
    
    // Dialogue Variables 
    [SerializeField] private GameObject DialogueParent;
    [SerializeField] private TextMeshProUGUI DialogueTitleText, DialogueBodyText;
    [SerializeField] private GameObject responseButtonPrefab;
    [SerializeField] private Transform responseButtonContainer;

    private DialogueNode savedNode;

    private void Awake()
    {
        // Check if there is already an instance of this manager
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        // Hide the dialogue
        HideDialogue();
    }

    public void StartDialogue(string title, DialogueNode node)
    {
        savedNode = node;
        // Display the dialogue
        ShowDialogue();
        // Set the dialogue title and body text
        DialogueTitleText.text = title;
        DialogueBodyText.text = node.dialogueText;
        
        
        DrinkChecker.isSatisfied = node.isSatisfied;
        
        // Remove any existing buttons in the container
        foreach (Transform child in responseButtonContainer) Destroy(child.gameObject);

        // Loop through each response and create a button
        foreach (DialogueResponse response in node.responses)
        {
            // Create a new button and set its text
            GameObject buttonObject = Instantiate(responseButtonPrefab, responseButtonContainer);
            buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = response.responseText;

            // Capture the current response to prevent closure issues
            DialogueResponse currentResponse = response;

            // Add an onClick listener for the button
            buttonObject.GetComponent<Button>().onClick.AddListener(() => SelectResponse(currentResponse, title));
        }
    }

    public void ServeDrink(CustomerManager customerManager)
    {
        DialogueBodyText.text = savedNode.recievedOrderResponse;
        GameObject nextCustomerButton = Instantiate(responseButtonPrefab, responseButtonContainer);
        nextCustomerButton.GetComponentInChildren<TextMeshProUGUI>().text = "Next Customer";
        
        nextCustomerButton.GetComponent<Button>().onClick.AddListener(() => customerManager.SpeakTo());
    }

    private void SelectResponse(DialogueResponse response, string title)
    {
        if (response.nextNode != null)
        {
            StartDialogue(title, response.nextNode);
        }
        else
        {
            HideDialogue();
        }
    }

    private void HideDialogue()
    {
        DialogueParent.SetActive(false);
    }
    
    private void ShowDialogue()
    {
        DialogueParent.SetActive(true);
    }
}
