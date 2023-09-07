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
        }


        public void BtnClickAction(int tempIndex)
        {

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