using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace Tianbo.Wang
{
    public class SceneGoManager : Wtb_SingleMono<SceneGoManager>
    {
        public Action<string, bool> AddBagAction;
        public Action ClearBagAction;

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
        public Transform 机箱盖;

        public GameObject CPU插槽;
        public GameObject 显卡插槽;
        public GameObject 内存条插槽;
        public GameObject 硬盘插槽;
        public GameObject 主板插槽;
        public GameObject 电源插槽;
        public GameObject 机箱盖插槽;


        public Transform cameraTrans;
        public Transform 安装CPUCameraPoint;
        public Transform 显示器CamTrans;
        public Transform 机箱CamTrans;
        public Transform 键盘鼠标CamTrans;
        public Transform 网线CamTrans;
        public Transform 音响CamTrans;

        public GameObject 显示器线;
        public GameObject 音响线;
        public GameObject 键盘线;
        public GameObject 鼠标线;
        public GameObject 网线;
        public GameObject 机箱电源线;

        public GameObject DragPoint_鼠标;
        public GameObject DragPoint_键盘;
        public GameObject DragPoint_音响;
        public GameObject DragPoint_显示器HDMI;
        public GameObject DragPoint_机箱电源插孔;
        public GameObject DragPoint_机箱USB;
        public GameObject DragPoint_机箱网线插孔;
        public GameObject DragPoint_机箱HDMI;
        public GameObject DragPoint_插排网线插座;
        public GameObject DragPoint_插排电源插座;


        bool shouldClickCPU插槽;
        bool shouldClickCPU外壳Open;
        bool shouldClickCPU外壳Close;
        bool shouldClickCPU散热器;
        bool shouldClickCPU硅脂注射器;

        bool shouldClick内存插槽;
        bool shouldClick主板;
        bool shouldClick主板插槽;
        bool shouldClick电源插槽;
        bool shouldClick显卡插槽;
        bool shouldClick硬盘插槽;
        bool shouldClick机箱盖插槽;


        Ray ray;
        RaycastHit hit;
        public Camera cam;
        void Update()
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    #region 安装CPU
                    //打开CPU盖板
                    if (shouldClickCPU外壳Open)
                    {
                        if (hit.transform.gameObject == CPU外壳)
                        {
                            shouldClickCPU外壳Open = false;
                            SwitchCPU外壳(true, false, () =>
                            {
                                //此时应该去选中UI里面的CPU，选中后调用 “点击了CPU的UI() ”方法

                            });
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                    //插上CPU
                    if (shouldClickCPU插槽)
                    {
                        if (hit.transform.gameObject == CPU插槽)
                        {
                            AddBagAction?.Invoke("CPU", false);
                            shouldClickCPU插槽 = false;
                            SwitchCPU(true, false, () =>
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
                    //关闭CPU盖板
                    if (shouldClickCPU外壳Close)
                    {
                        if (hit.transform.gameObject == CPU外壳)
                        {
                            shouldClickCPU外壳Close = false;
                            SwitchCPU外壳(false, false, () =>
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
                    #endregion


                    #region 安装散热器
                    //注射器注射硅脂
                    if (shouldClickCPU硅脂注射器)
                    {
                        if (hit.transform.gameObject == CPU硅脂注射器.gameObject)
                        {
                            shouldClickCPU硅脂注射器 = false;
                            SwitchCPU硅脂(true, false, () =>
                            {
                                //等待点击散热器UI

                            });
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                    //安装散热器
                    if (shouldClickCPU散热器)
                    {
                        if (hit.transform.gameObject == CPU外壳.gameObject)
                        {
                            AddBagAction?.Invoke("散热器", false);
                            shouldClickCPU散热器 = false;
                            Switch散热器(true, false, () =>
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
                    #endregion

                    #region 内存
                    if (shouldClick内存插槽)
                    {
                        if (hit.transform.gameObject == 内存条插槽.gameObject)
                        {
                            AddBagAction?.Invoke("内存", false);
                            shouldClick内存插槽 = false;
                            Switch内存(true, false, () =>
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
                    #endregion

                    #region 主板
                    if (shouldClick主板)
                    {
                        if (hit.transform.gameObject == 主板.gameObject)
                        {
                            shouldClick内存插槽 = false;
                            shouldClick主板插槽 = true;
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }

                    if (shouldClick主板插槽)
                    {
                        if (hit.transform.gameObject == 主板插槽.gameObject)
                        {
                            shouldClick主板插槽 = false;
                            Switch主板(true, false, () =>
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

                    #endregion

                    #region 电源
                    if (shouldClick电源插槽)
                    {
                        if (hit.transform.gameObject == 电源插槽)
                        {
                            AddBagAction?.Invoke("电源", false);
                            shouldClick电源插槽 = false;
                            Switch电源(true, false, () =>
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
                    #endregion
                    #region 显卡
                    if (shouldClick显卡插槽)
                    {
                        if (hit.transform.gameObject == 显卡插槽)
                        {
                            AddBagAction?.Invoke("显卡", false);
                            shouldClick显卡插槽 = false;
                            Switch显卡(true, false, () =>
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
                    #endregion

                    #region 硬盘
                    if (shouldClick硬盘插槽)
                    {
                        if (hit.transform.gameObject == 硬盘插槽)
                        {
                            AddBagAction?.Invoke("硬盘", false);
                            shouldClick硬盘插槽 = false;
                            Switch硬盘(true, false, () =>
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
                    #endregion
                    #region 机箱盖
                    if (shouldClick机箱盖插槽)
                    {
                        if (hit.transform.gameObject == 机箱盖插槽)
                        {
                            AddBagAction?.Invoke("机箱盖", false);
                            shouldClick机箱盖插槽 = false;
                            Switch机箱盖(true, false, () =>
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
                    #endregion

                    #region 第二模块
                    if (hit.transform.gameObject == DragPoint_鼠标)
                    {

                    }
                    if (hit.transform.gameObject == DragPoint_键盘)
                    {

                    }
                    if (hit.transform.gameObject == DragPoint_音响)
                    {

                    }
                    if (hit.transform.gameObject == DragPoint_显示器HDMI)
                    {

                    }
                    if (hit.transform.gameObject == DragPoint_机箱电源插孔)
                    {

                    }
                    if (hit.transform.gameObject == DragPoint_机箱USB)
                    {

                    }
                    if (hit.transform.gameObject == DragPoint_机箱网线插孔)
                    {

                    }
                    if (hit.transform.gameObject == DragPoint_机箱HDMI)
                    {

                    }
                    if (hit.transform.gameObject == DragPoint_插排网线插座)
                    {

                    }
                    if (hit.transform.gameObject == DragPoint_插排电源插座)
                    {

                    }

                    #endregion
                }
            }

        }

        void WrongTips()
        {

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
            显示器线.SetActive(false);
            音响线.SetActive(false);
            键盘线.SetActive(false);
            鼠标线.SetActive(false);
            网线.SetActive(false);
            机箱电源线.SetActive(false);

        }

        public void 设备拆除()
        {
            ClearBagAction?.Invoke();
            cam.transform.position = cameraTrans.position;
            cam.transform.rotation = cameraTrans.rotation;
            Switch机箱盖(false, true);
            Switch硬盘(false, true);
            Switch显卡(false, true);
            Switch电源(false, true);
            Switch主板(false, true);
            Switch内存(false, true);
            Switch散热器(false, true);
            SwitchCPU(false, true);

            Switch机箱盖(true, false, () =>
            {
                AddBagAction?.Invoke("机箱盖", true);
                Switch硬盘(true, false, () =>
                {
                    AddBagAction?.Invoke("硬盘", true);
                    Switch显卡(true, false, () =>
                    {
                        AddBagAction?.Invoke("显卡", true);
                        Switch电源(true, false, () =>
                        {
                            AddBagAction?.Invoke("电源", true);
                            Switch主板(true, false, () =>
                            {
                                Switch内存(true, false, () =>
                                {
                                    AddBagAction?.Invoke("内存", true);
                                    Switch散热器(true, false, () =>
                                    {
                                        AddBagAction?.Invoke("散热器", true);
                                        SwitchCPU(true, false, () =>
                                        {
                                            AddBagAction?.Invoke("CPU", true);
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
            cam.transform.position = 安装CPUCameraPoint.position;
            cam.transform.rotation = 安装CPUCameraPoint.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(true, true);
            Switch显卡(true, true);
            Switch电源(true, true);
            Switch主板(true, true);
            Switch内存(true, true);
            Switch散热器(true, true);
            SwitchCPU(true, true);
            shouldClickCPU外壳Open = true;
        }

        public void 安装散热器()
        {
            cam.transform.position = 安装CPUCameraPoint.position;
            cam.transform.rotation = 安装CPUCameraPoint.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(true, true);
            Switch显卡(true, true);
            Switch电源(true, true);
            Switch主板(true, true);
            Switch内存(true, true);
            Switch散热器(true, true);
            SwitchCPU(false, true);
            shouldClickCPU硅脂注射器 = true;
        }
        public void 安装内存()
        {
            cam.transform.position = 安装CPUCameraPoint.position;
            cam.transform.rotation = 安装CPUCameraPoint.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(true, true);
            Switch显卡(true, true);
            Switch电源(true, true);
            Switch主板(true, true);
            Switch内存(true, true);
            Switch散热器(false, true);
            SwitchCPU(false, true);

        }

        public void 安装主板()
        {
            cam.transform.position = 安装CPUCameraPoint.position;
            cam.transform.rotation = 安装CPUCameraPoint.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(true, true);
            Switch显卡(true, true);
            Switch电源(true, true);
            Switch主板(true, true);
            Switch内存(false, true);
            Switch散热器(false, true);
            SwitchCPU(false, true);

        }

        public void 安装电源()
        {
            cam.transform.position = 安装CPUCameraPoint.position;
            cam.transform.rotation = 安装CPUCameraPoint.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(true, true);
            Switch显卡(true, true);
            Switch电源(true, true);
            Switch主板(false, true);
            Switch内存(false, true);
            Switch散热器(false, true);
            SwitchCPU(false, true);

        }

        public void 安装显卡()
        {
            cam.transform.position = 安装CPUCameraPoint.position;
            cam.transform.rotation = 安装CPUCameraPoint.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(true, true);
            Switch显卡(true, true);
            Switch电源(false, true);
            Switch主板(false, true);
            Switch内存(false, true);
            Switch散热器(false, true);
            SwitchCPU(false, true);
        }
        public void 安装硬盘()
        {
            cam.transform.position = 安装CPUCameraPoint.position;
            cam.transform.rotation = 安装CPUCameraPoint.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(true, true);
            Switch显卡(false, true);
            Switch电源(false, true);
            Switch主板(false, true);
            Switch内存(false, true);
            Switch散热器(false, true);
            SwitchCPU(false, true);
        }

        public void 安装机箱盖()
        {
            cam.transform.position = 安装CPUCameraPoint.position;
            cam.transform.rotation = 安装CPUCameraPoint.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(false, true);
            Switch显卡(false, true);
            Switch电源(false, true);
            Switch主板(false, true);
            Switch内存(false, true);
            Switch散热器(false, true);
            SwitchCPU(false, true);
            机箱盖插槽.SetActive(true);
        }

        public void ClickUIAction(string uiName)
        {
            switch (uiName)
            {
                case "CPU":
                    shouldClickCPU插槽 = true;
                    break;
                case "散热器":
                    shouldClickCPU散热器 = true;
                    break;
                case "内存":
                    shouldClick内存插槽 = true;
                    break;
                case "电源":
                    shouldClick电源插槽 = true;
                    break;
                case "显卡":
                    shouldClick显卡插槽 = true;
                    break;
                case "硬盘":
                    shouldClick硬盘插槽 = true;
                    break;
                case "机箱盖":
                    shouldClick机箱盖插槽 = true;
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
                机箱盖.DOLocalMove(endPos, 0.3f).OnComplete(() => { FinishAction?.Invoke(); });
        }
        public void Switch硬盘(bool open, bool rightNow, Action FinishAction = null)
        {
            硬盘.DOKill();
            Vector3 endPos;
            if (open)
                endPos = new Vector3(1, -0.002705038f, 0.03184646f);
            else
                endPos = new Vector3(0.1333502f, -0.002705038f, 0.03184646f);
            if (rightNow)
                硬盘.localPosition = endPos;
            else
                硬盘.DOLocalMove(endPos, 0.3f).OnComplete(() => { FinishAction?.Invoke(); });
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
                显卡.DOLocalMove(endPos, 0.3f).OnComplete(() => { FinishAction?.Invoke(); });
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
                电源.DOLocalMove(endPos, 0.3f).OnComplete(() => { FinishAction?.Invoke(); });
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
                    主板.DOLocalMove(new Vector3(0.369f, 0.1349312f, -0.03486863f), 0.3f).OnComplete(() =>
                    {
                        主板.DOLocalMove(new Vector3(0.369f, 0.135f, 0.575f), 0.3f).OnComplete(() =>
                        {
                            主板.DOLocalMove(new Vector3(0.117f, 0.135f, 0.575f), 0.3f).OnComplete(() => { FinishAction?.Invoke(); });
                        });
                    });
                }
                else
                {
                    主板.localPosition = new Vector3(0.117f, 0.135f, 0.575f);
                    主板.DOLocalMove(new Vector3(0.369f, 0.135f, 0.575f), 0.3f).OnComplete(() =>
                    {
                        主板.DOLocalMove(new Vector3(0.369f, 0.1349312f, -0.03486863f), 0.3f).OnComplete(() =>
                        {
                            主板.DOLocalMove(new Vector3(0.1588569f, 0.1349312f, -0.03486863f), 0.3f).OnComplete(() => { FinishAction?.Invoke(); });
                        });
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
                内存.DOLocalMove(endPos, 0.3f).OnComplete(() => { FinishAction?.Invoke(); });
        }

        public void Switch散热器(bool open, bool rightNow, Action FinishAction = null)
        {
            散热器.DOKill();
            Vector3 endPos;
            if (open)
                endPos = new Vector3(0.1f, 0.009811332f, -0.01176217f);
            else
                endPos = new Vector3(-0.0007274255f, 0.009811332f, -0.01176217f);
            if (rightNow)
                散热器.localPosition = endPos;
            else
                散热器.DOLocalMove(endPos, 0.3f).OnComplete(() => { FinishAction?.Invoke(); });
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
                if (open)
                {
                    CPU.localPosition = new Vector3(0.07f, 0.009603716f, -0.01180644f);
                    CPU外壳.localEulerAngles = new Vector3(0, 0, 140);
                    CPU硅脂.localScale = Vector3.zero;
                }
                else
                {
                    CPU.localPosition = new Vector3(-0.0002732739f, 0.009603716f, -0.01180644f);
                    CPU外壳.localEulerAngles = new Vector3(0, 0, 0);
                    CPU硅脂.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                }

            }
            else
            {
                if (open)
                {
                    CPU.localPosition = new Vector3(-0.0002732739f, 0.009603716f, -0.01180644f);
                    CPU外壳.localEulerAngles = new Vector3(0, 0, 0);
                    CPU硅脂.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    CPU外壳.DOLocalRotate(new Vector3(0, 0, 140), 0.3f).OnComplete(() =>
                    {
                        CPU硅脂.DOScale(Vector3.zero, 0.3f).OnComplete(() =>
                        {
                            CPU.DOLocalMove(new Vector3(0.07f, 0.009603716f, -0.01180644f), 0.3f).OnComplete(() => { FinishAction?.Invoke(); });
                        });
                    });

                }
                else
                {
                    CPU.localPosition = new Vector3(0.07f, 0.009603716f, -0.01180644f);
                    CPU外壳.localEulerAngles = new Vector3(0, 0, 140);
                    CPU硅脂.localScale = Vector3.zero;
                    CPU.DOLocalMove(new Vector3(-0.0002732739f, 0.009603716f, -0.01180644f), 0.3f).OnComplete(() =>
                    {
                        CPU硅脂注射器.transform.localPosition = new Vector3(-0.0162f, 0.1344f, 0.0137f);
                        CPU硅脂注射器.transform.localEulerAngles = new Vector3(0, -50, 90);
                        CPU硅脂.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.3f).OnComplete(() =>
                        {
                            CPU硅脂注射器.transform.localPosition = new Vector3(-0.139f, -0.062f, 0.37f);
                            CPU硅脂注射器.transform.localEulerAngles = Vector3.zero;

                            CPU外壳.DOLocalRotate(new Vector3(0, 0, 0), 0.3f).OnComplete(() => { FinishAction?.Invoke(); });
                        });
                    });
                }
            }


        }

        public void SwitchCPU外壳(bool open, bool rightNow, Action FinishAction = null)
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
                CPU外壳.DOLocalRotate(endRot, 0.3f).OnComplete(() =>
                {
                    FinishAction?.Invoke();
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
                    CPU硅脂注射器.transform.localPosition = new Vector3(-0.0162f, 0.1344f, 0.0137f);
                    CPU硅脂注射器.transform.localEulerAngles = new Vector3(0, -50, 90);
                    CPU硅脂.DOScale(endScale, 0.3f).OnComplete(() =>
                    {
                        CPU硅脂注射器.transform.localPosition = new Vector3(-0.139f, -0.062f, 0.37f);
                        CPU硅脂注射器.transform.localEulerAngles = Vector3.zero;
                        FinishAction?.Invoke();
                    });
                }
                else
                {
                    CPU硅脂.DOScale(endScale, 0.3f).OnComplete(() =>
                    {
                        FinishAction?.Invoke();
                    });
                }
            }
        }


    }
}