using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputHandler inputHandler;

    private Vector2 moveInput;
    [SerializeField] private float moveSpeed;
    
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
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Apply movement
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);
    }
}
