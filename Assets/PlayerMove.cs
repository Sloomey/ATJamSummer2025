using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 _movementInput;

    void Update()
    {
        transform.position += (Vector3)(moveSpeed * Time.fixedDeltaTime * _movementInput);
    }

    public void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>().normalized;
    }

}
