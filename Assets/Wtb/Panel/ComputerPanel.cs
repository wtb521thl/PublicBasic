using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tianbo.Wang
{
    public class ComputerPanel : Wtb_PanelBase
    {
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

            BtnClickAction(0);

            SceneGoManager.Instance.SwitctToComputer();
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

        protected override void OnDestroy()
        {
            base.OnDestroy();
            for (int i = 0; i < btns.Length; i++)
            {
                BtnHoverUnBind(btns[i].gameObject);
            }
        }

    }
}