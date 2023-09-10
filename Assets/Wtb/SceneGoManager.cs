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


        public GameObject 显示器线;
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

        void Init()
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
                        if (hit.transform == CPU外壳)
                        {
                            shouldClickCPU外壳Open = false;
                            SwitchCPU外壳(true, false, () =>
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
                    //插上CPU
                    if (shouldClickCPU插槽)
                    {
                        if (hit.transform.gameObject == CPU插槽)
                        {
                            AddBagAction?.Invoke("CPU", false);
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
                    //关闭CPU盖板
                    if (shouldClickCPU外壳Close)
                    {
                        if (hit.transform == CPU外壳)
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
                        Debug.Log(hit.transform);
                        if (hit.transform == CPU硅脂注射器)
                        {
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
                    //安装散热器
                    if (shouldClickCPU散热器)
                    {
                        if (hit.transform == CPU外壳)
                        {
                            AddBagAction?.Invoke("散热器", false);
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
                    #endregion

                    #region 内存
                    if (shouldClick内存插槽)
                    {
                        if (hit.transform.gameObject == 内存条插槽)
                        {
                            AddBagAction?.Invoke("内存", false);
                            shouldClick内存插槽 = false;
                            Switch内存(false, false, () =>
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
                        Debug.Log(hit.transform);
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

                    if (shouldClick主板插槽)
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

                    #endregion

                    #region 电源
                    if (shouldClick电源插槽)
                    {
                        if (hit.transform.gameObject == 电源插槽)
                        {
                            AddBagAction?.Invoke("电源", false);
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
                    #endregion
                    #region 显卡
                    if (shouldClick显卡插槽)
                    {
                        if (hit.transform.gameObject == 显卡插槽)
                        {
                            AddBagAction?.Invoke("显卡", false);
                            shouldClick显卡插槽 = false;
                            Switch显卡(false, false, () =>
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
                            Switch硬盘(false, false, () =>
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
                    #endregion

                    #region 第二模块
                    if (shouldConnect显示器1)
                    {
                        if (hit.transform.gameObject == DragPoint_显示器HDMI)
                        {
                            shouldConnect显示器1 = false;
                            连接显示器2();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                    if (shouldConnect显示器2)
                    {
                        Debug.Log(hit.transform.gameObject);
                        if (hit.transform.gameObject == DragPoint_机箱HDMI)
                        {
                            shouldConnect显示器2 = false;
                            显示器线.SetActive(true);
                            连接电源1();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                    if (shouldConnect电源1)
                    {
                        if (hit.transform.gameObject == DragPoint_机箱电源插孔)
                        {
                            shouldConnect电源1 = false;
                            连接电源2();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                    if (shouldConnect电源2)
                    {
                        if (hit.transform.gameObject == DragPoint_插排电源插座)
                        {
                            shouldConnect电源2 = false;
                            机箱电源线.SetActive(true);
                            连接键盘1();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                    if (shouldConnect键盘1)
                    {
                        if (hit.transform.gameObject == DragPoint_键盘)
                        {
                            shouldConnect键盘1 = false;
                            连接键盘2();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                    if (shouldConnect键盘2)
                    {
                        if (hit.transform.gameObject == DragPoint_机箱USB)
                        {
                            shouldConnect键盘2 = false;
                            键盘线.SetActive(true);
                            连接鼠标1();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                    if (shouldConnect鼠标1)
                    {
                        if (hit.transform.gameObject == DragPoint_鼠标)
                        {
                            shouldConnect鼠标1 = false;
                            连接鼠标2();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                    if (shouldConnect鼠标2)
                    {
                        if (hit.transform.gameObject == DragPoint_机箱USB)
                        {
                            shouldConnect鼠标2 = false;
                            鼠标线.SetActive(true);
                            连接网线1();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                    if (shouldConnect网线1)
                    {
                        if (hit.transform.gameObject == DragPoint_机箱网线插孔)
                        {
                            shouldConnect网线1 = false;
                            连接网线2();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                    if (shouldConnect网线2)
                    {
                        if (hit.transform.gameObject == DragPoint_插排网线插座)
                        {
                            shouldConnect网线2 = false;
                            网线.SetActive(true);
                            连接音响1();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                    if (shouldConnect音响1)
                    {
                        if (hit.transform.gameObject == DragPoint_音响)
                        {
                            shouldConnect音响1 = false;
                            连接音响2();
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }
                    if (shouldConnect音响2)
                    {
                        if (hit.transform.gameObject == DragPoint_机箱USB)
                        {
                            音响线.SetActive(true);
                            shouldConnect音响2 = false;
                            Wtb_TipsDialog wtb_TipsDialog = (Wtb_TipsDialog)Wtb_DialogManager.Instance.OpenDialog(Wtb_DialogType.Wtb_TipsDialog);
                            wtb_TipsDialog.SetTipsInfo("完成当前模块", () =>
                            {
                                Wtb_PanelManager.Instance.BackToPanel(PanelType.SelectModePanel);
                            });
                        }
                        else
                        {
                            //错误提示
                            WrongTips();
                        }
                    }

                    #endregion
                }
            }

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

            显示器线.SetActive(false);
            音响线.SetActive(false);
            键盘线.SetActive(false);
            鼠标线.SetActive(false);
            网线.SetActive(false);
            机箱电源线.SetActive(false);
            InitSecond();

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
            SwitchCPU外壳(false, true);
            SwitchCPU硅脂(false, true);

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
                                        SwitchCPU硅脂(false, false, () =>
                                        {
                                            SwitchCPU外壳(true, false, () =>
                                            {
                                                SwitchCPU(true, false, () =>
                                                {
                                                    AddBagAction?.Invoke("CPU", true);
                                                    SwitchCPU外壳(false, false, () =>
                                                    {
                                                        Debug.Log("第一步动画完成");
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
            ClearBagAction?.Invoke();
            AddBagAction?.Invoke("机箱盖", true);
            AddBagAction?.Invoke("硬盘", true);
            AddBagAction?.Invoke("显卡", true);
            AddBagAction?.Invoke("电源", true);
            AddBagAction?.Invoke("内存", true);
            AddBagAction?.Invoke("散热器", true);
            AddBagAction?.Invoke("CPU", true);
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
            SwitchCPU外壳(false, true);
            SwitchCPU硅脂(false, true);
            Init();
            shouldClickCPU外壳Open = true;
        }

        public void 安装散热器()
        {
            ClearBagAction?.Invoke();
            AddBagAction?.Invoke("机箱盖", true);
            AddBagAction?.Invoke("硬盘", true);
            AddBagAction?.Invoke("显卡", true);
            AddBagAction?.Invoke("电源", true);
            AddBagAction?.Invoke("内存", true);
            AddBagAction?.Invoke("散热器", true);
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
            SwitchCPU外壳(false, true);
            SwitchCPU硅脂(false, true);
            Init();
            shouldClickCPU硅脂注射器 = true;
        }
        public void 安装内存()
        {
            ClearBagAction?.Invoke();
            AddBagAction?.Invoke("机箱盖", true);
            AddBagAction?.Invoke("硬盘", true);
            AddBagAction?.Invoke("显卡", true);
            AddBagAction?.Invoke("电源", true);
            AddBagAction?.Invoke("内存", true);
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
            SwitchCPU外壳(false, true);
            SwitchCPU硅脂(true, true);
            Init();
            shouldClick内存UI = true;
        }

        public void 安装主板()
        {
            ClearBagAction?.Invoke();
            AddBagAction?.Invoke("机箱盖", true);
            AddBagAction?.Invoke("硬盘", true);
            AddBagAction?.Invoke("显卡", true);
            AddBagAction?.Invoke("电源", true);
            cam.transform.position = cameraTrans.position;
            cam.transform.rotation = cameraTrans.rotation;
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
            ClearBagAction?.Invoke();
            AddBagAction?.Invoke("机箱盖", true);
            AddBagAction?.Invoke("硬盘", true);
            AddBagAction?.Invoke("显卡", true);
            AddBagAction?.Invoke("电源", true);
            cam.transform.position = cameraTrans.position;
            cam.transform.rotation = cameraTrans.rotation;
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
            ClearBagAction?.Invoke();
            AddBagAction?.Invoke("机箱盖", true);
            AddBagAction?.Invoke("硬盘", true);
            AddBagAction?.Invoke("显卡", true);
            cam.transform.position = cameraTrans.position;
            cam.transform.rotation = cameraTrans.rotation;
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
        public void 安装硬盘()
        {
            ClearBagAction?.Invoke();
            AddBagAction?.Invoke("机箱盖", true);
            AddBagAction?.Invoke("硬盘", true);
            cam.transform.position = cameraTrans.position;
            cam.transform.rotation = cameraTrans.rotation;
            Switch机箱盖(true, true);
            Switch硬盘(true, true);
            Switch显卡(false, true);
            Switch电源(false, true);
            Switch主板(false, true);
            Switch内存(false, true);
            Switch散热器(false, true);
            SwitchCPU(false, true);
            SwitchCPU外壳(false, true);
            SwitchCPU硅脂(true, true);
            Init();
            shouldClick硬盘UI = true;
        }

        public void 安装机箱盖()
        {
            ClearBagAction?.Invoke();
            AddBagAction?.Invoke("机箱盖", true);
            cam.transform.position = cameraTrans.position;
            cam.transform.rotation = cameraTrans.rotation;
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
                硬盘.DOLocalMove(endPos, animTime).OnComplete(() => { FinishAction?.Invoke(); });
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
                CPU外壳.DOLocalRotate(endRot, animTime).OnComplete(() =>
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
        void InitSecond()
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
        public void 连接显示器1()
        {
            cam.transform.position = 显示器CamTrans.position;
            cam.transform.rotation = 显示器CamTrans.rotation;
            InitSecond();
            shouldConnect显示器1 = true;
        }
        public void 连接显示器2()
        {
            cam.transform.position = 机箱CamTrans.position;
            cam.transform.rotation = 机箱CamTrans.rotation;
            InitSecond();
            shouldConnect显示器2 = true;
        }
        public void 连接电源1()
        {
            cam.transform.position = 机箱CamTrans.position;
            cam.transform.rotation = 机箱CamTrans.rotation;
            InitSecond();
            shouldConnect电源1 = true;
        }
        public void 连接电源2()
        {
            cam.transform.position = 网线CamTrans.position;
            cam.transform.rotation = 网线CamTrans.rotation;
            InitSecond();
            shouldConnect电源2 = true;
        }

        public void 连接键盘1()
        {
            cam.transform.position = 键盘鼠标CamTrans.position;
            cam.transform.rotation = 键盘鼠标CamTrans.rotation;
            shouldConnect键盘1 = true;
        }
        public void 连接键盘2()
        {
            cam.transform.position = 机箱CamTrans.position;
            cam.transform.rotation = 机箱CamTrans.rotation;
            InitSecond();
            shouldConnect键盘2 = true;
        }
        public void 连接鼠标1()
        {
            cam.transform.position = 键盘鼠标CamTrans.position;
            cam.transform.rotation = 键盘鼠标CamTrans.rotation;
            InitSecond();
            shouldConnect鼠标1 = true;
        }
        public void 连接鼠标2()
        {
            cam.transform.position = 机箱CamTrans.position;
            cam.transform.rotation = 机箱CamTrans.rotation;
            InitSecond();
            shouldConnect鼠标2 = true;
        }
        public void 连接网线1()
        {
            cam.transform.position = 机箱CamTrans.position;
            cam.transform.rotation = 机箱CamTrans.rotation;
            InitSecond();
            shouldConnect网线1 = true;
        }
        public void 连接网线2()
        {
            cam.transform.position = 网线CamTrans.position;
            cam.transform.rotation = 网线CamTrans.rotation;
            InitSecond();
            shouldConnect网线2 = true;
        }
        public void 连接音响1()
        {
            cam.transform.position = 音响CamTrans.position;
            cam.transform.rotation = 音响CamTrans.rotation;
            InitSecond();
            shouldConnect音响1 = true;
        }
        public void 连接音响2()
        {
            cam.transform.position = 机箱CamTrans.position;
            cam.transform.rotation = 机箱CamTrans.rotation;
            InitSecond();
            shouldConnect音响2 = true;
        }
        #endregion


    }
}