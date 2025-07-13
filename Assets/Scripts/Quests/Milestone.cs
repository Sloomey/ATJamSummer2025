using UnityEngine;

[System.Serializable]
public class Milestone
{
    public string MID;
    public enum QuestType { TALK, GIVE }

    //TODO this should just pull all NPCs?
    public enum NPC { BOB, CHRIS }

    public enum Item { APPLE, NONE }

    public QuestType type;
    public NPC target;
    public string dialogueOption;
    public Item item;

    //requirement to complete?
    //ID of required object?
    //event to fire upon completion?
}