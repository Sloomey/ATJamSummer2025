using UnityEngine;
using Ink.Runtime;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

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

    private void ConnectDialogueFunctions()
    {
        story.BindExternalFunction("TestFunction", (string name) => {
            Debug.Log(name);
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
