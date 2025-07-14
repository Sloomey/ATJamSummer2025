EXTERNAL StartQuest(quest)
EXTERNAL IsQuestActive(quest)
EXTERNAL EndQuest(quest)
EXTERNAL IsQuestFinished(quest)

->START
== START ==
{ IsQuestActive("DeliverApple"):
    -> NEXT
- else:
    -> BEGIN
}

==BEGIN==
-Hey you're the new kid, right...?
-Can you do something for me?
*   [What is it?]
    Give this apple to Sara.
    {StartQuest("DeliverApple")}
    ->END
*   [No]
    Alright whatever. 
    ->END
    
==NEXT==
{ IsQuestFinished("DeliverApple"):
    -> QFINISHED
- else:
    -> QNOTFINISHED
}

==QFINISHED==
-You actually gave it to her??
-HAHAHAHA that's hilarious. Sara hates apples!
->END

==QNOTFINISHED==
-Hey did you give it to her yet?
    ->END
