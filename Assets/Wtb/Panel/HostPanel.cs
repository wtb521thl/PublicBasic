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
            SceneGoManager.Instance.ClearBagAction += ClearBagAction;
            SceneGoManager.Instance.SwitctToHost();
            SceneGoManager.Instance.设备拆除();
            if (GameManager.Instance.isStudyMode)
            {

            }

        }

        private void ClearBagAction()
        {
            for (int i = 0; i < items.Count; i++)
            {
                Destroy(items[i].gameObject);
            }
            items.Clear();
        }

        private void AddBagAction(string arg1, bool arg2)
        {
            if (arg2)
            {
                GameObject go = Instantiate(itemIcon, bagTrans);
                go.SetActive(true);
                go.GetComponent<Button>().onClick.AddListener(() =>
                {
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
                    SceneGoManager.Instance.安装主板();
                    break;
                case 5:
                    SceneGoManager.Instance.安装电源();
                    break;
                case 6:
                    SceneGoManager.Instance.安装显卡();
                    break;
                case 7:
                    SceneGoManager.Instance.安装硬盘();
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
            SceneGoManager.Instance.ClearBagAction -= ClearBagAction;
        }

    }
}