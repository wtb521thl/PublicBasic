using System;
using System.Collections;
using System.Collections.Generic;
using Tianbo.Wang;
using UnityEngine;
using UnityEngine.UI;

public class DropGameObject : MonoBehaviour, IDropable
{
    public DragAndDropType dragAndDropType = DragAndDropType.Answer1;
    public void Drop(GameObject dragObj)
    {

        DragGameObject drag = dragObj.GetComponentInParent<DragGameObject>();

        Wtb_EventCenter.BroadcastEvent<IDragable, IDropable>(Wtb_EventSendType.DragAndDropAction, drag, this);
    }



    public DragAndDropType GetSelfType()
    {
        return dragAndDropType;
    }
    GameObject highlightObj;
    public void OpenSelfHighlight()
    {
        RectTransform selfRect = GetComponent<RectTransform>();
        if (selfRect != null)
        {
            if (highlightObj == null)
            {
                highlightObj = new GameObject(transform.name + "_HighlightObj");
                highlightObj.transform.SetParent(selfRect);
                RectTransform highlightRect = highlightObj.AddComponent<RectTransform>();
                LayoutElement layoutElement = highlightObj.AddComponent<LayoutElement>();
                layoutElement.ignoreLayout = true;
                Image image = highlightObj.AddComponent<Image>();
                image.color = new Color(0, 1, 0, 0.5f);
                image.raycastTarget = false;
                highlightRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, selfRect.GetSize().x);
                highlightRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, selfRect.GetSize().y);
                highlightRect.position = selfRect.GetCenter();
            }

        }
    }

    public void CloseSelfHighlight()
    {
        if (highlightObj != null)
        {
            DestroyImmediate(highlightObj);
        }
    }
}
