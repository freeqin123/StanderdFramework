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
using LYFrameWork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LYFrameWork
{
    public enum DemoEnum
    {
        ChangeObjsColor,
        MaxValue
    }
}
public class DemoStartUIForm : BaseUIForm
{
    void Awake()
    {

        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.Normal;//所属分层，与Canvas上的对应级别有关
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;//无特殊需求直接normal即可
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Pentrate;//是否存在可以防止触发点击事件的Mask


        // 给按钮注册事件
        UnityHelper.FindTheChildNode(gameObject, "Btn_OpenUIForm").GetComponent<UIButtonStandard>().addClickListener((a) => 
        {
            UIManager.Instance.ShowUIForms("DemoOnClickUIForm");//使用 UIManager.Instance.ShowUIForms来打开UIManager中已经添加的UIForm
        });

        UnityHelper.FindTheChildNode(gameObject, "Btn_ChangeColor").GetComponent<UIButtonStandard>().addClickListener(PressChangeColor);

    }

    private void PressChangeColor(ButtonNotification note)
    {
        NoteCenter.Instance.SendMsg((ushort)DemoEnum.ChangeObjsColor, Color.red);//请注意必须使用SendMsg发送消息，接收消息的一方需要注册，注册后接收才可以接收
    }
}
