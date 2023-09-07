using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tianbo.Wang
{
    public class Wtb_DrawLine : MaskableGraphic
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
            ShowVert();
        }

     
        public void ShowVert()
        {
            color = new Color(0, 0, 0, 0.5f);

            vertexQuadList.Clear();

            List<Vector3> bPoints = new List<Vector3>() { startPos, endPos };
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




    }

}