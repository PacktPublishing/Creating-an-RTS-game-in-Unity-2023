using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dragoncraft
{
    public class UnitSelectorComponent : MonoBehaviour
    {
        private float _distanceBetweenUnits = 2.0f;
        private MeshCollider _meshCollider = null;
        private Vector3 _startPosition;
        private List<UnitComponent> _units = new List<UnitComponent>();
        private float _minDistanceValue = 0.4f;

        private void Awake()
        {
            GameObject plane = GameObject.FindGameObjectWithTag("Plane");
            if (plane != null)
            {
                _meshCollider = plane.GetComponent<MeshCollider>();
            }
            if (_meshCollider == null)
            {
                Debug.LogError("Missing MeshCollider reference! Check if the Plane has the correct tag.");
            }
        }

        private void Update()
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _startPosition = GetMousePostion();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Vector3 endPosition = GetMousePostion();

                SelectUnits(_startPosition, endPosition);
            }

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                Vector3 movePosition = GetMousePostion();

                MoveSelectedUnits(movePosition);
            }
        }

        private Vector3 GetMousePostion()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitData;
            if (_meshCollider.Raycast(ray, out hitData, 1000))
            {
                return hitData.point;
            }

            return Vector3.zero;
        }

        private void SelectUnits(Vector3 startPostion, Vector3 endPosition)
        {
            _units.Clear();

            Vector3 center = (_startPosition + endPosition) / 2;
            float distance = Vector3.Distance(center, endPosition);
            distance = distance < _minDistanceValue ? _minDistanceValue : distance;
            Vector3 halfExtents = new Vector3(distance, distance, distance);

            GameObject model = null;
            ActionType actions = ActionType.None;

            Collider[] colliders = Physics.OverlapBox(center, halfExtents);
            foreach (Collider collider in colliders)
            {
                UnitComponent unit = collider.GetComponent<UnitComponent>();
                if (unit != null && !unit.IsDead)
                {
                    _units.Add(unit);

                    if (model == null)
                    {
                        model = collider.gameObject;
                        actions = unit.Actions;
                    }
                }
            }

            MessageQueueManager.Instance.SendMessage(new UpdateDetailsMessage { Units = _units, Model = model });
            MessageQueueManager.Instance.SendMessage(new UpdateActionsMessage { Actions = actions });
        }

        private void MoveSelectedUnits(Vector3 movePosition)
        {
            if (_units.Count == 0)
            {
                MessageQueueManager.Instance.SendMessage(new UpdateDetailsMessage { Units = _units, Model = null });
                MessageQueueManager.Instance.SendMessage(new UpdateActionsMessage { Actions = ActionType.None });
                return;
            }

            int rows = Mathf.RoundToInt(Mathf.Sqrt(_units.Count));
            int counter = 0;

            for (int i = 0; i < _units.Count; i++)
            {
                if (i > 0 && (i % rows) == 0)
                {
                    counter++;
                }
                float offsetX = (i % rows) * _distanceBetweenUnits;
                float offsetZ = counter * _distanceBetweenUnits;
                Vector3 offset = new Vector3(offsetX, 0, offsetZ);
                _units[i].MoveTo(movePosition + offset);
            }
        }
    }
}