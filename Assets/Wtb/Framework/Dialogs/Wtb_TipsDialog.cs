using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tianbo.Wang
{
    public class Wtb_TipsDialog : Wtb_DialogBase
    {
        public Text text;
        public Button sureBtn;
        public Button cancleBtn;

        public Action SureAction;
        public Action CancleAction;
        protected override void Init()
        {
            base.Init();
            sureBtn.onClick.AddListener(() =>
            {
                SureAction?.Invoke();
                CloseDialog();
            });
            if (cancleBtn != null)
            {
                cancleBtn.onClick.AddListener(() =>
                {
                    CancleAction?.Invoke();
                    CloseDialog();
                });
            }
            BtnHoverBind(sureBtn.gameObject);
        }

        protected virtual void OnDestroy()
        {
            BtnHoverUnBind(sureBtn.gameObject);
        }

        public void SetTipsInfo(string tipsText, Action SureAction = null, bool showCancleBtn = false, Action CancleAction = null)
        {
            text.text = tipsText;
            this.SureAction = SureAction;


            if (showCancleBtn)
            {
                this.CancleAction = CancleAction;
            }
            if (cancleBtn != null)
            {
                cancleBtn.transform.parent.gameObject.SetActive(showCancleBtn);

            }

        }
    }

}