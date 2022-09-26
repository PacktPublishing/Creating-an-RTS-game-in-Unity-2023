using UnityEngine;

namespace Dragoncraft
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private float _borderSize = 50f;

        [SerializeField]
        private float _panSpeed = 10f;

        [SerializeField]
        private Vector2 _panLimit = new Vector2(30f, 35f);

        [SerializeField]
        private float _scrollSpeed = 1000f;

        [SerializeField]
        private Vector2 _scrollLimit = new Vector2(5f, 10f);

        private Vector3 _initialPosition = Vector3.zero;
        private Camera _camera = null;

        private void Start()
        {
            // Store the initial position to use it in the movement calculation
            _initialPosition = transform.position;
            // Getting the camera component to update the orthographic size
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            UpdateZoom();
            UpdatePan();
        }

        private void UpdateZoom()
        {
            // Using the Unity API to get the mouse scroll wheel movement value
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            // The scroll value now is multiplied by the scroll speed and the deltaTime
            scroll = scroll * _scrollSpeed * Time.deltaTime;
            // Setting the camera orthographic size to the new zoom by adding the scroll value
            _camera.orthographicSize += scroll;
            // Using the Clamp method to make sure the orthographic size stays between the limits
            _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, _scrollLimit.x, _scrollLimit.y);
        }

        private void UpdatePan()
        {
            // Store the currect position so it can be modified
            Vector3 position = transform.position;

            // Checks if the mouse cursor position is within the border size
            if (Input.mousePosition.y >= Screen.height - _borderSize)
            {
                position.z += _panSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.y <= _borderSize)
            {
                position.z -= _panSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.x >= Screen.width - _borderSize)
            {
                position.x += _panSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.x <= _borderSize)
            {
                position.x -= _panSpeed * Time.deltaTime;
            }

            // Ignore the calculation if the camera did not change position
            if (position == transform.position)
            {
                return;
            }

            // Clamp both values between the pan limit also considering the initial position
            // We do not update the Y position because it does not affect the camera
            position.x = Mathf.Clamp(position.x, -_panLimit.x + _initialPosition.x, _panLimit.x + _initialPosition.x);
            position.z = Mathf.Clamp(position.z, -_panLimit.y + _initialPosition.z, _panLimit.y + _initialPosition.z);

            // Set the updated position to the camera transform
            transform.position = position;
        }
    }
}