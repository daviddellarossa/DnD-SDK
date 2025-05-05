using UnityEngine;

namespace Management.Scene
{
    [RequireComponent(typeof(Camera))]
    public class MainCamera : MonoBehaviour
    {
        public float cameraHeight = 15f;
        public float cameraDistance = 20f;
        public float angleX = 30f;
        public float angleY = 0f;

        void Start()
        {
            if (Camera.main != null)
            {
                Camera.main.orthographic = true;
                Camera.main.orthographicSize = 7f;
            }
            else 
            {
                DeeDeeR.MessageBroker.MessageBroker.Instance.Logger.Send_OnLog(this, "Logger", "Camera.main is null", LogType.Error);
            }
            
            transform.position = new Vector3(0, cameraHeight, -cameraDistance);
            transform.rotation = Quaternion.Euler(angleX, angleY, 0f);
        }
    }
}
