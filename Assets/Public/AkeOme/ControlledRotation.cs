using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Public.AkeOme
{
    /// <summary>
    /// オブジェクトを回転させる
    /// 上下操作はマウス
    /// </summary>
    public class ControlledRotation : MonoBehaviour, IDragHandler
    {
        [SerializeField, Header("回転速度")] private float _rotationSpeed = 100f;
        [SerializeField] private Button _button;
        [SerializeField, Header("回転を初期化")] private Button _resetButton;
        [SerializeField] private float _zRotation = 45f;
        private float verticalRotation = 0f; // 縦方向の回転角度を保存
        private Vector3 _defaultRotation;
        private float mouseY;

        private void Start()
        {
            _defaultRotation = transform.eulerAngles;
            if (_button)
            {
                _button.onClick.AddListener(RotateOnClick);
            }

            if (_resetButton)
            {
                _resetButton.onClick.AddListener(ResetOnClick);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            mouseY = Input.GetAxis("Mouse Y");

            // 回転角度を計算 (スピードを調整)
            verticalRotation = mouseY * _rotationSpeed * Time.deltaTime;
            // オブジェクトを回転 ※ワールド座標
            transform.eulerAngles += new Vector3(verticalRotation, 0f, 0f);
        }

        private void RotateOnClick()
        {
            transform.eulerAngles += new Vector3(0f, 0f, _zRotation);
        }

        private void ResetOnClick()
        {
            transform.eulerAngles = _defaultRotation;
        }
    }
}