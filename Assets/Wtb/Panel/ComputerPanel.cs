using UnityEngine.UI;

namespace Tianbo.Wang
{
    public class ComputerPanel : Wtb_PanelBase
    {
        public Button[] btns;

        protected override void Awake()
        {
            base.Awake();
            SceneGoManager.Instance.SetStepAction += SetStepAction;

            for (int i = 0; i < btns.Length; i++)
            {
                int tempIndex = i;
                BtnHoverBind(btns[i].gameObject);

                btns[i].onClick.AddListener(() =>
                {
                    BtnClickAction(tempIndex);
                });
            }
            SceneGoManager.Instance.SwitctToComputer();
            BtnClickAction(0);
        }

        private void SetStepAction(string stepName)
        {
            for (int i = 0; i < btns.Length; i++)
            {
                if (btns[i].GetComponentInChildren<Text>().text == stepName)
                {
                    btns[i].transform.Find("SelectImage").GetComponent<Image>().enabled = true;
                }
                else
                {
                    btns[i].transform.Find("SelectImage").GetComponent<Image>().enabled = false;
                }
            }
        }

        public void BtnClickAction(int tempIndex)
        {
            if (tempIndex != 0 && !GameManager.Instance.isStudyMode)
            {
                return;
            }
            switch (tempIndex)
            {
                case 0:
                    SceneGoManager.Instance.连接显示器1();
                    break;
                case 1:
                    SceneGoManager.Instance.连接电源1();
                    break;
                case 2:
                    SceneGoManager.Instance.连接键盘1();
                    break;
                case 3:
                    SceneGoManager.Instance.连接鼠标1();
                    break;
                case 4:
                    SceneGoManager.Instance.连接网线1();
                    break;
                case 5:
                    SceneGoManager.Instance.连接音响1();
                    break;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            for (int i = 0; i < btns.Length; i++)
            {
                BtnHoverUnBind(btns[i].gameObject);
            }

            SceneGoManager.Instance.SetStepAction -= SetStepAction;
        }

    }
}