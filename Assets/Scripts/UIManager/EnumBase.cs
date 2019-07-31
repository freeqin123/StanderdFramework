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
using UnityEngine;

namespace LYFrameWork
{
    public enum UIBaseEvent
    {
        unselectOther = 0,
        lockOther,
        unlockOther,

        MaxValue
    }
}