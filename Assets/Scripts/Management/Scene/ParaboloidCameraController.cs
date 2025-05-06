using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Management.Scene
{
    public class ParaboloidCameraController : MonoBehaviour
    {
        [Header("Paraboloid Settings")]
        [Tooltip("Coefficient for the paraboloid equation (y = a * (x² + z²))")]
        [SerializeField] private float paraboloidCoefficient = 0.1f;
        
        [Tooltip("Height of the paraboloid center (Y coordinate)")]
        [SerializeField] private float centerHeight = 0.0f;
        
        [Header("Camera Settings")]
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
        
        [Tooltip("Horizontal movement speed on the paraboloid")]
        [SerializeField] private float horizontalMoveSpeed = 20.0f;
        
        [Tooltip("Vertical movement speed on the paraboloid")]
        [SerializeField] private float verticalMoveSpeed = 5.0f;
        
        [Tooltip("Smoothing factor for camera height changes (lower values = smoother)")]
        [Range(1f, 20f)]
        [SerializeField] private float heightSmoothingSpeed = 10.0f;
        
        [Tooltip("Multiplier for center movement speed when holding shift")]
        [SerializeField] private float sprintMultiplier = 2.0f;
        
        [Header("Camera Settings")]
        
        // Center point of the paraboloid
        private Vector3 _centerPoint;
        
        // Camera position relative to center in cylindrical coordinates
        private float _cameraHeight;
        private float _targetCameraHeight; // Target height for smooth interpolation
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
            
            // Ensure min/max values are valid
            minHeight = Mathf.Max(0.1f, minHeight);
            maxHeight = Mathf.Max(minHeight + 1.0f, maxHeight);
            
            // Initialize camera height using the configurable parameter
            _cameraHeight = Mathf.Clamp(initialCameraHeight, minHeight, maxHeight);
            _targetCameraHeight = _cameraHeight; // Initialize target height
            
            // Initialize center point with the specified height
            _centerPoint = new Vector3(0, centerHeight, 0);
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
            
            // Update target camera height above center based on scroll
            _targetCameraHeight += scrollValue * verticalMoveSpeed * Time.deltaTime * 10f;
            _targetCameraHeight = Mathf.Clamp(_targetCameraHeight, minHeight, maxHeight);
        }
        
        private void Update()
        {
            // Smoothly interpolate camera height towards target height
            if (!Mathf.Approximately(_cameraHeight, _targetCameraHeight))
            {
                _cameraHeight = Mathf.Lerp(_cameraHeight, _targetCameraHeight, Time.deltaTime * heightSmoothingSpeed);
                
                // Update camera position if height changed
                UpdateCameraPosition();
            }
            
            // Move center point with WASD
            MoveCenterPoint();
            
            // Handle horizontal rotation on paraboloid if middle mouse is pressed
            if (_isRotating)
            {
                RotateOnParaboloid();
            }
        }
        
        private void MoveCenterPoint()
        {
            // Skip if no input
            if (_moveInput.sqrMagnitude < 0.01f)
                return;

            // Get movement values from the input system
            float horizontalInput = _moveInput.x; // AD keys
            float verticalInput = _moveInput.y; // WS keys

            // Get camera's forward direction and project it onto XZ plane
            Vector3 cameraForward = transform.forward;
            Vector3 projectedForward = Vector3.ProjectOnPlane(cameraForward, Vector3.up).normalized;

            // Get camera's right vector (already on XZ plane since camera only rotates around Y)
            Vector3 cameraRight = transform.right;

            // Calculate movement direction using camera-relative vectors
            Vector3 direction = (cameraRight * horizontalInput + projectedForward * verticalInput).normalized;

            // Apply sprint multiplier if sprinting
            float currentSpeed = _isSprinting ? centerMoveSpeed * sprintMultiplier : centerMoveSpeed;

            float angle = Vector3.Angle(cameraForward, projectedForward);
            
            // Calculate compensation factor based on angle between camera forward and its projection to compensate for inclination
            float compensationFactor = Mathf.Sin(angle * Mathf.Deg2Rad);
            
            // Prevent division by very small numbers
            if (compensationFactor < 0.01f)
                compensationFactor = 0.01f;
            compensationFactor = 1f;
            var currentSpeedByDeltaTime = currentSpeed * Time.deltaTime;
            
            // Move the center point, preserving its Y coordinate (centerHeight)
            _centerPoint += new Vector3(direction.x * currentSpeedByDeltaTime / compensationFactor, 0, direction.z * currentSpeedByDeltaTime);

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
            // Ensure minimum height above the center is maintained
            // This guarantees the camera is at least minHeight units above the center point
            float heightAboveCenter = Mathf.Max(_cameraHeight, minHeight);
        
            // Using the modified paraboloid equation: (y - centerHeight) = a * (x² + z²)
            // We can solve for the radius r = sqrt((heightAboveCenter/a))
            // where r² = x² + z²
            float radius = Mathf.Sqrt(heightAboveCenter / paraboloidCoefficient);
        
            // Convert angle to Cartesian coordinates using the calculated radius
            float x = radius * Mathf.Cos(_cameraAngle * Mathf.Deg2Rad);
            float z = radius * Mathf.Sin(_cameraAngle * Mathf.Deg2Rad);
        
            // Calculate the absolute height (world space) by adding the height above center to the center's height
            float absoluteY = _centerPoint.y + heightAboveCenter;
        
            // Set camera position relative to ground level (not center point's height)
            // We use _centerPoint.x and _centerPoint.z, but calculate y independently
            transform.position = new Vector3(_centerPoint.x + x, absoluteY, _centerPoint.z + z);
            
            // Always make the camera look at the center point
            transform.LookAt(_centerPoint);
        }
    }
}
