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
        }

        public void BtnClickAction(int tempIndex)
        {
            switch (tempIndex)
            {
                case 0:

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