using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tianbo.Wang
{
    public class MainPanel : Wtb_PanelBase
    {
        public Button startBtn;

        protected override void Awake()
        {
            base.Awake();
            startBtn.onClick.AddListener(StartBtnClick);
            BtnHoverBind(startBtn.gameObject);
            ExitBtn(startBtn.gameObject);

            //SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            BtnHoverUnBind(startBtn.gameObject);
        }

        private void StartBtnClick()
        {
            Wtb_PanelManager.Instance.OpenPanel(PanelType.SelectModePanel);
        }
    }
}