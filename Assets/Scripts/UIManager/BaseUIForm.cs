/***
 * 
 *    Title: "SUIFW" UI框架项目
 *           主题: UI窗体的父类
 *    Description: 
 *           功能：定义所有UI窗体的父类。
 *           定义四个生命周期
 *           
 *           1：Display 显示状态。
 *           2：Hiding 隐藏状态
 *           3：ReDisplay 再显示状态。
 *           4：Freeze 冻结状态。
 *           
 *                  
 *    Date: 2017
 *    Version: 0.1版本
 *    Modify Recoder: 
 *    
 *   
 */
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

namespace LYFrameWork
{
    public class BaseUIForm : MonoBase
    {
        /*字段*/
        private UIType _CurrentUIType = new UIType();

        /* 属性*/
        //当前UI窗体类型
        public UIType CurrentUIType
        {
            get { return _CurrentUIType; }
            set { _CurrentUIType = value; }
        }

        public string uiID { get; set; }

        #region  窗体的四种(生命周期)状态

        /// <summary>
        /// 显示状态
        /// </summary>
	    public virtual void Display()
        {
            if(!this.gameObject.activeSelf)
                this.gameObject.SetActive(true);
            //设置模态窗体调用(必须是弹出窗体)
            if (_CurrentUIType.UIForms_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject, _CurrentUIType.UIForm_LucencyType);
            }
            else if (_CurrentUIType.UIForms_Type == UIFormType.PopUpAttendant)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject, _CurrentUIType.UIForm_LucencyType,true);
            }
        }

        /// <summary>
        /// 显示并且出现在特定位置
        /// </summary>
        public virtual void Display(Vector3 _pos)
        {
            if(!this.gameObject.activeSelf)
                this.gameObject.SetActive(true);
            transform.position = _pos;
            //设置模态窗体调用(必须是弹出窗体)
            if (_CurrentUIType.UIForms_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject, _CurrentUIType.UIForm_LucencyType);
            }
        }

        /// <summary>
        /// 隐藏状态
        /// </summary>
	    public virtual void Hiding()
        {
            this.gameObject.SetActive(false);
            //取消模态窗体调用
            if (_CurrentUIType.UIForms_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().CancelMaskWindow();
            }
        }

        /// <summary>
        /// 重新显示状态
        /// </summary>
	    public virtual void Redisplay()
        {
            if (!this.gameObject.activeSelf)
                this.gameObject.SetActive(true);
            //设置模态窗体调用(必须是弹出窗体)
            if (_CurrentUIType.UIForms_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject, _CurrentUIType.UIForm_LucencyType);
            }
        }

        /// <summary>
        /// 冻结状态
        /// </summary>
	    public virtual void Freeze()
        {
            if (!this.gameObject.activeSelf)
                this.gameObject.SetActive(true);
        }


        #endregion

        #region 封装子类常用的方法

        /// <summary>
        /// 注册按钮事件
        /// </summary>
        /// <param name="buttonName">按钮节点名称</param>
        /// <param name="delHandle">委托：需要注册的方法</param>
	    //protected void RigisterButtonObjectEvent(string buttonName, OnClickHandler action)
     //   {
     //       Transform goButtonTrans = UnityHelper.FindTheChildNode(this.gameObject, buttonName);
     //       //给按钮注册事件方法
     //       if (goButtonTrans != null)
     //       {
     //           goButtonTrans.GetComponent<UIButtonBase>().addClickListener(action);
     //       }
     //   }
       


        /// <summary>
        /// 打开UI窗体
        /// </summary>
        /// <param name="uiFormName"></param>
	    protected void OpenUIForm(string uiFormName)
        {
            UIManager.Instance.ShowUIForms(uiFormName);
        }

        /// <summary>
        /// 关闭当前UI窗体
        /// </summary>
	    protected void CloseUIForm()
        {
            //string strUIFromName = string.Empty;            //处理后的UIFrom 名称
            //int intPosition = -1;

            //strUIFromName = GetType().ToString();             //命名空间+类名
            //intPosition = strUIFromName.IndexOf('.');
            //if (intPosition != -1)
            //{
            //    //剪切字符串中“.”之间的部分
            //    strUIFromName = strUIFromName.Substring(intPosition + 1);
            //}

            UIManager.Instance.CloseUIForms(uiID);
        }

        #endregion

    }
}