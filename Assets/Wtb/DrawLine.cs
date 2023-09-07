using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MaskableGraphic, ICanvasRaycastFilter
{

    List<List<UIVertex>> vertexQuadList = new List<List<UIVertex>>();

    public float lineWidth = 2;

    public Vector3 startPos;
    public Vector3 endPos;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        for (int i = 0; i < vertexQuadList.Count; i++)
        {
            vh.AddUIVertexQuad(vertexQuadList[i].ToArray());
        }
    }


    void Update()
    {
        //设定rect大小正好能圈住线
        rectTransform.position = endPos + (startPos - endPos) / 2f;
        rectTransform.sizeDelta = new Vector2(Mathf.Abs(endPos.x - startPos.x), Mathf.Abs(endPos.y - startPos.y));
        AddVert();
    }

    /// <summary>
    /// 增加点位
    /// </summary>
    public void AddVert()
    {

        vertexQuadList.Clear();

        //贝塞尔的两个中间点位，Start点位在左边和右边的情况区分
        Vector3 tempMiddle1Pos = startPos + Vector3.Distance(startPos, endPos) / 5f * ((startPos.x < endPos.x) ? Vector3.right : Vector3.left);
        Vector3 tempMiddle2Pos = endPos + Vector3.Distance(startPos, endPos) / 5f * ((startPos.x < endPos.x) ? Vector3.left : Vector3.right);
        //增加贝塞尔点位
        List<Vector3> bPoints = CreatThreeBezierCurve(startPos, endPos, tempMiddle1Pos, tempMiddle2Pos);
        //初始的方向设置为向上垂直方向
        Vector3 lastVerticalDir = (startPos.x < endPos.x) ? Vector3.up : Vector3.down;
        for (int i = 0; i < bPoints.Count - 1; i++)
        {
            //当前的线段方向
            Vector3 curDir = bPoints[i] - bPoints[i + 1];
            //通过当前线段方向计算垂直方向
            Vector3 curVerticalDir = Vector3.Cross(curDir.normalized, Vector3.forward).normalized;
            //已知两个相邻点位，并且已知线段的粗细，可以计算出四边形
            List<UIVertex> vertexQuad = GetTwoPointMesh(bPoints[i], bPoints[i + 1], lastVerticalDir, curVerticalDir);

            vertexQuadList.Add(vertexQuad);
            lastVerticalDir = curVerticalDir;
        }
        SetVerticesDirty();
    }


    /// <summary>
    /// 获得两个点之间的面片
    /// </summary>
    /// <param name="vertexQuad"></param>
    private List<UIVertex> GetTwoPointMesh(Vector3 _startPos, Vector3 _endPos, Vector3 beforeTwoNodeVerticalDir, Vector3 nextTwoNodeVerticalDir)
    {
        List<UIVertex> vertexQuad = new List<UIVertex>();

        //  v0-------v1
        //   |              |
        //  v3-------v2
        //           ↑
        //       width

        Vector3 v0 = _startPos - beforeTwoNodeVerticalDir * lineWidth - rectTransform.position;
        Vector3 v1 = _startPos + beforeTwoNodeVerticalDir * lineWidth - rectTransform.position;

        Vector3 v3 = _endPos - nextTwoNodeVerticalDir * lineWidth - rectTransform.position;
        Vector3 v2 = _endPos + nextTwoNodeVerticalDir * lineWidth - rectTransform.position;

        UIVertex uIVertex = new UIVertex();
        uIVertex.position = v0;
        uIVertex.color = color;
        vertexQuad.Add(uIVertex);
        UIVertex uIVertex1 = new UIVertex();
        uIVertex1.position = v1;
        uIVertex1.color = color;
        vertexQuad.Add(uIVertex1);

        UIVertex uIVertex2 = new UIVertex();
        uIVertex2.position = v2;
        uIVertex2.color = color;
        vertexQuad.Add(uIVertex2);

        UIVertex uIVertex3 = new UIVertex();
        uIVertex3.position = v3;
        uIVertex3.color = color;
        vertexQuad.Add(uIVertex3);

        return vertexQuad;
    }


    /// <summary>
    /// 设置线段的碰撞
    /// </summary>
    /// <param name="sp"></param>
    /// <param name="eventCamera"></param>
    /// <returns></returns>
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        bool isEnterMesh = false;
        for (int i = 0; i < vertexQuadList.Count; i++)
        {
            if (IsContainInQuad(sp, transform.position + vertexQuadList[i][0].position, transform.position + vertexQuadList[i][1].position, transform.position + vertexQuadList[i][2].position, transform.position + vertexQuadList[i][3].position))
            {
                isEnterMesh = true;
                break;
            }
        }
        return isEnterMesh;
    }

    bool IsContainInQuad(Vector3 point, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        //      p1-----p2
        //      |       |
        //      | point |
        //      |       |
        //      p4-----p3

        Vector2 p1p2 = p1 - p2;
        Vector2 p1p = p1 - point;
        Vector2 p3p4 = p3 - p4;
        Vector2 p3p = p3 - point;

        Vector2 p4p1 = p4 - p1;
        Vector2 p4p = p4 - point;
        Vector2 p2p3 = p2 - p3;
        Vector2 p2p = p2 - point;

        bool isBetweenP1P2_P3P4 = CrossAB(p1p2, p1p) * CrossAB(p3p4, p3p) > 0;
        bool isBetweenP4P1_P2P3 = CrossAB(p4p1, p4p) * CrossAB(p2p3, p2p) > 0;

        return isBetweenP1P2_P3P4 && isBetweenP4P1_P2P3;

    }

    float CrossAB(Vector2 a, Vector2 b)
    {
        return a.x * b.y - b.x * a.y;
    }

    public float nultiple = 8;
    /// <summary>
    /// 三阶贝塞尔
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    /// <param name="middlePoint1"></param>
    public List<Vector3> CreatThreeBezierCurve(Vector3 startPoint, Vector3 endPoint, Vector3 middlePoint1, Vector3 middlePoint2)
    {
        List<Vector3> allPoints = new List<Vector3>();

        for (int i = 0; i < nultiple; i++)
        {
            float tempPercent = (float)i / (float)nultiple;
            float dis1 = Vector3.Distance(startPoint, middlePoint1);
            Vector3 pointL1 = startPoint + Vector3.Normalize(middlePoint1 - startPoint) * dis1 * tempPercent;
            float dis2 = Vector3.Distance(middlePoint1, middlePoint2);
            Vector3 pointL2 = middlePoint1 + Vector3.Normalize(middlePoint2 - middlePoint1) * dis2 * tempPercent;
            float dis3 = Vector3.Distance(pointL1, pointL2);
            Vector3 pointLeft = pointL1 + Vector3.Normalize(pointL2 - pointL1) * dis3 * tempPercent;

            float dis4 = Vector3.Distance(middlePoint2, endPoint);
            Vector3 pointR1 = middlePoint2 + Vector3.Normalize(endPoint - middlePoint2) * dis4 * tempPercent;
            float dis5 = Vector3.Distance(pointL2, pointR1);
            Vector3 pointRight = pointL2 + Vector3.Normalize(pointR1 - pointL2) * dis5 * tempPercent;

            float disLeftAndRight = Vector3.Distance(pointLeft, pointRight);
            Vector3 linePoint = pointLeft + Vector3.Normalize(pointRight - pointLeft) * disLeftAndRight * tempPercent;
            allPoints.Add(linePoint);
        }
        allPoints.Add(endPoint);
        return allPoints;
    }

}