using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Componenet Variables")]
    [SerializeField] VariableJoystick _joystick;
    CharacterController _characterController;
    Animator _playerAnimator;

    [Header("Character Variables")]
    [SerializeField] float _moveSpeed = 7f;
    [SerializeField] float _rotationSpeed = 15f;
    Vector3 _moveDirection;
    public bool IsCarrying;

    void Start()
    {
        // Get necessary components
        _playerAnimator = GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get input from the joystick
        float horizontalInput = _joystick.Horizontal;
        float verticalInput = _joystick.Vertical;

        // Calculate total movement amount (0 for idle & 0.1 for walking)
        float walkAmount = Mathf.Clamp(Mathf.Abs(_joystick.Horizontal + _joystick.Vertical), 0f, 0.2f);

        // Calculate movement direction based on input
        _moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Only when there is some significant movement input, move & rotate the character to face the direction of movement
        if (walkAmount > 0.1)
        {
            // avoid small value so that movement & animation synchronize
            walkAmount = (walkAmount < 0.2f) ? 0.2f : walkAmount;

            // Calculate the target rotation based on the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);

            // Smoothly rotate the character towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            // Move the character using CharacterController
            _characterController.Move(_moveDirection * _moveSpeed * Time.deltaTime);
        }

        // Set Idle & walk animation parameter
        _playerAnimator.SetFloat("WalkAmount", walkAmount);
        _playerAnimator.SetBool("IsCarrying", IsCarrying);
    }
}
