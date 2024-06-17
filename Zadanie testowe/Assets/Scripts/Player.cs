using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 inputVector;

    void Update()
    {
        inputVector = new Vector2(0,0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1f;
        }

        inputVector = inputVector.normalized;

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        transform.forward = moveDirection;

    }
}
