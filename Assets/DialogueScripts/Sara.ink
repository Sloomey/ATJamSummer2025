EXTERNAL StartQuest(quest)
EXTERNAL IsQuestActive(quest)
EXTERNAL EndQuest(quest)
EXTERNAL IsQuestFinished(quest)


->BEGIN

==BEGIN==
{ IsQuestFinished("DeliverApple"):
    -> HATRED
- else:
    -> START
}

==START==
-Hi I'm Sara! Nice to meet you!
 *  [Leave]
    ->END
 *  {IsQuestActive("DeliverApple")} [Bob said to give this to you]
    {EndQuest("DeliverApple")}
    Oh... okay.
    ->END
    
==HATRED==
-I don't want to talk to you anymore.
->END
