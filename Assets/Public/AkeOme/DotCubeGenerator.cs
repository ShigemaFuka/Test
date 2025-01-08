using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Public.AkeOme
{
    /// <summary>
    /// ドットを生成
    /// </summary>
    public class DotCubeGenerator : MonoBehaviour
    {
        [SerializeField, Header("ドットグリッドデータ")] private EditableDotPattern _editableDotPattern;
        [SerializeField, Header("Cube プレハブ")] private GameObject _cubePrefab; // Cube プレハブ
        [SerializeField, Header("配置のオフセット")] private Vector3 _gridOffset = new(0, 0, 0); // 配置する基準点からのオフセット
        [SerializeField, Header("ドット間隔")] private float _dotSpacing = 1.0f; // 各ドット間の間隔
        [SerializeField, Header("回転角度 (度)")] private float _rotationAngleX = 0f; // X 軸方向の回転角度
        [SerializeField] private Button _target;
        private List<GameObject> _spawnedCubes = new();


        private void Start()
        {
            if (_target != null)
            {
                _target.onClick.AddListener(GenerateCubes);
            }
        }

        /// <summary>
        /// ドットの位置に Cube を配置する
        /// </summary>
        private void GenerateCubes()
        {
            // 既存のCubeを削除
            foreach (var cube in _spawnedCubes)
            {
                Destroy(cube);
            }

            _spawnedCubes.Clear();
            for (var charIndex = 0; charIndex < _editableDotPattern._grids.Count; charIndex++)
            {
                var characterGrid = _editableDotPattern._grids[charIndex];

                // 各行列内のドット位置を調べる
                for (var row = 0; row < characterGrid.Count; row++)
                {
                    for (var col = 0; col < characterGrid[row].Count; col++)
                    {
                        if (characterGrid[row][col] == 1) // ドットが有効な場合
                        {
                            var cubePosition = new Vector3(
                                _gridOffset.x + col * _dotSpacing +
                                charIndex * (characterGrid[0].Count * _dotSpacing), // X 軸: 列
                                _gridOffset.y, // Y 軸: 一定
                                _gridOffset.z - row * _dotSpacing // Z 軸: 行
                            );

                            var rotatedPosition = RotateAroundX(cubePosition, _rotationAngleX);

                            var cube = Instantiate(_cubePrefab, rotatedPosition, Quaternion.identity, transform);
                            _spawnedCubes.Add(cube); // 作成したCubeをリストに追加
                        }
                    }
                }
            }
        }

        /// <summary>
        /// X 軸を中心に座標を回転
        /// </summary>
        /// <param name="position"></param>
        /// <param name="angleInDegrees"></param>
        /// <returns></returns>
        private Vector3 RotateAroundX(Vector3 position, float angleInDegrees)
        {
            var radians = angleInDegrees * Mathf.Deg2Rad;
            var cos = Mathf.Cos(radians);
            var sin = Mathf.Sin(radians);

            // 回転行列を適用 (X 軸方向の回転)
            var y = position.y * cos - position.z * sin;
            var z = position.y * sin + position.z * cos;

            return new Vector3(position.x, y, z);
        }
    }
}