using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 _movementInput;
    private bool insideNPCZone = false;
    private List<IItem> Inventory;

    private void Start()
    {

        Inventory = new List<IItem>(); 
    }

    void Update()
    {
        transform.position += (Vector3)(moveSpeed * Time.fixedDeltaTime * _movementInput);
    }

    //check if within distance of an NPC
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            insideNPCZone = true;
            Debug.Log("inside npc zone");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            insideNPCZone = false;
            Debug.Log("left npc zone");
        }
    }

    public void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>().normalized;
    }

    public void OnInteract(InputValue inputValue)
    {
        if (insideNPCZone)
        {
            AddApple();
            Debug.Log("ItemApple added to Inventory");
            PrintInventory();
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
