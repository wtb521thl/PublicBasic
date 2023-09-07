using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WisdomTree.Common.Function;

namespace Tianbo.Wang
{
    public class GameManager : Wtb_SingleMono<GameManager>
    {

        bool isUpload = false;


        DateTime startTime;

        List<Score> scoresList = new List<Score>();


        public bool isStudyMode;

        void Awake()
        {
            ProjectDatas.Init();
        }

        void Start()
        {
            Wtb_PanelManager.Instance.OpenPanel(PanelType.MainPanel);
            startTime = DateTime.Now;
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }

        public string GetTotleTIme()
        {
            DateTime curTime = DateTime.Now;
            TimeSpan curSpan = new TimeSpan(curTime.Ticks);
            TimeSpan startSpan = new TimeSpan(startTime.Ticks);

            double offsetSecond = curSpan.Subtract(startSpan).TotalSeconds;
            return FormatSToHMS(offsetSecond);
        }

        //把秒变成时:分:秒
        public static string FormatSToHMS(double _time)
        {
            double _hour = _time / 3600;
            double _min = 0;
            double _sec = 0;
            if (_hour > 0)
            {
                _min = (_time % 3600) / 60;
                _sec = _min > 0 ? (_time % 3600) % 60 : _time % 3600;
            }
            else
            {
                _min = _time / 60;
                _sec = _min > 0 ? _time % 60 : _time;
            }

            return _hour > 0 ? string.Format("{0:00}:{1:00}:{2:00}", _hour, _min, _sec) : string.Format("{0:00}:{1:00}", _min, _sec);
        }



        public int GetMouduleScore(string module)
        {
            int score = 0;
            for (int i = 0; i < scoresList.Count; i++)
            {
                Debug.Log("aaaaaaaaaaaaaaaaaa=" + scoresList[i].module);
                if (scoresList[i].module == module)
                {
                    Debug.Log(module + "===" + scoresList[i].curValue);
                    score += scoresList[i].curValue;
                }
            }
            return score;
        }
        public void AddToList(string module, string itemKey, int itemScore, string correctAnswer, string curAnswer)
        {
            Score find = scoresList.Find((p) => { return p.module == module && p.scoreKey == itemKey; });
            if (find == null)
                scoresList.Add(new Score(module, itemKey, itemScore, correctAnswer, curAnswer));
            else
            {
                find.curValue = itemScore;
            }
        }

        public void CheckFinish()
        {
            if (isUpload)
            {
                return;
            }

        }
    }

    public class Score
    {
        public string correctAnswer;
        public string curAnswer;
        public string module;
        public string scoreKey;
        public int curValue;
        public Score(string module, string scoreKey, int curValue, string correctAnswer, string curAnswer)
        {
            this.module = module;
            this.scoreKey = scoreKey;
            this.curValue = curValue;
            this.correctAnswer = correctAnswer;
            this.curAnswer = curAnswer;
        }
    }

}
