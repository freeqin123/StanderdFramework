/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：人大光媒设计
* 类 名 称：NotificationDelegCenter
* 创建日期：2019/03/07  
* 作者名称：刘沄
* 功能描述：消息中心逻辑类，负责收发消息。
* 修改记录：
* 添加备注：
* 
******************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace LYFrameWork
{
    //C#在类定义外可以声明方法的签名（Delegate，代理或委托），但是不能声明真正的方法。

    public delegate void OnNotificationDelegate(Notification note);

    public class NotificationDelegCenter
    {
        private static NotificationDelegCenter instance;

        public static NotificationDelegCenter Instance
        {
            get
            {
                if (null == instance)
                    instance = new NotificationDelegCenter();
                return instance;
            }
        }

        public const int c_eventNum = 1024;

        private OnNotificationDelegate[] eventListerners = new OnNotificationDelegate[c_eventNum];

        private NotificationDelegCenter()
        {
            
        }

        //监听事件
        //添加监听事件

        public void addEventListener(ushort t_num, OnNotificationDelegate listener)
        {
            if (t_num >= 0 && t_num < c_eventNum)
            {
                eventListerners[t_num] += listener;
            }
            else
            {
                Debug.Log("注册监听事件失败，ID为：" + t_num);
            }
        }

        //移除监听事件

        public void removeEventListener(ushort t_num, OnNotificationDelegate listener)
        {
            if (t_num >= 0 && t_num < c_eventNum)
            {
                eventListerners[t_num] -= listener;
            }
            else
            {
                Debug.Log("移除监听事件失败，ID为：" + t_num);
            }
        }

        //移除所有监听事件
        public void removeEventListener()
        {
            Array.Clear(eventListerners, 0, eventListerners.Length);
        }

        //派发事件
        //派发数据
        public void dispatchMsg(Notification note)
        {
            if (note.noteID >= 0 && note.noteID < c_eventNum)
            {
                if (eventListerners[note.noteID] == null)
                {
                    Debug.Log(note.noteID);
                    return;
                }
                else
                    eventListerners[note.noteID](note);
            }
            else
            {
                Debug.Log("派发监听事件失败，ID为：" + note.noteID);
            }
        }
    }
}