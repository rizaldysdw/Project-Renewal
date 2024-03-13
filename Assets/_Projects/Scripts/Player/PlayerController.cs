using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Move
    private Vector2 moveInput;
    private Vector2 currentMoveInput;
    private Vector2 moveInputVelocity;
    private Vector2 moveInputLastFrame;
    private float moveInputSmoothTime = .1f;
    [SerializeField] private float moveSpeed;

    // Inventory
    private InventoryManager inventoryManager;
    private bool openInventoryPressed;

    // Interaction
    private ItemSO equippedItem;
    private FarmTile lastHitTile;
    private RaycastHit2D lastHit;
    private bool interactPressed;
    public bool shouldPerformRaycast;

    // Mood
    private Vector2 increaseMoodInput;
    [SerializeField] private GameObject playerMoodBarContainer;
    [SerializeField, Range(0, 100f)] private float moodLevel = 100f;
    private const float maxMoodLevel = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = InventoryManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMoveInput();
        HandleInteraction();
    }

    private void HandleMoveInput()
    {
        // Smooth move input values
        currentMoveInput = Vector2.SmoothDamp(currentMoveInput, moveInput, ref moveInputVelocity, moveInputSmoothTime);
        
        // Apply movement
        transform.Translate(currentMoveInput * moveSpeed * Time.deltaTime);
    }

    private void HandleInteraction()
    {
        if (shouldPerformRaycast)
        {
            // Cast a ray from the current position (transform) in the currentMoveInput direction
            RaycastHit2D hit = Physics2D.Raycast(transform.position, currentMoveInput.normalized);
            

            if (hit.collider)
            {
                FarmTile raycastTile = hit.collider.GetComponent<FarmTile>();
                lastHitTile = raycastTile;

                if (interactPressed)
                {
                    // Handle interaction with the object hit by the ray
                    lastHitTile.FillWater();

                    Debug.Log("Interacted with: " + hit.collider.gameObject.name);
                }

                // Store lastHit for next frame
                lastHit = hit;
            }
            else
            {
                // There's no new hit
                if (interactPressed)
                {
                    // Handle interaction with the object hit by the ray
                    lastHitTile.FillWater();

                    Debug.Log("Interacted with: " + hit.collider.gameObject.name);
                }
            }

            Debug.DrawRay(transform.position, currentMoveInput, Color.red);
        }
    }

    private ItemSO GetEquippedItem()
    {
        return inventoryManager.GetSelectedItemData();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        float minInputMagnitude = 0.5f;

        if (moveInput.x > minInputMagnitude && moveInput.y > minInputMagnitude)
        {
            moveInput.x = 1f;
            moveInput.y = 1f;
        }
        else if (currentMoveInput.x < -minInputMagnitude && moveInput.y > minInputMagnitude)
        {
            moveInput.x = -1f;
            moveInput.y = 1f;
        }
        else if (moveInput.x > minInputMagnitude && moveInput.y < -minInputMagnitude)
        {
            moveInput.x = 1f;
            moveInput.y = -1f;
        }
        else if (moveInput.x < -minInputMagnitude && moveInput.y < -minInputMagnitude)
        {
            moveInput.x = -1f;
            moveInput.y = -1f;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        interactPressed = !context.canceled;
        StartCoroutine(ResetInteractStart());
    }

    private IEnumerator ResetInteractStart()
    {
        yield return new WaitForEndOfFrame();
        interactPressed = false;
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        openInventoryPressed = context.ReadValueAsButton();
    }

    public void OnIncreaseMood(InputAction.CallbackContext context)
    {
        increaseMoodInput = context.ReadValue<Vector2>();
        moodLevel += increaseMoodInput.y;

        if (moodLevel >= 100f)
        {
            moodLevel = 100f;
        }
        else if (moodLevel <= 0f)
        {
            moodLevel = 0f;
        }

        RectTransform moodBarRectTransform = playerMoodBarContainer.GetComponent<RectTransform>();
        moodBarRectTransform.localScale = new Vector3((moodLevel / maxMoodLevel), 1f, 1f);
    }
}
