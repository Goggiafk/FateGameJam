/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public string englishName;
    [TextArea(3, 10)]
    public string[] sentences;
    [TextArea(3, 10)]
    public string[] sentencesInEnglish;
}*/

using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Line
{
    [TextArea(2, 5)]
    public string text;
    [TextArea(2, 5)]
    public string englishText;
}


[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Dialogue : ScriptableObject
{
    public Character character;
    public Line[] lines;
    public Choice choice;
    public Dialogue nextDialogue;
    public Exodus consequence;
}