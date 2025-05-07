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

        [Header("Camera Rotation")]
        [Tooltip("Rotation speed around X axis (up/down)")]
        [SerializeField] private float rotationSpeedX = 0.2f;
        
        [Tooltip("Rotation speed around Y axis (left/right)")]
        [SerializeField] private float rotationSpeedY = 0.2f;
        
        [Tooltip("Minimum angle for X rotation")]
        [SerializeField] private float minAngleX = 10.0f;
        
        [Tooltip("Maximum angle for X rotation")]
        [SerializeField] private float maxAngleX = 80.0f;

        private InputSystem_Actions _inputActions;
        private InputAction _moveAction;
        private InputAction _sprintAction;
        private InputAction _lookAction;
        private InputAction _rotateAction;
        
        private Vector2 _moveInput;
        private Vector2 _previousMousePosition;
        private bool _isSprinting;
        private bool _isRotating;
        private Vector3 _pivotPoint;
        
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
            
            // Initialize the pivot point
            UpdatePivotPoint();
        }
        
        private void OnEnable()
        {
            // Use Camera action map instead of Player action map
            _moveAction = _inputActions.Camera.Move;
            _moveAction.Enable();
        
            _sprintAction = _inputActions.Camera.Sprint;
            _sprintAction.Enable();
            
            // Setup camera rotation inputs
            _rotateAction = _inputActions.Camera.Rotate;
            _rotateAction.Enable();
            
            _lookAction = _inputActions.Camera.Look;
            _lookAction.Enable();
        
            // Connect event handlers
            _moveAction.performed += OnMovePerformed;
            _moveAction.canceled += OnMoveCanceled;
        
            _sprintAction.performed += OnSprintPerformed;
            _sprintAction.canceled += OnSprintCanceled;
            
            _rotateAction.performed += OnRotatePerformed;
            _rotateAction.canceled += OnRotateCanceled;
        }
        
        private void OnDisable()
        {
            _moveAction.performed -= OnMovePerformed;
            _moveAction.canceled -= OnMoveCanceled;
            _sprintAction.performed -= OnSprintPerformed;
            _sprintAction.canceled -= OnSprintCanceled;
            _rotateAction.performed -= OnRotatePerformed;
            _rotateAction.canceled -= OnRotateCanceled;
        
            _moveAction.Disable();
            _sprintAction.Disable();
            _rotateAction.Disable();
            _lookAction.Disable();
        }
        
        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
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
        
        private void OnRotatePerformed(InputAction.CallbackContext context)
        {
            _isRotating = true;
            _previousMousePosition = Mouse.current.position.ReadValue();
            UpdatePivotPoint();
        }
        
        private void OnRotateCanceled(InputAction.CallbackContext context)
        {
            _isRotating = false;
        }

        private void Update()
        {
            // Handle camera movement
            MoveCamera();
            
            // Handle camera rotation if middle mouse is pressed
            if (_isRotating)
            {
                RotateCamera();
            }
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
            
            // Update pivot point if we moved
            if (movementDirection.magnitude > 0)
            {
                UpdatePivotPoint();
            }
        }
        
        private void RotateCamera()
        {
            // Get current mouse position
            Vector2 currentMousePosition = Mouse.current.position.ReadValue();
            
            // Calculate mouse delta
            Vector2 mouseDelta = currentMousePosition - _previousMousePosition;
            
            // Calculate rotation angles based on mouse movement
            float rotationX = -mouseDelta.y * rotationSpeedX; // Invert Y axis for intuitive control
            float rotationY = mouseDelta.x * rotationSpeedY;
            
            // Apply rotation limits to X angle (vertical rotation)
            angleX = Mathf.Clamp(angleX + rotationX, minAngleX, maxAngleX);
            angleY = (angleY + rotationY) % 360; // Keep Y angle (horizontal rotation) within 0-360 range
            
            // Save original position
            Vector3 originalPosition = transform.position;
            
            // Temporarily move camera to pivot point
            transform.position = _pivotPoint;
            
            // Apply new rotation angles
            transform.rotation = Quaternion.Euler(angleX, angleY, 0);
            
            // Move camera back to its position relative to pivot
            transform.Translate(new Vector3(0, cameraHeight, -cameraDistance), Space.Self);
            
            // Update previous mouse position for next frame
            _previousMousePosition = currentMousePosition;
            
            // Update pivot point after rotation
            UpdatePivotPoint();
        }
        
        [Header("Ground Detection")]
        [Tooltip("Layer mask for ground detection")]
        [SerializeField] private LayerMask groundLayer;
        
        private void UpdatePivotPoint()
        {
            // Cast a ray from the camera position along its forward direction
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            
            // Use Physics.Raycast to detect the actual Ground object in the scene
            if (Physics.Raycast(ray, out hit, 1000f, groundLayer))
            {
                // Get the intersection point from the raycast hit
                _pivotPoint = hit.point;
            }
            else
            {
                // Fallback if no ground is hit
                Debug.LogWarning("Camera raycast didn't hit ground. Using fallback pivot point.");
                _pivotPoint = new Vector3(transform.position.x, 0, transform.position.z + cameraDistance);
            }
        }
    }
}