#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Public.AkeOme
{
    /// <summary>
    /// エディター拡張でリストをインスペクター上に表示
    /// </summary>
    [CustomEditor(typeof(EditableDotPattern))]
    public class MultiGridManagerEditor : Editor
    {
        private int characterToDelete = 0; // 削除対象の文字インデックス

        public override void OnInspectorGUI()
        {
            var script = (EditableDotPattern)target;

            script._akeOme = EditorGUILayout.Toggle("アケオメを表示", script._akeOme);
            script._dotSize = EditorGUILayout.FloatField("ドットサイズ", script._dotSize);
            script._spacing = EditorGUILayout.FloatField("文字間のスペース", script._spacing);

            EditorGUILayout.Space();

            // 各文字を描画
            for (var charIndex = 0; charIndex < script._grids.Count; charIndex++)
            {
                var characterGrid = script._grids[charIndex];
                EditorGUILayout.LabelField($"文字 {charIndex + 1}", EditorStyles.boldLabel);
                EditorGUILayout.Space(10);

                // インスペクター上での描画エリアを計算
                var drawingArea = GUILayoutUtility.GetRect(1, characterGrid.Count * script._dotSize);

                Handles.BeginGUI();
                for (var row = 0; row < characterGrid.Count; row++)
                {
                    for (var col = 0; col < characterGrid[row].Count; col++)
                    {
                        // 各ドットの位置
                        var position = new Vector2(
                            drawingArea.xMin + col * script._dotSize +
                            charIndex * (characterGrid[0].Count * script._dotSize + script._spacing),
                            drawingArea.yMin + row * script._dotSize
                        );

                        // ドットの色
                        var color = characterGrid[row][col] == 1 ? Color.black : Color.white;

                        // ドットを描画
                        Handles.color = color;
                        Handles.DrawSolidDisc(position, Vector3.forward, script._dotSize / 2f);

                        // クリックで値を切り替え
                        if (Event.current.type == EventType.MouseDown &&
                            Vector2.Distance(Event.current.mousePosition, position) < script._dotSize / 2f)
                        {
                            characterGrid[row][col] = 1 - characterGrid[row][col]; // 0なら1に、1なら0に
                            GUI.changed = true; // 変更を保存
                        }
                    }
                }

                Handles.EndGUI();
                EditorGUILayout.Space();
            }


            // スクリプトの変更を保存
            if (GUI.changed)
            {
                EditorUtility.SetDirty(script);
            }

            // 文字を削除
            GUILayout.BeginHorizontal();
            GUILayout.Label("削除する文字のインデックス:");
            characterToDelete = EditorGUILayout.IntField(characterToDelete); // インデックスを指定
            GUILayout.EndHorizontal();

            if (GUILayout.Button("文字を削除"))
            {
                if (EditorUtility.DisplayDialog("確認", $"インデックス {characterToDelete} の文字を削除しますか？", "はい", "いいえ"))
                {
                    script.RemoveCharacter(characterToDelete);
                }
            }

            if (GUILayout.Button("文字を追加"))
            {
                script.AddCharacter();
            }

            if (GUILayout.Button("行を追加"))
            {
                script.AddRow();
            }

            if (GUILayout.Button("列を追加"))
            {
                script.AddColumn(); // キャラクター0に列を追加 (例)
            }

            // クリアボタン
            if (GUILayout.Button("全クリア"))
            {
                for (var charIndex = 0; charIndex < script._grids.Count; charIndex++)
                {
                    var characterGrid = script._grids[charIndex];

                    // 各行列内のドット位置を調べる
                    for (var row = 0; row < characterGrid.Count; row++)
                    {
                        for (var col = 0; col < characterGrid[row].Count; col++)
                        {
                            if (characterGrid[row][col] == 1) // ドットが有効な場合
                            {
                                characterGrid[row][col] = 0;
                            }
                        }
                    }
                }
            }
        }
    }
}
#endif