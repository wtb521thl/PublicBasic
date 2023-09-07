using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class ChangeAllTextProFont: EditorWindow 
{

    string path = "/Wtb/Resources/Prefabs";

    Transform objParnet;

    bool textOrTextPro;

    TMP_FontAsset font;
    Font textFont;

    List<GameObject> allChanedObjs = new List<GameObject>();

    [MenuItem("Tools/批量修改预设字体")]
    public static void OpenChangeFontForTextProWindows()
    {
        ChangeAllTextProFont selfWindows = EditorWindow.CreateWindow<ChangeAllTextProFont>("修改指定目录下所有的字体");
    }



    private void OnGUI()
    {
        path = EditorGUILayout.TextField("目录是：", path);

        objParnet = (Transform)EditorGUILayout.ObjectField(objParnet, typeof(Transform), true);

        textOrTextPro = EditorGUILayout.Toggle("是Text还是TextMeshPro?",textOrTextPro);

        if (textOrTextPro)
        {
            textFont = (Font)EditorGUILayout.ObjectField("字体是：", textFont, typeof(Font), true);
        }
        else
        {
            font = (TMP_FontAsset)EditorGUILayout.ObjectField("字体是：", font, typeof(TMP_FontAsset), true);
        }

        if (GUILayout.Button("修改"))
        {
            SetFont();
        }

        if (allChanedObjs.Count > 0)
        {
            EditorGUILayout.LabelField("下面是修改过的预设体：");
        }
        GUIStyle gUIStyle = new GUIStyle(GUI.skin.customStyles[2]);
        gUIStyle.fixedWidth = 500;
        EditorGUILayout.BeginVertical(gUIStyle);
        for (int i = 0; i < allChanedObjs.Count; i++)
        {
            allChanedObjs[i] = (GameObject)EditorGUILayout.ObjectField("第" + i + "个物体：", allChanedObjs[i], typeof(GameObject), false);
        }
        EditorGUILayout.EndVertical();

    }

    void SetFont()
    {
        allChanedObjs.Clear();

        if (objParnet != null)
        {
            if (textOrTextPro)
            {
                Text[] allChildTransText = objParnet.GetComponentsInChildren<Text>(true);
                for (int i = 0; i < allChildTransText.Length; i++)
                {
                    allChildTransText[i].font = textFont;
                    allChanedObjs.Add(allChildTransText[i].gameObject);
                }
            }
            else
            {
                TextMeshProUGUI[] allChildTransTextPro = objParnet.GetComponentsInChildren<TextMeshProUGUI>(true);
                for (int i = 0; i < allChildTransTextPro.Length; i++)
                {
                    allChildTransTextPro[i].font = font;
                    allChanedObjs.Add(allChildTransTextPro[i].gameObject);
                }
            }

        }


        string[] allFiles = Directory.GetFiles(Application.dataPath + path, "*.prefab", SearchOption.AllDirectories);
        for (int i = 0; i < allFiles.Length; i++)
        {
            string fullPath = allFiles[i];
            string assetPath = fullPath.Replace(Application.dataPath, "Assets");

            GameObject tempPrefab = (GameObject)PrefabUtility.InstantiatePrefab((GameObject)AssetDatabase.LoadAssetAtPath<GameObject>(assetPath));
            if (tempPrefab == null)
            {
                continue;
            }
            if (textOrTextPro)
            {
                Text[] text = tempPrefab.GetComponentsInChildren<Text>(true);
                for (int j = 0; j < text.Length; j++)
                {
                    Debug.Log(text[j].name);
                    text[j].font = textFont;
                }
                if (text.Length > 0)
                {
                    GameObject tempPrefabObj = PrefabUtility.SaveAsPrefabAsset(tempPrefab, fullPath);
                    allChanedObjs.Add(tempPrefabObj);
                }
            }
            else
            {
                TextMeshProUGUI[] textMeshProUGUIs = tempPrefab.GetComponentsInChildren<TextMeshProUGUI>(true);
                for (int j = 0; j < textMeshProUGUIs.Length; j++)
                {
                    Debug.Log(textMeshProUGUIs[j].name);
                    textMeshProUGUIs[j].font = font;
                }
                if (textMeshProUGUIs.Length > 0)
                {
                    GameObject tempPrefabObj = PrefabUtility.SaveAsPrefabAsset(tempPrefab, fullPath);
                    allChanedObjs.Add(tempPrefabObj);
                }
            }


            DestroyImmediate(tempPrefab);
        }
        Debug.Log("修改成功");
    }
}
