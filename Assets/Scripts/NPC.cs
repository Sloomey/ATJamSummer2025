using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    private GameObject floatingTextInstance;
    private TextMeshProUGUI textComponent;
    public TextAsset dialogue;
    private GameControl gameControl;

    public TextAsset week1HallwayDialogue;
    public TextAsset week1ClassDialogue;
    public TextAsset week2HallwayDialogue;
    public TextAsset week2ClassDialogue;
    public TextAsset week3HallwayDialogue;
    public TextAsset week3ClassDialogue;
    public TextAsset picnicDialogue;

    public bool atWeek1Hallway;
    public bool atWeek1Class;
    public bool atWeek2Hallway;
    public bool atWeek2Class;
    public bool atWeek3Hallway;
    public bool atWeek3Class;

    private Animator Animator;

    private void Awake()
    {
        gameControl = GameObject.FindAnyObjectByType<GameControl>();
    }

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

        switch (gameControl.gameWeek)
        {
            case 1:
                if (SceneManager.GetActiveScene().name == "HallwayIdea" && !atWeek1Hallway)
                {
                    Destroy(gameObject);
                }
                if (SceneManager.GetActiveScene().name == "ClassroomIdea" && !atWeek1Class)
                {
                    Destroy(gameObject);
                }
                break;

            case 2:
                if (SceneManager.GetActiveScene().name == "HallwayIdea" && !atWeek2Hallway)
                {
                    Destroy(gameObject);
                }
                if (SceneManager.GetActiveScene().name == "ClassroomIdea" && !atWeek2Class)
                {
                    Destroy(gameObject);
                }
                break;

            case 3:
                if (SceneManager.GetActiveScene().name == "HallwayIdea" && !atWeek3Hallway)
                {
                    Destroy(gameObject);
                }
                if (SceneManager.GetActiveScene().name == "ClassroomIdea" && !atWeek3Class)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    private void OnDestroy()
    {
        if (floatingTextInstance)
        {
            Destroy(floatingTextInstance);
        }
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

    public void SwitchDialogue(TextAsset newDialogue)
    {
        dialogue = newDialogue;
    }
}
