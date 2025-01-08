using UnityEngine;
using UnityEngine.UI;

namespace Public.AkeOme
{
    /// <summary>
    /// テキストの縮小拡大
    /// </summary>
    public class StretchText : MonoBehaviour
    {
        [SerializeField] private float _widthScale = 0.25f;
        [SerializeField] private float _heightScale = 5f;
        [SerializeField] private float _size = 5f;
        [SerializeField] private float _rotation = 0f;
        [SerializeField] private string _message;
        private RectTransform _textRectTransform;
        private Text _text;

        private void Start()
        {
            _text = GetComponent<Text>();
            _textRectTransform = _text.GetComponent<RectTransform>();
            _text.text = _message;
            StretchTextToAspectRatio();
        }

        private void StretchTextToAspectRatio()
        {
            if (_textRectTransform == null) return;
            // 拡大縮小
            _textRectTransform.localScale = new Vector2(_size * _widthScale, _size * _heightScale);
            _textRectTransform.rotation = Quaternion.Euler(0f, 0f, _rotation);
        }
    }
}