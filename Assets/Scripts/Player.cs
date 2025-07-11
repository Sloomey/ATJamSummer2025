using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 _movementInput;
    private bool insideNPCZone = false;

    void Update()
    {
        transform.position += (Vector3)(moveSpeed * Time.fixedDeltaTime * _movementInput);
    }

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
            Debug.Log("interacted with npc");
        }
    }
}
