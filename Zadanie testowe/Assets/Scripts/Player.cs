using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerInputActions playerInputActions;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    private void Awake()
    {
       playerInputActions = new PlayerInputActions();
       playerInputActions.Player.Enable();
    }

    void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        float rotateSpeed = 5f;

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

    }
}
