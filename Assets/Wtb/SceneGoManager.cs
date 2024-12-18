﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using HighlightingSystem;

namespace Tianbo.Wang
{
    public class SceneGoManager : Wtb_SingleMono<SceneGoManager>
    {
        public Action<int, string, bool> AddBagAction;
        public Action<string> SetStepAction;

        public GameObject host;
        public GameObject computer;

        public Transform CPU;
        public Transform CPU外壳;
        public Transform CPU硅脂;
        public Transform CPU硅脂注射器;
        public Transform 散热器;
        public Transform 内存;
        public Transform 主板;
        public Transform 电源;
        public Transform 显卡;
        public Transform 硬盘;
        public Transform 硬盘盖;
        public Transform 机箱盖;

        public GameObject CPU插槽;
        public GameObject 显卡插槽;
        public GameObject 内存条插槽;
        public GameObject 硬盘插槽;
        public GameObject 主板插槽;
        public GameObject 电源插槽;
        public GameObject 机箱盖插槽;


        public Transform cameraTrans;
        /// <summary>
        /// 远处的起始点
        /// </summary>
        public Transform cameraTransFar;
        public Transform 安装CPUCameraPoint;
        public Transform 安装散热器CameraPoint;
        public Transform 安装散热硅脂CameraPoint;
        public Transform 安装内存条CameraPoint;
        public Transform 安装主板CameraPoint;
        public Transform 安装电源CameraPoint;
        public Transform 安装显卡CameraPoint;
        public Transform 安装硬盘CameraPoint;


        public GameObject 显示器HDMI线;
        public GameObject 显示器DP线;
        public GameObject 显示器VGA线;
        public GameObject 音响线;
        public GameObject 键盘线;
        public GameObject 鼠标线;
        public GameObject 网线;
        public GameObject 机箱电源线;

        #region 第二模块组装配件
        public Transform 显示器CamTrans;
        public Transform 机箱CamTrans;
        public Transform 键盘鼠标CamTrans;
        public Transform 网线CamTrans;
        public Transform 音响CamTrans;

        public Transform 显示器机箱线CamTrans;
        public Transform 机箱电源线CamTrans;

        public Transform 键盘鼠标机箱线CamTrans;
        public Transform 网线机箱线CamTrans;
        public Transform 音响机箱线CamTrans;

        public GameObject DragPoint_鼠标;
        public GameObject DragPoint_键盘;
        public GameObject DragPoint_音响;
        public GameObject DragPoint_显示器HDMI;
        public GameObject DragPoint_显示器DP;
        public GameObject DragPoint_显示器VGA;
        public GameObject DragPoint_机箱电源插孔;
        public GameObject DragPoint_机箱USB;
        public GameObject DragPoint_机箱音频插孔;
        public GameObject DragPoint_机箱网线插孔;
        public GameObject DragPoint_机箱HDMI;
        public GameObject DragPoint_机箱DP;
        public GameObject DragPoint_机箱VGA;
        public GameObject DragPoint_插排网线插座;
        public GameObject DragPoint_插排电源插座;


        bool shouldConnect显示器1;
        bool shouldConnect显示器2;
        bool shouldConnect电源1;
        bool shouldConnect电源2;
        bool shouldConnect鼠标1;
        bool shouldConnect鼠标2;
        bool shouldConnect键盘1;
        bool shouldConnect键盘2;
        bool shouldConnect网线1;
        bool shouldConnect网线2;
        bool shouldConnect音响1;
        bool shouldConnect音响2;

        #endregion

        bool shouldClickCPUUI;
        bool shouldClickCPU插槽;
        bool shouldClickCPU外壳Open;
        bool shouldClickCPU外壳Close;
        bool shouldClickCPU散热器UI;
        bool shouldClickCPU散热器;
        bool shouldClickCPU硅脂注射器;
        bool shouldClick内存UI;
        bool shouldClick内存插槽;
        bool shouldClick主板;
        bool shouldClick主板插槽;
        bool shouldClick电源UI;
        bool shouldClick电源插槽;
        bool shouldClick显卡UI;
        bool shouldClick显卡插槽;
        bool shouldClick硬盘UI;
        bool shouldClick硬盘插槽;
        bool shouldClick机箱盖UI;
        bool shouldClick机箱盖插槽;

        bool shouldClickHDMIUI;
        bool shouldClickDPUI;
        bool shouldClickVGAUI;
        bool shouldClick电源线UI;
        bool shouldClickUSBUI;
        bool shouldClick网线UI;
        bool shouldClick音频线UI;


        public void Init()
        {
            shouldClickCPUUI = false;
            shouldClickCPU插槽 = false;
            shouldClickCPU外壳Open = false;
            shouldClickCPU外壳Close = false;
            shouldClickCPU散热器UI = false;
            shouldClickCPU散热器 = false;
            shouldClickCPU硅脂注射器 = false;
            shouldClick内存UI = false;
            shouldClick内存插槽 = false;
            shouldClick主板 = false;
            shouldClick主板插槽 = false;
            shouldClick电源UI = false;
            shouldClick电源插槽 = false;
            shouldClick显卡UI = false;
            shouldClick显卡插槽 = false;
            shouldClick硬盘UI = false;
            shouldClick硬盘插槽 = false;
            shouldClick机箱盖UI = false;
            shouldClick机箱盖插槽 = false;

        }

