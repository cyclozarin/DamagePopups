using System.Collections;
using TMPro;
using UnityEngine;

namespace DamagePopups.Behaviours
{
    internal class PopupFadeOuter : MonoBehaviour
    {
        private Vector3 _randomDesiredFadeOutPosition;
        private float _fadeOutTime = 3f;
        private TextMeshPro _textMesh = null!;

        private void Start()
        {
            _textMesh = GetComponent<TextMeshPro>();
            _randomDesiredFadeOutPosition = gameObject.transform.position + Random.insideUnitSphere;
            StartCoroutine(MoveAndFadeOut());
        }

        private IEnumerator MoveAndFadeOut()
        {
            float _counter = 0f;
            while (_counter < _fadeOutTime)
            {
                _counter += Time.deltaTime;
                float _smoothValue = _counter / _fadeOutTime;
                gameObject.transform.LookAt(MainCamera.instance.transform.position);
                gameObject.transform.Rotate(new(0, 180, 0));
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _randomDesiredFadeOutPosition, _fadeOutTime);
                _textMesh.color = Color.Lerp(_textMesh.color, Color.black, _smoothValue);
                _textMesh.alpha = Mathf.Lerp(_textMesh.alpha, 0f, _smoothValue);
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}
