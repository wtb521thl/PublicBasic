using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tianbo.Wang
{
    public enum Wtb_DialogType
    {
        Wtb_TipsDialog,
    }

    /// <summary>
    /// 弹窗管理类
    /// </summary>
    public class Wtb_DialogManager : Wtb_SingleMono<Wtb_DialogManager>
    {
        public const string ResourceDialogPath = "Prefabs/Dialogs/";

        public Transform dialogPanel;

        Stack<Wtb_DialogBase> openingDialogs = new Stack<Wtb_DialogBase>();

        private void Awake()
        {
            dialogPanel = GameObject.Find("DialogParent").transform;
        }

        public Wtb_DialogBase OpenDialog(Wtb_DialogType dialogType, string source = "", bool canMultiplyOpen = false)
        {
            if (!canMultiplyOpen && openingDialogs.Count > 0 && openingDialogs.Peek().name == dialogType.ToString())
            {
                return null;
            }
            GameObject dialogObj = Instantiate(Resources.Load<GameObject>(ResourceDialogPath + dialogType.ToString()), dialogPanel);
            dialogObj.name = dialogType.ToString();
            Wtb_DialogBase tempDialog = dialogObj.GetComponent<Wtb_DialogBase>();
            tempDialog.source = source;
            tempDialog.canOpenMultiply = canMultiplyOpen;
            openingDialogs.Push(tempDialog);
            Wtb_EventCenter.BroadcastEvent<string, bool>(Wtb_EventSendType.DialogChangeStateAction, dialogObj.name, true);
            return tempDialog;
        }

        public bool DialogIsOpening(Wtb_DialogType dialogType)
        {
            if (openingDialogs.Count > 0)
            {
                Wtb_DialogBase[] dialogBases = openingDialogs.ToArray();
                for (int i = 0; i < dialogBases.Length; i++)
                {
                    if (dialogBases[i].name == dialogType.ToString())
                    {
                        return true;
                    }
                }
                return true;
            }
            return false;
        }

        public void CloseDialog()
        {
            if (openingDialogs.Count != 0)
            {
                GameObject tempDesObj = openingDialogs.Pop().gameObject;
                if (tempDesObj != null)
                {
                    Wtb_EventCenter.BroadcastEvent<string, bool>(Wtb_EventSendType.DialogChangeStateAction, tempDesObj.name, false);
                    DestroyImmediate(tempDesObj);
                }

            }
        }

        List<Wtb_DialogBase> tempObjs = new List<Wtb_DialogBase>();
        public void CloseDialog(Wtb_DialogType dialogType)
        {

            tempObjs.Clear();

            for (int i = 0; i < openingDialogs.Count; i++)
            {
                tempObjs.Add(openingDialogs.Pop());
            }

            for (int i = 0; i < tempObjs.Count; i++)
            {
                if (tempObjs[i].name == dialogType.ToString())
                {
                    Wtb_EventCenter.BroadcastEvent<string, bool>(Wtb_EventSendType.DialogChangeStateAction, dialogType.ToString(), false);
                    DestroyImmediate(tempObjs[i].gameObject);
                    tempObjs.RemoveAt(i);
                    break;
                }
            }

            for (int i = tempObjs.Count - 1; i >= 0; i--)
            {
                openingDialogs.Push(tempObjs[i]);
            }


        }

    }



}