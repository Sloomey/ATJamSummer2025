using Ink.Parsed;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> activeQuests;
    List<Quest> allQuests;

    private void Start()
    {
        activeQuests = new List<Quest>();
    }

    void AdvanceQuest(Quest quest)
    {

    }

    void GiveQuestReward(Quest quest)
    {

    }

    void NotifyQuestUI(string notification)
    {

    }
}
