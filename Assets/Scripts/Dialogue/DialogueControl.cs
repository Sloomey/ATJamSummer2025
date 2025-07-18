using UnityEngine;
using Ink.Runtime;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Mono.Cecil.Cil;
using UnityEngine.SceneManagement;

public class DialogueControl : MonoBehaviour
{
    public TextAsset inkFile;
    public GameObject textBox;
    public GameObject customButton;
    public GameObject optionPanel;

    static Story story;
    TextMeshProUGUI message;
    static Choice choiceSelected;

    private bool dialogueOpen;

    public bool IsDialogueOpen()
    {
        return dialogueOpen;
    }

    private void Start()
    {
        story = new Story(inkFile.text);
        ConnectDialogueFunctions();

        message = textBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        choiceSelected = null;

        dialogueOpen = false;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (story.canContinue)
            {
                AdvanceDialogue();
            }
            else if (!story.canContinue && textBox.activeSelf && story.currentChoices.Count <= 0) // Seeing if the textbox is still on screen
            {
                CloseDialogue();
            }
        }
    }

    public Quest GetQuestByString(string str)
    {

        QuestManager _qc = GameObject.FindAnyObjectByType<QuestManager>();
        for (int i = 0; i < _qc.activeQuests.Count; i++)
        {
            if (_qc.activeQuests[i].QID == str)
            {
                return _qc.activeQuests[i];
            }
        }
        Debug.Log("NO QUEST RETURNED");
        return null;
    }

    private void ConnectDialogueFunctions()
    {
        story.BindExternalFunction("TestFunction", (string name) => {
            Debug.Log(name);
        });
        story.BindExternalFunction("StartQuest", (string quest) => {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().StartQuest(quest);
        });
        story.BindExternalFunction("EndQuest", (string quest) => {
            Quest tempQ = GetQuestByString(quest);
            tempQ.finished = true;
        });
        story.BindExternalFunction("IsQuestFinished", (string quest) => {
            Quest tempQ = GetQuestByString(quest);
            if (tempQ == null)
            {
                return false;
            }
            if (tempQ.finished)
            {
                return true;
            }
            return false;
        });
        story.BindExternalFunction("IsQuestActive", (string questName) =>
        {

            QuestManager _qc = GameObject.FindAnyObjectByType<QuestManager>();

            bool questActive = false;

            for (int i = 0; i < _qc.activeQuests.Count; i++)
            {
                if (_qc.activeQuests[i].QID == questName)
                {
                    questActive = true;
                }
            }

            if (questActive)
            {
                return true;
            }
            else
            {
                return false;
            }

        });
        story.BindExternalFunction("SitDown", () => {
            GameObject.FindFirstObjectByType<ClassroomControl>().SitDown();
        });
        story.BindExternalFunction("EndGame", () => {
            GameObject.FindFirstObjectByType<PicnicControl>().EndGame();
        });
    }
    public void OpenDialogue()
    {
        if (!dialogueOpen)
        {
            dialogueOpen = true;

            story = new Story(inkFile.text);
            ConnectDialogueFunctions();

            textBox.SetActive(true);
            customButton.SetActive(true);
            optionPanel.SetActive(true);

            choiceSelected = null;
            AdvanceDialogue();
        }
    }

    private void CloseDialogue()
    {
        dialogueOpen = false;

        textBox.SetActive(false);
        customButton.SetActive(false);
        optionPanel.SetActive(false);
    }

    private void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));

        if (story.currentChoices.Count > 0)
        {
            StartCoroutine(ShowChoices());
        }
    }

    public static void SetDecision(object element)
    {
        choiceSelected = (Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    void AdvanceFromDecision()
    {
        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }
        choiceSelected = null;
        AdvanceDialogue();

    }

    private IEnumerator TypeSentence (string sentence)
    {
        message.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return null;
        }
    }

    private IEnumerator ShowChoices()
    {
        List<Choice> _choices = story.currentChoices;

        for (int i = 0; i < _choices.Count; i++)
        {
            GameObject temp = Instantiate(customButton, optionPanel.transform);
            temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _choices[i].text;
            temp.AddComponent<Selectable>();
            temp.GetComponent<Selectable>().element = _choices[i];
            temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });
        }

        yield return new WaitUntil(() => { return choiceSelected != null;  });

        AdvanceFromDecision();
    }

}
