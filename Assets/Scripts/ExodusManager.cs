
using System.Collections;
using System.IO;
using TMPro;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExodusManager : MonoBehaviour
{
    [Header("All the references")]

    public DialogueManager dialmanager;

    public static ExodusManager Instance;
    public ChoiceManager choiceManager;
    //public TMP_InputField field;
    public Exodus exodus;
    public Sprite dialSprite;

    

   // public AudioClip endingClip;
    public AudioSource musicSource;
    public AudioSource effectSource;


    [Header("Character & Atributes Lists")]

    public Dialogue[] dialogues;
    public AudioClip[] audioStuff;
    public GameObject[] atributes;
    public GameObject[] allCharacters;
    public string[] characterNames;
    public GameObject[] characters;
    public GameObject[] storyCharacters = new GameObject[10];
    int storyCharacterId = 0;
    public GameObject[] exodusCharacters = new GameObject[10];
    int exodusCharacterId = 0;
    public GameObject currentCharacter;
    //int idOfRest = 0;
    //private string[] savedRest = new string[100];


    [Header("Some variables")]

    public static int peopleDead = 0;

    int currentDay = 0;

    int charactersToSpawn = 1;
    bool toSpawn = true;
    bool toHide = false;
    Color c;
    Image rend;

    public string[] eventCounters = new string[100];
    public int[] whenToAppear = new int[100];
    public int[] eventInts = new int[100];
    int idOfEventCounter = 0;

    [Header("UI Elements")]

    public GameObject endScreen;

    public GameObject[] cum = new GameObject[100];

    public Text dayCounter;

    public Button dayOverButton;

    public GameObject ui;
    public GameObject dayOverMenu;


    int idKok = 0;
    private string[] eventHolder = new string[999];
    private byte[] eventHolderId = new byte[999];

    
    void Awake()
    {
        /*if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad (gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (PlayerPrefs.HasKey("LoadScene"))
        {

            dayOverMenu.SetActive(true);   
            Start();
            LoadAll(PlayerPrefs.GetString("LoadScene"));
            PlayerPrefs.DeleteKey("LoadScene");
        }*/
    }
    public void Change(Exodus _exodus)
    {
        exodus = _exodus;
        Initialize();
    }


    private void Initialize()
    {
        //PlayerPrefs.SetInt(exodus.nameOfExodus, exodus.exodusInt);
        toSpawn = exodus.spawnNext;
        toHide = exodus.HideCurrentCharacter;
        eventHolder[idKok] = exodus.nameOfExodus;
        eventHolderId[idKok] = exodus.exodusInt;
        idKok += 1;
        CheckEvents();
    }

    public void CleanEvent(string exod)
    {
        for (int i = 0; i < idKok; i++)
        {
            if (exod == eventHolder[i]) { 
                eventHolder[i] = null;
                eventHolderId[i] = 0;
            }
        }
    }

    public void CreateExodus(string exodus)
    {
        idKok++;
        eventHolder[idKok] = exodus;
        CheckEvents();
    }
    public void CleanEvents()
    {
        for (int i = 0; i < eventHolder.Length; i++)
        {
            eventHolder[i] = null;
            eventHolderId[i] = 0;
            idKok = 0;
        }
    }
    /// <summary>
    /// //////////////////////////////////// START
    /// </summary>
    public void Start()
    {
        PlayerPrefs.SetInt("_language_index", 1);

        rend = endScreen.GetComponent<Image>();
        c = rend.color;
        c.a = 0f;
        rend.color = c;

        dayOverButton.interactable = false;

        atributes[2].SetActive(true);
        atributes[1].SetActive(true);
        atributes[0].SetActive(false);

        if (currentDay < 1)
        {
            NextDay();
        }

        
    }
    /// <summary>
    /// ////////////////////////////////////// EXODUSES
    /// </summary>




    /*public void addRest(GameObject _object)
    {
        savedRest[idOfRest] = _object.name;
        idOfRest++;
    }
    public void removeRest(GameObject _object)
    {
        for (int i = 0; i < idOfRest; i++)
        {
           if( savedRest[i] == _object.name)
            {
                savedRest[i] = null;
            }
        }
    }
    */
    private void CheckEvents()
    {


        for (int i = 0; i < eventHolder.Length; i++)
        {


            switch (eventHolder[i])
            {
                case "startGame":
                    effectSource.PlayOneShot(audioStuff[0]);
                    musicSource.Stop();
                    musicSource.clip = audioStuff[1];
                    
                    atributes[5].GetComponent<Image>().sprite = dialSprite;
                    atributes[3].SetActive(true);
                    StartCoroutine(Timer(1.5f, () => { atributes[3].SetActive(false); atributes[4].SetActive(true); dialmanager.ChangeDialogue(dialogues[0]); musicSource.Play(); }));
                    atributes[0].SetActive(true);
                    atributes[1].SetActive(false);
                    atributes[2].SetActive(false);
                    break;
                case "GirlAppear":
                    atributes[7].SetActive(true);
                    break;
                case "GirlDisappear":
                    atributes[7].SetActive(false);
                    break;
                case "Henry":
                    eventCounters[idOfEventCounter] = "old";
                    switch (eventHolderId)
                    {
                        default:
                            whenToAppear[idOfEventCounter] = currentDay + 1;
                            break;
                    }
                    
                    break;
            }

        }

        CleanEvents();


        if (toHide)
        {
            //StartCoroutine(Timer(0.8f, () => { currentCharacter.SetActive(false); atributes[6].SetActive(false); }));
            currentCharacter.SetActive(false); atributes[6].SetActive(false);
        }
        if (toSpawn)
        {
            //StartCoroutine(JustFade(() => { SpawnCharacter(); }));
            SpawnCharacter();
        }
    }


    public void addAb(string key)
    {
        ChoiceManager.optionList[ChoiceManager.optionId] = key;
        ChoiceManager.optionNum[ChoiceManager.optionId] = 1;
        ChoiceManager.optionId++;
    }

    public void DownParams()
    {
        toSpawn = false;
    }

    public void UpParams()
    {
        toSpawn = false;
    }

    /// <summary>
    /// ///////////////////CHARACTER SPAWN
    /// </summary>

    private void StoryCharacter()
    {
        Debug.Log(storyCharacterId);
        storyCharacterId--;

        currentCharacter = storyCharacters[storyCharacterId];
        storyCharacters[storyCharacterId] = null;
        
        currentCharacter.SetActive(true);
        
    }

    private void ExodusCharacter()
    {

        currentCharacter = exodusCharacters[exodusCharacterId];
        currentCharacter.SetActive(true);
    }
    public void SpawnCharacter()
    {
        atributes[6].SetActive(true);
        if (exodusCharacterId > 0)
        {
            exodusCharacterId--;
            ExodusCharacter();
        } else 
        if (storyCharacterId > 0)
        {
            
            StoryCharacter();
        }
        else if (!(characters.Length <= 0) && charactersToSpawn > 1)
        {
            charactersToSpawn--;
            RandomCharacter();
        } else
        {
            dayOverButton.interactable = true;
        }
    }

    public void RandomCharacter()
    {
        
        /*var randomCharacters = Resources.FindObjectsOfTypeAll<GameObject>();
        
        for (int i = 0; i < randomCharacters.Length; i++)
        {
            if (ra == characterNames[m] && child.tag == "Character")
            {
                temporaryCharacter[m] = child.gameObject;
                Debug.Log(temporaryCharacter[m]);
                m++;
            }
        }
        */
        int random = Random.Range(0, characters.Length);
        characters[random].SetActive(true);
        currentCharacter = characters[random];
        characters[random] = null;
        
        for (int i = random; i < characters.Length; i++)
        {
            if ((i + 1) >= characters.Length || characters[i + 1] == null)
                break;
            else
                characters[i] = characters[i + 1];
            characters[i + 1] = null;
        }
        GameObject[] tempCharacters = characters;
        characters = new GameObject[tempCharacters.Length - 1];
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] = tempCharacters[i];
        }

        for (int i = 0; i < characterNames.Length; i++)
        {
            characterNames[i] = null;
        }
        for (int i = 0; i < characters.Length; i++)
        {
            characterNames[i] = characters[i].name;
        }
    }

    /// <summary>
    /// ////////////////////ITEM SYSTEM
    /// </summary>
    



    /// <summary>
    /// ////////////////////UPGRADE SYSTEM
    /// </summary>



   
    /// <summary>
    /// ///////////////////DAY SYSTEM
    /// </summary>

    public void DayOver()
    {
        dayOverButton.interactable = false;
        //SaveAll(true);
    }
    public void UdpdateDay()
    {
        dayCounter.text = currentDay.ToString();
    }

    public void clearCounter(int id)
    {
        eventCounters[id] = null;
        whenToAppear[id] = 0;
        eventInts[id] = 0;
    }

    public void NextDay()
    {
        charactersToSpawn = 1;
        currentDay++;
       

        for (int i = 0; i < idOfEventCounter; i++)
        {
            Debug.Log(eventCounters[i]);


            if (whenToAppear[i] == currentDay)
            {
                switch (eventCounters[i])
                {

                    case "old":
                        atributes[8].SetActive(true);
                        storyCharacters[storyCharacterId] = allCharacters[1];
                        storyCharacterId++;
                        break;
                }
            }
        }
        switch (currentDay)
        {

            default:
                dialmanager.ChangeDialogue(dialogues[0]);
                break;
            case 1:
                 break;
        }
        
        
        dayCounter.text = currentDay.ToString();
    }

    /// <summary>
    /// ///////////////////FADING
    /// </summary>
    
    /*IEnumerator JustFade(System.Action action)
    {
        endScreen.SetActive(true);
        for (float f = 0.05f; f <= 1; f += 0.1f)
        {
            c = rend.color;
            c.a = f;
            rend.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        endScreen.SetActive(false);
        action();
        StartCoroutine(JustUnFade());
    }
    IEnumerator JustUnFade()
    {
        endScreen.SetActive(true);
        for (float f = 1; f >= 0; f -= 0.1f)
        {
            c = rend.color;
            c.a = f;
            rend.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        endScreen.SetActive(false);
    }

    IEnumerator Fade()
    {
        ui.SetActive(false);
        endScreen.SetActive(true);
        for(float f = 0.05f; f <= 1; f += 0.05f)
        {
            c = rend.color;
            c.a = f;
            rend.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }


    IEnumerator FadeToMenu()
    {
        c.a = 0f;
        endScreen.SetActive(true);
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            c = rend.color;
            c.a = f;
            rend.color = c;
            yield return new WaitForSeconds(0.2f);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }*/
    IEnumerator Timer(float time, System.Action action)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        action();
    }

    /// <summary>
    /// ////////////////// SAVE & LOAD SECTION
    /// </summary>
   

    public void DelayBeforeLoad(string path)
    {
        PlayerPrefs.SetString("LoadScene", path);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void DeleteSave(string path)
    {
        File.Delete(path);
    }
    /*
    public void LoadAll(string path)
    {
        SaveData.current = (SaveData)Serialization.Load(path);
        storyCharacterId = SaveData.current.stotyCharacterIdS;
        moneyAmmount = SaveData.current.money;
        sawDustAmmount = SaveData.current.sawDust;
        happinesAmmount = SaveData.current.happines;
        developmentAmmount = SaveData.current.development;
        eventHolder = SaveData.current.eventHolderS;
        eventHolderId = SaveData.current.eventHolderIdS;
        idKok = SaveData.current.idKokS;
        characterNames = SaveData.current.characterNamesS;
        idOfRest = SaveData.current.idOfRestS;
        savedRest = SaveData.current.savedRestS;
        currentDay = SaveData.current.currentDayS;
        idOfUpgrade = SaveData.current.idOfUpgradeS;

        peopleAgainst = SaveData.current.peopleAgainstS;
        relationWithMex = SaveData.current.relationWithMexS;
        moneyToMex = SaveData.current.moneyToMexS;

        ChoiceManager.optionList = SaveData.current.optionListS;
        ChoiceManager.optionNum = SaveData.current.optionNumS;
        ChoiceManager.optionId = SaveData.current.optionIdS;
        

        idOfEventCounter = SaveData.current.idOfEventCounterS;
        eventCounters = SaveData.current.eventCountersS;
        whenToAppear = SaveData.current.whenToAppearS;
        eventInts = SaveData.current.eventIntsS;

        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] = null;
        }

        GameObject[] temporaryCharacter = new GameObject[100];

        var children = Resources.FindObjectsOfTypeAll<GameObject>();
        

        int m = 0;
        int n = 0;
        int l = 0;
        int o = 0;
            foreach (var child in children)
            {

            for (int i = 0; i < savedRest.Length; i++)
            {
                if (child.name == savedRest[i] && child.tag == "Item")
                {
                    child.SetActive(true);
                    
                }
            }
            Debug.Log("id:" + idOfUpgrade);
            for (int i = 0; i < idOfUpgrade; i++)
            {
                Debug.Log(upgradedObjects[i]);

                if (child.name == SaveData.current.upgradeObject[i] && child.tag == "Item")
                {
                    RelistUpgrades(SaveData.current.upgradeName[i], SaveData.current.englishUpgradeName[i], SaveData.current.upgradePrice[i], child, i);
                    
                }
            }
            
            }
        foreach (var child in children)
        {
            for (int i = 0; i < characterNames.Length && characterNames[i] != null; i++)
            {
                if (child.name == characterNames[i] && child.tag == "Character")
                {
                    characters[i] = child.gameObject;
                }
            }
        }
        int randAm = 0;
        
        for (int i = 0; i < characters.Length; i++)
        {
            if(characters[i] != null)
                randAm++;
        }

        GameObject[] randCharacters = new GameObject[randAm];

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] != null)
                randCharacters[i] = characters[i];
        }

        characters = randCharacters;

        for (int i = 0; i < SaveData.current.numberOfItems; i++)
        {
            itemList[i].name = SaveData.current.itemName[i];
            itemList[i].englishName = SaveData.current.englishItemName[i];
            itemList[i].description = SaveData.current.itemDescription[i];
            itemList[i].englishDescription = SaveData.current.englishItemDescription[i];
            string nameOfSprite = SaveData.current.spriteName[i];
            itemList[i].cost = SaveData.current.itemCost[i];
            itemList[i].type = SaveData.current.itemSellType[i];
            adTheGet(itemList[i].name, itemList[i].englishName, itemList[i].description, itemList[i].englishDescription, nameOfSprite, itemList[i].cost, itemList[i].type);
        }



        toSpawn = false;
        CheckEvents();
        ReloadUI();
        allCharacters[4].SetActive(false);
        dayCounter.text = currentDay.ToString();
        buttonsPop.SetActive(false);
    }

    public GameObject prefab;
    public string[] saveFiles;
    public GameObject loadArea;
    public void loadSave()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/WolfAdventuresSaves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/WolfAdventuresSaves");
        }

        saveFiles = Directory.GetFiles(Application.persistentDataPath + "/WolfAdventuresSaves");

        for (int i = 0; i < saveFiles.Length; i++)
        {
            GameObject cos = Instantiate(prefab);
            cos.transform.SetParent(loadArea.transform, false);
            cos.transform.localScale = Vector3.one;
            cos.transform.localPosition = new Vector3(0, i * (-30), 0);

            var index = i;
            cos.GetComponent<Button>().onClick.AddListener(() =>
            {
                DelayBeforeLoad(saveFiles[index]);
            });
            cos.GetComponentInChildren<Text>().text = saveFiles[index].Replace(Application.persistentDataPath + "/WolfAdventuresSaves", "");
        }
        Destroy(prefab);
    }
    public void SaveAll(bool auto)
    {
        var objCharacters = GameObject.FindGameObjectsWithTag("Character");
        for (int i = 0; i < objCharacters.Length; i++)
        {
            SaveData.current.savedCharactersS[i] = objCharacters[i].name;
        }
        SaveData.current.stotyCharacterIdS = storyCharacterId;
        SaveData.current.money = moneyAmmount;
        SaveData.current.sawDust = sawDustAmmount;
        SaveData.current.happines = happinesAmmount;
        SaveData.current.development = developmentAmmount;
        SaveData.current.eventHolderS = eventHolder;
        SaveData.current.eventHolderIdS = eventHolderId;
        SaveData.current.characterNamesS = characterNames;
        SaveData.current.idKokS = idKok;
        SaveData.current.savedRestS = savedRest;
        SaveData.current.idOfRestS = idOfRest;
        SaveData.current.currentDayS = currentDay;
        SaveData.current.idOfUpgradeS = idOfUpgrade;

        SaveData.current.moneyToMexS = moneyToMex;
        SaveData.current.relationWithMexS = relationWithMex;
        SaveData.current.peopleAgainstS = peopleAgainst;

        SaveData.current.numberOfItems = idOfItem;
        SaveData.current.optionListS = ChoiceManager.optionList;
        SaveData.current.optionIdS = ChoiceManager.optionId;
        SaveData.current.optionNumS = ChoiceManager.optionNum;


        SaveData.current.idOfEventCounterS = idOfEventCounter;
        SaveData.current.eventCountersS = eventCounters;
        SaveData.current.whenToAppearS = whenToAppear;
        SaveData.current.eventIntsS = eventInts;

        for (int i = 0; i < idOfItem; i++)
        {
            Debug.Log(info[i].name);
 
                SaveData.current.itemName[i] = itemList[i].name;
                SaveData.current.englishItemName[i] = itemList[i].englishName;
                SaveData.current.itemDescription[i] = itemList[i].description;
                SaveData.current.englishItemDescription[i] = itemList[i].englishDescription;
                SaveData.current.spriteName[i] = itemList[i].itemSprite.name;
                SaveData.current.itemCost[i] = itemList[i].cost;
                SaveData.current.itemSellType[i] = itemList[i].type;
            
        }
        SaveData.current.upgradeId = idOfUpgrade;

        Debug.Log(idOfUpgrade);
        for (int i = 0; i < idOfUpgrade; i++)
        {

            Debug.Log(info[i].name);
            if (info[i].upgradeObject != null)
            {
                SaveData.current.upgradeName[i] = info[i].name;
                SaveData.current.englishUpgradeName[i] = info[i].englishName;
                SaveData.current.upgradePrice[i] = info[i].price;
                SaveData.current.upgradeObject[i] = info[i].upgradeObject.name;
            }
        }
        
        if (auto)
        {
            if (PlayerPrefs.GetInt("_language_index") == 1)
                SaveSerializable("Автосейв");
            else
                SaveSerializable("AutoSave");
        }
        else
            SaveSerializable(field.text);
    }

    public void SaveSerializable(string name) {
        Serialization.Save(name, SaveData.current);
    }*/

}
