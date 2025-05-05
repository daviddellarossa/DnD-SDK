using UnityEngine;
using UnityEngine.InputSystem;

namespace Management.Scene
{
    public class ParaboloidCameraController : MonoBehaviour
    {
        [Header("Paraboloid Settings")]
        [Tooltip("Coefficient for the paraboloid equation (y = a * (x² + z²))")]
        [SerializeField] private float paraboloidCoefficient = 0.5f;
        
        [Tooltip("Minimum height of the camera on the paraboloid")]
        [SerializeField] private float minHeight = 2.0f;
        
        [Tooltip("Maximum height of the camera on the paraboloid")]
        [SerializeField] private float maxHeight = 20.0f;
        
        [Header("Movement Settings")]
        [Tooltip("Movement speed of the center point")]
        [SerializeField] private float centerMoveSpeed = 5.0f;
        
        [Tooltip("Horizontal movement speed on the paraboloid")]
        [SerializeField] private float horizontalMoveSpeed = 20.0f;
        
        [Tooltip("Vertical movement speed on the paraboloid")]
        [SerializeField] private float verticalMoveSpeed = 5.0f;
        
        [Tooltip("Multiplier for center movement speed when holding shift")]
        [SerializeField] private float sprintMultiplier = 2.0f;
        
        [Header("Camera Settings")]
        [Tooltip("Target for the camera to look at")]
        [SerializeField] private Transform lookTarget;
        
        // Center point of the paraboloid
        private Vector3 _centerPoint = Vector3.zero;
        
        // Camera position relative to center in cylindrical coordinates
        private float _cameraRadialDistance = 10.0f;
        private float _cameraHeight;
        private float _cameraAngle;
        
        // Input system variables
        private InputSystem_Actions _inputActions;
        private InputAction _moveAction;
        private InputAction _sprintAction;
        private InputAction _rotateAction;
        private InputAction _scrollAction;
        
        private Vector2 _moveInput;
        private Vector2 _previousMousePosition;
        private bool _isSprinting;
        private bool _isRotating;
        
        private void Awake()
        {
            _inputActions = new InputSystem_Actions();
            
            // Initialize camera height
            _cameraHeight = Mathf.Clamp(5.0f, minHeight, maxHeight);
            
            // Create look target if not assigned
            if (lookTarget == null)
            {
                GameObject targetObj = new GameObject("CameraLookTarget");
                lookTarget = targetObj.transform;
                lookTarget.position = _centerPoint;
            }
        }
        
        private void Start()
        {
            // Set initial camera position on the paraboloid
            UpdateCameraPosition();
            
            // Set camera to look at the center point
            transform.LookAt(lookTarget);
        }
        
        private void OnEnable()
        {
            // Setup input actions
            _moveAction = _inputActions.Camera.Move;
            _moveAction.Enable();
            
            _sprintAction = _inputActions.Camera.Sprint;
            _sprintAction.Enable();
            
            _rotateAction = _inputActions.Camera.Rotate;
            _rotateAction.Enable();
            
            _scrollAction = _inputActions.Camera.Scroll;
            _scrollAction.Enable();
            
            // Connect event handlers
            _moveAction.performed += OnMovePerformed;
            _moveAction.canceled += OnMoveCanceled;
            
            _sprintAction.performed += OnSprintPerformed;
            _sprintAction.canceled += OnSprintCanceled;
            
            _rotateAction.performed += OnRotatePerformed;
            _rotateAction.canceled += OnRotateCanceled;
            
            _scrollAction.performed += OnScrollPerformed;
        }
        
        private void OnDisable()
        {
            // Disconnect event handlers
            _moveAction.performed -= OnMovePerformed;
            _moveAction.canceled -= OnMoveCanceled;
            _sprintAction.performed -= OnSprintPerformed;
            _sprintAction.canceled -= OnSprintCanceled;
            _rotateAction.performed -= OnRotatePerformed;
            _rotateAction.canceled -= OnRotateCanceled;
            _scrollAction.performed -= OnScrollPerformed;
            
            // Disable input actions
            _moveAction.Disable();
            _sprintAction.Disable();
            _rotateAction.Disable();
            _scrollAction.Disable();
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
        }
        
        private void OnRotateCanceled(InputAction.CallbackContext context)
        {
            _isRotating = false;
        }
        
        private void OnScrollPerformed(InputAction.CallbackContext context)
        {
            float scrollValue = context.ReadValue<Vector2>().y;
            
            // Update camera height based on scroll
            _cameraHeight += scrollValue * verticalMoveSpeed * Time.deltaTime * 10f;
            _cameraHeight = Mathf.Clamp(_cameraHeight, minHeight, maxHeight);
            
            // Update camera position to stay on the paraboloid
            UpdateCameraPosition();
        }
        
        private void Update()
        {
            // Move center point with WASD
            MoveCenterPoint();
            
            // Handle horizontal rotation on paraboloid if middle mouse is pressed
            if (_isRotating)
            {
                RotateOnParaboloid();
            }
            
            // Always update look target to follow center point
            lookTarget.position = _centerPoint;
            
            // Make camera look at the center point
            transform.LookAt(lookTarget);
        }
        
        private void MoveCenterPoint()
        {
            // Skip if no input
            if (_moveInput.sqrMagnitude < 0.01f)
                return;
                
            // Get movement values from the input system
            float horizontalInput = _moveInput.x;
            float verticalInput = _moveInput.y;
            
            // Calculate movement direction in world space
            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;
            
            // Apply sprint multiplier if sprinting
            float currentSpeed = _isSprinting ? centerMoveSpeed * sprintMultiplier : centerMoveSpeed;
            
            // Move the center point
            _centerPoint += direction * (currentSpeed * Time.deltaTime);
            
            // Update camera position to follow the center point while staying on the paraboloid
            UpdateCameraPosition();
        }
        
        private void RotateOnParaboloid()
        {
            // Get current mouse position
            Vector2 currentMousePosition = Mouse.current.position.ReadValue();
            
            // Calculate mouse delta
            Vector2 mouseDelta = currentMousePosition - _previousMousePosition;
            
            // Only use horizontal movement (mouseDelta.x) as per requirements
            float horizontalMovement = mouseDelta.x * horizontalMoveSpeed * Time.deltaTime;
            
            // Update camera angle around the center point
            _cameraAngle += horizontalMovement;
            
            // Keep angle in [0, 360) range
            _cameraAngle = _cameraAngle % 360f;
            if (_cameraAngle < 0)
                _cameraAngle += 360f;
                
            // Update camera position to stay on the paraboloid
            UpdateCameraPosition();
            
            // Update previous mouse position for next frame
            _previousMousePosition = currentMousePosition;
        }
        
        private void UpdateCameraPosition()
        {
            // Convert cylindrical coordinates to Cartesian coordinates
            float x = _cameraRadialDistance * Mathf.Cos(_cameraAngle * Mathf.Deg2Rad);
            float z = _cameraRadialDistance * Mathf.Sin(_cameraAngle * Mathf.Deg2Rad);
            
            // Calculate height on paraboloid based on radial distance
            // Formula: y = a * (x^2 + z^2) = a * r^2
            float paraboloidHeight = paraboloidCoefficient * (_cameraRadialDistance * _cameraRadialDistance);
            
            // Ensure minimum height is maintained
            float y = Mathf.Max(paraboloidHeight, _cameraHeight);
            
            // Set camera position relative to center point
            transform.position = _centerPoint + new Vector3(x, y, z);
        }
    }
}
