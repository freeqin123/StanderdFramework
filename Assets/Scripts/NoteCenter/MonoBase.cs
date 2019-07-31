using UnityEngine;
using System.Collections;
using System;

namespace LYFrameWork
{
    public interface ILock
    {
        void Lock();
        void Unlock();
    }

    public class MonoBase : MonoBehaviour,ILock
    {
        protected bool isLock = false;

        public virtual void Lock()
        {
            isLock = true;
        }

        public virtual void Unlock()
        {
            isLock = false;
        }

        protected Transform m_transform;

        protected virtual void Init()
        {

        }
    }
}
