/*******************************************************************************
* 版权声明：北京润尼尔网络科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 项目名称：Buskit3D
* 类 名 称：QianYinControl
* 创建日期：2018/11/29  
* 作者名称：王志远
* 功能描述：命令系统业务逻辑处理(撤销重做)
* 修改记录：
* 2018/11/29 添加备注
* 
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using LYFrameWork;
using UnityEngine;

public class DemoSceneLogic : MonoBase,IProcessEvent
{
    ushort[] m_msgIDs;
    // 消息接收方需要进行消息注册
    private void Awake()
    {
        m_msgIDs = new ushort[] {
            (ushort)DemoEnum.ChangeObjsColor,
        };
        NoteCenter.Instance.Regist(m_msgIDs, this);//消息注册


    }
    private void Start()
    {
        UIManager.Instance.ShowUIForms("DemoStartUIForm");
    }

    /// <summary>
    /// 重写ProcessEvent用于接收消息
    /// </summary>
    /// <param name="note"></param>
    public void ProcessEvent(Notification note)
    {
        if (note.noteID == (ushort)DemoEnum.ChangeObjsColor)
        {
            GetComponent<MeshRenderer>().material.color = (Color)note.data;
        }
    }
    //当前UI销毁时需要取消注册的消息
    private void OnDestroy()
    {
        NoteCenter.Instance.UnRegist(m_msgIDs, this);//取消注册
    }


}
