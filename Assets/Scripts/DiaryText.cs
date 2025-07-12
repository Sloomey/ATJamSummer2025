using TMPro;
using UnityEngine;
using Ink.Runtime;

public class DiaryText : MonoBehaviour
{
    public TextAsset inkFile;
    public TextMeshProUGUI textBox;

    static Story journalLog;

    private void Start()
    {
        string _jLogStr = "";
        Story _journal = new Story(inkFile.text);

        while (_journal.canContinue)
        {
            _jLogStr += _journal.Continue() + "\n";
        }

        textBox.text = _jLogStr;
    }
}
