using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public struct Option
{
    [TextArea(2, 5)]
    public string text;
    [TextArea(2, 5)]
    public string textInEnglish;
    public Dialogue dialogue;
}

[CreateAssetMenu(fileName = "New Question", menuName = "Question")]
public class Choice : ScriptableObject
{
    public Character character;
    [TextArea(2, 5)]
    public string text;
    [TextArea(2, 5)]
    public string textInEnglish;
    
    public Option[] options;
}