using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public Choice choice;
    public Text choiceText;
    public Button optionButton;

    public ExodusManager exodusManager;

    public static string[] optionList = new string[10];
    public static int[] optionNum = new int[10];
    public static int optionId = 0;



    public static bool doubleSpacing = false;

    public Dialogue[] endDialogues;

    OptionManager mexDial = null;

    private List<OptionManager> optionManagers = new List<OptionManager>();

    public void repeatChange(Choice _choice)
    {
        RemoveChoices();
        choice = _choice;
        gameObject.SetActive(true);
        Initialize();
    }
    public void Change(Choice _choice)
    {
        RemoveChoices();
        choice = _choice;
        gameObject.SetActive(true);
        Initialize();
        repeatChange(choice);
    }

    public void Hide(Dialogue dialogue)
    {
        RemoveChoices();
        gameObject.SetActive(false);
    }

    private void RemoveChoices()
    {
        foreach (OptionManager c in optionManagers)
            Destroy(c.gameObject);

        optionManagers.Clear();
    }

    private void Initialize()
    {
       // if (PlayerPrefs.GetInt("_language_index") == 1)
            choiceText.text = choice.text;
        //else
          //  choiceText.text = choice.textInEnglish;

        choiceText.color = choice.character.characterColor;
        
        for(int index = 0; index < choice.options.Length; index++)
        {
            OptionManager c = OptionManager.AddChoiceButton(optionButton, choice.options[index], index);
            optionManagers.Add(c);
        }

        



        

        for (int i = 0; i <= optionId; i++)
        {
            Debug.Log(optionList[i]);
        }

            for (int i = 0; i <= optionId; i++)
        {
            Debug.Log(optionList[i]); 
            switch (optionList[i])
            {
              
                case "help":
                    Option help;
                    help.text = "Позвать на помощь";
                    help.textInEnglish = "Call for help";
                    help.dialogue = endDialogues[0];
                    doubleSpacing = true;
                    switch (choice.text)
                    {

                       case "Как мне выбраться?":
                            OptionManager helpM = OptionManager.AddChoiceButton(optionButton, help, choice.options.Length);
                            optionManagers.Add(helpM);
                            break;

                        case "Нужно что-то делать":
                            mexDial = OptionManager.AddChoiceButton(optionButton, help, choice.options.Length);
                            optionManagers.Add(mexDial);
                            break;
                        case "Кажется, это конец":
                            mexDial = OptionManager.AddChoiceButton(optionButton, help, choice.options.Length);
                            optionManagers.Add(mexDial);
                            break;
                    }
                    break;

            }
            
        }

        
            
        optionButton.gameObject.SetActive(false);
    }
}
