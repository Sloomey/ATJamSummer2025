using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NPC : MonoBehaviour
{

    public GameObject floatingTextPrefab;
    private GameObject floatingTextInstance;
    private TextMeshProUGUI textComponent;
    public TextAsset dialogue;


    void Update()
    {
        if (floatingTextInstance)
        {
            // Follow the GameObject's position
            floatingTextInstance.transform.position = transform.position + Vector3.up * 2f;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        textComponent.text = "E";

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        textComponent.text = "";

    }

    private void Start()
    {
        // Instantiate floating text above this GameObject
        floatingTextInstance = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        textComponent = floatingTextInstance.GetComponentInChildren<TextMeshProUGUI>();

        // Set the name
        textComponent.text = "";

    ChangeDialogueScript.SwitchDialogue += SwitchDialogue;
    }

    private void OnDestroy()
    {
        if (floatingTextInstance)
        {
            Destroy(floatingTextInstance);
        }
        ChangeDialogueScript.SwitchDialogue -= SwitchDialogue;
    }

    public void InteractedWith()
    {
        DialogueControl _dc = Object.FindAnyObjectByType<DialogueControl>();

        if (_dc != null)
        {
            _dc.inkFile = dialogue;
            _dc.OpenDialogue();
        }
        else
        {
            Debug.LogError("No Dialogue Control Found. Can't show NPC Dialogue.");
        }
    }

    private void SwitchDialogue(TextAsset newDialogue)
    {
        dialogue = newDialogue;
    }
}
