using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QuestObject", order = 1)]
public class Quest : ScriptableObject
{
    public string QID;
    public Milestone[] milestones;
    public bool finished = false;
    //unique event to fire upon completion?
    //reward for completion? 
}