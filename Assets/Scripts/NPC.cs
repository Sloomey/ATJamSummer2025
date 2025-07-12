using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public TextAsset dialogue;

    private void Start()
    {
        ChangeDialogueScript.SwitchDialogue += SwitchDialogue;
    }

    private void OnDestroy()
    {
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
