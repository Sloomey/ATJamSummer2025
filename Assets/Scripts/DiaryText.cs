using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class DiaryText : MonoBehaviour
{

    public TextMeshProUGUI textBox;

    public TextAsset week1Text;
    public TextAsset week2Text;
    public TextAsset week3Text;

    public AudioSource Audio;

    Animator anim;

    static Story journalLog;

    public bool switchScenes = false;

    public GameControl gameControl;

    private TextAsset inkFile;

    private Story journal;

    private void Start()
    {
        switch (gameControl.gameWeek)
        {
            case 1:

                journal = new Story(week1Text.text);
                Audio.Play();
                break;
            case 2:
                journal = new Story(week2Text.text);
                Audio.Stop();
                break;
            case 3:
                journal = new Story(week3Text.text);
                Audio.Stop();
                break;
        }


        anim = GetComponent<Animator>();

        string _jLogStr = "";
        

        while (journal.canContinue)
        {
            _jLogStr += journal.Continue() + "\n";
        }

        textBox.text = _jLogStr;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("FadeSwitch", true);
        }

        if (switchScenes)
        {
            SwitchScenes();
        }
    }

    public void SwitchScenes()
    {
        SceneManager.LoadScene(sceneName: "HallwayIdea");
    }
}
