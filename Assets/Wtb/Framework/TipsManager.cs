using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace Tianbo.Wang
{
    public class TipsManager : Wtb_SingleMono<TipsManager>
    {
        GameObject tipsPrefab;
        public void ShowTips(string textStr,Transform tempParent)
        {
            if (tipsPrefab == null)
            {
                tipsPrefab = Resources.Load<GameObject>("Prefabs/TextTips");
            }
            GameObject tempTipsGo = Instantiate(tipsPrefab, tempParent);
            RectTransform tempRectTrans = tempTipsGo.GetComponent<RectTransform>();
            tempRectTrans.anchoredPosition = Vector3.zero;
            tempTipsGo.GetComponentInChildren<Text>().text = textStr;

            tempRectTrans.DOLocalMoveY(tempRectTrans.localPosition.y - 100, 0.3f).OnStart(() =>
            {
                tempTipsGo.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
            }).OnComplete(() =>
            {
                Wait(2, () =>
                {
                    tempRectTrans.DOLocalMoveY(tempRectTrans.localPosition.y + 100, 0.3f).OnStart(() =>
                     {
                         tempTipsGo.GetComponent<CanvasGroup>().DOFade(0, 0.3f);
                     }).OnComplete(() =>
                     {
                         DestroyImmediate(tempTipsGo);
                     });
                });
            });
        }

        void Wait(float seconds, Action FinishAction)
        {
            StartCoroutine(WaitSeconds(seconds, FinishAction));
        }

        IEnumerator WaitSeconds(float seconds, Action FinishAction)
        {
            yield return new WaitForSeconds(seconds);
            FinishAction?.Invoke();
        }

    }


}