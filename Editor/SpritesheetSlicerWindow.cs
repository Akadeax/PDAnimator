using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpritesheetSlicerWindow : EditorWindow
{
    [MenuItem("Window/PDA/Spritesheet slicer")]
    public static void ShowWindow()
    {
        GetWindow<SpritesheetSlicerWindow>("Spritesheet slicer");
    }

    Vector2 pivotPoint = new(0.5f, 0.5f);

    int sliceWidth = 16;
    int sliceHeight = 16;
    int slicePixelsPerUnit = 16;

    private void OnGUI()
    {
        pivotPoint = EditorGUILayout.Vector2Field("Pivot", pivotPoint);
        sliceWidth = EditorGUILayout.IntField("Slice width", sliceWidth);
        sliceHeight = EditorGUILayout.IntField("Slice height", sliceHeight);
        slicePixelsPerUnit = EditorGUILayout.IntField("Sprite Pixels per unit", slicePixelsPerUnit);

        if (GUILayout.Button("Apply settings to selected textures"))
        {
            SpritesheetMethods.SplitSprites(sliceWidth, sliceHeight, slicePixelsPerUnit, pivotPoint);
        }
    }
}