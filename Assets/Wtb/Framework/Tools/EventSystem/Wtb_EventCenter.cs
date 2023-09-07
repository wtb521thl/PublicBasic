// *************************************************************************************************************
// 创建者: 王天博
// 创建时间: 2019/09/17 16:07:07
// 功能: 
// 版 本：v 1.2.0
// *************************************************************************************************************


using System;
using System.Collections.Generic;
using UnityEngine;

public class Wtb_EventCenter
{
    public static Dictionary<Wtb_EventSendType, Delegate> allEvents = new Dictionary<Wtb_EventSendType, Delegate>();


    public static void AddListener(Wtb_EventSendType eventType, CallBackClass.Callback callBack)
    {

        if (allEvents.ContainsKey(eventType))
        {
            //Debug.Log("事件已经存在→" + eventType.ToString());
        }
        else
        {
            allEvents.Add(eventType, null);
        }
        allEvents[eventType] = (CallBackClass.Callback)allEvents[eventType] + callBack;
    }
    public static void AddListener<T>(Wtb_EventSendType eventType, CallBackClass.Callback<T> callBack)
    {

        if (allEvents.ContainsKey(eventType))
        {
            //Debug.Log("事件已经存在→" + eventType.ToString());
        }
        else
        {
            allEvents.Add(eventType, null);
        }
        allEvents[eventType] = (CallBackClass.Callback<T>)allEvents[eventType] + callBack;
    }
    public static void AddListener<T, U>(Wtb_EventSendType eventType, CallBackClass.Callback<T, U> callBack)
    {

        if (allEvents.ContainsKey(eventType))
        {
            //Debug.Log("事件已经存在→" + eventType.ToString());
        }
        else
        {
            allEvents.Add(eventType, null);
        }
        allEvents[eventType] = (CallBackClass.Callback<T, U>)allEvents[eventType] + callBack;
    }
    public static void AddListener<T, U, V>(Wtb_EventSendType eventType, CallBackClass.Callback<T, U, V> callBack)
    {

        if (allEvents.ContainsKey(eventType))
        {
            //Debug.Log("事件已经存在→" + eventType.ToString());
        }
        else
        {
            allEvents.Add(eventType, null);
        }
        allEvents[eventType] = (CallBackClass.Callback<T, U, V>)allEvents[eventType] + callBack;
    }

    public static void AddListener<T, U, V, W>(Wtb_EventSendType eventType, CallBackClass.Callback<T, U, V, W> callBack)
    {

        if (allEvents.ContainsKey(eventType))
        {
            //Debug.Log("事件已经存在→" + eventType.ToString());
        }
        else
        {
            allEvents.Add(eventType, null);
        }
        allEvents[eventType] = (CallBackClass.Callback<T, U, V, W>)allEvents[eventType] + callBack;
    }

    public static void RemoveListener(Wtb_EventSendType eventType, CallBackClass.Callback callBack)
    {
        if (allEvents.ContainsKey(eventType))
        {
            if (allEvents[eventType] == null)
            {
                //Debug.LogError("没有当前事件，可能已经删除→" + eventType.ToString());
            }
            else
            {
                allEvents[eventType] = (CallBackClass.Callback)allEvents[eventType] - callBack;
            }
        }
        else
        {
            //Debug.LogError("事件已经删除→" + eventType.ToString());
        }
    }
    public static void RemoveListener<T>(Wtb_EventSendType eventType, CallBackClass.Callback<T> callBack)
    {
        if (allEvents.ContainsKey(eventType))
        {
            if (allEvents[eventType] == null)
            {
                //Debug.LogError("没有当前事件，可能已经删除→" + eventType.ToString());
            }
            else
            {
                allEvents[eventType] = (CallBackClass.Callback<T>)allEvents[eventType] - callBack;
            }

        }
        else
        {
            //Debug.LogError("事件已经删除→" + eventType.ToString());
        }
    }
    public static void RemoveListener<T, U>(Wtb_EventSendType eventType, CallBackClass.Callback<T, U> callBack)
    {
        if (allEvents.ContainsKey(eventType))
        {
            if (allEvents[eventType] == null)
            {
                //Debug.LogError("没有当前事件，可能已经删除→" + eventType.ToString());
            }
            else
            {
                allEvents[eventType] = (CallBackClass.Callback<T, U>)allEvents[eventType] - callBack;
            }
        }
        else
        {
            //Debug.LogError("事件已经删除→" + eventType.ToString());
        }
    }
    public static void RemoveListener<T, U, V>(Wtb_EventSendType eventType, CallBackClass.Callback<T, U, V> callBack)
    {
        if (allEvents.ContainsKey(eventType))
        {
            if (allEvents[eventType] == null)
            {
                //Debug.LogError("没有当前事件，可能已经删除→" + eventType.ToString());
            }
            else
            {
                allEvents[eventType] = (CallBackClass.Callback<T, U, V>)allEvents[eventType] - callBack;
                string funcName = callBack.Method.ReflectedType.FullName + " 类中的方法：" + callBack.Method.Name;
            }
        }
        else
        {
            //Debug.LogError("事件已经删除→" + eventType.ToString());
        }
    }

