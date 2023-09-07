
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tianbo.Wang
{
    public interface IDragable
    {
        DragAndDropType GetSelfType();
        void BeginDrag();
        void Drag(Vector3 mousePosOffset);
        void Drop();
    }

    public interface IDropable
    {
        DragAndDropType GetSelfType();
        void Drop(GameObject dragObj);
        void OpenSelfHighlight();
        void CloseSelfHighlight();
    }

    public enum DragAndDropType
    {
        Answer1,
        Answer2,
    }
}