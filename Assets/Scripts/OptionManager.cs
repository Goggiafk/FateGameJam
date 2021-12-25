using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class DialogueChangeEvent : UnityEvent<Dialogue> { }
public class OptionManager : MonoBehaviour
{
    public Option option;
    public DialogueChangeEvent dialogueChangeEvent;
    public GameObject textArea;


    public static OptionManager AddChoiceButton(Button choiceButtonTemplate, Option option, int index)
    {
        int buttonSpacing = -55;
        if (ChoiceManager.doubleSpacing)
            buttonSpacing *= 2;
        
        Button button = Instantiate(choiceButtonTemplate);

        button.transform.SetParent(choiceButtonTemplate.transform.parent);
        button.transform.localScale = Vector3.one;
        button.transform.localPosition = new Vector3(0, index * buttonSpacing, 0);
        button.name = "Choice " + (index + 1);
        button.gameObject.SetActive(true);

        OptionManager choiceManager = button.GetComponent<OptionManager>();
        choiceManager.option = option;

        ChoiceManager.doubleSpacing = false;

        return choiceManager;
    }

    private void Start()
    {
        if (dialogueChangeEvent == null)
            dialogueChangeEvent = new DialogueChangeEvent();

        if (PlayerPrefs.GetInt("_language_index") == 1)
            GetComponent<Button>().GetComponentInChildren<Text>().text = option.text;
        else
            GetComponent<Button>().GetComponentInChildren<Text>().text = option.textInEnglish;
    }

    public void MakeChoice()
    {
        textArea.SetActive(true);
        dialogueChangeEvent.Invoke(option.dialogue);
    }
}
