using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tianbo.Wang
{
    public enum PanelType
    {
        MainPanel,
        SelectModePanel,
        HostPanel,
        ComputerPanel
    }
    public class Wtb_PanelManager : Wtb_SingleMono<Wtb_PanelManager>
    {
        const string panelsPath = "Prefabs/Panels/";

        public PanelType curOpenPanel;

        Transform panelParent;

        Stack<GameObject> allPanels = new Stack<GameObject>();

        public Action NoPanelAction;
        void Awake()
        {
            panelParent = GameObject.Find("PanelParent").transform;
        }
        public GameObject OpenPanel(PanelType panelType)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>(panelsPath + panelType.ToString()), panelParent);
            go.GetComponent<Wtb_PanelBase>().Init();
            go.name = panelType.ToString();
            allPanels.Push(go);
            return go;
        }

        public GameObject OpenPanel<T>() where T : Wtb_PanelBase
        {
            string tName = typeof(T).Name;
            GameObject go = Instantiate(Resources.Load<GameObject>(panelsPath + tName), panelParent);
            go.GetComponent<Wtb_PanelBase>().Init();
            go.name = tName;
            allPanels.Push(go);
            return go;
        }
        /// <summary>
        /// 跳转到之前的某一界面
        /// </summary>
        /// <param name="panelType"></param>
        public GameObject BackToPanel(PanelType panelType)
        {
            GameObject[] gos = allPanels.ToArray();
            bool isFind = false;
            for (int i = 0; i < gos.Length; i++)
            {
                if (gos[i].name == panelType.ToString())
                {
                    isFind = true;
                    break;
                }
            }
            if (!isFind)
            {
                Debug.Log("没找到要跳转的画布：" + panelType.ToString());
                return null;
            }
            for (int i = allPanels.Count - 1; i >= 0; i--)
            {
                GameObject tempPopGo = allPanels.Peek();
                if (tempPopGo.name != panelType.ToString())
                {
                    tempPopGo = allPanels.Pop();
                    DestroyImmediate(tempPopGo);
                }
                else
                {
                    tempPopGo.GetComponent<Wtb_PanelBase>().Init();
                    return tempPopGo;
                }
            }
            Debug.Log("没找到要跳转的画布：" + panelType.ToString());
            return null;
        }

        public GameObject BackToPanel<T>() where T : Wtb_PanelBase
        {
            string tName = typeof(T).Name;
            GameObject[] gos = allPanels.ToArray();
            bool isFind = false;
            for (int i = 0; i < gos.Length; i++)
            {
                if (gos[i].name == tName)
                {
                    isFind = true;
                    break;
                }
            }
            if (!isFind)
            {
                Debug.Log("没找到要跳转的画布：" + tName);
                return null;
            }
            for (int i = allPanels.Count - 1; i >= 0; i--)
            {
                GameObject tempPopGo = allPanels.Peek();
                if (tempPopGo.name != tName)
                {
                    tempPopGo = allPanels.Pop();
                    DestroyImmediate(tempPopGo);
                }
                else
                {
                    tempPopGo.GetComponent<Wtb_PanelBase>().Init();
                    return tempPopGo;
                }
            }
            Debug.Log("没找到要跳转的画布：" + tName);
            return null;
        }

        /// <summary>
        /// 关闭最上层的画布
        /// </summary>
        public void ClosePanel()
        {
            GameObject tempPanel = allPanels.Pop();
            allPanels.Peek().GetComponent<Wtb_PanelBase>().Init();
            if (allPanels.Count == 0)
            {
                NoPanelAction?.Invoke();
            }
            DestroyImmediate(tempPanel);
        }
    }
}