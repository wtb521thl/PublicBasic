using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tianbo.Wang
{
    public class DragGameObject : MonoBehaviour, IDragable
    {
        public object dragObjParam;
        Vector3 startPos;
        GameObject moveObj;
        Transform parent;

        public DragAndDropType dragAndDropType = DragAndDropType.Answer1;

        Transform hoverImageTrans;

        void Awake()
        {
            hoverImageTrans = transform.Find("HoverImage");
            if (hoverImageTrans != null)
            {
                hoverImageTrans.gameObject.SetActive(false);
            }
        }

        public void BeginDrag()
        {
            if (hoverImageTrans != null)
            {
                hoverImageTrans.gameObject.SetActive(true);
            }
            parent = GameObject.Find("DragObjParent").transform;
            moveObj = Instantiate(gameObject, parent);
            if (moveObj.transform.Find("HoverImage") != null)
            {
                moveObj.transform.Find("HoverImage").gameObject.SetActive(true);
            }
            Graphic[] graphics = moveObj.GetComponentsInChildren<Graphic>();
            for (int i = 0; i < graphics.Length; i++)
            {
                graphics[i].raycastTarget = false;
            }
            moveOvjRect = moveObj.transform.GetRectTransform();
            moveOvjRect.anchorMin = new Vector2(0, 0);
            moveOvjRect.anchorMax = new Vector2(0, 0);
            moveOvjRect.pivot = new Vector2(0, 0);
            moveOvjRect.sizeDelta = transform.GetRectTransform().sizeDelta;
            moveOvjRect.position = (Vector2)transform.position - transform.GetRectTransform().GetSize() / 2f;
            startPos = moveOvjRect.position;
            if (hoverImageTrans != null)
            {
                hoverImageTrans.gameObject.SetActive(true);
            }
        }
        RectTransform moveOvjRect;
        public void Drag(Vector3 mousePosOffset)
        {
            if (moveOvjRect == null)
            {
                moveOvjRect = moveObj.transform.GetRectTransform();
            }
            moveOvjRect.position = startPos + (Vector3)(transform.root.localToWorldMatrix * mousePosOffset);
        }

        public void Drop()
        {
            if (moveObj != null)
            {
                DestroyImmediate(moveObj);
            }
            if (hoverImageTrans != null)
            {
                hoverImageTrans.gameObject.SetActive(false);
            }
        }

        public DragAndDropType GetSelfType()
        {
            return dragAndDropType;
        }

    }
}