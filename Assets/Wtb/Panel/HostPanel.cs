using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tianbo.Wang
{
    public class HostPanel : Wtb_PanelBase
    {
        public Transform bagTrans;

        public GameObject itemIcon;

        List<GameObject> items = new List<GameObject>();


        public Button[] btns;


        protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < btns.Length; i++)
            {
                int tempIndex = i;
                BtnHoverBind(btns[i].gameObject);

                btns[i].onClick.AddListener(() =>
                {
                    BtnClickAction(tempIndex);
                });
            }
            SceneGoManager.Instance.AddBagAction += AddBagAction;
            SceneGoManager.Instance.SetStepAction += ClearBagAction;
            SceneGoManager.Instance.SwitctToHost();
            SceneGoManager.Instance.设备拆除();

        }

        private void ClearBagAction(string stepName)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Destroy(items[i].gameObject);
            }
            items.Clear();

            for (int i = 0; i < btns.Length; i++)
            {
                if (btns[i].GetComponentInChildren<Text>().text == stepName)
                {
                    btns[i].transform.Find("SelectImage").GetComponent<Image>().enabled = true;
                }
                else
                {
                    btns[i].transform.Find("SelectImage").GetComponent<Image>().enabled = false;
                }
            }
        }
        Image lastSelectImage;
        private void AddBagAction(string arg1, bool arg2)
        {
            if (arg2)
            {
                GameObject go = Instantiate(itemIcon, bagTrans);
                Debug.Log(arg1);
                Sprite sprite = Resources.Load<Sprite>("截图/" + arg1);
                Image image = go.transform.Find("Sprite").GetComponent<Image>();
                image.sprite = sprite;
                RectTransform tempRect = image.rectTransform;
                tempRect.sizeDelta = new Vector2(tempRect.sizeDelta.x, (float)sprite.texture.height / sprite.texture.width * tempRect.sizeDelta.x);
                go.SetActive(true);
                go.GetComponentInChildren<Text>().text = arg1;
                go.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (lastSelectImage != null)
                    {
                        lastSelectImage.enabled = false;
                    }
                    lastSelectImage = go.transform.Find("HoverImage").GetComponent<Image>();
                    lastSelectImage.enabled = true;
                    SceneGoManager.Instance.ClickUIAction(go.name);
                });
                go.name = arg1;
                items.Add(go);
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].name == arg1)
                    {
                        Destroy(items[i]);
                        items.RemoveAt(i);
                        break;
                    }

                }
            }

        }

        public void BtnClickAction(int tempIndex)
        {
            if (tempIndex != 0 && !GameManager.Instance.isStudyMode)
            {
                return;
            }
            Debug.Log(tempIndex);
            switch (tempIndex)
            {
                case 0:
                    SceneGoManager.Instance.设备拆除();
                    break;
                case 1:
                    SceneGoManager.Instance.安装CPU();
                    break;
                case 2:
                    SceneGoManager.Instance.安装散热器();
                    break;
                case 3:
                    SceneGoManager.Instance.安装内存();
                    break;
                case 4:
                    SceneGoManager.Instance.安装硬盘();
                    break;
                case 5:
                    SceneGoManager.Instance.安装主板();
                    break;
                case 6:
                    SceneGoManager.Instance.安装电源();
                    break;
                case 7:
                    SceneGoManager.Instance.安装显卡();
                    break;
                case 8:
                    SceneGoManager.Instance.安装机箱盖();
                    break;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            for (int i = 0; i < btns.Length; i++)
            {
                BtnHoverUnBind(btns[i].gameObject);
            }

            SceneGoManager.Instance.AddBagAction -= AddBagAction;
            SceneGoManager.Instance.SetStepAction -= ClearBagAction;
        }

    }
}