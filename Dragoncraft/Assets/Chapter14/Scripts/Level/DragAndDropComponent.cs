using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Dragoncraft
{
    public class DragAndDropComponent : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _layerMask;

        [SerializeField]
        private string _tag = "Building";

        private GameObject _selectedGameObject;
        private float _startPositionY;
        private float _positionYWhileMoving;

        private void Update()
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (_selectedGameObject == null)
                {
                    StartDragging();
                }
                else
                {
                    StopDragging();
                }
            }

            if (_selectedGameObject != null)
            {
                DragObject();
            }
        }

        private void StartDragging()
        {
            RaycastHit hit = GetRaycastHit();
            if (hit.collider == null || !hit.collider.CompareTag(_tag))
            {
                return;
            }

            _selectedGameObject = hit.collider.gameObject;
            _startPositionY = _selectedGameObject.transform.position.y;
            _positionYWhileMoving = _startPositionY + 1;

            if (_selectedGameObject.TryGetComponent<NavMeshObstacle>(out var navMeshObstacle))
            {
                navMeshObstacle.enabled = false;
            }

            Cursor.visible = false;
        }

        private void StopDragging()
        {
            Vector3 worldPosition = GetWorldMousePosition();
            _selectedGameObject.transform.position = new Vector3(worldPosition.x, _startPositionY, worldPosition.z);

            if (_selectedGameObject.TryGetComponent<NavMeshObstacle>(out var navMeshObstacle))
            {
                navMeshObstacle.enabled = true;
            }

            _selectedGameObject = null;
            Cursor.visible = true;
        }

        private void DragObject()
        {
            Vector3 worldPosition = GetWorldMousePosition();
            _selectedGameObject.transform.position = new Vector3(worldPosition.x, _positionYWhileMoving, worldPosition.z);
        }

        private Vector3 GetWorldMousePosition()
        {
            Vector3 selectedPosition = Camera.main.WorldToScreenPoint(_selectedGameObject.transform.position);
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectedPosition.z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            return worldPosition;
        }

        private RaycastHit GetRaycastHit()
        {
            Vector3 worldMousePositionMax = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane));
            Vector3 worldMousePositionMin = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

            RaycastHit hit;
            Physics.Raycast(worldMousePositionMin, worldMousePositionMax - worldMousePositionMin, out hit, 1000, _layerMask);
            return hit;
        }
    }
}