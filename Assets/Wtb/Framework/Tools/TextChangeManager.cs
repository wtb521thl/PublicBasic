using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Tianbo.Wang
{
    public class TextChangeManager : Wtb_SingleMono<TextChangeManager>
    {
        GameObject tempInputFieldObj;
        Transform parentTrans;
        private void Awake()
        {
            parentTrans = GameObject.Find("Canvas").transform;
        }
        Color textColor;

        public void OpenChangeInput(Text text, Action<string> FinishAction)
        {
            textColor = text.color;

            text.color = new Color(1, 1, 1, 0);

            NewInputField(text, (str) =>
             {
                 text.color = textColor;
                 text.text = str;
                 FinishAction?.Invoke(str);
                 CloseChangeInput();
             });
        }

        void NewInputField(Text text, Action<string> FinishChangeAction)
        {
            tempInputFieldObj = new GameObject("TempInput");
            RectTransform tempRect = tempInputFieldObj.AddComponent<RectTransform>();
            tempRect.SetParent(parentTrans);
            InputField inputField = tempInputFieldObj.AddComponent<InputField>();
            tempRect.localScale = text.rectTransform.lossyScale;
            tempRect.anchorMin = text.rectTransform.anchorMin;
            tempRect.anchorMax = text.rectTransform.anchorMax;
            tempRect.pivot = text.rectTransform.pivot;
            tempRect.sizeDelta = text.rectTransform.sizeDelta;
            tempRect.position = text.rectTransform.position;

            GameObject tempTextObj = new GameObject("TempInputText");
            RectTransform tempTextRect = tempTextObj.AddComponent<RectTransform>();
            tempTextRect.SetParent(tempRect);
            tempTextRect.anchorMin = Vector2.zero;
            tempTextRect.anchorMax = Vector2.one;
            tempTextRect.pivot = Vector2.zero;
            tempTextRect.anchoredPosition = Vector2.zero;
            tempTextRect.sizeDelta = Vector2.zero;
            tempTextRect.localScale = Vector3.one;
            Text tempText = tempTextObj.AddComponent<Text>();
            tempText.text = text.text;
            tempText.font = text.font;
            tempText.fontSize = text.fontSize;
            tempText.color = textColor;
            tempText.alignment = text.alignment;
            tempText.supportRichText = false;



            inputField.targetGraphic = tempText;
            inputField.text = text.text;
            inputField.textComponent = tempText;

            //InputField_WebGL inputField_WebGL = tempInputFieldObj.AddComponent<InputField_WebGL>();
            //inputField_WebGL.OnFocus(UIManager.Instance.GetComponent<Canvas>());

            inputField.onEndEdit.AddListener((str) => { FinishChangeAction?.Invoke(str); });
            inputField.ActivateInputField();
            inputField.Select();

        }


        public void CloseChangeInput()
        {
            if (tempInputFieldObj != null)
            {
                Destroy(tempInputFieldObj);
            }
        }
    }
}