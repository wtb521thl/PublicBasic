using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tianbo.Wang
{
    public class Wtb_DialogBase : MonoBehaviour
    {
        public Button closeDialogBtn;
        /// <summary>
        /// 关闭弹窗的全屏mask
        /// </summary>
        public GameObject closeMask;
        public bool canMouseDrag = false;

        Vector2 mouseStartPos;
        public GameObject topArea;
        public GameObject moveWindows;

        protected RectTransform moveWindowsRect;

        public Action DragWindowsAction;

        public string source = "";

        protected virtual void Awake()
        {
            Init();
            if (closeDialogBtn != null)
                closeDialogBtn.onClick.AddListener(() =>
                {
                    CloseDialog();
                });
            InitDragDialog();
            OpenDialogAnim();
            if (closeMask != null)
                Wtb_EventTriggerListener.Get(closeMask).onLeftClick = (go) => { CloseDialog(); };
        }

        protected virtual void Start()
        {

        }

        protected virtual void Init()
        {
            if (moveWindows == null)
            {
                moveWindows = transform.Find("DialogPrefab").gameObject;
            }
            moveWindowsRect = moveWindows.GetComponent<RectTransform>();
            if (canMouseDrag)
                topArea = moveWindows.transform.Find("TopArea").gameObject;
            Inited();
        }

        protected virtual void Inited()
        {

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

        private void EnterBtn(GameObject go)
        {
            go.transform.Find("HoverImage").GetComponent<Image>().enabled = true;
        }
        private void ExitBtn(GameObject go)
        {
            go.transform.Find("HoverImage").GetComponent<Image>().enabled = false;
        }
        /// <summary>
        /// 限制不能超出四个边界
        /// </summary>
        /// <param name="selfRect"></param>
        protected void SetDialogInScreen(RectTransform selfRect, RectTransform canvasRect)
        {
            if (selfRect.GetCenter().y + selfRect.GetSize().y / 2f > canvasRect.GetCenter().y + canvasRect.GetSize().y / 2f)
            {
                selfRect.position = new Vector3(selfRect.position.x, canvasRect.GetCenter().y + canvasRect.GetSize().y / 2f - selfRect.GetSize().y / 2f);
            }
            if (selfRect.GetCenter().y - selfRect.GetSize().y / 2f < canvasRect.GetCenter().y - canvasRect.GetSize().y / 2f)
            {
                selfRect.position = new Vector3(selfRect.position.x, canvasRect.GetCenter().y - canvasRect.GetSize().y / 2f + selfRect.GetSize().y / 2f);
            }
            if (selfRect.GetCenter().x + selfRect.GetSize().x / 2f > canvasRect.GetCenter().x + canvasRect.GetSize().x / 2f)
            {
                selfRect.position = new Vector3(canvasRect.GetCenter().x + canvasRect.GetSize().x / 2f - selfRect.GetSize().x / 2f, selfRect.position.y);
            }
            if (selfRect.GetCenter().x - selfRect.GetSize().x / 2f < canvasRect.GetCenter().x - canvasRect.GetSize().x / 2f)
            {
                selfRect.position = new Vector3(canvasRect.GetCenter().x - canvasRect.GetSize().x / 2f + selfRect.GetSize().x / 2f, selfRect.position.y);
            }
        }

        protected virtual void InitDragDialog()
        {

            if (canMouseDrag && topArea != null && moveWindows != null)
            {
                Wtb_EventTriggerListener.Get(topArea).onBeginDrag += BeginDrag;
                Wtb_EventTriggerListener.Get(topArea).onDrag += Dragging;
            }
        }


        protected virtual void BeginDrag(GameObject go)
        {

            mouseStartPos = Input.mousePosition;

        }

        protected virtual void Dragging(GameObject go)
        {
            Rect screenRect = Screen.safeArea;
            if (screenRect.Contains(Input.mousePosition))
            {
                Vector2 offset = (Vector2)Input.mousePosition - mouseStartPos;
                moveWindowsRect.anchoredPosition += offset;
                mouseStartPos = Input.mousePosition;
                DragWindowsAction?.Invoke();
            }
        }


        protected virtual void CloseDialog(object tempParam = null)
        {
            if (tempParam != null)
                Wtb_EventCenter.BroadcastEvent<string, string, object>(Wtb_EventSendType.DialogInfo, source, this.GetType().Name, tempParam);
            source = "";
            CloseDialogAnim(() =>
            {
                Wtb_DialogManager.Instance.CloseDialog();
            });
        }



        protected virtual void OpenDialogAnim(Action FinishAction = null)
        {
            moveWindowsRect.localScale = Vector3.one;
            FinishAction?.Invoke();
        }


        protected virtual void CloseDialogAnim(Action FinishAction = null)
        {
            moveWindowsRect.localScale = Vector3.zero;
            FinishAction?.Invoke();
        }
    }
}