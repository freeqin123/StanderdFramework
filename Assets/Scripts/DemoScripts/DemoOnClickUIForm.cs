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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoOnClickUIForm : BaseUIForm
{
    void Awake()
    {

        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.PopUp;//所属分层，与Canvas上的对应级别有关
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;//无特殊需求直接normal即可
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;//是否存在可以防止触发点击事件的Mask


        // 给按钮注册事件
        UnityHelper.FindTheChildNode(gameObject, "Btn_CloseUIForm").GetComponent<UIButtonStandard>().addClickListener((a) => 
        {
            UIManager.Instance.CloseUIForms("DemoOnClickUIForm");// 使用 UIManager.Instance.CloseUIForms关闭对应UIForm,如果继承了BaseUIForm可以直接使用已封装的CloseUIForm
            //CloseUIForm();
        });

    }
}
