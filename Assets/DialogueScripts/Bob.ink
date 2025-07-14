EXTERNAL StartQuest(quest)
EXTERNAL IsQuestActive(quest)

->START
== START ==
{ IsQuestActive("DeliverApple"):
    -> NEXT
- else:
    -> BEGIN
}

->BEGIN
==BEGIN==
-Hey you're the new kid, right...?
-Can you do something for me?
*   [What is it?]
    Give this to Sara.
    {StartQuest("DeliverApple")}
    ->END
*   [No]
    Alright whatever. 
    ->END
    
==NEXT==
-Hey
    ->END
