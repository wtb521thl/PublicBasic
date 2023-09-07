using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tianbo.Wang
{
    public static class RaycastAllManager
    {
        public static List<RaycastResult> RayCastAll()
        {
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);
            return raycastResults;
        }
    }
}