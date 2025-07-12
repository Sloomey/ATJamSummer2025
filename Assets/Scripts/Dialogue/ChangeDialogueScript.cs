using System;
using UnityEngine;

public class ChangeDialogueScript : StateMachineBehaviour
{

    static public event Action<TextAsset> SwitchDialogue;

    public TextAsset dialogue;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SwitchDialogue?.Invoke(dialogue);
    }
}
