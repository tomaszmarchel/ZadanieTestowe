using System;
using UnityEngine;

public class Player : MonoBehaviour, IArmoryObjectParent
{
    public static Player Instace {  get; private set; }

    public event EventHandler<OnSelectedCounterChangedArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    //Input
    private PlayerInputActions playerInputActions;

    //Vars
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;

    // TO INTERFACE
    [SerializeField] Transform playerItemPoint;
    private ArmoryObject armoryObject;

    //Movement
    private bool isWalking;
    private Vector3 lastInteractionDir;

    //Interaction
    private ClearCounter selectedCounter;

    private void Start()
    {
        gameInput.OnInteractionAction += GameInput_OnInteractionAction;
    }

    private void Awake()
    {
        Instace = this;
    }

    private void GameInput_OnInteractionAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    void Update()
    {
        HandleMovement();
        HandeInteraction();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandeInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        float interactionDistance = 2f;

        if (moveDirection != Vector3.zero)
        {
            lastInteractionDir = moveDirection;
        }

        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactionDistance, layerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 1f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if (canMove)
            {
                moveDirection = moveDirectionX;
            }
            else
            {
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if (canMove)
                {
                    moveDirection = moveDirectionZ;
                }
                else
                {
                    //Canot Move
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        isWalking = moveDirection != Vector3.zero;

        float rotateSpeed = 5f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetCounterTopPoint()
    {
        return playerItemPoint;
    }

    public void SetArmoryObject(ArmoryObject armoryObject)
    {
        this.armoryObject = armoryObject;
    }

    public ArmoryObject GetArmoryObject()
    {
        return armoryObject;
    }

    public void ClearArmoryObject()
    {
        this.armoryObject = null;
    }

    public bool HasArmoryObject()
    {
        return this.armoryObject != null;
    }
}
