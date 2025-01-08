using System.Collections.Generic;
using UnityEngine;

namespace Public.AkeOme
{
    /// <summary>
    /// ドットの情報
    /// </summary>
    public class EditableDotPattern : MonoBehaviour
    {
        [Header("パターン設定")] [Tooltip("各文字の5x5パターンをリストで定義")]
        public List<List<List<int>>> _grids = new()
        {
            new List<List<int>>()
            {
                new List<int> { 0, 0, 0, 0, 0, 0, 0 },
                new List<int> { 0, 0, 0, 0, 0, 0, 0 },
                new List<int> { 0, 0, 0, 0, 0, 0, 0 },
                new List<int> { 0, 0, 0, 0, 0, 0, 0 },
                new List<int> { 0, 0, 0, 0, 0, 0, 0 },
                new List<int> { 0, 0, 0, 0, 0, 0, 0 },
                new List<int> { 0, 0, 0, 0, 0, 0, 0 },
            }
        };

        private List<List<List<int>>> _gridsAkeOme = new()
        {
            new List<List<int>>()
            {
                new List<int> { 0, 0, 0, 0, 0, 0, 0 },
                new List<int> { 0, 1, 1, 1, 1, 1, 0 },
                new List<int> { 0, 0, 0, 0, 0, 1, 0 },
                new List<int> { 0, 0, 0, 1, 1, 0, 0 },
                new List<int> { 0, 0, 0, 1, 0, 0, 0 },
                new List<int> { 0, 0, 1, 1, 0, 0, 0 },
                new List<int> { 0, 0, 0, 0, 0, 0, 0 },
            },
            new List<List<int>>()
            {
                new List<int> { 0, 0, 0, 0, 0, 0, 0 },
                new List<int> { 0, 0, 1, 0, 0, 0, 0 },
                new List<int> { 0, 0, 1, 1, 1, 1, 0 },
                new List<int> { 0, 1, 0, 0, 1, 0, 0 },
                new List<int> { 0, 0, 0, 0, 1, 0, 0 },
                new List<int> { 0, 0, 1, 1, 0, 0, 0 },
                new List<int> { 0, 0, 0, 0, 0, 0, 0 },
            },
            new List<List<int>>()
            {
                new List<int> { 0, 0, 0, 0, 0, 0, 0 },
                new List<int> { 0, 0, 0, 0, 1, 0, 0 },
                new List<int> { 0, 1, 1, 1, 1, 1, 0 },
                new List<int> { 0, 0, 0, 1, 1, 0, 0 },
                new List<int> { 0, 0, 1, 0, 1, 0, 0 },
                new List<int> { 0, 1, 0, 0, 1, 0, 0 },
                new List<int> { 0, 0, 0, 0, 0, 0, 0 },
            },
            new List<List<int>>()
            {
                new List<int> { 0, 0, 0, 0, 0, 0, 0 },
                new List<int> { 0, 0, 0, 0, 0, 1, 0 },
                new List<int> { 0, 0, 1, 0, 1, 0, 0 },
                new List<int> { 0, 0, 0, 1, 0, 0, 0 },
                new List<int> { 0, 0, 1, 0, 1, 0, 0 },
                new List<int> { 0, 1, 0, 0, 0, 1, 0 },
                new List<int> { 0, 0, 0, 0, 0, 0, 0 },
            }
        };

        [Header("アケオメを表示")] public bool _akeOme;
        [Header("ドットのサイズ")] public float _dotSize = 20f;
        [Header("文字間のスペース")] public float _spacing = 30f;

        private void Start()
        {
            if (_akeOme)
            {
                _grids = _gridsAkeOme;
            }
        }

        /// <summary>
        /// 新しい文字を追加
        /// </summary>
        public void AddCharacter()
        {
            var newCharacter = new List<List<int>>();
            for (var i = 0; i < _grids[0].Count; i++) // 空グリッド
            {
                var newCharacter2 = new List<int>();
                for (var j = 0; j < _grids[0][0].Count; j++) // 空グリッド
                {
                    newCharacter2.Add(0);
                }

                newCharacter.Add(newCharacter2);
            }

            _grids.Add(newCharacter);
        }

        /// <summary>
        /// 指定したインデックスの文字を削除
        /// </summary>
        /// <param name="index"></param>
        public void RemoveCharacter(int index)
        {
            if (index >= 0 && index < _grids.Count)
            {
                _grids.RemoveAt(index);
                Debug.Log($"文字インデックス {index} を削除しました。");
            }
            else
            {
                Debug.LogWarning("無効なインデックスです。");
            }
        }

        /// <summary>
        /// 文字に新しい行を追加
        /// </summary>
        public void AddRow()
        {
            for (var charIndex = 0; charIndex < _grids.Count; charIndex++)
            {
                if (charIndex >= 0 && charIndex < _grids.Count)
                {
                    var cols = _grids[charIndex][0].Count; // 現在の列数を取得
                    var newRow = new List<int>(new int[cols]); // 全て0の行を作成
                    _grids[charIndex].Add(newRow); // 文字に行を追加
                }
                else
                {
                    Debug.LogWarning($"Invalid charIndex: {charIndex}");
                }
            }
        }

        /// <summary>
        /// 文字に新しい列を追加
        /// </summary>
        public void AddColumn()
        {
            for (var charIndex = 0; charIndex < _grids.Count; charIndex++)
            {
                if (charIndex >= 0 && charIndex < _grids.Count)
                {
                    foreach (var row in _grids[charIndex])
                    {
                        row.Add(0); // 各行に0を追加
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid charIndex: {charIndex}");
                }
            }
        }
    }
}