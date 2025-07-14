using System;
using UnityEngine;

public class ChangeDialogueScript : StateMachineBehaviour
{
    public TextAsset dialogue;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC _par = animator.GetComponentInParent<NPC>();
        _par.SwitchDialogue(dialogue);
    }
}
