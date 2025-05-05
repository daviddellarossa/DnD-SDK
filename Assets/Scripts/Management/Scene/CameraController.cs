using UnityEngine;
using UnityEngine.InputSystem;

namespace Management.Scene
{
    public class CameraController : MonoBehaviour
    {
        [Header("Camera Settings")]
        [Tooltip("Height of the camera from the ground")]
        [SerializeField] private float cameraHeight = 15.0f;
    
        [Tooltip("Distance of the camera from the center")]
        [SerializeField] private float cameraDistance = 20.0f;
    
        [Tooltip("X-axis rotation angle of the camera")]
        [SerializeField] private float angleX = 30.0f;
    
        [Tooltip("Y-axis rotation angle of the camera")]
        [SerializeField] private float angleY;
    
        [Header("Camera Movement")]
        [Tooltip("Movement speed of the camera")]
        [SerializeField] private float moveSpeed = 5.0f;
    
        [Tooltip("Orthographic size of the camera")]
        [SerializeField] private float orthographicSize = 7f;

        [Tooltip("Multiplier for camera speed when holding shift")]
        [SerializeField] private float sprintMultiplier = 2.0f;

        private InputSystem_Actions _inputActions;
        private InputAction _moveAction;
        private InputAction _sprintAction;
        private Vector2 _moveInput;
        private bool _isSprinting;

        private void Awake()
        {
            _inputActions = new InputSystem_Actions();
        }

        private void Start()
        {
            if (Camera.main != null)
            {
                Camera.main.orthographic = true;
                Camera.main.orthographicSize = orthographicSize;
            }
        
            // Position and rotate the camera
            transform.position = new Vector3(0, cameraHeight, -cameraDistance);
            transform.rotation = Quaternion.Euler(angleX, angleY, 0f);
        }

        private void OnEnable()
        {
            _moveAction = _inputActions.Player.Move;
            _moveAction.Enable();
        
            _sprintAction = _inputActions.Player.Sprint;
            _sprintAction.Enable();
        
            _moveAction.performed += OnMovePerformed;
            _moveAction.canceled += OnMoveCanceled;
        
            _sprintAction.performed += OnSprintPerformed;
            _sprintAction.canceled += OnSprintCanceled;
        }

        private void OnDisable()
        {
            _moveAction.performed -= OnMovePerformed;
            _moveAction.canceled -= OnMoveCanceled;
            _sprintAction.performed -= OnSprintPerformed;
            _sprintAction.canceled -= OnSprintCanceled;
        
            _moveAction.Disable();
            _sprintAction.Disable();
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        
            Debug.Log($"Horizontal: {_moveInput.x}, Vertical: {_moveInput.y}");
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            _moveInput = Vector2.zero;
        }

        private void OnSprintPerformed(InputAction.CallbackContext context)
        {
            _isSprinting = true;
        }

        private void OnSprintCanceled(InputAction.CallbackContext context)
        {
            _isSprinting = false;
        }

        private void Update()
        {
            MoveCamera();
        }

        private void MoveCamera()
        {
            // Get movement values from the input system
            float horizontalInput = _moveInput.x;
        
            // Correction of the Y movement to compensate for camera inclination
            float verticalInput = _moveInput.y / Mathf.Sin(angleX * Mathf.Deg2Rad);
        
            // Calculate final speed (sprint if shift is held)
            float currentSpeed = moveSpeed;
            if (_isSprinting)
                currentSpeed *= sprintMultiplier;

            // Create a direction vector that accounts for input magnitude
            Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        
            // Apply movement in world coordinates
            transform.Translate(movementDirection * (currentSpeed * Time.deltaTime), Space.World);
        }
    }
}