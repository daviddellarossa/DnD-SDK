using UnityEngine;

namespace Management.UI
{
    public class DecalManager : MonoBehaviour
    {
        [SerializeField] private MeshRenderer decalRenderer;
        [SerializeField] private float decalSize = 0.5f;
        [SerializeField] private Color decalColor = new Color(0, 1, 0, 0.5f); // Green, semi-transparent
        [SerializeField] private bool rotateDecal = true;
        [SerializeField] private float rotationSpeed = 30f;
        [SerializeField] private bool pulseEffect = true;
        [SerializeField] private float pulseSpeed = 1f;
        [SerializeField] private float pulseMin = 0.8f;
        [SerializeField] private float pulseMax = 1.2f;
        
        private Material decalMaterial;
        private Transform decalTransform;
        private bool isVisible = false;
        
        void Awake()
        {
            decalTransform = transform;
            
            // Get or create renderer
            if (decalRenderer == null)
            {
                decalRenderer = GetComponent<MeshRenderer>();
                if (decalRenderer == null)
                {
                    SetupDecalVisuals();
                }
            }
            
            // Get material
            decalMaterial = decalRenderer.material;
            
            // Hide by default
            HideDecal();
        }
        
        void Update()
        {
            if (isVisible)
            {
                // Rotate the decal if enabled
                if (rotateDecal)
                {
                    decalTransform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                }
                
                // Apply pulse effect if enabled
                if (pulseEffect)
                {
                    float scale = Mathf.Lerp(pulseMin, pulseMax, 
                        (Mathf.Sin(Time.time * pulseSpeed) + 1) * 0.5f);
                    
                    decalTransform.localScale = new Vector3(
                        decalSize * scale, 
                        decalSize * scale, 
                        decalSize * scale);
                }
            }
        }
        
        private void SetupDecalVisuals()
        {
            // Create a simple quad for the decal
            GameObject decalObject = new GameObject("DecalMesh");
            decalObject.transform.SetParent(transform);
            decalObject.transform.localPosition = Vector3.zero;
            
            MeshFilter meshFilter = decalObject.AddComponent<MeshFilter>();
            decalRenderer = decalObject.AddComponent<MeshRenderer>();
            
            // Create a simple quad mesh
            Mesh mesh = new Mesh();
            float halfSize = 0.5f;
            
            mesh.vertices = new Vector3[]
            {
                new Vector3(-halfSize, 0, -halfSize),
                new Vector3(halfSize, 0, -halfSize),
                new Vector3(halfSize, 0, halfSize),
                new Vector3(-halfSize, 0, halfSize)
            };
            
            mesh.uv = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 1)
            };
            
            mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
            mesh.RecalculateNormals();
            
            meshFilter.mesh = mesh;
            
            // Create material
            decalMaterial = new Material(Shader.Find("Transparent/Diffuse"));
            decalMaterial.color = decalColor;
            decalRenderer.material = decalMaterial;
            
            // Adjust transform
            decalTransform.localScale = new Vector3(decalSize, decalSize, decalSize);
        }
        
        public void SetPosition(Vector3 position)
        {
            decalTransform.position = position;
        }
        
        public void ShowDecal()
        {
            decalRenderer.enabled = true;
            isVisible = true;
        }
        
        public void HideDecal()
        {
            decalRenderer.enabled = false;
            isVisible = false;
        }
        
        public void SetColor(Color color)
        {
            decalColor = color;
            if (decalMaterial != null)
            {
                decalMaterial.color = color;
            }
        }
        
        public void SetSize(float size)
        {
            decalSize = size;
            decalTransform.localScale = new Vector3(size, size, size);
        }
    }
}
