using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Move
    private Vector2 moveInput;
    private Vector2 currentMoveInput;
    private Vector2 moveInputVelocity;
    private float moveInputSmoothTime = .1f;
    [SerializeField] private float moveSpeed;


    // Mood
    private Vector2 increaseMoodInput;
    [SerializeField] private GameObject playerMoodBarContainer;
    [SerializeField, Range(0, 100f)] private float moodLevel = 100f;
    private const float maxMoodLevel = 100f;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleMoveInput();
    }

    private void HandleMoveInput()
    {
        // Smooth move input values

        currentMoveInput = Vector2.SmoothDamp(currentMoveInput, moveInput, ref moveInputVelocity, moveInputSmoothTime);
        // Apply movement
        transform.Translate(currentMoveInput * moveSpeed * Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

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

    private void OnIncreaseMood(InputValue value)
    {
        increaseMoodInput = value.Get<Vector2>();
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
