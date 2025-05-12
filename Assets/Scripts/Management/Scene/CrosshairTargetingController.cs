using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Management.Scene
{
public class CrosshairDecalController : MonoBehaviour
    {
        [Header("Crosshair Settings")] [SerializeField]
        private DecalProjector mouseCrosshair;

        [SerializeField] private DecalProjector targetCrosshair;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float decalOffset = 0.1f;
        
        private Transform playerTransform;

        [SerializeField] private float destinationReachedDistance = 0.5f;

        private Vector3 targetPosition;
        private bool hasActiveTarget = false;

        private void Awake()
        {
            // Get references if not assigned in inspector
            if (mainCamera == null)
                mainCamera = Camera.main;

            if (mouseCrosshair == null)
                mouseCrosshair = GetComponent<DecalProjector>();

            // Make sure target crosshair is disabled initially
            if (targetCrosshair != null)
            {
                targetCrosshair.enabled = false;
            }
            else
            {
                Debug.LogError("Target crosshair decal projector not assigned!");
            }

            // Make sure player transform is assigned
            if (playerTransform == null)
            {
                Debug.LogError("Player transform not assigned!");
            }
        }

        private void Update()
        {
            // Update the mouse-following crosshair
            UpdateMouseCrosshair();

            // Handle mouse click to set destination
            if (Input.GetMouseButtonDown(0))
            {
                SetTargetPosition();
            }

            // Check if player has reached the target
            if (hasActiveTarget && targetCrosshair != null && targetCrosshair.enabled)
            {
                CheckIfDestinationReached();
            }
        }

        private void UpdateMouseCrosshair()
        {
            if (mouseCrosshair == null || mainCamera == null) return;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, groundLayer))
            {
                // Position the mouse crosshair slightly above the hit point
                Vector3 position = hitInfo.point + Vector3.up * decalOffset;
                mouseCrosshair.transform.position = position;
            }
        }

        private void SetTargetPosition()
        {
            if (!targetCrosshair || !mainCamera) return;

            if (!playerTransform)
            {
                var playerObject = GameObject.FindGameObjectWithTag("Player");
                if (playerObject)
                {
                    playerTransform = playerObject.transform;
                }
            }
            
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, groundLayer))
            {
                targetPosition = hitInfo.point;

                // Position the target crosshair at the click position
                targetCrosshair.transform.position = targetPosition + Vector3.up * decalOffset;

                // Make sure the target crosshair is enabled
                targetCrosshair.enabled = true;
                hasActiveTarget = true;

                // Here you would call your player movement code
                // MovePlayerToPosition(targetPosition);
            }
        }

        private void CheckIfDestinationReached()
        {
            if (playerTransform == null) return;

            // Calculate horizontal distance (ignore Y axis for ground movement)
            Vector2 playerPos2D = new Vector2(playerTransform.position.x, playerTransform.position.z);
            Vector2 targetPos2D = new Vector2(targetPosition.x, targetPosition.z);

            float distanceToTarget = Vector2.Distance(playerPos2D, targetPos2D);

            if (distanceToTarget <= destinationReachedDistance)
            {
                // Player has reached the destination
                targetCrosshair.enabled = false;
                hasActiveTarget = false;
            }
        }
    }
}