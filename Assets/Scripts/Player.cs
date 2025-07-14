using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Player : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 _movementInput;
    private List<IItem> Inventory;
    private NPC npcNextToRef;

    private Animator animator;
    public Quest scriptableObjectValues;

    private void Start()
    {
        scriptableObjectValues.finished = false;
        Inventory = new List<IItem>();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveSpeed * Time.fixedDeltaTime * _movementInput);
    }

    //check if within distance of an NPC
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            Debug.Log("inside npc zone");
            npcNextToRef = collision.gameObject.GetComponent<NPC>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            Debug.Log("left npc zone");
            npcNextToRef = null;
        }
    }

    public void OnMove(InputValue inputValue)
    {
        DialogueControl dc = GameObject.FindAnyObjectByType<DialogueControl>();
        if (!dc.IsDialogueOpen())
        {
            _movementInput = inputValue.Get<Vector2>().normalized;

            if (_movementInput.x != 0 || _movementInput.y != 0)
            {
                animator.SetFloat("X", _movementInput.x);
                animator.SetFloat("Y", _movementInput.y);

                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
    }



    public void StartQuest(string quest)
    {
        QuestManager _qc = GameObject.FindAnyObjectByType<QuestManager>();
        if (scriptableObjectValues.QID == quest)
        {
            _qc.activeQuests.Add(scriptableObjectValues);

        }
    }

    public void OnInteract(InputValue inputValue)
    {
        if (npcNextToRef != null)
        {
            npcNextToRef.InteractedWith();
        }
    }

    public void PrintInventory()
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            Debug.Log("Inventory slot " + i + " contains " + Inventory[i]);
        }
    }
    public void AddApple()
    {
        ItemApple apple = new ItemApple();
        Inventory.Add(apple);
    }
}
