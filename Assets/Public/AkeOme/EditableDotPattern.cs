using System.Collections.Generic;
using UnityEngine;

public class EditableDotPattern : MonoBehaviour
{
    [Header("パターン設定")] [Tooltip("各文字の5x5パターンをリストで定義")]
    public readonly List<List<List<int>>> grids = new List<List<List<int>>>
    {
        new List<List<int>>()
        {
            new List<int> { 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0 },
            new List<int> { 0, 0, 0, 0, 0 },
        }
    };

    [Header("ドットのサイズ")] public float _dotSize = 20f;
    [Header("文字間のスペース")] public float _spacing = 30f;

    /// <summary>
    /// 新しい文字を追加
    /// </summary>
    public void AddCharacter()
    {
        List<List<int>> newCharacter = new List<List<int>>();
        for (int i = 0; i < 5; i++) // 5x5の空グリッド
        {
            newCharacter.Add(new List<int> { 0, 0, 0, 0, 0 });
        }

        grids.Add(newCharacter);
    }
}