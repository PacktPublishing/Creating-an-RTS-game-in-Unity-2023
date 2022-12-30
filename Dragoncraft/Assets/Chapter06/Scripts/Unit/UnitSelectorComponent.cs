using System.Collections.Generic;
using UnityEngine;

namespace Dragoncraft
{
    public class UnitSelectorComponent : MonoBehaviour
    {
        private float _distanceBetweenUnits = 2.0f;
        private MeshCollider _meshCollider = null;
        private Vector3 _startPosition;
        private List<UnitComponent> _units = new List<UnitComponent>();

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
            foreach (UnitComponent unit in _units)
            {
                unit.Selected(false);
            }
            _units.Clear();

            Vector3 center = (_startPosition + endPosition) / 2;
            float distance = Vector3.Distance(center, endPosition);
            Vector3 halfExtents = new Vector3(distance, distance, distance);

            Collider[] colliders = Physics.OverlapBox(center, halfExtents);
            foreach (Collider collider in colliders)
            {
                UnitComponent unit = collider.GetComponent<UnitComponent>();
                if (unit != null)
                {
                    unit.Selected(true);
                    _units.Add(unit);
                }
            }
        }

        private void MoveSelectedUnits(Vector3 movePosition)
        {
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