using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Dragoncraft
{
    public class DetailsUpdater : MonoBehaviour
    {
        [SerializeField]
        private ObjectPoolComponent _objectPool;

        [SerializeField]
        private int _maxObjectsPerRow = 5;

        [SerializeField]
        private GameObject _row1;

        [SerializeField]
        private GameObject _row2;

        [SerializeField]
        private GameObject _portraitModel;

        private void OnEnable()
        {
            MessageQueueManager.Instance.AddListener<UpdateDetailsMessage>(OnDetailsUpdated);
        }

        private void OnDisable()
        {
            MessageQueueManager.Instance.RemoveListener<UpdateDetailsMessage>(OnDetailsUpdated);
        }

        private void OnDetailsUpdated(UpdateDetailsMessage message)
        {
            CleanUpRows();
            UpdateRows(message.Units);
            UpdateModel(message.Model);
        }

        private void CleanUpRows()
        {
            RemoveObjectsFromRow(_row1);
            RemoveObjectsFromRow(_row2);
        }

        private void RemoveObjectsFromRow(GameObject row)
        {
            for (int i = 0; i < row.transform.childCount; i++)
            {
                row.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void UpdateRows(List<UnitComponent> units)
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (i < _maxObjectsPerRow)
                {
                    AddObjectToRow(_row1, _objectPool.GetObject());
                }
                else if (i < _maxObjectsPerRow * 2)
                {
                    AddObjectToRow(_row2, _objectPool.GetObject());
                }
                else
                {
                    Debug.Log($"More than {_maxObjectsPerRow * 2} units selected");
                    break;
                }
            }
        }

        private void AddObjectToRow(GameObject row, GameObject selectedUnit)
        {
            if (selectedUnit != null)
            {
                selectedUnit.transform.SetParent(row.transform, false);
            }
        }

        private void UpdateModel(GameObject model)
        {
            for (int i = 0; i < _portraitModel.transform.childCount; i++)
            {
                Destroy(_portraitModel.transform.GetChild(i).gameObject);
            }

            if (model == null)
            {
                return;
            }

            GameObject newPortrait = Instantiate(model);
            ResetTransform(newPortrait);
            RemoveUnitComponent(newPortrait);
            newPortrait.SetLayerMaskToAllChildren("Portrait");
        }

        private void ResetTransform(GameObject portrait)
        {
            portrait.transform.position = Vector3.zero;
            portrait.transform.rotation = Quaternion.identity;
            portrait.transform.SetParent(_portraitModel.transform, false);
        }

        private void RemoveUnitComponent(GameObject portrait)
        {
            BaseCharacter character = portrait.GetComponent<BaseCharacter>();
            Destroy(character);

            CollisionComponent collision = portrait.GetComponent<CollisionComponent>();
            Destroy(collision);

            NavMeshAgent navMeshAgent = portrait.GetComponent<NavMeshAgent>();
            Destroy(navMeshAgent);

            Rigidbody rigidbody = portrait.GetComponent<Rigidbody>();
            Destroy(rigidbody);

            SphereCollider sphereCollider = portrait.GetComponent<SphereCollider>();
            Destroy(sphereCollider);
        }
    }
}