using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Patron", menuName = "Patron/Patron Base")]
public class CustomerBase : ScriptableObject
{
    public Drink order;
    public String patronName;
    public Dialogue availableDialogue;

    public Texture customerPortrait;
}