        float animTime = 0.5f;
        Ray ray;
        RaycastHit hit;
        public Camera cam;
        RectTransform canvasTrans;
        //GameObject ooo;
        void Update()
        {
            if (canvasTrans == null)
            {
                canvasTrans = GameObject.Find("Canvas").GetComponent<RectTransform>();
            }
            float xP = (float)Screen.width / 1920f;
            float yP = (float)Screen.height / 1080f;

            Vector2 finalPoint = new Vector2(Input.mousePosition.x / xP, Input.mousePosition.y / yP);

            ray = cam.ScreenPointToRay(finalPoint);
            if (Physics.Raycast(ray, out hit))
            {
                //if (ooo == null)
                //    ooo = GameObject.CreatePrimitive(PrimitiveType.Cube) ;
                //ooo.transform.localScale = Vector3.one * 0.1f;
                //ooo.transform.position = hit.point;
                //ooo.layer = 2;
                #region 安装CPU
                //打开CPU盖板
                if (shouldClickCPU外壳Open)
                {
                    HighlightObj(CPU外壳.gameObject);

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform == CPU外壳)
                        {
                            shouldClickCPU外壳Open = false;
                            SwitchCPU外壳(true, false, false, () =>
                            {
                                //此时应该去选中UI里面的CPU，选中后调用 “点击了CPU的UI() ”方法
                                shouldClickCPUUI = true;
                            });
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }

                }
                //插上CPU
                if (shouldClickCPU插槽)
                {
                    HighlightObj(CPU插槽);

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == CPU插槽)
                        {
                            AddBagAction?.Invoke(0, "CPU", false);
                            shouldClickCPU插槽 = false;
                            SwitchCPU(false, false, () =>
                            {
                                shouldClickCPU外壳Close = true;
                            });
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }


                }
                //关闭CPU盖板
                if (shouldClickCPU外壳Close)
                {
                    HighlightObj(CPU外壳.gameObject);

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform == CPU外壳)
                        {
                            shouldClickCPU外壳Close = false;
                            SwitchCPU外壳(false, false, true, () =>
                            {
                                //进入下一步骤
                                安装散热器();
                            });
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }


                }
                #endregion


