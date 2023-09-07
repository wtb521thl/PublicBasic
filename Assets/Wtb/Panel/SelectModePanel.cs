using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tianbo.Wang
{
    public class SelectModePanel : Wtb_PanelBase
    {
        public Button studyHostBtn;
        public Button testHostBtn;

        public Button studyComputerBtn;
        public Button testComputerBtn;
        protected override void Awake()
        {
            base.Awake();
            studyHostBtn.onClick.AddListener(StudyHostBtnClick);
            testHostBtn.onClick.AddListener(TestHostBtnClick);
            studyComputerBtn.onClick.AddListener(StudyComputerBtnClick);
            testComputerBtn.onClick.AddListener(TestComputerBtnClick);
            BtnHoverBind(studyHostBtn.gameObject);
            BtnHoverBind(testHostBtn.gameObject);
            BtnHoverBind(studyComputerBtn.gameObject);
            BtnHoverBind(testComputerBtn.gameObject);

            //SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);
        }

        private void StudyHostBtnClick()
        {
            GameManager.Instance.isStudyMode = true;
            Wtb_PanelManager.Instance.OpenPanel(PanelType.HostPanel);
        }
        private void TestHostBtnClick()
        {
            GameManager.Instance.isStudyMode = false;
            Wtb_PanelManager.Instance.OpenPanel(PanelType.HostPanel);
        }

        private void StudyComputerBtnClick()
        {
            GameManager.Instance.isStudyMode = true;
            Wtb_PanelManager.Instance.OpenPanel(PanelType.ComputerPanel);
        }

        private void TestComputerBtnClick()
        {
            GameManager.Instance.isStudyMode = false;
            Wtb_PanelManager.Instance.OpenPanel(PanelType.ComputerPanel);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            BtnHoverUnBind(studyHostBtn.gameObject);
            BtnHoverUnBind(testHostBtn.gameObject);
            BtnHoverUnBind(studyComputerBtn.gameObject);
            BtnHoverUnBind(testComputerBtn.gameObject);
        }

    }
}