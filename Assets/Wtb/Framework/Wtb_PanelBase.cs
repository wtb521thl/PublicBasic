using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WisdomTree.Xuxiaohao.Function;

namespace Tianbo.Wang
{
    public class Wtb_PanelBase : MonoBehaviour
    {

        public Button fullScreenBtn;
        public Button unFullScreenBtn;

        public Button returnBtn;
        public Button homeBtn;
        public Button reportBtn;

        protected virtual void Awake()
        {

            if (fullScreenBtn != null)
            {
                fullScreenBtn?.onClick.AddListener(() => { WebGLFullScreen.WebFullScreen(); });
                BtnHoverBind(fullScreenBtn.gameObject);
                BtnHoverBind(unFullScreenBtn.gameObject);
                unFullScreenBtn?.onClick.AddListener(() => { WebGLFullScreen.WebSmallScreen(); });
            }
            if(returnBtn !=null)
            {
                returnBtn.onClick.AddListener(ReturnBtnClick);
                BtnHoverBind(returnBtn.gameObject);
            }

            if (homeBtn != null)
            {
                homeBtn.onClick.AddListener(HomeBtnClick);
                BtnHoverBind(homeBtn.gameObject);
            }
        }

        public virtual void Init()
        {
        
        }

        void LateUpdate()
        {
            if (Screen.fullScreen)
            {
                if (!unFullScreenBtn.gameObject.activeSelf)
                    unFullScreenBtn.gameObject.SetActive(true);
                if (fullScreenBtn.gameObject.activeSelf)
                    fullScreenBtn.gameObject.SetActive(false);
            }
            else
            {
                if (unFullScreenBtn.gameObject.activeSelf)
                    unFullScreenBtn.gameObject.SetActive(false);
                if (!fullScreenBtn.gameObject.activeSelf)
                    fullScreenBtn.gameObject.SetActive(true);
            }
        }


        protected virtual void OnDestroy()
        {

            if (fullScreenBtn != null)
            {
                BtnHoverUnBind(fullScreenBtn.gameObject);
                BtnHoverUnBind(unFullScreenBtn.gameObject);
            }
            if (returnBtn != null)
                BtnHoverUnBind(returnBtn.gameObject);
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
            go.transform.Find("HoverImage").GetComponent<Image>().enabled = true;
        }
        protected void ExitBtn(GameObject go)
        {
            go.transform.Find("HoverImage").GetComponent<Image>().enabled = false;
        }

        public virtual void ReturnBtnClick()
        {
            Wtb_PanelManager.Instance.ClosePanel();
        }

        public virtual void HomeBtnClick()
        {
            Wtb_PanelManager.Instance.BackToPanel(PanelType.MainPanel);
        }
    }
}