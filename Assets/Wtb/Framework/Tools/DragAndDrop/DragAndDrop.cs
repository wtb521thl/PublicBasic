using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System;

namespace Tianbo.Wang
{
    public class DragAndDrop : MonoBehaviour
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        Vector2 startMousePos;
        Vector2 dragMousePos;
        bool isDrag = false;
        GameObject dragObj;
        IDragable dragable;
        IDropable dropable;

        Ray ray;
        RaycastHit hit;

        Vector2 offset;


        public Action<GameObject, GameObject> DropAction;


        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (isDrag)
                {
                    raycastResults = RaycastAllManager.RayCastAll();
                    dropable = null;
                    if (raycastResults.Count != 0)
                    {
                        dropable = raycastResults[0].gameObject.GetComponentInParent<IDropable>();
                        if (dropable != null && dropable.GetSelfType() == dragable.GetSelfType())//类型相同才可以交互
                        {
                            dropable.Drop(dragObj);
                        }
                        DropAction?.Invoke(dragObj, raycastResults[0].gameObject);
                    }
                    if (dropable == null || dropable.GetSelfType() != dragable.GetSelfType())
                    {
                        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit))
                        {
                            dropable = hit.transform.GetComponentInParent<IDropable>();
                            if (dropable != null && dropable.GetSelfType() == dragable.GetSelfType())//类型相同才可以交互
                            {
                                dropable.Drop(dragObj);
                            }
                        }
                    }
                    if (dragable != null)
                    {
                        dragable.Drop();
                        dragable = null;
                    }
                    isDrag = false;
                }
                DeleteAllDropablesShow();
            }
            if (!isDrag)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    raycastResults = RaycastAllManager.RayCastAll();
                    if (raycastResults.Count > 0)
                    {
                        dragable = raycastResults[0].gameObject.GetComponentInParent<IDragable>();
                        if (dragable != null)
                        {
                            dragObj = raycastResults[0].gameObject;
                            dragable.BeginDrag();
                            startMousePos = Input.mousePosition;
                            isDrag = true;
                            GetAllDropablesShow();
                        }
                    }
                }
            }
            else if (dragable != null)
            {
                dragMousePos = Input.mousePosition;
                offset = (dragMousePos - startMousePos) / GameObject.Find("Canvas").transform.localScale.x; //为了适应不同分辨率offset/Canvas的缩放值
                dragable.Drag(offset);
            }
        }
        List<IDropable> showHighlightObjs = new List<IDropable>();
        void GetAllDropablesShow()
        {
            MonoBehaviour[] allMonos = FindObjectsOfType<MonoBehaviour>();
            showHighlightObjs.Clear();
            for (int i = 0; i < allMonos.Length; i++)
            {
                if (typeof(IDropable).IsAssignableFrom(allMonos[i].GetType()))
                {
                    if (((IDropable)allMonos[i]).GetSelfType() == dragable.GetSelfType())
                    {
                        IDropable dropable = (IDropable)allMonos[i];
                        dropable.OpenSelfHighlight();
                        showHighlightObjs.Add(dropable);
                    }
                }
            }
        }

        void DeleteAllDropablesShow()
        {
            for (int i = 0; i < showHighlightObjs.Count; i++)
            {
                if (showHighlightObjs[i] != null)
                {
                    showHighlightObjs[i].CloseSelfHighlight();
                }
            }
        }
    }
}