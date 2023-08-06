using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using PDA;

public class PixelCreateAnimationMenu
{
    [MenuItem("Window/PDA/Animation Wizard")]
    public static void AnimationWizard()
    {
        PDAAnimationWizard.LaunchEditor();
    }
}

public class PDAAnimationWizard : EditorWindow
{
    enum Mode
    {
        Single, FourDirectional
    }

    Mode mode;

    #region Universal Variables

    Texture2D spritesheet;
    string animName = "";

    #endregion

    #region Builtin Methods
    public static void LaunchEditor()
    {
        var editorWindow = GetWindow<PDAAnimationWizard>("PDA Animation Wizard");
        editorWindow.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();

        spritesheet = (Texture2D)EditorGUILayout.ObjectField(spritesheet, typeof(Texture2D), false);
        animName = EditorGUILayout.TextField("Animation Name", animName);

        EditorGUILayout.Space();

        mode = (Mode)EditorGUILayout.EnumPopup(mode);

        switch(mode)
        {
            case Mode.Single:
                SingleClipRender();
                break;
            case Mode.FourDirectional:
                FourDirectionalClipRender();
                break;
        }

        EditorGUILayout.EndVertical();

        Repaint();
    }
    #endregion


    string GetFolderPath()
    {
        string path = AssetDatabase.GetAssetPath(spritesheet);

        int lastSlashIndex = path.LastIndexOf('/');
        int lastBackslashIndex = path.LastIndexOf('\\');

        string folderPath = path.Substring(0, Mathf.Max(lastSlashIndex, lastBackslashIndex) + 1);

        return folderPath;
    }

    List<Sprite> GetSprites()
    {
        string path = AssetDatabase.GetAssetPath(spritesheet);
        var objects = AssetDatabase.LoadAllAssetsAtPath(path);

        List<Sprite> sprites = objects
            .Where(q => q is Sprite).Cast<Sprite>()
            .OrderBy(x => int.Parse(x.name.Split('_').Last()))
            .ToList();

        return sprites;
    }



    #region Single
    float singleDuration = 1f;

    void SingleClipRender()
    {
        singleDuration = EditorGUILayout.FloatField("Animation Duration", singleDuration);

        if (GUILayout.Button("Generate"))
        {
            List<Sprite> sprites = GetSprites();

            GenerateSingleClip(animName, singleDuration, sprites, 0, sprites.Count);
        }
    }

    PDAnimationClip GenerateSingleClip(string name, float duration, List<Sprite> sprites, int startIndex, int count)
    {
        PDAnimationClip clip = CreateInstance<PDAnimationClip>();
        clip.name = name;
        clip.duration = duration;
        clip.sprites = sprites.GetRange(startIndex, count);

        string finalPath = AssetDatabase.GenerateUniqueAssetPath(GetFolderPath() + $"{name}.asset");
        AssetDatabase.CreateAsset(clip, finalPath);

        return clip;
    }
    #endregion



    #region FourDirectional
    float fourDirDuration = 1f;

    void FourDirectionalClipRender()
    {
        fourDirDuration = EditorGUILayout.FloatField("Animation Duration", fourDirDuration);
        EditorGUILayout.LabelField("Make sure texture is in order Up-Right-Down-Left!");

        if (GUILayout.Button("Generate"))
        {
            GenerateFourDirectionalClip(animName);
        }
    }

    PDAnimationClip4D GenerateFourDirectionalClip(string baseName)
    {
        List<Sprite> sprites = GetSprites();
        PDAnimationClip4D container = CreateInstance<PDAnimationClip4D>();

        int framesPerAnim = sprites.Count / 4;

        container.up =    GenerateSingleClip(baseName + "Up",    fourDirDuration, sprites, 0 * framesPerAnim, framesPerAnim);
        container.right = GenerateSingleClip(baseName + "Right", fourDirDuration, sprites, 1 * framesPerAnim, framesPerAnim);
        container.down =  GenerateSingleClip(baseName + "Down",  fourDirDuration, sprites, 2 * framesPerAnim, framesPerAnim);
        container.left =  GenerateSingleClip(baseName + "Left",  fourDirDuration, sprites, 3 * framesPerAnim, framesPerAnim);

        string finalPath = AssetDatabase.GenerateUniqueAssetPath(GetFolderPath() + $"{animName}.asset");
        AssetDatabase.CreateAsset(container, finalPath);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = container;

        return container;
    }
    #endregion
}
