/*******************************************************************************
* 版本声明：v1.0.0
* 类 名 称：ChineseInputWebGL
* 创建日期：2020-03-16 15:15:20
* 作者名称：
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：  
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

namespace WisdomTree.Common.Function
{
    /// <summary>
    /// WebGL InputField Js 引用
    /// </summary>
    public static class ChineseInputWebGL
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    public static extern void InputShow(string GameObjectName, string InputID, string text, string fontsize, string indexStr, string inputRectStr,bool isMultiNewLine);  
#else
        public static void InputShow(string GameObjectName, string InputID, string text, string fontsize, string indexStr, string inputRectStr, bool isMultiNewLine) { }
#endif

    }
}

