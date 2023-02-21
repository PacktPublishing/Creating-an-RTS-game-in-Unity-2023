using TMPro;
using UnityEngine;

namespace Dragoncraft
{
    public class DamageFeedbackUI : MonoBehaviour
    {
        [SerializeField]
        private ObjectPoolComponent _objectPool;

        private void OnEnable()
        {
            MessageQueueManager.Instance.AddListener<DamageFeedbackMessage>(OnDamageFeedback);
        }

        private void OnDisable()
        {
            MessageQueueManager.Instance.RemoveListener<DamageFeedbackMessage>(OnDamageFeedback);
        }

        private void OnDamageFeedback(DamageFeedbackMessage message)
        {
            GameObject damageFeedback = _objectPool.GetObject();
            damageFeedback.transform.SetParent(transform, false);

            Vector3 position = Camera.main.WorldToScreenPoint(message.Position);
            RectTransform rectTransform = damageFeedback.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = position + new Vector3(Random.value * 100, Random.value * 10, 0);

            TMP_Text damageText = damageFeedback.GetComponentInChildren<TMP_Text>();
            damageText.text = $"-{message.Damage}";
        }
    }
}
