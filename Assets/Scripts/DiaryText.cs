using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class DiaryText : MonoBehaviour
{
    public TextAsset inkFile;
    public TextMeshProUGUI textBox;

    Animator anim;

    static Story journalLog;

    public bool switchScenes = false;

    private void Start()
    {

        anim = GetComponent<Animator>();

        string _jLogStr = "";
        Story _journal = new Story(inkFile.text);

        while (_journal.canContinue)
        {
            _jLogStr += _journal.Continue() + "\n";
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
