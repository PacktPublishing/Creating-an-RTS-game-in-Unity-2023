using UnityEngine;
using UnityEngine.UI;

namespace Dragoncraft
{
    // Ensures that the game object will have both required components
    [RequireComponent(typeof(Image), typeof(RectTransform))]
    public class UnitSelectorUI : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Image _selector;
        private Vector3 _startPosition;
        private Vector3 _offset;

        private void Awake()
        {
            // Gets the RectTransform component used to position and scale the selector image
            _rectTransform = GetComponent<RectTransform>();
            // Gets the Image component to show/hide depending on the player mouse input
            _selector = GetComponent<Image>();
            // Hides the image by default
            _selector.enabled = false;
            // The offset will reduce half of the screen size to the pouse position
            _offset = new Vector3(Screen.width / 2, Screen.height / 2);
        }

        public void Update()
        {
            // Checks for the left mouse button pressed
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // Shows the image
                _selector.enabled = true;
                // Sets the start position to the current mouse position
                _startPosition = Input.mousePosition;
                // Sets the image position to the current mouse position minus the offset
                _rectTransform.localPosition = Input.mousePosition - _offset;
            }

            // Checks for the left mouse button held pressed
            if (Input.GetKey(KeyCode.Mouse0))
            {
                // Scales the image based on the difference from the start mouse position and the current mouse position
                _rectTransform.localScale = _startPosition - Input.mousePosition;
            }

            // Checks for the left mouse button released
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                // Hides the image when the left mouse button is released
                _selector.enabled = false;
            }
        }
    }
}