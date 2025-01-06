using System.Collections.Generic;

[System.Serializable]
public class DialogueNode
{
    public string dialogueText;
    public List<DialogueResponse> responses;

    public string recievedOrderResponse;
    
    public bool isSatisfied;

    internal bool IsLastNode()
    {
        return responses.Count <= 0;
    }
}