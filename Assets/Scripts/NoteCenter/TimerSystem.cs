/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：Timer
* 创建日期：2019-03-15 11:30:17
* 作者名称：刘沄
* CLR 版本：4.0.30319.42000
* 功能描述：TimerSystem(计时器，用于处理消息发送)
* 修改记录：
* 日期 描述： 
* 
******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LYFrameWork
{
    public class Timer
    {
        public Timer(Action<object> tAction, float tRemainingTime)
        {
            remainingTime = tRemainingTime;
            action = tAction;
        }

        public Timer(Action<object> tAction, float tRemainingTime, object tUserData)
        {
            remainingTime = tRemainingTime;
            action = tAction;
            userData = tUserData;
        }
        public object userData;
        public float remainingTime;
        public Action<object> action;
    }

    public interface ITimerSystem
    {
        void AddTimer(Action<object> action ,float remainingTime, object userData);
        void AddTimer(Action<object> action, float remainingTime);
        void RemoveTimer(Action<object> action);
        void RegistUpdate(Action action);
        void UnregistUpdate(Action action);
    }

    public interface IUpdate
    {
        void Update();
    }

    public class TimerSystem : MonoBehaviour, ITimerSystem
    {
        #region 单例获取
        public static TimerSystem Instance
        {
            get
            {
                return _instance;
            }
        }

         static TimerSystem _instance; 
        #endregion

        private void Awake()
        {
            _instance = this;        
        }

        private List<Timer> _timerList = new List<Timer>();
        private List<Timer> _timerRemoveList = new List<Timer>();

        public Action onUpdate;

        public void OnUpdate()
        {
            if (onUpdate != null)
                onUpdate();
        }

        private void Update()
        {
            OnUpdate();
        }

        private void FixedUpdate()
        {
            if (_timerList.Count > 0)
            {
                //foreach (var timer in _timerList)
                for (int i = 0; i < _timerList.Count; i++)
                {
                    _timerList[i].remainingTime -= Time.fixedDeltaTime;
                    if (_timerList[i].remainingTime <= 0)
                    {
                        if (_timerList[i].action != null)
                        {
                            _timerList[i].action(_timerList[i].userData);
                            _timerRemoveList.Add(_timerList[i]);
                        }
                    }
                }
            }

            if (_timerRemoveList.Count > 0)
            {
                for (int i = 0; i < _timerRemoveList.Count; i++)
                {
                    _timerList.Remove(_timerRemoveList[i]);
                }
            }
        }

        public void AddTimer(Action<object> action, float remainingTime)
        {
            _timerList.Add(new Timer(action, remainingTime));
        }

        public void RemoveTimer(Action<object> action)
        {
            for (int i = 0; i < _timerList.Count; i++)
            {
                if(_timerList[i].action == action)
                {
                    _timerRemoveList.Add(_timerList[i]);
                    return;
                }
            }
        }
        public void ClearTimer()
        {
            for (int i = 0; i < _timerList.Count; i++)
            {
                _timerRemoveList.Add(_timerList[i]);
            }
            for (int i = 0; i < _timerRemoveList.Count; i++)
            {
                _timerList.Remove(_timerRemoveList[i]);
            }
        }

        public void AddTimer(Action<object> action, float remainingTime, object userData)
        {
            _timerList.Add(new Timer(action, remainingTime, userData));
        }

        public void RegistUpdate(Action action)
        {
            onUpdate += action;
        }

        public void UnregistUpdate(Action action)
        {
            onUpdate -= action;
        }
    }
}