    public static void RemoveListener<T, U, V, W>(Wtb_EventSendType eventType, CallBackClass.Callback<T, U, V, W> callBack)
    {
        if (allEvents.ContainsKey(eventType))
        {
            if (allEvents[eventType] == null)
            {
                //Debug.LogError("没有当前事件，可能已经删除→" + eventType.ToString());
            }
            else
            {
                allEvents[eventType] = (CallBackClass.Callback<T, U, V, W>)allEvents[eventType] - callBack;
                string funcName = callBack.Method.ReflectedType.FullName + " 类中的方法：" + callBack.Method.Name;
            }
        }
        else
        {
            //Debug.LogError("事件已经删除→" + eventType.ToString());
        }
    }

    public static void RemoveAllListener(Wtb_EventSendType eventType)
    {
        if (allEvents.ContainsKey(eventType))
        {
            allEvents[eventType] = null;
        }
    }

    public static void RemoveAllListener()
    {
        allEvents.Clear();
    }


    public static void BroadcastEvent(Wtb_EventSendType eventType)
    {
        Delegate d;
        if (allEvents.TryGetValue(eventType, out d))
        {
            CallBackClass.Callback callback = d as CallBackClass.Callback;

            if (callback != null)
            {
                callback();
            }
            else
            {
                allEvents.Remove(eventType);
                //Debug.LogError("事件已经移除Key→" + eventType.ToString());
            }
        }
    }


    public static void BroadcastEvent<T>(Wtb_EventSendType eventType, T args)
    {
        Delegate d;
        if (allEvents.TryGetValue(eventType, out d))
        {
            CallBackClass.Callback<T> callback = d as CallBackClass.Callback<T>;

            if (callback != null)
            {
                callback(args);
            }
            else
            {
                allEvents.Remove(eventType);
                //Debug.LogError("事件已经移除Key→" + eventType.ToString());
            }
        }
    }

    public static void BroadcastEvent<T, U>(Wtb_EventSendType eventType, T args1, U args2)
    {
        Delegate d;
        if (allEvents.TryGetValue(eventType, out d))
        {
            CallBackClass.Callback<T, U> callback = d as CallBackClass.Callback<T, U>;

            if (callback != null)
            {
                callback(args1, args2);
            }
            else
            {
                allEvents.Remove(eventType);
                //Debug.LogError("事件已经移除Key→" + eventType.ToString());
            }
        }
    }
    public static void BroadcastEvent<T, U, V>(Wtb_EventSendType eventType, T args1, U args2, V arg3)
    {
        Delegate d;
        if (allEvents.TryGetValue(eventType, out d))
        {
            CallBackClass.Callback<T, U, V> callback = d as CallBackClass.Callback<T, U, V>;

            if (callback != null)
            {
                callback(args1, args2, arg3);
            }
            else
            {
                allEvents.Remove(eventType);
                //Debug.LogError("事件已经移除Key→" + eventType.ToString());
            }
        }
    }

    public static void BroadcastEvent<T, U, V, W>(Wtb_EventSendType eventType, T args1, U args2, V arg3, W arg4)
    {
        Delegate d;
        if (allEvents.TryGetValue(eventType, out d))
        {
            CallBackClass.Callback<T, U, V, W> callback = d as CallBackClass.Callback<T, U, V, W>;

            if (callback != null)
            {
                callback(args1, args2, arg3, arg4);
            }
            else
            {
                allEvents.Remove(eventType);
                //Debug.LogError("事件已经移除Key→" + eventType.ToString());
            }
        }
    }

}
public class CallBackClass
{
    //无参数
    public delegate void Callback();
    //一个参数
    public delegate void Callback<T>(T arg1);
    //2个参数
    public delegate void Callback<T, U>(T arg1, U arg2);
    //3个参数
    public delegate void Callback<T, U, V>(T arg1, U arg2, V arg3);
    public delegate void Callback<T, U, V, W>(T arg1, U arg2, V arg3, W arg4);
}