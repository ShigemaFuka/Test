#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EditableDotPattern))]
public class MultiGridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditableDotPattern script = (EditableDotPattern)target;

        // ドットサイズとスペースの設定
        script._dotSize = EditorGUILayout.FloatField("ドットサイズ", script._dotSize);
        script._spacing = EditorGUILayout.FloatField("文字間のスペース", script._spacing);

        EditorGUILayout.Space();

        // 各文字を描画
        for (int charIndex = 0; charIndex < script.grids.Count; charIndex++)
        {
            List<List<int>> characterGrid = script.grids[charIndex];
            EditorGUILayout.LabelField($"文字 {charIndex + 1}", EditorStyles.boldLabel);
            EditorGUILayout.Space(10);
            
            // 描画エリアを計算
            Rect drawingArea = GUILayoutUtility.GetRect(1, characterGrid.Count * script._dotSize);

            Handles.BeginGUI();
            for (int row = 0; row < characterGrid.Count; row++)
            {
                for (int col = 0; col < characterGrid[row].Count; col++)
                {
                    // 各ドットの位置
                    Vector2 position = new Vector2(
                        drawingArea.xMin + col * script._dotSize + charIndex * (characterGrid[0].Count * script._dotSize + script._spacing),
                        drawingArea.yMin + row * script._dotSize
                    );

                    // ドットの色
                    Color color = characterGrid[row][col] == 1 ? Color.black : Color.white;

                    // ドットを描画
                    Handles.color = color;
                    Handles.DrawSolidDisc(position, Vector3.forward, script._dotSize / 2f);

                    // クリックで値を切り替え
                    if (Event.current.type == EventType.MouseDown && Vector2.Distance(Event.current.mousePosition, position) < script._dotSize / 2f)
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
        
        // 文字を追加するボタン
        if (GUILayout.Button("文字を追加"))
        {
            script.AddCharacter(); // 文字追加メソッドを呼び出す
        }

        // クリアボタン
        if (GUILayout.Button("全クリア"))
        {
            foreach (var characterGrid in script.grids)
            {
                for (int row = 0; row < characterGrid.Count; row++)
                {
                    for (int col = 0; col < characterGrid[row].Count; col++)
                    {
                        characterGrid[row][col] = 0;
                    }
                }
            }
        }
    }
}
#endif