                #region 安装散热器
                //注射器注射硅脂
                if (shouldClickCPU硅脂注射器)
                {
                    HighlightObj(CPU硅脂注射器.gameObject);

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform == CPU硅脂注射器)
                        {
                            cam.transform.DOMove(安装散热器CameraPoint.position, 1f);
                            cam.transform.DORotate(安装散热器CameraPoint.localEulerAngles, 1f);
                            shouldClickCPU硅脂注射器 = false;
                            SwitchCPU硅脂(true, false, () =>
                            {
                                //等待点击散热器UI
                                shouldClickCPU散热器UI = true;
                            });
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }


                }
                //安装散热器
                if (shouldClickCPU散热器)
                {
                    HighlightObj(CPU外壳.gameObject);

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform == CPU外壳)
                        {
                            AddBagAction?.Invoke(0, "散热器", false);
                            shouldClickCPU散热器 = false;
                            Switch散热器(false, false, () =>
                            {
                                //进入下一步骤
                                安装内存();
                            });
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }


                }
                #endregion

                #region 内存
                if (shouldClick内存插槽)
                {
                    HighlightObj(内存条插槽);

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == 内存条插槽)
                        {
                            AddBagAction?.Invoke(0, "内存", false);
                            shouldClick内存插槽 = false;
                            Switch内存(false, false, () =>
                            {
                                //下一模块
                                安装硬盘();

                            });
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }


                }
                #endregion


                #region 硬盘
                if (shouldClick硬盘插槽)
                {
                    HighlightObj(硬盘插槽);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == 硬盘插槽)
                        {
                            AddBagAction?.Invoke(0, "硬盘", false);
                            shouldClick硬盘插槽 = false;
                            Switch硬盘(false, false, () =>
                            {
                                //下一模块
                                安装主板();
                            });
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }


                }
                #endregion

                #region 主板
                if (shouldClick主板)
                {
                    HighlightObj(主板.gameObject);

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform == 主板)
                        {
                            shouldClick主板 = false;
                            shouldClick主板插槽 = true;
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }


                }

                if (shouldClick主板插槽)
                {
                    HighlightObj(主板插槽);

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == 主板插槽)
                        {
                            shouldClick主板插槽 = false;
                            Switch主板(false, false, () =>
                            {
                                //下一模块
                                安装电源();
                            });
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }


                }

                #endregion

                #region 电源
                if (shouldClick电源插槽)
                {
                    HighlightObj(电源插槽);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == 电源插槽)
                        {
                            AddBagAction?.Invoke(0, "电源", false);
                            shouldClick电源插槽 = false;
                            Switch电源(false, false, () =>
                            {
                                //下一模块
                                安装显卡();
                            });
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }


                }
                #endregion
                #region 显卡
                if (shouldClick显卡插槽)
                {
                    HighlightObj(显卡插槽);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == 显卡插槽)
                        {
                            AddBagAction?.Invoke(0, "显卡", false);
                            shouldClick显卡插槽 = false;
                            Switch显卡(false, false, () =>
                            {
                                //下一模块
                                安装机箱盖();
                            });
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }


                }
                #endregion

                #region 机箱盖
                if (shouldClick机箱盖插槽)
                {
                    HighlightObj(机箱盖插槽);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == 机箱盖插槽)
                        {
                            AddBagAction?.Invoke(0, "机箱盖", false);
                            shouldClick机箱盖插槽 = false;
                            Switch机箱盖(false, false, () =>
                            {
                                //进入第二模块，准备主机连线
                                机箱盖插槽.SetActive(false);
                                Wtb_TipsDialog wtb_TipsDialog = (Wtb_TipsDialog)Wtb_DialogManager.Instance.OpenDialog(Wtb_DialogType.Wtb_TipsDialog);
                                wtb_TipsDialog.SetTipsInfo("完成当前模块", () =>
                                {

                                    Wtb_PanelManager.Instance.BackToPanel(PanelType.SelectModePanel);
                                });
                            });
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }


                }
                #endregion

                #region 第二模块

                if (shouldConnect显示器1)
                {
                    GameObject tempGo = null;
                    switch (isHdmiOrDpOrVga)
                    {
                        case 0:
                            tempGo = DragPoint_显示器HDMI;
                            break;
                        case 1:
                            tempGo = DragPoint_显示器DP;
                            break;
                        case 2:
                            tempGo = DragPoint_显示器VGA;
                            break;
                    }
                    HighlightObj(tempGo);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == tempGo)
                        {
                            shouldClickHDMIUI = false;
                            shouldClickDPUI = false;
                            shouldClickVGAUI = false;
                            shouldConnect显示器1 = false;
                            连接显示器2();
                        }
                        else
                        {
                            //错误提示
                            //   WrongTips();
                        }
                    }


                }
                else
                if (shouldConnect显示器2)
                {
                    GameObject tempGo = null;
                    GameObject tempLineGo = null;
                    switch (isHdmiOrDpOrVga)
                    {
                        case 0:
                            tempGo = DragPoint_机箱HDMI;
                            tempLineGo = 显示器HDMI线;
                            break;
                        case 1:
                            tempGo = DragPoint_机箱DP;
                            tempLineGo = 显示器DP线;
                            break;
                        case 2:
                            tempGo = DragPoint_机箱VGA;
                            tempLineGo = 显示器VGA线;
                            break;
                    }
                    HighlightObj(tempGo);
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log(hit.transform.gameObject);
                        Debug.Log(tempGo);
                        if (hit.transform.gameObject == tempGo)
                        {
                            shouldConnect显示器2 = false;
                            tempLineGo.SetActive(true);

                            cam.transform.DOMove(显示器机箱线CamTrans.position, 1f);
                            cam.transform.DORotate(显示器机箱线CamTrans.localEulerAngles, 1f);

                            HighlightObjAutoOff(tempLineGo, () =>
                            {
                                连接电源1();
                            });

                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }


                }
                else
                if (shouldConnect电源1)
                {
                    HighlightObj(DragPoint_机箱电源插孔);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == DragPoint_机箱电源插孔)
                        {
                            shouldConnect电源1 = false;
                            shouldClick电源线UI = false;
                            连接电源2();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }


                }
                else
                if (shouldConnect电源2)
                {
                    HighlightObj(DragPoint_插排电源插座);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == DragPoint_插排电源插座)
                        {
                            shouldConnect电源2 = false;

                            cam.transform.DOMove(机箱电源线CamTrans.position, 1f);
                            cam.transform.DORotate(机箱电源线CamTrans.localEulerAngles, 1f);
                            机箱电源线.SetActive(true);

                            HighlightObjAutoOff(机箱电源线, () =>
                            {
                                连接键盘1();
                            });


                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                }
                else
                if (shouldConnect键盘1)
                {
                    HighlightObj(DragPoint_键盘);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == DragPoint_键盘)
                        {
                            shouldConnect键盘1 = false;
                            shouldClickUSBUI = false;
                            连接键盘2();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                }
                else
                if (shouldConnect键盘2)
                {
                    HighlightObj(DragPoint_机箱USB);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == DragPoint_机箱USB)
                        {
                            shouldConnect键盘2 = false;
                            cam.transform.DOMove(键盘鼠标机箱线CamTrans.position, 1f);
                            cam.transform.DORotate(键盘鼠标机箱线CamTrans.localEulerAngles, 1f);
                            键盘线.SetActive(true);

                            HighlightObjAutoOff(键盘线, () =>
                            {
                                连接鼠标1();
                            });

                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                }
                else
                if (shouldConnect鼠标1)
                {
                    HighlightObj(DragPoint_鼠标);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == DragPoint_鼠标)
                        {
                            shouldClickUSBUI = false;
                            shouldConnect鼠标1 = false;
                            连接鼠标2();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                }
                else
                if (shouldConnect鼠标2)
                {
                    HighlightObj(DragPoint_机箱USB);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == DragPoint_机箱USB)
                        {
                            shouldConnect鼠标2 = false;
                            cam.transform.DOMove(键盘鼠标机箱线CamTrans.position, 1f);
                            cam.transform.DORotate(键盘鼠标机箱线CamTrans.localEulerAngles, 1f);
                            鼠标线.SetActive(true);

                            HighlightObjAutoOff(鼠标线, () =>
                            {
                                连接网线1();
                            });


                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                }
                else
                if (shouldConnect网线1)
                {
                    HighlightObj(DragPoint_机箱网线插孔);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == DragPoint_机箱网线插孔)
                        {
                            shouldClick网线UI = false;
                            shouldConnect网线1 = false;
                            连接网线2();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                }
                else
                if (shouldConnect网线2)
                {
                    HighlightObj(DragPoint_插排网线插座);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == DragPoint_插排网线插座)
                        {
                            shouldConnect网线2 = false;
                            cam.transform.DOMove(网线机箱线CamTrans.position, 1f);
                            cam.transform.DORotate(网线机箱线CamTrans.localEulerAngles, 1f);
                            网线.SetActive(true);
                            HighlightObjAutoOff(网线, () =>
                            {
                                连接音响1();
                            });


                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                }
                else
                if (shouldConnect音响1)
                {
                    HighlightObj(DragPoint_音响);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == DragPoint_音响)
                        {
                            shouldClick音频线UI = false;
                            shouldConnect音响1 = false;
                            连接音响2();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                }
                else
                if (shouldConnect音响2)
                {
                    HighlightObj(DragPoint_机箱音频插孔);
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.gameObject == DragPoint_机箱音频插孔)
                        {
                            shouldConnect音响2 = false;

                            cam.transform.DOMove(音响机箱线CamTrans.position, 1f);
                            cam.transform.DORotate(音响机箱线CamTrans.localEulerAngles, 1f);
                            音响线.SetActive(true);
                            HighlightObjAutoOff(音响线, () =>
                            {
                                Wtb_TipsDialog wtb_TipsDialog = (Wtb_TipsDialog)Wtb_DialogManager.Instance.OpenDialog(Wtb_DialogType.Wtb_TipsDialog);
                                wtb_TipsDialog.SetTipsInfo("完成当前模块", () =>
                                {
                                    Wtb_PanelManager.Instance.BackToPanel(PanelType.SelectModePanel);
                                });
                            });



                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                }

                #endregion

            }

        }

        private void HighlightObj(GameObject highlightObj)
        {
            if (!GameManager.Instance.isStudyMode)
            {
                return;
            }
            Highlighter highlighter = highlightObj.GetComponent<Highlighter>();
            if (highlighter == null)
            {
                highlighter = highlightObj.gameObject.AddComponent<Highlighter>();
            }
            highlighter.Hover(Color.yellow);
        }

        private void HighlightObjAutoOff(GameObject highlightObj, Action FinishAction)
        {
            Highlighter highlighter = highlightObj.GetComponent<Highlighter>();
            if (highlighter == null)
            {
                highlighter = highlightObj.gameObject.AddComponent<Highlighter>();
            }
            highlighter.ConstantOnImmediate(Color.green);
            highlighter.TweenStart();
            StartCoroutine(WaitClose(() =>
            {
                highlighter.TweenStop();
                highlighter.ConstantOffImmediate();
                FinishAction?.Invoke();
            }));

        }
        IEnumerator WaitClose(Action FinishAction)
        {
            yield return new WaitForSeconds(2);
            FinishAction?.Invoke();
        }

        Transform tempParent;
        void WrongTips()
        {
            if (!GameManager.Instance.isStudyMode)
            {
                if (tempParent == null)
                {
                    tempParent = GameObject.Find("Canvas").transform;
                }
                TipsManager.Instance.ShowTips("操作有误", tempParent);
            }
        }

        public void SwitctToHost()
        {
            host.SetActive(true);
            computer.SetActive(false);
        }
        public void SwitctToComputer()
        {
            host.SetActive(false);
            computer.SetActive(true);

            显示器HDMI线.SetActive(false);
            显示器DP线.SetActive(false);
            显示器VGA线.SetActive(false);
            音响线.SetActive(false);
            键盘线.SetActive(false);
            鼠标线.SetActive(false);
            网线.SetActive(false);
            机箱电源线.SetActive(false);
            InitSecond();

        }

        public void 设备拆除()
        {

            SetStepAction?.Invoke("设备拆除");

            cam.transform.DOKill();
            cam.transform.position = cameraTransFar.position;
            cam.transform.rotation = cameraTransFar.rotation;
            cam.transform.DOMove(cameraTrans.position, 1f);
            cam.transform.DORotate(cameraTrans.localEulerAngles, 1f).OnComplete(() =>
            {

                Switch机箱盖(false, true);
                Switch硬盘(false, true);
                Switch显卡(false, true);
                Switch电源(false, true);
                Switch主板(false, true);
                Switch内存(false, true);
                Switch散热器(false, true);
                SwitchCPU(false, true);
                SwitchCPU外壳(false, true);
                SwitchCPU硅脂(false, true);

                Switch机箱盖(true, false, () =>
                {
                    AddBagAction?.Invoke(0, "机箱盖", true);

                    Switch显卡(true, false, () =>
                    {
                        AddBagAction?.Invoke(0, "显卡", true);
                        Switch电源(true, false, () =>
                        {
                            AddBagAction?.Invoke(0, "电源", true);
                            Switch主板(true, false, () =>
                            {
                                Switch硬盘(true, false, () =>
                                {
                                    AddBagAction?.Invoke(0, "硬盘", true);
                                    Switch内存(true, false, () =>
                                {
                                    AddBagAction?.Invoke(0, "内存", true);
                                    Switch散热器(true, false, () =>
                                    {
                                        AddBagAction?.Invoke(0, "散热器", true);
                                        SwitchCPU硅脂(false, false, () =>
                                        {
                                            SwitchCPU外壳(true, false, false, () =>
                                            {
                                                SwitchCPU(true, false, () =>
                                                {
                                                    AddBagAction?.Invoke(0, "CPU", true);
                                                    SwitchCPU外壳(false, false, false, () =>
                                                    {
                                                        Debug.Log("第一步动画完成");
                                                        安装CPU();
                                                    });
                                                });
                                            });
                                        });

                                    });
                                });
                                });
                            });
                        });
                    });
                });

            });

        }

        public void 安装CPU()
        {
            SetStepAction?.Invoke("安装CPU");
            AddBagAction?.Invoke(0, "机箱盖", true);
            AddBagAction?.Invoke(0, "显卡", true);
            AddBagAction?.Invoke(0, "电源", true);
            AddBagAction?.Invoke(0, "硬盘", true);
            AddBagAction?.Invoke(0, "内存", true);
            AddBagAction?.Invoke(0, "散热器", true);
            AddBagAction?.Invoke(0, "CPU", true);


            cam.transform.DOMove(安装CPUCameraPoint.position, 1f);
            cam.transform.DORotate(安装CPUCameraPoint.localEulerAngles, 1f);

            //cam.transform.position = 安装CPUCameraPoint.position;
            //cam.transform.rotation = 安装CPUCameraPoint.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(true, true);
            Switch显卡(true, true);
            Switch电源(true, true);
            Switch主板(true, true);
            Switch内存(true, true);
            Switch散热器(true, true);
            SwitchCPU(true, true);
            SwitchCPU外壳(false, true);
            SwitchCPU硅脂(false, true);
            Init();
            shouldClickCPU外壳Open = true;
        }

        public void 安装散热器()
        {
            SetStepAction?.Invoke("安装 散热器");
            AddBagAction?.Invoke(0, "机箱盖", true);
            AddBagAction?.Invoke(0, "显卡", true);
            AddBagAction?.Invoke(0, "电源", true);
            AddBagAction?.Invoke(0, "硬盘", true);
            AddBagAction?.Invoke(0, "内存", true);
            AddBagAction?.Invoke(0, "散热器", true);

            cam.transform.DOMove(安装散热硅脂CameraPoint.position, 1f);
            cam.transform.DORotate(安装散热硅脂CameraPoint.localEulerAngles, 1f);


            //cam.transform.position = 安装散热器CameraPoint.position;
            //cam.transform.rotation = 安装散热器CameraPoint.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(true, true);
            Switch显卡(true, true);
            Switch电源(true, true);
            Switch主板(true, true);
            Switch内存(true, true);
            Switch散热器(true, true);
            SwitchCPU(false, true);
            SwitchCPU外壳(false, true);
            SwitchCPU硅脂(false, true);
            Init();
            shouldClickCPU硅脂注射器 = true;
        }
        public void 安装内存()
        {
            SetStepAction?.Invoke("安装内存");
            AddBagAction?.Invoke(0, "机箱盖", true);
            AddBagAction?.Invoke(0, "显卡", true);
            AddBagAction?.Invoke(0, "电源", true);
            AddBagAction?.Invoke(0, "硬盘", true);
            AddBagAction?.Invoke(0, "内存", true);

            cam.transform.DOMove(安装内存条CameraPoint.position, 1f);
            cam.transform.DORotate(安装内存条CameraPoint.localEulerAngles, 1f);
            //cam.transform.position = 安装内存条CameraPoint.position;
            //cam.transform.rotation = 安装内存条CameraPoint.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(true, true);
            Switch显卡(true, true);
            Switch电源(true, true);
            Switch主板(true, true);
            Switch内存(true, true);
            Switch散热器(false, true);
            SwitchCPU(false, true);
            SwitchCPU外壳(false, true);
            SwitchCPU硅脂(true, true);
            Init();
            shouldClick内存UI = true;
        }
        public void 安装硬盘()
        {
            SetStepAction?.Invoke("安装硬盘");
            AddBagAction?.Invoke(0, "机箱盖", true);
            AddBagAction?.Invoke(0, "显卡", true);
            AddBagAction?.Invoke(0, "电源", true);
            AddBagAction?.Invoke(0, "硬盘", true);

            cam.transform.DOMove(安装硬盘CameraPoint.position, 1f);
            cam.transform.DORotate(安装硬盘CameraPoint.localEulerAngles, 1f);
            //cam.transform.position = cameraTrans.position;
            //cam.transform.rotation = cameraTrans.rotation;
            Switch机箱盖(true, true);
            Switch显卡(true, true);
            Switch电源(true, true);
            Switch主板(true, true);
            Switch硬盘(true, true);
            Switch内存(false, true);
            Switch散热器(false, true);
            SwitchCPU(false, true);
            SwitchCPU外壳(false, true);
            SwitchCPU硅脂(true, true);
            Init();
            shouldClick硬盘UI = true;
        }

        public void 安装主板()
        {
            SetStepAction?.Invoke("安装主板");
            AddBagAction?.Invoke(0, "机箱盖", true);
            AddBagAction?.Invoke(0, "显卡", true);
            AddBagAction?.Invoke(0, "电源", true);
            AddBagAction?.Invoke(0, "硬盘", true);


            cam.transform.DOMove(安装主板CameraPoint.position, 1f);
            cam.transform.DORotate(安装主板CameraPoint.localEulerAngles, 1f);
            //cam.transform.position = cameraTrans.position;
            //cam.transform.rotation = cameraTrans.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(true, true);
            Switch显卡(true, true);
            Switch电源(true, true);
            Switch主板(true, true);
            Switch内存(false, true);
            Switch散热器(false, true);
            SwitchCPU(false, true);
            SwitchCPU外壳(false, true);
            SwitchCPU硅脂(true, true);
            Init();
            shouldClick主板 = true;
        }

        public void 安装电源()
        {
            SetStepAction?.Invoke("安装电源");
            AddBagAction?.Invoke(0, "机箱盖", true);
            AddBagAction?.Invoke(0, "显卡", true);
            AddBagAction?.Invoke(0, "电源", true);

            cam.transform.DOMove(安装电源CameraPoint.position, 1f);
            cam.transform.DORotate(安装电源CameraPoint.localEulerAngles, 1f);

            //cam.transform.position = cameraTrans.position;
            //cam.transform.rotation = cameraTrans.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(true, true);
            Switch显卡(true, true);
            Switch电源(true, true);
            Switch主板(false, true);
            Switch内存(false, true);
            Switch散热器(false, true);
            SwitchCPU(false, true);
            SwitchCPU外壳(false, true);
            SwitchCPU硅脂(true, true);
            Init();
            shouldClick电源UI = true;
        }

        public void 安装显卡()
        {
            SetStepAction?.Invoke("安装 独立显卡");
            AddBagAction?.Invoke(0, "机箱盖", true);
            AddBagAction?.Invoke(0, "显卡", true);

            cam.transform.DOMove(安装显卡CameraPoint.position, 1f);
            cam.transform.DORotate(安装显卡CameraPoint.localEulerAngles, 1f);
            //cam.transform.position = cameraTrans.position;
            //cam.transform.rotation = cameraTrans.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(true, true);
            Switch显卡(true, true);
            Switch电源(false, true);
            Switch主板(false, true);
            Switch内存(false, true);
            Switch散热器(false, true);
            SwitchCPU(false, true);
            SwitchCPU外壳(false, true);
            SwitchCPU硅脂(true, true);
            Init();
            shouldClick显卡UI = true;
        }


        public void 安装机箱盖()
        {
            SetStepAction?.Invoke("安装机箱");
            AddBagAction?.Invoke(0, "机箱盖", true);

            cam.transform.DOMove(cameraTrans.position, 1f);
            cam.transform.DORotate(cameraTrans.localEulerAngles, 1f);
            //cam.transform.position = cameraTrans.position;
            //cam.transform.rotation = cameraTrans.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(false, true);
            Switch显卡(false, true);
            Switch电源(false, true);
            Switch主板(false, true);
            Switch内存(false, true);
            Switch散热器(false, true);
            SwitchCPU(false, true);
            SwitchCPU外壳(false, true);
            SwitchCPU硅脂(true, true);
            机箱盖插槽.SetActive(true);
            Init();
            shouldClick机箱盖UI = true;
        }
        /// <summary>
        /// 0是HDMI，1是DP，2是VGA
        /// </summary>
        int isHdmiOrDpOrVga = 0;
        public void ClickUIAction(string uiName)
        {
            Debug.Log(uiName);
            switch (uiName)
            {
                case "CPU":
                    if (shouldClickCPUUI)
                    {
                        shouldClickCPUUI = false;
                        shouldClickCPU插槽 = true;
                    }
                    break;
                case "散热器":
                    if (shouldClickCPU散热器UI)
                    {
                        shouldClickCPU散热器UI = false;
                        shouldClickCPU散热器 = true;
                    }
                    break;
                case "内存":
                    if (shouldClick内存UI)
                    {
                        shouldClick内存UI = false;
                        shouldClick内存插槽 = true;
                    }
                    break;
                case "电源":
                    if (shouldClick电源UI)
                    {
                        shouldClick电源UI = false;
                        shouldClick电源插槽 = true;
                    }
                    break;
                case "显卡":
                    if (shouldClick显卡UI)
                    {
                        shouldClick显卡UI = false;
                        shouldClick显卡插槽 = true;
                    }
                    break;
                case "硬盘":
                    if (shouldClick硬盘UI)
                    {
                        shouldClick硬盘UI = false;
                        shouldClick硬盘插槽 = true;
                    }
                    break;
                case "机箱盖":
                    if (shouldClick机箱盖UI)
                    {
                        shouldClick机箱盖UI = false;
                        shouldClick机箱盖插槽 = true;
                    }
                    break;

                case "HDMI接口":
                    if (shouldClickHDMIUI)
                    {
                        InitConnect3dBools();
                        isHdmiOrDpOrVga = 0;
                        shouldConnect显示器1 = true;
                    }
                    break;
                case "DP接口":
                    if (shouldClickDPUI)
                    {
                        InitConnect3dBools();
                        isHdmiOrDpOrVga = 1;
                        shouldConnect显示器1 = true;
                    }
                    break;
                case "VGA接口":
                    if (shouldClickVGAUI)
                    {
                        InitConnect3dBools();
                        isHdmiOrDpOrVga = 2;
                        shouldConnect显示器1 = true;
                    }
                    break;
                case "USB2.0":
                    if (shouldClickUSBUI)
                    {
                        InitConnect3dBools();
                        if (isKeyboard)
                            shouldConnect键盘1 = true;
                        else
                            shouldConnect鼠标1 = true;
                    }
                    break;
                case "电源线":
                    if (shouldClick电源线UI)
                    {
                        InitConnect3dBools();
                        shouldConnect电源1 = true;
                    }
                    break;
                case "以太网接口":
                    if (shouldClick网线UI)
                    {
                        InitConnect3dBools();
                        shouldConnect网线1 = true;
                    }
                    break;
                case "3.5毫米音频接口":
                    if (shouldClick音频线UI)
                    {
                        InitConnect3dBools();
                        shouldConnect音响1 = true;
                    }
                    break;
            }
        }


        public void Switch机箱盖(bool open, bool rightNow, Action FinishAction = null)
        {
            机箱盖.DOKill();
            Vector3 endPos;
            if (open)
                endPos = new Vector3(0.08f, -0.000212553f, -0.01207775f);
            else
                endPos = new Vector3(0.009559387f, -0.000212553f, -0.01207775f);
            if (rightNow)
                机箱盖.localPosition = endPos;
            else
                机箱盖.DOLocalMove(endPos, animTime).OnComplete(() => { FinishAction?.Invoke(); });
        }


        public void Switch显卡(bool open, bool rightNow, Action FinishAction = null)
        {
            显卡.DOKill();
            Vector3 endPos;
            if (open)
                endPos = new Vector3(0.1f, 0.0004668936f, -0.01119314f);
            else
                endPos = new Vector3(0.004904306f, 0.0004668936f, -0.01119314f);
            if (rightNow)
                显卡.localPosition = endPos;
            else
                显卡.DOLocalMove(endPos, animTime).OnComplete(() => { FinishAction?.Invoke(); });
        }

        public void Switch电源(bool open, bool rightNow, Action FinishAction = null)
        {
            电源.DOKill();
            Vector3 endPos;
            if (open)
                endPos = new Vector3(1, 0.006446421f, -0.2157426f);
            else
                endPos = new Vector3(0.1887167f, 0.006446421f, -0.2157426f);
            if (rightNow)
                电源.localPosition = endPos;
            else
                电源.DOLocalMove(endPos, animTime).OnComplete(() => { FinishAction?.Invoke(); });
        }
        public void Switch主板(bool open, bool rightNow, Action FinishAction = null)
        {
            主板.DOKill();
            Vector3 endPos;
            if (open)
                endPos = new Vector3(0.117f, 0.135f, 0.575f);
            else
                endPos = new Vector3(0.1588569f, 0.1349312f, -0.03486863f);
            if (rightNow)
            {
                主板.localPosition = endPos;
            }
            else
            {

                if (open)
                {
                    主板.localPosition = new Vector3(0.1588569f, 0.1349312f, -0.03486863f);
                    主板.DOLocalMove(new Vector3(0.369f, 0.1349312f, -0.03486863f), animTime).OnComplete(() =>
                    {
                        主板.DOLocalMove(new Vector3(0.369f, 0.135f, 0.575f), animTime).OnComplete(() =>
                        {
                            主板.DOLocalMove(new Vector3(0.117f, 0.135f, 0.575f), animTime).OnComplete(() => { FinishAction?.Invoke(); });
                        });
                    });
                }
                else
                {
                    主板.localPosition = new Vector3(0.117f, 0.135f, 0.575f);
                    主板.DOLocalMove(new Vector3(0.369f, 0.135f, 0.575f), animTime).OnComplete(() =>
                    {
                        主板.DOLocalMove(new Vector3(0.369f, 0.1349312f, -0.03486863f), animTime).OnComplete(() =>
                        {
                            主板.DOLocalMove(new Vector3(0.1588569f, 0.1349312f, -0.03486863f), animTime).OnComplete(() => { FinishAction?.Invoke(); });
                        });
                    });
                }
            }

        }
        public void Switch硬盘(bool open, bool rightNow, Action FinishAction = null)
        {
            硬盘.DOKill();
            硬盘盖.DOKill();
            Vector3 endPos;
            Vector3 endPosgaizi;
            if (open)
            {
                endPosgaizi = new Vector3(1.44f, 0.189f, -0.1783336f);
                endPos = new Vector3(1.396f, 0.189f, -0.1682017f);
            }
            else
            {
                endPosgaizi = new Vector3(0.4175687f, 0.1890412f, -0.1783336f);
                endPos = new Vector3(0.413185f, 0.1887054f, -0.1682017f);
            }
            if (rightNow)
            {
                硬盘.localPosition = endPos;
                硬盘盖.localPosition = endPosgaizi;
            }

            else
            {
                if (open)
                {
                    硬盘盖.DOLocalMove(endPosgaizi, animTime).OnComplete(() =>
                    {
                        硬盘.DOLocalMove(endPos, animTime).OnComplete(() => { FinishAction?.Invoke(); });
                    });
                }
                else
                {
                    硬盘.DOLocalMove(endPos, animTime).OnComplete(() =>
                    {
                        硬盘盖.DOLocalMove(endPosgaizi, animTime).OnComplete(() => { FinishAction?.Invoke(); });
                    });
                }


            }
        }

        public void Switch内存(bool open, bool rightNow, Action FinishAction = null)
        {
            内存.DOKill();
            Vector3 endPos;
            if (open)
                endPos = new Vector3(0.1f, 0.009921147f, -0.003622067f);
            else
                endPos = new Vector3(0.0006704569f, 0.009921147f, -0.003622067f);
            if (rightNow)
                内存.localPosition = endPos;
            else
                内存.DOLocalMove(endPos, animTime).OnComplete(() => { FinishAction?.Invoke(); });
        }

        public void Switch散热器(bool open, bool rightNow, Action FinishAction = null)
        {
            散热器.DOKill();
            Vector3 endPos;
            if (open)
                endPos = new Vector3(0.0959f, 0.009811332f, -0.01176217f);
            else
                endPos = new Vector3(-0.0007274255f, 0.009811332f, -0.01176217f);
            Debug.Log(endPos + "==" + rightNow);
            if (rightNow)
                散热器.localPosition = endPos;
            else
                散热器.DOLocalMove(endPos, animTime).OnComplete(() => { FinishAction?.Invoke(); });
        }

        public void SwitchCPU(bool open, bool rightNow, Action FinishAction = null)
        {
            CPU.DOKill();
            Vector3 endPos;
            if (open)
                endPos = new Vector3(0.1f, 0.009603716f, -0.01180644f);
            else
                endPos = new Vector3(-0.0002732739f, 0.009603716f, -0.01180644f);
            if (rightNow)
            {
                CPU.localPosition = endPos;
            }
            else
            {
                CPU.DOLocalMove(endPos, animTime).OnComplete(() => { FinishAction?.Invoke(); });
            }

        }


        public void SwitchCPU外壳(bool open, bool rightNow, bool containCpu = false, Action FinishAction = null)
        {
            CPU外壳.DOKill();
            Vector3 endRot;
            if (open)
                endRot = new Vector3(0, 0, 140);
            else
                endRot = Vector3.zero;
            if (rightNow)
            {
                CPU外壳.localEulerAngles = endRot;
            }
            else
            {
                CPU外壳.DOLocalRotate(endRot, animTime).OnComplete(() =>
                {
                    Transform temp外壳 = CPU外壳.Find("JHDX_JSJ_Cpu01");
                    temp外壳.DOKill();
                    if (containCpu)
                    {
                        temp外壳.DOLocalMove(new Vector3(0.00051f, 0.00058f, 0.00072f), 0.3f).OnComplete(() =>
                        {
                            temp外壳.DOLocalMove(new Vector3(0.0549f, 0.0187f, 0), 0.3f).OnComplete(() =>
                            {
                                FinishAction?.Invoke();
                            });
                        });
                        temp外壳.DOLocalRotate(new Vector3(16f, 0f, 0f), 0.3f);
                    }
                    else
                    {
                        temp外壳.localPosition = Vector3.zero;
                        temp外壳.localEulerAngles = Vector3.zero;
                        FinishAction?.Invoke();
                    }

                });
            }


        }

        public void SwitchCPU硅脂(bool open, bool rightNow, Action FinishAction = null)
        {
            CPU硅脂.DOKill();
            Vector3 endScale;
            if (open)
                endScale = new Vector3(0.1f, 0.1f, 0.1f);
            else
                endScale = Vector3.zero;
            if (rightNow)
            {
                CPU硅脂.localScale = endScale;
            }
            else
            {
                if (open)
                {
                    CPU硅脂注射器.transform.localPosition = new Vector3(-0.0535f, 0.1344f, 0.6312f);
                    CPU硅脂注射器.transform.localEulerAngles = new Vector3(0, -50, 90);
                    CPU硅脂.DOScale(endScale, animTime).OnComplete(() =>
                    {
                        CPU硅脂注射器.transform.localPosition = new Vector3(-0.139f, -0.062f, 0.37f);
                        CPU硅脂注射器.transform.localEulerAngles = Vector3.zero;
                        FinishAction?.Invoke();
                    });
                }
                else
                {
                    CPU硅脂.DOScale(endScale, animTime).OnComplete(() =>
                    {
                        FinishAction?.Invoke();
                    });
                }
            }
        }



        #region 第二模块
        public void InitSecond()
        {
            InitConnect3dBools();

            InitUIBools();

        }

        private void InitConnect3dBools()
        {
            shouldConnect显示器1 = false;
            shouldConnect显示器2 = false;
            shouldConnect电源1 = false;
            shouldConnect电源2 = false;
            shouldConnect鼠标1 = false;
            shouldConnect鼠标2 = false;
            shouldConnect键盘1 = false;
            shouldConnect键盘2 = false;
            shouldConnect网线1 = false;
            shouldConnect网线2 = false;
            shouldConnect音响1 = false;
            shouldConnect音响2 = false;
        }

        private void InitUIBools()
        {
            shouldClickHDMIUI = false;
            shouldClickDPUI = false;
            shouldClickVGAUI = false;
            shouldClick电源线UI = false;
            shouldClickUSBUI = false;
            shouldClick网线UI = false;
            shouldClick音频线UI = false;
        }

        void AddToBag2()
        {
            AddBagAction?.Invoke(1, "HDMI接口", true);
            AddBagAction?.Invoke(1, "DP接口", true);
            AddBagAction?.Invoke(1, "VGA接口", true);
            AddBagAction?.Invoke(1, "USB2.0", true);
            AddBagAction?.Invoke(1, "USB-C", true);
            AddBagAction?.Invoke(1, "以太网接口", true);
            AddBagAction?.Invoke(1, "3.5毫米音频接口", true);
            AddBagAction?.Invoke(1, "电源线", true);
        }
        public void 连接显示器1()
        {
            显示器HDMI线.SetActive(false);
            显示器VGA线.SetActive(false);
            显示器DP线.SetActive(false);
            cam.transform.DOKill();
            cam.transform.position = cameraTransFar.position;
            cam.transform.rotation = cameraTransFar.rotation;
            cam.transform.DOMove(显示器CamTrans.position, 1f);
            cam.transform.DORotate(显示器CamTrans.localEulerAngles, 1f);
            //cam.transform.position = 显示器CamTrans.position;
            //cam.transform.rotation = 显示器CamTrans.rotation;
            InitSecond();
            shouldClickHDMIUI = true;
            shouldClickDPUI = true;
            shouldClickVGAUI = true;
            SetStepAction?.Invoke("连接 显示器");
            AddToBag2();
            if (tempParent == null)
            {
                tempParent = GameObject.Find("Canvas").transform;
            }
            TipsManager.Instance.ShowTips("首先选择左侧线材，再选择接口", tempParent);
        }
        public void 连接显示器2()
        {
            cam.transform.DOMove(机箱CamTrans.position, 1f);
            cam.transform.DORotate(机箱CamTrans.localEulerAngles, 1f);
            //cam.transform.position = 机箱CamTrans.position;
            //cam.transform.rotation = 机箱CamTrans.rotation;
            InitConnect3dBools();
            shouldConnect显示器2 = true;
        }
        public void 连接电源1()
        {
            机箱电源线.SetActive(false);
            cam.transform.DOMove(机箱CamTrans.position, 1f);
            cam.transform.DORotate(机箱CamTrans.localEulerAngles, 1f);
            //cam.transform.position = 机箱CamTrans.position;
            //cam.transform.rotation = 机箱CamTrans.rotation;
            InitSecond();
            shouldClick电源线UI = true;

            SetStepAction?.Invoke("连接电源");
            AddToBag2();
        }
        public void 连接电源2()
        {
            cam.transform.DOMove(网线CamTrans.position, 1f);
            cam.transform.DORotate(网线CamTrans.localEulerAngles, 1f);
            //cam.transform.position = 网线CamTrans.position;
            //cam.transform.rotation = 网线CamTrans.rotation;
            InitConnect3dBools();
            shouldConnect电源2 = true;
        }
        bool isKeyboard = false;
        public void 连接键盘1()
        {
            键盘线.SetActive(false);

            cam.transform.DOMove(键盘鼠标CamTrans.position, 1f);
            cam.transform.DORotate(键盘鼠标CamTrans.localEulerAngles, 1f);
            //cam.transform.position = 键盘鼠标CamTrans.position;
            //cam.transform.rotation = 键盘鼠标CamTrans.rotation;
            InitSecond();
            isKeyboard = true;
            shouldClickUSBUI = true;
            SetStepAction?.Invoke("连接键盘");
            AddToBag2();
        }
        public void 连接键盘2()
        {
            cam.transform.DOMove(机箱CamTrans.position, 1f);
            cam.transform.DORotate(机箱CamTrans.localEulerAngles, 1f);
            //cam.transform.position = 机箱CamTrans.position;
            //cam.transform.rotation = 机箱CamTrans.rotation;
            InitConnect3dBools();
            shouldConnect键盘2 = true;
        }
        public void 连接鼠标1()
        {
            鼠标线.SetActive(false);
            cam.transform.DOMove(键盘鼠标CamTrans.position, 1f);
            cam.transform.DORotate(键盘鼠标CamTrans.localEulerAngles, 1f);
            //cam.transform.position = 键盘鼠标CamTrans.position;
            //cam.transform.rotation = 键盘鼠标CamTrans.rotation;
            InitSecond();
            isKeyboard = false;
            shouldClickUSBUI = true;
            SetStepAction?.Invoke("连接鼠标");
            AddToBag2();
        }
        public void 连接鼠标2()
        {
            cam.transform.DOMove(机箱CamTrans.position, 1f);
            cam.transform.DORotate(机箱CamTrans.localEulerAngles, 1f);
            //cam.transform.position = 机箱CamTrans.position;
            //cam.transform.rotation = 机箱CamTrans.rotation;
            InitConnect3dBools();
            shouldConnect鼠标2 = true;
        }
        public void 连接网线1()
        {
            网线.SetActive(false);
            cam.transform.DOMove(机箱CamTrans.position, 1f);
            cam.transform.DORotate(机箱CamTrans.localEulerAngles, 1f);
            //cam.transform.position = 机箱CamTrans.position;
            //cam.transform.rotation = 机箱CamTrans.rotation;
            InitSecond();
            shouldClick网线UI = true;
            SetStepAction?.Invoke("连接网线");
            AddToBag2();
        }
        public void 连接网线2()
        {
            cam.transform.DOMove(网线CamTrans.position, 1f);
            cam.transform.DORotate(网线CamTrans.localEulerAngles, 1f);
            //cam.transform.position = 网线CamTrans.position;
            //cam.transform.rotation = 网线CamTrans.rotation;
            InitConnect3dBools();
            shouldConnect网线2 = true;
        }
        public void 连接音响1()
        {
            音响线.SetActive(false);
            cam.transform.DOMove(音响CamTrans.position, 1f);
            cam.transform.DORotate(音响CamTrans.localEulerAngles, 1f);
            //cam.transform.position = 音响CamTrans.position;
            //cam.transform.rotation = 音响CamTrans.rotation;
            InitSecond();
            shouldClick音频线UI = true;
            SetStepAction?.Invoke("连接音响");
            AddToBag2();
        }
        public void 连接音响2()
        {
            cam.transform.DOMove(机箱CamTrans.position, 1f);
            cam.transform.DORotate(机箱CamTrans.localEulerAngles, 1f);
            //cam.transform.position = 机箱CamTrans.position;
            //cam.transform.rotation = 机箱CamTrans.rotation;
            InitConnect3dBools();
            shouldConnect音响2 = true;
        }
        #endregion


    }
}