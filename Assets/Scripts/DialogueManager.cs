using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class ChoiceEvent : UnityEvent<Choice> { }

[System.Serializable]
public class ExodusEvent : UnityEvent<Exodus> { }

public class DialogueManager : MonoBehaviour
{

    public ChoiceEvent choiceEvent = new ChoiceEvent();
    public ExodusEvent exodusEvent = new ExodusEvent();
    public Dialogue dialogue;
    public GameObject chatSystem;
    public GameObject character;
    //public GameObject dialogueArea;
    public Text characterNameText;
    public Text chatText;
    public float delay = 0.1f;
    public AudioClip clip;
    public AudioSource mainSource;
    int activeLineIndex;
    bool dialogueStarted;
    GameObject characterInstantiated;
    int timesClicked = 0;

    public void ChangeDialogue(Dialogue nextDialogue)
    {
        dialogueStarted = false;
        dialogue = nextDialogue;
        AdvanceLine();
        
    }
    void Update()
    {
        
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Return)) && chatText.isActiveAndEnabled && Time.timeScale != 0)
        {
            AdvanceLineForButton();
        }
    }
    private void Initialize()
    {
        dialogueStarted = true;
        activeLineIndex = 0;
    }

    public void ShitCode(string sentence)
    {
        chatText.text = "";
        foreach (char letter in sentence)
        {

            chatText.text += letter;
            /*if ((prevLetter == '<' && letter == '/'))
            {
                isTagged = false;
            }
            else if ((prevLetter == '<' && letter == 's') || isTagged)
            {
                isTagged = true;
                if (isTagged)
                {
                    string tempLetter = "";
                    if (letter == '>' || letter == '<')
                        tempLetter = "";
                    else
                        tempLetter += letter;
                    isTagged = true;
                    string hex = color.r.ToString("X2");
                    string M = "<color=#ff0000ff>" + tempLetter + "</color>";
                    chatText.text += M;
                }
                else if (prevLetter == '<' && letter == '/')
                {
                    isTagged = false;
                }
            }
            else
            {
                string tempLetter = "";
                if (letter == '>' || letter == 's' || letter == '<')
                    tempLetter = "";
                else
                    tempLetter += letter;
                chatText.text += tempLetter;
            }
            prevLetter = letter;*/
        }
        }

    public void AdvanceLineForButton() {
        if (dialogue == null) return;
        if (!dialogueStarted) Initialize();

        if (activeLineIndex < dialogue.lines.Length)
        {
            timesClicked++;
            if (timesClicked == 1)
            {
                StopAllCoroutines();
                if (dialogue.lines[activeLineIndex - 1].text != null)
                {
                    if (PlayerPrefs.GetInt("_language_index") == 1)
                        ShitCode(dialogue.lines[activeLineIndex - 1].text);
                    else
                        ShitCode(dialogue.lines[activeLineIndex - 1].englishText);
                }
                
            }
            else if (timesClicked == 2)
            {
                timesClicked = 0;
                DisplayNextSentence();
            }

        }
        else
            AdvanceDialogue();
    }
    public void AdvanceLine()
    {
        if (dialogue == null) return;
        if (!dialogueStarted) Initialize();

        if (activeLineIndex < dialogue.lines.Length)
        {
            
           
                DisplayNextSentence();
            
        }
        else
            AdvanceDialogue();

    }

    private void AdvanceDialogue()
    {
        StopAllCoroutines();
        if (dialogue.choice != null)
        {
            this.gameObject.SetActive(false);
            choiceEvent.Invoke(dialogue.choice);
        }
        else if (dialogue.nextDialogue != null)
        {
            if (dialogue.consequence != null)
                exodusEvent.Invoke(dialogue.consequence);
            ChangeDialogue(dialogue.nextDialogue);
        }
        else
            EndDialogue();
    }

    /*public Text nameText;

    bool option;

    private Queue<string> sentences;
    void Start()
    {
        sentences = new Queue<string>();
    }
    
    public void StartDialogue(Dialogue dialogue, Choice choice)
    {
        if (PlayerPrefs.GetInt("_language_index") == 1)
        {
            nameText.text = dialogue.name;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            
            DisplayNextSentence();
        } else
        {
            choice1.GetComponent<Text>().text = 
            choice2.GetComponent<Text>().text = dialogue.ChoiceTwoEnglishName;

            nameText.text = dialogue.englishName;

            sentences.Clear();

            foreach (string sentence in dialogue)
            {
                sentences.Enqueue(sentence);
            }

            choice.choice1.GetComponent<DialogueTrigger>().TriggerOption1();
            DisplayNextSentence();
        }
    }
    */
    private void DisplayNextSentence()
    {
        clip = dialogue.character.characterVoice;
        
        characterNameText.color = dialogue.character.characterColor;
        Line line = dialogue.lines[activeLineIndex];
        StopAllCoroutines();

        //if (checkIfChanged)
        //    checkIfChanged = false;

        if (PlayerPrefs.GetInt("_language_index") == 1)
        {
            characterNameText.text = dialogue.character.characterName;
            StartCoroutine(TypeSentence(line.text));
        }
        else
        {
            characterNameText.text = dialogue.character.characterNameInEnglish;
            StartCoroutine(TypeSentence(line.englishText));
        }
        activeLineIndex += 1;
    }

    char prevLetter;
    bool isTagged = false;
    Color32 color;
    IEnumerator TypeSentence(string sentence)
    {
        chatText.text = "";
        foreach (char letter in sentence)
        {

            chatText.text += letter;
            mainSource.PlayOneShot(clip);
            yield return new WaitForSeconds(delay);
            /*if ((prevLetter == '<' && letter == '/'))
            {
                isTagged = false;
            }
            else if ((prevLetter == '<' && letter == 's') || isTagged)
            {
                isTagged = true;
                if (isTagged)
                {
                    string tempLetter = "";
                    if (letter == '>' || letter == 's' || letter == '<')
                        tempLetter = "";
                    else
                        tempLetter += letter;
                    isTagged = true;
                    string hex = color.r.ToString("X2");
                    string M = "<color=#ff0000ff>" + tempLetter + "</color>";
                    chatText.text += M;
                    mainSource.PlayOneShot(clip);
                    yield return new WaitForSeconds(delay);
                }
                else if (prevLetter == '<' && letter == '/')
                {
                    isTagged = false;
                }
            }
            else
            {
                string tempLetter = "";
                if (letter == '>' || letter == 's' || letter == '<')
                    tempLetter = "";
                else
                    tempLetter += letter;
                chatText.text += tempLetter;
                mainSource.PlayOneShot(clip);
                yield return new WaitForSeconds(delay);
            }
            prevLetter = letter;*/
        }
        timesClicked = 1;
    }


    void EndDialogue()
    {
        if (dialogue.consequence != null)
            exodusEvent.Invoke(dialogue.consequence);

        dialogue = null;
        dialogueStarted = false;
        chatSystem.SetActive(false);
    }
}
