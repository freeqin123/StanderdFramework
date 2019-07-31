using UnityEngine;
using System;
using System.Collections.Generic;

namespace LYFrameWork
{
    public interface IProcessEvent
    {
        void ProcessEvent(Notification note);
    }


    public class NoteCenter
    {
        #region 单例获取
        private class NoteCenterNested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static NoteCenterNested()
            {
            }

            internal static readonly NoteCenter _instance = new NoteCenter();
        }

        public static NoteCenter Instance
        {
            get
            {
                return NoteCenterNested._instance;
            }
        }
        #endregion

        public const int c_eventNum = 255;
        public const int c_spaceFrame = 10;

        private OnNotificationDelegate[] eventListerners = new OnNotificationDelegate[c_eventNum];

        private Dictionary<ushort,Notification> _waitingNotificationDic = new Dictionary<ushort, Notification>();
        private List<ushort> _waitingRemoveNotificationList = new List<ushort>();

        private NoteCenter()
        {
            TimerSystem.Instance.RegistUpdate(Update);
        }

         ~NoteCenter()
        {
            _waitingNotificationDic.Clear();
            TimerSystem.Instance.UnregistUpdate(Update);
        }

        void Update()
        {
            if(Time.frameCount % c_spaceFrame == 0)
            {
                foreach (KeyValuePair<ushort, Notification> kvp in _waitingNotificationDic)
                {
                    DispatchMsgAsync(kvp.Value);
                }

                for (int i = 0; i < _waitingRemoveNotificationList.Count; i++)
                {
                    _waitingNotificationDic.Remove(_waitingRemoveNotificationList[i]);
                }

                _waitingRemoveNotificationList.Clear();
            }
        }

        //注册事件
        public void Regist(ushort[] msgIDs, IProcessEvent processEventHandler)
        {
            //if (msgIDs == null)
            //    Debug.Log("监听事件数组为Null，名字为：" + name);
            foreach (ushort t_ushort in msgIDs)
            {
                AddEventListener(t_ushort, processEventHandler.ProcessEvent);
            }
        }

        public void UnRegist(ushort[] msgIDs, IProcessEvent processEventHandler)
        {
            //if (_msgIDs == null)
            //    Debug.Log("监听事件数组为Null，名字为：" + name);
            foreach (ushort t_ushort in msgIDs)
            {
                RemoveEventListener(t_ushort, processEventHandler.ProcessEvent);
            }
        }

        //监听事件
        //添加监听事件

        void AddEventListener(ushort t_num, OnNotificationDelegate listener)
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

        void RemoveEventListener(ushort t_num, OnNotificationDelegate listener)
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
        public void RemoveEventListener()
        {
            Array.Clear(eventListerners, 0, eventListerners.Length);
        }

        public void SendMsg(ushort aNoteID, params object[] dataParam)
        {
            DispatchMsg(new Notification(aNoteID, dataParam));
        }

        public void SendMsg(ushort aNoteID)
        {
            DispatchMsg(new Notification(aNoteID));
        }

        //派发事件
        //派发数据
        void DispatchMsg(Notification note)
        {
            if (note.noteID >= 0 && note.noteID < c_eventNum)
            {
                if (eventListerners[note.noteID] == null)
                {
                    if (!_waitingNotificationDic.ContainsKey(note.noteID) && !_waitingRemoveNotificationList.Contains(note.noteID))
                    {
                        _waitingNotificationDic.Add(note.noteID, note);
                    }
                    return;
                }
                else
                {
                    eventListerners[note.noteID](note);
                }
            }
            else
            {
                Debug.Log("派发监听事件失败，ID为：" + note.noteID);
            }
        }

      void DispatchMsgAsync(Notification note)
        {
            if (note.noteID >= 0 && note.noteID < c_eventNum)
            {
                if (eventListerners[note.noteID] == null)
                {
                    return;
                }
                else
                {
                    if (_waitingRemoveNotificationList.Contains(note.noteID))
                        return;
                    _waitingRemoveNotificationList.Add(note.noteID);
                    eventListerners[note.noteID](note);
                }
            }
            else
            {
                Debug.Log("派发监听事件失败，ID为：" + note.noteID);
            }
        }

    }
}
