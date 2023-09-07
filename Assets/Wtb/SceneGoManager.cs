using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Tianbo.Wang
{
    public class SceneGoManager : Wtb_SingleMono<SceneGoManager>
    {
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

        public Transform cameraTrans;
        public Transform 显示器CamTrans;
        public Transform 机箱CamTrans;
        public Transform 键盘鼠标CamTrans;
        public Transform 网线CamTrans;
        public Transform 音响CamTrans;
        public void SwitctToHost()
        {
            host.SetActive(true);
            computer.SetActive(false);
        }
        public void SwitctToComputer()
        {
            host.SetActive(false);
            computer.SetActive(true);
        }

        public void Switch机箱盖(bool open)
        {
            if (open)
            {
                机箱盖.DOLocalMove(new Vector3(0.03f, -0.000212553f, -0.01207775f), 0.3f);
            }
            else
            {
                机箱盖.DOLocalMove(new Vector3(0.009559387f, -0.000212553f, -0.01207775f), 0.3f);
            }
        }
        public void Switch硬盘(bool open)
        {
            if (open)
            {
                硬盘.DOLocalMove(new Vector3(0.549f, -0.002705038f, 0.03184646f), 0.3f);
            }
            else
            {
                硬盘.DOLocalMove(new Vector3(0.1333502f, -0.002705038f, 0.03184646f), 0.3f);
            }
        }

        public void Switch显卡(bool open)
        {
            if (open)
            {
                显卡.DOLocalMove(new Vector3(0.07f, 0.0004668936f, -0.01119314f), 0.3f);
            }
            else
            {
                显卡.DOLocalMove(new Vector3(0.004904306f, 0.0004668936f, -0.01119314f), 0.3f);
            }
        }

        public void Switch电源(bool open)
        {
            if (open)
            {
                电源.DOLocalMove(new Vector3(0.51f, 0.006446421f, -0.2157426f), 0.3f);
            }
            else
            {
                电源.DOLocalMove(new Vector3(0.1887167f, 0.006446421f, -0.2157426f), 0.3f);
            }
        }
        public void Switch主板(bool open)
        {
            if (open)
            {
                主板.localPosition = new Vector3(0.1588569f, 0.1349312f, -0.03486863f);
                主板.DOLocalMove(new Vector3(0.369f, 0.1349312f, -0.03486863f), 0.3f).OnComplete(() =>
                {
                    主板.DOLocalMove(new Vector3(0.369f, 0.135f, 0.575f), 0.3f).OnComplete(() =>
                    {
                        主板.DOLocalMove(new Vector3(0.117f, 0.135f, 0.575f), 0.3f);
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
                        主板.DOLocalMove(new Vector3(0.1588569f, 0.1349312f, -0.03486863f), 0.3f);
                    });
                });
            }
        }

        public void Switch内存(bool open)
        {
            if (open)
            {
                内存.DOLocalMove(new Vector3(0.07f, 0.009921147f, -0.003622067f), 0.3f);
            }
            else
            {
                内存.DOLocalMove(new Vector3(0.0006704569f, 0.009921147f, -0.003622067f), 0.3f);
            }
        }

        public void Switch散热器(bool open)
        {
            if (open)
            {
                散热器.DOLocalMove(new Vector3(0.07f, 0.009811332f, -0.01176217f), 0.3f);
            }
            else
            {
                散热器.DOLocalMove(new Vector3(-0.0007274255f, 0.009811332f, -0.01176217f), 0.3f);
            }
        }

        public void SwitchCPU(bool open)
        {
            if (open)
            {
                CPU.localPosition = new Vector3(-0.0002732739f, 0.009603716f, -0.01180644f);
                CPU外壳.localEulerAngles = new Vector3(0, 0, 0);
                CPU硅脂.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                CPU外壳.DOLocalRotate(new Vector3(0, 0, 140), 0.3f).OnComplete(() =>
                {
                    CPU硅脂.DOLocalMove(Vector3.zero, 0.3f).OnComplete(() =>
                    {
                        CPU.DOLocalMove(new Vector3(0.07f, 0.009603716f, -0.01180644f), 0.3f);
                    });
                });

            }
            else
            {
                CPU.DOLocalMove(new Vector3(-0.0002732739f, 0.009603716f, -0.01180644f), 0.3f).OnComplete(() =>
                {
                    CPU硅脂注射器.transform.localPosition = new Vector3(-0.0162f, 0.1344f, 0.0137f);
                    CPU硅脂注射器.transform.localEulerAngles = new Vector3(0, -50, 90);
                    CPU硅脂.DOLocalMove(new Vector3(0.1f, 0.1f, 0.1f), 0.3f).OnComplete(() =>
                    {
                        CPU硅脂注射器.transform.localPosition = new Vector3(-0.139f, -0.062f, 0.37f);
                        CPU硅脂注射器.transform.localEulerAngles = Vector3.zero;

                        CPU外壳.DOLocalRotate(new Vector3(0, 0, 0), 0.3f);
                    });
                });
            }
        }
    }
}