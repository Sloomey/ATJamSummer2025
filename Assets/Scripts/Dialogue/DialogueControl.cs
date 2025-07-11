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
    public bool isTalking = false;

    static Story story;
    TextMeshProUGUI message;
    static Choice choiceSelected;

    private void Start()
    {
        story = new Story(inkFile.text);

        message = textBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        choiceSelected = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (story.canContinue)
            {
                AdvanceDialogue();

                if (story.currentChoices.Count > 0)
                {
                    StartCoroutine(ShowChoices());
                }
            }
        }
    }

    private void AdvanceDialogue()
    {
        string currentSentence = story.Continue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence));
    }

    public static void SetDecision(object element)
    {
        choiceSelected = (Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    void AdvanceFromDecision()
    {
        optionPanel.SetActive(false);
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

        optionPanel.SetActive(true);

        yield return new WaitUntil(() => { return choiceSelected != null;  });

        AdvanceFromDecision();
    }

}
