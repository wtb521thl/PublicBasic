using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace Tianbo.Wang
{
    public class FadeInOutManager : Wtb_SingleMono<FadeInOutManager>
    {
        public Image fadeInOutImage;

        Tween fadeInTween;
        Tween fadeOutTween;

        void InitImage()
        {
            if (fadeInOutImage == null)
            {
                fadeInOutImage = GameObject.Find("FadeInOrOut").GetComponent<Image>();
            }
        }

        public void FadeOut(Action FinishAction, float time = 1f)
        {
            InitImage();
            if (fadeInTween != null)
            {
                fadeInTween.Kill();
            }
            if (fadeOutTween != null)
            {
                fadeOutTween.Kill();
            }
            fadeOutTween = fadeInOutImage.DOColor(new Color(0, 0, 0, 0), 1f).OnComplete(() =>
            {
                FinishAction?.Invoke();
            });
        }

        public void FadeIn(Action FinishAction, float time = 0.5f)
        {
            InitImage();
            if (fadeInTween != null)
            {
                fadeInTween.Kill();
            }
            if (fadeOutTween != null)
            {
                fadeOutTween.Kill();
            }
            fadeInTween = fadeInOutImage.DOColor(new Color(0, 0, 0, 1), 0.5f).OnComplete(() =>
            {
                FinishAction?.Invoke();
            });
        }
    }
}