using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tianbo.Wang
{

    public class ShowInfoManager : Wtb_SingleMono<ShowInfoManager>
    {
        public Transform parent;
        public Dictionary<string, GameObject> objDic = new Dictionary<string, GameObject>();
        public RectTransform canvasTrans;
        void Awake()
        {
            canvasTrans = GameObject.Find("Canvas").GetComponent<RectTransform>();
        }
        public void ShowInfoBig(string info, Vector2 position)
        {
            string prefabPath = "Prefabs/ShowInfoBig";
            GameObject tempGo;
            if (!objDic.ContainsKey(prefabPath))
            {
                tempGo = GameObject.Instantiate(Resources.Load<GameObject>(prefabPath), parent);

                objDic.Add(prefabPath, tempGo);
            }
            else
            {
                tempGo = objDic[prefabPath];
            }
            tempGo.transform.SetAsLastSibling();
            tempGo.SetActive(true);
            tempGo.GetComponentInChildren<Text>().text = info;

            RectTransform rectTransform = tempGo.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = position;
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            //Debug.Log(rectTransform.anchoredPosition.y + rectTransform.sizeDelta.y+"==="+Screen.height);
            float tempY = GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.y;
            if (rectTransform.anchoredPosition.y + rectTransform.sizeDelta.y > tempY)
            {
                rectTransform.anchoredPosition -= new Vector2(0, rectTransform.anchoredPosition.y + rectTransform.sizeDelta.y - tempY);
            }
        }
        public void ShowInfo(string info, Vector2 position)
        {
            string prefabPath = "Prefabs/ShowInfo";
            GameObject tempGo;
            if (!objDic.ContainsKey(prefabPath))
            {
                tempGo = GameObject.Instantiate(Resources.Load<GameObject>(prefabPath), parent);

                objDic.Add(prefabPath, tempGo);
            }
            else
            {
                tempGo = objDic[prefabPath];
            }
            tempGo.transform.SetAsLastSibling();
            tempGo.SetActive(true);
            tempGo.GetComponentInChildren<Text>().text = info;

            RectTransform rectTransform = tempGo.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = position;
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            //Debug.Log(rectTransform.anchoredPosition.y + rectTransform.sizeDelta.y+"==="+Screen.height);
            float tempY = GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.y;
            if (rectTransform.anchoredPosition.y + rectTransform.sizeDelta.y > tempY)
            {
                rectTransform.anchoredPosition -= new Vector2(0, rectTransform.anchoredPosition.y + rectTransform.sizeDelta.y - tempY);
            }
        }
        public void CloseBigInfo()
        {
            string prefabPath = "Prefabs/ShowInfoBig";
            if (objDic.ContainsKey(prefabPath))
            {
                objDic[prefabPath].SetActive(false);
            }

        }
        public void CloseInfo()
        {
            string prefabPath = "Prefabs/ShowInfo";
            if (objDic.ContainsKey(prefabPath))
            {
                objDic[prefabPath].SetActive(false);
            }

        }

        public void ShowBtnInfos(string[] btns, Vector2 position, Action<string> ClickAction, Action<GameObject, bool> HoverAction)
        {
            string prefabPath = "Prefabs/ShowBtnInfos";
            GameObject tempGo;
            if (!objDic.ContainsKey(prefabPath))
            {
                tempGo = GameObject.Instantiate(Resources.Load<GameObject>(prefabPath), parent);
                objDic.Add(prefabPath, tempGo);
            }
            else
            {
                tempGo = objDic[prefabPath];
            }
            tempGo.transform.SetAsLastSibling();
            tempGo.SetActive(true);
            Transform tempParent = tempGo.transform.Find("Parent");
            for (int i = tempParent.childCount - 1; i >= 0; i--)
            {
                GameObject.DestroyImmediate(tempParent.GetChild(i).gameObject);
            }


            tempGo.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                ClickAction?.Invoke("Mask");
            });
            for (int i = 0; i < btns.Length; i++)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/BtnPrefab"), tempParent);
                Wtb_EventTriggerListener.Get(go).onEnter = (g) => { HoverAction?.Invoke(g, true); };
                Wtb_EventTriggerListener.Get(go).onExit = (g) => { HoverAction?.Invoke(g, false); };
                go.name = btns[i];
                BtnHoverBind(go);
                string tempBtnName = go.name;
                Button btn = go.GetComponent<Button>();
                btn.GetComponentInChildren<Text>().text = tempBtnName;
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    ClickAction?.Invoke(tempBtnName);
                });

            }

            tempGo.GetComponent<RectTransform>().anchoredPosition = position;
        }


        protected void BtnHoverBind(GameObject btnObj)
        {
            Wtb_EventTriggerListener.Get(btnObj).onEnter += EnterBtn;
            Wtb_EventTriggerListener.Get(btnObj).onExit += ExitBtn;
        }
        protected void BtnHoverUnBind(GameObject btnObj)
        {
            Wtb_EventTriggerListener.Get(btnObj).onEnter -= EnterBtn;
            Wtb_EventTriggerListener.Get(btnObj).onExit -= ExitBtn;
        }

        protected void EnterBtn(GameObject go)
        {
            if (go.transform.Find("HoverImage"))
                go.transform.Find("HoverImage").GetComponent<Image>().enabled = true;
        }
        protected void ExitBtn(GameObject go)
        {
            if (go.transform.Find("HoverImage"))
                go.transform.Find("HoverImage").GetComponent<Image>().enabled = false;
        }

        public void CloseBtnInfos()
        {
            string prefabPath = "Prefabs/ShowBtnInfos";
            if (objDic.ContainsKey(prefabPath))
            {
                objDic[prefabPath].SetActive(false);
            }

        }

    }

}