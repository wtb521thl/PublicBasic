using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Tianbo.Wang
{
    public class ComputerPanel : Wtb_PanelBase
    {
        public Button[] btns;

        public Transform bagTrans;

        public GameObject itemIcon;

        List<GameObject> items = new List<GameObject>();

        protected override void Awake()
        {
            base.Awake();
            SceneGoManager.Instance.SetStepAction += SetStepAction;
            SceneGoManager.Instance.AddBagAction += AddBagAction;
            for (int i = 0; i < btns.Length; i++)
            {
                int tempIndex = i;
                BtnHoverBind(btns[i].gameObject);

                btns[i].onClick.AddListener(() =>
                {
                    BtnClickAction(tempIndex);
                });
            }
            SceneGoManager.Instance.SwitctToComputer();
            BtnClickAction(0);

            if (!GameManager.Instance.isStudyMode)
            {
                Wtb_TipsDialog wtb_TipsDialog = (Wtb_TipsDialog)Wtb_DialogManager.Instance.OpenDialog(Wtb_DialogType.Wtb_TipsDialog);
                wtb_TipsDialog.SetTipsInfo("请认真选择，每次选择只有一次机会");
            }

        }

        private void SetStepAction(string stepName)
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

        public void BtnClickAction(int tempIndex)
        {
            if (tempIndex != 0 && !GameManager.Instance.isStudyMode)
            {
                return;
            }
            switch (tempIndex)
            {
                case 0:
                    SceneGoManager.Instance.连接显示器1();
                    break;
                case 1:
                    SceneGoManager.Instance.连接电源1();
                    break;
                case 2:
                    SceneGoManager.Instance.连接键盘1();
                    break;
                case 3:
                    SceneGoManager.Instance.连接鼠标1();
                    break;
                case 4:
                    SceneGoManager.Instance.连接网线1();
                    break;
                case 5:
                    SceneGoManager.Instance.连接音响1();
                    break;
            }
        }

        Image lastSelectImage;
        private void AddBagAction(int index,string arg1, bool arg2)
        {
            if (index != 1)
            {
                return;
            }
            if (arg2)
            {
                GameObject go = Instantiate(itemIcon, bagTrans);
                Debug.Log(arg1);
                Sprite sprite = Resources.Load<Sprite>("各种线材/" + arg1);
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



        protected override void OnDestroy()
        {
            base.OnDestroy();
            for (int i = 0; i < btns.Length; i++)
            {
                BtnHoverUnBind(btns[i].gameObject);
            }
            SceneGoManager.Instance.AddBagAction -= AddBagAction;
            SceneGoManager.Instance.SetStepAction -= SetStepAction;

            SceneGoManager.Instance.InitSecond();
        }

    }
}