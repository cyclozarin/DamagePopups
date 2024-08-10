using System.Collections;
using TMPro;
using UnityEngine;

namespace DamagePopups.Behaviours
{
    internal class UiPopupFadeOuter : MonoBehaviour
    {
        private float _fadeOutTime = 3f;
        private TextMeshProUGUI _textMesh = null!;

        private void Start()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
            StartCoroutine(MoveAndFadeOut());
        }

        private IEnumerator MoveAndFadeOut()
        {
            float _counter = 0f;
            while (_counter < _fadeOutTime)
            {
                _counter += Time.deltaTime;
                float _smoothValue = _counter / _fadeOutTime;
                gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, gameObject.transform.localPosition + gameObject.transform.right, _fadeOutTime);
                _textMesh.color = Color.Lerp(_textMesh.color, Color.black, _smoothValue);
                _textMesh.alpha = Mathf.Lerp(_textMesh.alpha, 0f, _smoothValue);
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}
