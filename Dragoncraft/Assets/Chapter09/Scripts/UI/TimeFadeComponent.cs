using TMPro;
using UnityEngine;

namespace Dragoncraft
{
    public class TimeFadeComponent : MonoBehaviour
    {
        [SerializeField]
        private float _timeToLive = 2;

        private float _counter;
        private Color _originalColor;
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
            _originalColor = _text.color;
        }

        private void OnEnable()
        {
            _counter = _timeToLive;
            _text.color = _originalColor;
        }

        private void Update()
        {
            _counter -= Time.deltaTime;
            if (_counter < 0)
            {
                gameObject.SetActive(false);
                return;
            }

            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a - (Time.deltaTime / _timeToLive));
        }
    }
}
