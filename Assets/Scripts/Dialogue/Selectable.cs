using UnityEngine;

public class Selectable : MonoBehaviour
{

    public object element;
    public void Decide()
    {
        DialogueControl.SetDecision(element);
    }
}
