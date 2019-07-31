using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LYFrameWork
{
    public enum EAINotificationReceiver
    {
        Target,
        Area,
        Activity
    }

    public class AINotification : EventArgs
    {
        public ushort noteID;
        public EAINotificationReceiver receiver;
        public object[] dataArray { get; set; }
        public object data
        {
            get
            {
                return dataArray[0];
            }
        }

        public object data1
        {
            get
            {
                return dataArray[1];
            }
        }


        public AINotification(EAINotificationReceiver areceiver, ushort aNoteID, params object[] dataParam) { receiver = areceiver; noteID = aNoteID; dataArray = dataParam; }
        public AINotification(EAINotificationReceiver areceiver, ushort aNoteID) { receiver = areceiver; noteID = aNoteID; }
    }
}

namespace LYFrameWork
{
    public class Notification:EventArgs
	{
		public ushort noteID;

        public object[] dataArray { get; set; }
        public object data
        {
            get {
                    return dataArray[0];
                }
        }

        public object data1
        {
            get
            {
                return dataArray[1];
            }
        }


        public Notification(ushort aNoteID, params object[] dataParam) { noteID = aNoteID; dataArray = dataParam; }
        public Notification(ushort aNoteID) { noteID = aNoteID; }
    }
    public class ButtonNotification : EventArgs
    {
        public object data;

        public object sender;

        public ButtonNotification(object aData) { data = aData; }

        public ButtonNotification(object aData, object aSender) { data = aData; sender = aSender; }

        public ButtonNotification() { data = null; sender = null; }
    }
}