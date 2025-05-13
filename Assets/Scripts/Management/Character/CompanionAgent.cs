using System.Collections;
using DnD.Code.Scripts.Characters;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Management.Character
{
    public class CompanionAgent : MonoBehaviour
    {
        [SerializeField]
        private InputSystem_Actions inputActions;

        [SerializeField]
        private float moveSpeed = 5f;

        [SerializeField]
        private float rotationSpeed = 10f;

        [SerializeField]
        private float stoppingDistance = 0.1f;

        private InputAction moveToPointAction;
        private bool isMoving = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Log("Character start");
        }
        
        void Awake()
        {
            Debug.Log("Character awoken");
            
            // Initialize input actions if not already set
            if (inputActions == null)
            {
                inputActions = new InputSystem_Actions();
            }
            
            // Get the MoveToPoint action and subscribe to it
            moveToPointAction = inputActions.InGame.MoveToPoint;
            moveToPointAction.performed += OnMoveToPoint;
            
            // Enable the action map
            inputActions.InGame.Enable();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        private void OnMoveToPoint(InputAction.CallbackContext context)
        {
            // Get the mouse position from Mouse.current
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            
            // Log the position for debugging
            Debug.Log($"MoveToPoint action triggered at mouse position: {mousePosition}");
            
            // Convert screen position to world position
            Vector3 worldPosition = GetWorldPositionFromMouse(mousePosition);
            
            // Implement your character movement logic here
            MoveCharacterToPosition(worldPosition);
        }
        
        private Vector3 GetWorldPositionFromMouse(Vector2 mousePosition)
        {
            // Create a ray from the camera through the mouse position
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            
            // Use Physics.Raycast to find where the ray hits the ground
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, LayerMask.GetMask("Ground")))
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.green, 1.0f);
                return hitInfo.point;
            }
            
            // If no hit on Ground layer, try a general raycast
            if (Physics.Raycast(ray, out hitInfo, 100f))
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.yellow, 1.0f);
                return hitInfo.point;
            }
            
            // If no hit at all, project to a plane at y=0
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            if (groundPlane.Raycast(ray, out float distance))
            {
                Vector3 point = ray.GetPoint(distance);
                Debug.DrawLine(ray.origin, point, Color.red, 1.0f);
                return point;
            }
            
            return Vector3.zero;
        }
        
        private void MoveCharacterToPosition(Vector3 targetPosition)
        {
            // Set target position (excluding y to keep character at same height)
            Vector3 targetPosWithCurrentHeight = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            
            // Log the destination
            Debug.Log($"Moving character to world position: {targetPosWithCurrentHeight}");
            
            // Check if we have a NavMeshAgent component
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                // Use NavMeshAgent for movement (recommended for pathfinding)
                agent.SetDestination(targetPosWithCurrentHeight);
                
                // Optional: Handle animation triggers here if needed
                // StartCoroutine(HandleMovementAnimation(agent));
            }
            else
            {
                // Fall back to direct movement if no NavMeshAgent is available
                if (isMoving)
                {
                    StopAllCoroutines(); // Stop any existing movement
                }
                
                isMoving = true;
                StartCoroutine(MoveOverTime(targetPosWithCurrentHeight));
            }
        }
        
        private IEnumerator MoveOverTime(Vector3 targetPosition)
        {
            while (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                // Calculate direction and move
                Vector3 direction = (targetPosition - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
                
                // Rotate towards movement direction
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
                
                yield return null;
            }
            
            // Snap to final position
            transform.position = targetPosition;
            isMoving = false;
            
            // Trigger any "arrived at destination" logic here
            Debug.Log("Character arrived at destination");
        }
        
        /*
        // Optional method to handle animations based on NavMeshAgent state
        private IEnumerator HandleMovementAnimation(NavMeshAgent agent)
        {
            // Example: Play movement animation while agent is moving
            while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
            {
                // You could set animator parameters here
                // animator.SetBool("IsMoving", true);
                yield return null;
            }
            
            // When character stops moving
            // animator.SetBool("IsMoving", false);
            Debug.Log("Character arrived at destination");
        }
        */
        
        private void OnDestroy()
        {
            // Clean up by unsubscribing from the input action
            if (moveToPointAction != null)
            {
                moveToPointAction.performed -= OnMoveToPoint;
            }
            
            // Disable and dispose of input actions
            inputActions?.Dispose();
        }
    }
}