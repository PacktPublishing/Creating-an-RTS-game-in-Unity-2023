using System.Collections.Generic;
using UnityEngine;

namespace Dragoncraft
{
    public class FogComponent : MonoBehaviour
    {
        [SerializeField]
        private float _radius = 7;

        [SerializeField]
        private float _initialArea = 14;

        [SerializeField]
        private LayerMask _layerMask;

        private Mesh _mesh;
        private Vector3[] _vertices;
        private Color[] _colors;

        private void Start()
        {
            Initialize();
            ClearInitialArea();
        }

        private void Update()
        {
            foreach (GameObject unit in LevelManager.Instance.Units)
            {
                UpdateUnit(unit);
            }
        }

        private void UpdateUnit(GameObject unit)
        {
            Ray ray = new Ray(Camera.main.transform.position, unit.transform.position - Camera.main.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, _layerMask, QueryTriggerInteraction.Collide))
            {
                RemoveFog(hit.point);
            }
        }

        private void RemoveFog(Vector3 point)
        {
            for (int i = 0; i < _vertices.Length; i++)
            {
                Vector3 position = transform.TransformPoint(_vertices[i]);

                float distance = Vector3.SqrMagnitude(position - point);
                float radiusSquare = _radius * _radius;
                if (distance < radiusSquare)
                {
                    float alpha = Mathf.Min(_colors[i].a, distance / radiusSquare);
                    _colors[i].a = alpha;
                }
            }

            _mesh.colors = _colors;
        }

        private void Initialize()
        {
            _mesh = GetComponent<MeshFilter>().mesh;
            _vertices = _mesh.vertices;

            _colors = new Color[_vertices.Length];
            for (int i = 0; i < _colors.Length; i++)
            {
                _colors[i] = Color.black;
            }

            _mesh.colors = _colors;
        }

        private void ClearInitialArea()
        {
            float fogPosY = transform.position.y;
            float distance = _initialArea;

            List<Vector3> area = new List<Vector3>
            {
                new Vector3(0, fogPosY, distance), //top-center
                new Vector3(-distance, fogPosY, distance), //top-left
                new Vector3(distance, fogPosY, distance), //top-right

                new Vector3(0, fogPosY, 0), //mid-center
                new Vector3(-distance, fogPosY, 0), //mid-left
                new Vector3(distance, fogPosY, 0), //mid-right

                new Vector3(0, fogPosY, -distance), //bottom-center
                new Vector3(-distance, fogPosY, -distance), //bottom-left
                new Vector3(distance, fogPosY, -distance), //bottom-right
            };

            foreach (Vector3 point in area)
            {
                RemoveFog(point);
            }
        }
    }
}

