using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Management.Scene
{
    public class ParaboloidCameraController : MonoBehaviour
    {
        // Constants for float comparison thresholds
        private const float POSITION_EPSILON = 0.001f;
        private const float ROTATION_EPSILON = 0.01f;
        
        private void OnValidate()
        {
            ValidateHeightLimits();
            
            // Validate inverse coefficient
            if (paraboloidCoefficient <= 0)
            {
                paraboloidCoefficient = 10f;
                Debug.LogWarning("Inverse paraboloid coefficient must be greater than zero. Setting to default value.");
            }
        }
        
        private void ValidateHeightLimits()
        {
            // Ensure min/max values are valid
            minHeight = Mathf.Max(0.1f, minHeight);
            maxHeight = Mathf.Max(minHeight + 1.0f, maxHeight);
        }
        [Header("Paraboloid Settings")]
        [Tooltip("Coefficient (a) for the paraboloid equation (y = 1/a * (x² + z²)). Higher values = flatter curve.")]
        [SerializeField] private float paraboloidCoefficient = 10f;
        
        [Tooltip("Height of the paraboloid center (Y coordinate)")]
        [SerializeField] private float centerHeight = 0.0f;
        
        [Header("Camera Settings")]
        [Tooltip("Camera to be controlled (if null, will use Camera.main)")]
        [SerializeField] private Camera controlledCamera;
        
        [Tooltip("Initial height of the camera above the paraboloid center")]
        [Range(0.1f, 50.0f)]
        [SerializeField] private float initialCameraHeight = 5.0f;
        
        [Tooltip("Minimum height of the camera above the paraboloid center")]
        [Range(0.1f, 10.0f)]
        [SerializeField] private float minHeight = 2.0f;
        
        [Tooltip("Maximum height of the camera above the paraboloid center")]
        [Range(5.0f, 50.0f)]
        [SerializeField] private float maxHeight = 20.0f;
        
        [Header("Movement Settings")]
        [Tooltip("Movement speed of the center point")]
        [SerializeField] private float centerMoveSpeed = 5.0f;
        
        [Tooltip("Horizontal rotation speed on the paraboloid")]
        [SerializeField] private float horizontalRotationSpeed = 20.0f;
        
        [Tooltip("Vertical movement speed on the paraboloid")]
        [SerializeField] private float verticalMoveSpeed = 5.0f;
        
        [Tooltip("Multiplier for center movement speed when holding shift")]
        [SerializeField] private float sprintMultiplier = 2.0f;
        
        
        [Tooltip("Smoothing factor for movement (lower values = smoother)")]
        [Range(1f, 20f)]
        [SerializeField] private float movementSmoothFactor = 10.0f;
        
        [Tooltip("Smoothing factor for rotation (lower values = smoother)")]
        [Range(1f, 20f)]
        [SerializeField] private float rotationSmoothFactor = 10.0f;
        
        [Tooltip("Smoothing factor for camera height changes (lower values = smoother)")]
        [Range(1f, 20f)]
        [SerializeField] private float heightSmoothFactor = 10.0f;

        [Header("Camera Settings")]
        
        // Center point of the paraboloid
        private Vector3 _centerPoint;
        
        // Target position for smooth movement
        private Vector3 _targetCenterPoint;
        
        // Camera position relative to center in cylindrical coordinates
        private float _cameraHeight;
        private float _targetCameraHeight; // Target height for smooth interpolation
        private float _cameraAngle;
        private float _targetCameraAngle; // Target angle for smooth rotation
        
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
            
            // If no camera is assigned, use Camera.main
            if (controlledCamera == null)
            {
                controlledCamera = Camera.main;
                if (controlledCamera == null)
                {
                    Debug.LogError("No camera assigned and Camera.main not found. ParaboloidCameraController will not function correctly.");
                }
            }
            
            // Ensure min/max values are valid
            ValidateHeightLimits();
            
            // Initialize camera height using the configurable parameter
            _cameraHeight = Mathf.Clamp(initialCameraHeight, minHeight, maxHeight);
            _targetCameraHeight = _cameraHeight; // Initialize target height
            
            // Initialize camera angle
            _cameraAngle = 0f;
            _targetCameraAngle = 0f;
            
            // Initialize center point with the specified height
            _centerPoint = new Vector3(0, centerHeight, 0);
            _targetCenterPoint = _centerPoint;
        }
        
        private void Start()
        {
            // Set initial camera position on the paraboloid
            // (This will also set the camera to look at the center point)
            UpdateCameraPosition();
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
            
            // Scale scroll value for more consistent behavior across platforms
            // Use a fixed time delta to ensure consistent scroll speed regardless of frame rate
            const float scrollTimeScale = 0.02f;
            
            // Update target camera height above center based on scroll
            _targetCameraHeight += scrollValue * verticalMoveSpeed * scrollTimeScale;
            _targetCameraHeight = Mathf.Clamp(_targetCameraHeight, minHeight, maxHeight);
        }
        
        /// <summary>
        /// Checks if a value is close enough to its target to be considered stable
        /// </summary>
        private bool IsPositionStable(float current, float target, float epsilon = POSITION_EPSILON)
        {
            return Mathf.Abs(current - target) < epsilon;
        }
        
        private void Update()
        {
            // Skip all camera-related updates if no camera is available
            if (controlledCamera == null)
                return;
                
            // Quick check for any user input or ongoing transitions
            bool hasUserInput = _moveInput.sqrMagnitude > 0.01f || _isRotating;
            bool hasOngoingTransition = !IsPositionStable(_cameraHeight, _targetCameraHeight) || 
                                       !IsPositionStable(_cameraAngle, _targetCameraAngle, ROTATION_EPSILON) ||
                                       Vector3.SqrMagnitude(_centerPoint - _targetCenterPoint) > POSITION_EPSILON;
            
            // Skip updates if no input and no ongoing transitions
            if (!hasUserInput && !hasOngoingTransition)
                return;
                
            bool positionChanged = false;
            
            // Process movement input (if any)
            if (_moveInput.sqrMagnitude > 0.01f)
            {
                MoveCenterPoint();
            }
            
            // Check if center point is still transitioning to target
            if (Vector3.SqrMagnitude(_centerPoint - _targetCenterPoint) > POSITION_EPSILON)
            {
                // Continue smooth transition of center point
                _centerPoint = Vector3.Lerp(_centerPoint, _targetCenterPoint, Time.deltaTime * movementSmoothFactor);
                
                // Ensure Y coordinate stays at centerHeight
                _centerPoint.y = centerHeight;
                
                positionChanged = true;
            }
            
            // Smoothly interpolate camera height towards target height
            if (!IsPositionStable(_cameraHeight, _targetCameraHeight))
            {
                _cameraHeight = Mathf.Lerp(_cameraHeight, _targetCameraHeight, Time.deltaTime * heightSmoothFactor);
                positionChanged = true;
            }
            
            // Smoothly interpolate camera angle towards target angle
            if (!IsPositionStable(_cameraAngle, _targetCameraAngle, ROTATION_EPSILON))
            {
                // Find the shortest path for rotation (clockwise or counter-clockwise)
                float angleDifference = Mathf.DeltaAngle(_cameraAngle, _targetCameraAngle);
                
                // Apply smooth rotation
                _cameraAngle = Mathf.Lerp(_cameraAngle, _cameraAngle + angleDifference, Time.deltaTime * rotationSmoothFactor);
                
                // Keep angle in [0, 360) range
                _cameraAngle = _cameraAngle % 360f;
                if (_cameraAngle < 0)
                    _cameraAngle += 360f;
                    
                positionChanged = true;
            }
            
            // Handle horizontal rotation on paraboloid if middle mouse is pressed
            if (_isRotating)
            {
                RotateOnParaboloid();
                positionChanged = true;
            }
            
            // Update camera position if anything changed
            if (positionChanged)
            {
                UpdateCameraPosition();
            }
        }
        
        private void MoveCenterPoint()
        {
            // Get movement values from the input system
            float horizontalInput = _moveInput.x; // AD keys
            float verticalInput = _moveInput.y; // WS keys
            
            // Get camera's forward direction and project it onto XZ plane
            Vector3 cameraForward = controlledCamera.transform.forward;
            Vector3 projectedForward = Vector3.ProjectOnPlane(cameraForward, Vector3.up).normalized;
            
            // Get camera's right vector (already on XZ plane since camera only rotates around Y)
            Vector3 cameraRight = controlledCamera.transform.right;
        
            // Calculate movement direction using camera-relative vectors
            Vector3 direction = (cameraRight * horizontalInput + projectedForward * verticalInput).normalized;
        
            // Apply sprint multiplier if sprinting
            float currentSpeed = _isSprinting ? centerMoveSpeed * sprintMultiplier : centerMoveSpeed;
            
            // Calculate new target position
            _targetCenterPoint += new Vector3(direction.x * currentSpeed, 0, direction.z * currentSpeed) * Time.deltaTime;
            
            // Ensure Y coordinate of target stays at centerHeight
            _targetCenterPoint.y = centerHeight;
        }
        
        private void RotateOnParaboloid()
        {
            // Get current mouse position
            Vector2 currentMousePosition = Mouse.current.position.ReadValue();
            
            // Calculate mouse delta (only if the mouse position has actually changed)
            Vector2 mouseDelta = currentMousePosition - _previousMousePosition;
            
            // Only proceed if there's actual mouse movement
            if (mouseDelta.sqrMagnitude > POSITION_EPSILON)
            {
                // Calculate horizontal movement based on mouse delta
                float horizontalMovement = mouseDelta.x * horizontalRotationSpeed * Time.deltaTime;
                
                // Update target camera angle around the center point
                _targetCameraAngle += horizontalMovement;
                
                // Keep target angle in [0, 360) range
                _targetCameraAngle = _targetCameraAngle % 360f;
                if (_targetCameraAngle < 0)
                    _targetCameraAngle += 360f;
                
                // Update previous mouse position for next frame
                _previousMousePosition = currentMousePosition;
            }
        }

        private void UpdateCameraPosition()
        {
            // Ensure minimum height above the center is maintained
            float heightAboveCenter = Mathf.Max(_cameraHeight, minHeight);
        
            // Calculate radius using the formula r = sqrt(heightAboveCenter * inverseCoeff)
            // This gives us the distance from center on the XZ plane
            float radius = Mathf.Sqrt(heightAboveCenter * paraboloidCoefficient);
        
            // Convert polar coordinates to Cartesian coordinates
            // Pre-compute sin and cos values for better performance
            float angleRad = _cameraAngle * Mathf.Deg2Rad;
            float x = radius * Mathf.Cos(angleRad);
            float z = radius * Mathf.Sin(angleRad);
        
            // Calculate final world position of camera
            Vector3 cameraPosition = new Vector3(
                _centerPoint.x + x,
                _centerPoint.y + heightAboveCenter,
                _centerPoint.z + z
            );
            
            // Update camera position and orientation
            controlledCamera.transform.position = cameraPosition;
            controlledCamera.transform.LookAt(_centerPoint);
        }
    }
}
