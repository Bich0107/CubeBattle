using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateTextureEditor : EditorWindow
{
    private string textureName = "NewTexture";
    private int width = 256;
    private int height = 256;
    private Color color1 = Color.red;
    private Color color2 = Color.blue;
    private FilterMode filterMode = FilterMode.Point;

    [MenuItem("Tools/Create Texture Editor")]
    public static void ShowWindow()
    {
        GetWindow<CreateTextureEditor>("Create Texture");
    }

    private void OnGUI()
    {
        GUILayout.Label("Texture Settings", EditorStyles.boldLabel);

        textureName = EditorGUILayout.TextField("Texture name", textureName);
        width = EditorGUILayout.IntField("Width", width);
        height = EditorGUILayout.IntField("Height", height);
        filterMode = (FilterMode)EditorGUILayout.EnumPopup("Filter Mode", filterMode);
        color1 = EditorGUILayout.ColorField("Color 1", color1);
        color2 = EditorGUILayout.ColorField("Color 2", color2);

        if (GUILayout.Button("Create Texture"))
        {
            CreateTexture();
        }
    }

    private void CreateTexture()
    {
        // Create a new Texture2D
        Texture2D texture = new Texture2D(width, height);

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                // Example pattern: Checkerboard or gradient
                Color color = ((x + y) % 2 == 0) ? color1 : color2;
                texture.SetPixel(x, y, color);  // Set the pixel color
            }
        }

        texture.Apply();

        // Determine the relative path inside the project folder
        string relativePath = "Assets/Textures/" + textureName + ".png";

        // Ensure the directory exists
        string directoryPath = Path.GetDirectoryName(relativePath);
        if (!AssetDatabase.IsValidFolder(directoryPath))
        {
            AssetDatabase.CreateFolder("Assets/Textures", Path.GetFileName(directoryPath));
        }

        // Save the texture as an asset
        byte[] pngData = texture.EncodeToPNG();
        //File.WriteAllBytes(Path.Combine(Application.dataPath, relativePath), pngData);
        File.WriteAllBytes(Path.Combine(Application.dataPath, "Textures", textureName + ".png"), pngData);
        AssetDatabase.ImportAsset(relativePath);

        // force update field that cant be encode
        TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(relativePath);
        importer.isReadable = true;
        importer.filterMode = filterMode;
        AssetDatabase.ImportAsset(relativePath, ImportAssetOptions.ForceUpdate);

        AssetDatabase.Refresh();
    }
}
