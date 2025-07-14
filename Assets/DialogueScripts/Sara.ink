EXTERNAL IsQuestActive(quest)
EXTERNAL AdvanceQuest(quest)

->START
==START==
-Hi I'm Sara! Nice to meet you!
 *  [Leave]
    ->END
 * {IsQuestActive("DeliverApple")} [Bob said to give this to you]
    Oh... okay.
    ->END
