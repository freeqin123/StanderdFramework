﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LYFrameWork;
using System.Collections.Generic;

public class UIManager:MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    protected Dictionary<string, GameObject> m_childGameObjects = new Dictionary<string, GameObject>();

    public void RegistGameObject(string t_name, GameObject t_go)
    {
        if (!m_childGameObjects.ContainsKey(t_name))
        {
            m_childGameObjects.Add(t_name, t_go);
        }
        else
        {
            Debug.Log("Manager字典中已经存在该物体，物体名称：" + t_name);
        }
    }

    public GameObject GetGameObject(string t_name)
    {
        if (m_childGameObjects.ContainsKey(t_name))
        {
            return m_childGameObjects[t_name];
        }
        else
        {
            Debug.Log("你想找的物体不存在Manager字典中，物体名称：" + t_name);
            return null;
        }
    }

    public void UnRegistGameObject(string t_name)
    {
        if (m_childGameObjects.ContainsKey(t_name))
        {
            m_childGameObjects.Remove(t_name);
            Debug.Log("删除物体: " + t_name);
        }
        else
        {
            Debug.Log("你想要从Manager字典中删除的物体不存在，物体名称：" + t_name);
        }
    }

    //UI窗体预设路径(参数1：窗体预设名称，2：表示窗体预设路径)
    private Dictionary<string, string> _DicFormsPaths;
    //缓存所有UI窗体
    private Dictionary<string, BaseUIForm> _DicALLUIForms;
    //当前显示的UI窗体
    private Dictionary<string, BaseUIForm> _DicCurrentShowUIForms;
    //定义“栈”集合,存储显示当前所有[反向切换]的窗体类型
    private Stack<BaseUIForm> _StaCurrentUIForms;
    //UI根节点
    private Transform _TraCanvasTransfrom = null;
    //全屏幕显示的节点
    private Transform _TraNormal = null;
    //固定显示的节点
    private Transform _TraFixed = null;
    //弹出节点
    private Transform _TraPopUp = null;
    //固定显示的节点
    private Transform _TraTip = null;
    //UI管理脚本的节点
    private Transform _TraUIScripts = null;

    private Camera _UICamera;
    /// <summary>
    /// 得到实例
    /// </summary>
    /// <returns></returns

   public void ClearAll()
    {
        foreach(Transform t1  in _TraNormal)
        {
            Destroy(t1.gameObject);
        }
        foreach (Transform t2 in _TraFixed)
        {
            Destroy(t2.gameObject);
        }
        foreach (Transform t3 in _TraPopUp)
        {
            if (t3.name == "_UIMaskPanel")
            {
                continue;
            }
            Destroy(t3.gameObject);
        }
        foreach (Transform t4 in _TraTip)
        {
            Destroy(t4.gameObject);
        }

        _DicCurrentShowUIForms.Clear();
        _DicALLUIForms.Clear();
        _StaCurrentUIForms.Clear();
    }

    //初始化核心数据，加载“UI窗体路径”到集合中。
    public void Awake()
    {
        instance = this;
        //字段初始化
        _DicALLUIForms = new Dictionary<string, BaseUIForm>();
        _DicCurrentShowUIForms = new Dictionary<string, BaseUIForm>();
        _DicFormsPaths = new Dictionary<string, string>();
        _StaCurrentUIForms = new Stack<BaseUIForm>();
        //初始化加载（根UI窗体）Canvas预设
        InitRootCanvasLoading();
        //得到UI根节点、全屏节点、固定节点、弹出节点
        _TraCanvasTransfrom = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS).transform;
        _TraNormal = _TraCanvasTransfrom.Find("Normal");
        _TraFixed = _TraCanvasTransfrom.Find("Fixed");
        _TraTip = _TraCanvasTransfrom.Find("Tip");
        _TraPopUp = _TraCanvasTransfrom.Find("PopUp");
        _TraUIScripts = _TraCanvasTransfrom.Find("_ScriptMgr");
        //_UICamera = GameObject.FindGameObjectWithTag("_TagUICamera").GetComponent<Camera>();
        //把本脚本作为“根UI窗体”的子节点。
        this.gameObject.transform.SetParent(_TraUIScripts, false);
        //"根UI窗体"在场景转换的时候，不允许销毁
        DontDestroyOnLoad(_TraCanvasTransfrom);
        //初始化“UI窗体预设”路径数据
        //先写简单的，后面我们使用Json做配置文件，来完善。
        if (_DicFormsPaths != null)
        {
            _DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            _DicFormsPaths.Add("DemoOnClickUIForm", @"UIPrefabs\DemoOnClickUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
            //_DicFormsPaths.Add("DemoStartUIForm", @"UIPrefabs\DemoStartUIForm");
        }
    }

    public void SwitchUIForms(string uiFormName)
    {        //参数的检查
        if (string.IsNullOrEmpty(uiFormName)) return;
        if (_DicCurrentShowUIForms.ContainsKey(uiFormName))
            CloseUIForms(uiFormName);
        else
            ShowUIForms(uiFormName);
    }

    /// <summary>
    /// 显示（打开）UI窗体
    /// 功能：
    /// 1: 根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
    /// 2: 根据不同的UI窗体的“显示模式”，分别作不同的加载处理
    /// </summary>
    /// <param name="uiFormName">UI窗体预设的名称</param>
    public void ShowUIForms(string uiFormName)
    {
        BaseUIForm baseUIForms = null;                    //UI窗体基类

        //参数的检查
        if (string.IsNullOrEmpty(uiFormName)) return;

        //根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
        baseUIForms = LoadFormsToAllUIFormsCatch(uiFormName);
        if (baseUIForms == null) return;

        //是否清空“栈集合”中得数据
        if (baseUIForms.CurrentUIType.IsClearStack)
        {
            ClearStackArray();
        }

        //根据不同的UI窗体的显示模式，分别作不同的加载处理
        switch (baseUIForms.CurrentUIType.UIForms_ShowMode)
        {
            case UIFormShowMode.Normal:                 //“普通显示”窗口模式
                                                        //把当前窗体加载到“当前窗体”集合中。
                LoadUIToCurrentCache(uiFormName);
                break;
            case UIFormShowMode.ReverseChange:          //需要“反向切换”窗口模式
                PushUIFormToStack(uiFormName);
                break;
            case UIFormShowMode.HideOther:              //“隐藏其他”窗口模式
                EnterUIFormsAndHideOther(uiFormName);
                break;
            default:
                break;
        }
    }

    public void ShowUIForms(string uiFormName,Vector3 _pos)
    {
        BaseUIForm baseUIForms = null;                    //UI窗体基类

        //参数的检查
        if (string.IsNullOrEmpty(uiFormName)) return;

        //根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
        baseUIForms = LoadFormsToAllUIFormsCatch(uiFormName);
        if (baseUIForms == null) return;

        //是否清空“栈集合”中得数据
        if (baseUIForms.CurrentUIType.IsClearStack)
        {
            ClearStackArray();
        }

        //根据不同的UI窗体的显示模式，分别作不同的加载处理
        switch (baseUIForms.CurrentUIType.UIForms_ShowMode)
        {
            case UIFormShowMode.Normal:                 //“普通显示”窗口模式
                                                        //把当前窗体加载到“当前窗体”集合中。
                LoadUIToCurrentCache(uiFormName, _pos);
                break;
            case UIFormShowMode.ReverseChange:          //需要“反向切换”窗口模式
                PushUIFormToStack(uiFormName);
                break;
            case UIFormShowMode.HideOther:              //“隐藏其他”窗口模式
                EnterUIFormsAndHideOther(uiFormName);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 关闭（返回上一个）窗体
    /// </summary>
    /// <param name="uiFormName"></param>
    public void CloseUIForms(string uiFormName)
    {
        BaseUIForm baseUiForm;                          //窗体基类

        //参数检查
        if (string.IsNullOrEmpty(uiFormName)) return;
        //“所有UI窗体”集合中，如果没有记录，则直接返回
        _DicALLUIForms.TryGetValue(uiFormName, out baseUiForm);
        if (baseUiForm == null) return;
        //根据窗体不同的显示类型，分别作不同的关闭处理
        switch (baseUiForm.CurrentUIType.UIForms_ShowMode)
        {
            case UIFormShowMode.Normal:
                //普通窗体的关闭
                ExitUIForms(uiFormName);
                break;
            case UIFormShowMode.ReverseChange:
                //反向切换窗体的关闭
                PopUIFroms();
                break;
            case UIFormShowMode.HideOther:
                //隐藏其他窗体关闭
                ExitUIFormsAndDisplayOther(uiFormName);
                break;

            default:
                break;
        }
    }

    #region  显示“UI管理器”内部核心数据，测试使用

    /// <summary>
    /// 显示"所有UI窗体"集合的数量
    /// </summary>
    /// <returns></returns>
    public int ShowALLUIFormCount()
    {
        if (_DicALLUIForms != null)
        {
            return _DicALLUIForms.Count;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// 显示"当前窗体"集合中数量
    /// </summary>
    /// <returns></returns>
    public int ShowCurrentUIFormsCount()
    {
        if (_DicCurrentShowUIForms != null)
        {
            return _DicCurrentShowUIForms.Count;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// 显示“当前栈”集合中窗体数量
    /// </summary>
    /// <returns></returns>
    public int ShowCurrentStackUIFormsCount()
    {
        if (_StaCurrentUIForms != null)
        {
            return _StaCurrentUIForms.Count;
        }
        else
        {
            return 0;
        }
    }

    #endregion

    #region 私有方法
    //初始化加载（根UI窗体）Canvas预设
    private void InitRootCanvasLoading()
    {
        ResourcesMgr.GetInstance().LoadAsset(SysDefine.SYS_PATH_CANVAS, false);
    }

    /// <summary>
    /// 根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
    /// 功能： 检查“所有UI窗体”集合中，是否已经加载过，否则才加载。
    /// </summary>
    /// <param name="uiFormsName">UI窗体（预设）的名称</param>
    /// <returns></returns>
    private BaseUIForm LoadFormsToAllUIFormsCatch(string uiFormsName)
    {
        BaseUIForm baseUIResult = null;                 //加载的返回UI窗体基类

        _DicALLUIForms.TryGetValue(uiFormsName, out baseUIResult);
        if (baseUIResult == null)
        {
            //加载指定名称的“UI窗体”
            baseUIResult = LoadUIForm(uiFormsName);
        }

        return baseUIResult;
    }

    /// <summary>
    /// 加载指定名称的“UI窗体”
    /// 功能：
    ///    1：根据“UI窗体名称”，加载预设克隆体。
    ///    2：根据不同预设克隆体中带的脚本中不同的“位置信息”，加载到“根窗体”下不同的节点。
    ///    3：隐藏刚创建的UI克隆体。
    ///    4：把克隆体，加入到“所有UI窗体”（缓存）集合中。
    /// 
    /// </summary>
    /// <param name="uiFormName">UI窗体名称</param>
    private BaseUIForm LoadUIForm(string uiFormName)
    {
        string strUIFormPaths = null;                   //UI窗体路径
        GameObject goCloneUIPrefabs = null;             //创建的UI克隆体预设
        BaseUIForm baseUiForm = null;                     //窗体基类


        //根据UI窗体名称，得到对应的加载路径
        _DicFormsPaths.TryGetValue(uiFormName, out strUIFormPaths);
        //根据“UI窗体名称”，加载“预设克隆体”
        if (!string.IsNullOrEmpty(strUIFormPaths))
        {
            goCloneUIPrefabs = ResourcesMgr.GetInstance().LoadAsset(strUIFormPaths, false);
            goCloneUIPrefabs.name = uiFormName;
        }
        //设置“UI克隆体”的父节点（根据克隆体中带的脚本中不同的“位置信息”）
        if (_TraCanvasTransfrom != null && goCloneUIPrefabs != null)
        {
            baseUiForm = goCloneUIPrefabs.GetComponent<BaseUIForm>();
            if (baseUiForm == null)
            {
                Debug.Log("baseUiForm==null! ,请先确认窗体预设对象上是否加载了baseUIForm的子类脚本！ 参数 uiFormName=" + uiFormName);
                return null;
            }
            switch (baseUiForm.CurrentUIType.UIForms_Type)
            {
                case UIFormType.Normal:                 //普通窗体节点
                    goCloneUIPrefabs.transform.SetParent(_TraNormal, false);
                    break;
                case UIFormType.Fixed:                  //固定窗体节点
                    goCloneUIPrefabs.transform.SetParent(_TraFixed, false);
                    break;
                case UIFormType.Tip:                  //固定窗体节点
                    goCloneUIPrefabs.transform.SetParent(_TraTip, false);
                    break;
                case UIFormType.PopUp:                  //弹出窗体节点
                    goCloneUIPrefabs.transform.SetParent(_TraPopUp, false);
                    break;
                case UIFormType.PopUpAttendant:                  //弹出窗体节点
                    goCloneUIPrefabs.transform.SetParent(_TraPopUp, false);
                    break;
                default:
                    break;
            }

            //设置隐藏
            //goCloneUIPrefabs.SetActive(false);
            //把克隆体，加入到“所有UI窗体”（缓存）集合中。
            _DicALLUIForms.Add(uiFormName, baseUiForm);
            return baseUiForm;
        }
        else
        {
            Debug.Log("_TraCanvasTransfrom==null Or goCloneUIPrefabs==null!! ,Plese Check!, 参数uiFormName=" + uiFormName);
        }

        Debug.Log("出现不可以预估的错误，请检查，参数 uiFormName=" + uiFormName);
        return null;
    }//Mehtod_end

    /// <summary>
    /// 把当前窗体加载到“当前窗体”集合中
    /// </summary>
    /// <param name="uiFormName">窗体预设的名称</param>
    private void LoadUIToCurrentCache(string uiFormName)
    {
        BaseUIForm baseUiForm;                          //UI窗体基类
        BaseUIForm baseUIFormFromAllCache;              //从“所有窗体集合”中得到的窗体

        //如果“正在显示”的集合中，存在整个UI窗体，则直接返回
        _DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUiForm);
        if (baseUiForm != null) return;
        //把当前窗体，加载到“正在显示”集合中
        _DicALLUIForms.TryGetValue(uiFormName, out baseUIFormFromAllCache);
        if (baseUIFormFromAllCache != null)
        {
            _DicCurrentShowUIForms.Add(uiFormName, baseUIFormFromAllCache);
            baseUIFormFromAllCache.uiID = uiFormName;
            baseUIFormFromAllCache.Display();           //显示当前窗体
        }
    }

    /// <summary>
    /// 把当前窗体加载到“当前窗体”集合中并且出现在特定位置
    /// </summary>
    /// <param name="uiFormName">窗体预设的名称</param>
    private void LoadUIToCurrentCache(string uiFormName,Vector2 _pos)
    {
        BaseUIForm baseUiForm;                          //UI窗体基类
        BaseUIForm baseUIFormFromAllCache;              //从“所有窗体集合”中得到的窗体

        //如果“正在显示”的集合中，存在整个UI窗体，则直接返回
        _DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUiForm);
        if (baseUiForm != null) return;
        //把当前窗体，加载到“正在显示”集合中
        _DicALLUIForms.TryGetValue(uiFormName, out baseUIFormFromAllCache);
        if (baseUIFormFromAllCache != null)
        {
            _DicCurrentShowUIForms.Add(uiFormName, baseUIFormFromAllCache);
            baseUIFormFromAllCache.uiID = uiFormName;
            baseUIFormFromAllCache.Display(_pos);           //显示当前窗体
        }
    }

    /// <summary>
    /// UI窗体入栈
    /// </summary>
    /// <param name="uiFormName">窗体的名称</param>
    private void PushUIFormToStack(string uiFormName)
    {
        BaseUIForm baseUIForm;                          //UI窗体

        //判断“栈”集合中，是否有其他的窗体，有则“冻结”处理。
        if (_StaCurrentUIForms.Count > 0)
        {
            BaseUIForm topUIForm = _StaCurrentUIForms.Peek();
            //栈顶元素作冻结处理
            topUIForm.Freeze();
        }
        //判断“UI所有窗体”集合是否有指定的UI窗体，有则处理。
        _DicALLUIForms.TryGetValue(uiFormName, out baseUIForm);
        if (baseUIForm != null)
        {
            baseUIForm.uiID = uiFormName;
            //当前窗口显示状态
            baseUIForm.Display();
            //把指定的UI窗体，入栈操作。
            _StaCurrentUIForms.Push(baseUIForm);
        }
        else
        {
            Debug.Log("baseUIForm==null,Please Check, 参数 uiFormName=" + uiFormName);
        }
    }

    /// <summary>
    /// 退出指定UI窗体
    /// </summary>
    /// <param name="strUIFormName"></param>
    private void ExitUIForms(string strUIFormName)
    {
        BaseUIForm baseUIForm;                          //窗体基类

        //"正在显示集合"中如果没有记录，则直接返回。
        _DicCurrentShowUIForms.TryGetValue(strUIFormName, out baseUIForm);
        if (baseUIForm == null) return;
        //指定窗体，标记为“隐藏状态”，且从"正在显示集合"中移除。
        baseUIForm.Hiding();
        _DicCurrentShowUIForms.Remove(strUIFormName);
    }

    //（“反向切换”属性）窗体的出栈逻辑
    private void PopUIFroms()
    {
        if (_StaCurrentUIForms.Count >= 2)
        {
            //出栈处理
            BaseUIForm topUIForms = _StaCurrentUIForms.Pop();
            //做隐藏处理
            topUIForms.Hiding();
            //出栈后，下一个窗体做“重新显示”处理。
            BaseUIForm nextUIForms = _StaCurrentUIForms.Peek();
            nextUIForms.Redisplay();
        }
        else if (_StaCurrentUIForms.Count == 1)
        {
            //出栈处理
            BaseUIForm topUIForms = _StaCurrentUIForms.Pop();
            //做隐藏处理
            topUIForms.Hiding();
        }
    }

    /// <summary>
    /// (“隐藏其他”属性)打开窗体，且隐藏其他窗体
    /// </summary>
    /// <param name="strUIName">打开的指定窗体名称</param>
    private void EnterUIFormsAndHideOther(string strUIName)
    {
        BaseUIForm baseUIForm;                          //UI窗体基类
        BaseUIForm baseUIFormFromALL;                   //从集合中得到的UI窗体基类


        //参数检查
        if (string.IsNullOrEmpty(strUIName)) return;

        _DicCurrentShowUIForms.TryGetValue(strUIName, out baseUIForm);
        if (baseUIForm != null) return;

        //把“正在显示集合”与“栈集合”中所有窗体都隐藏。
        foreach (BaseUIForm baseUI in _DicCurrentShowUIForms.Values)
        {
            baseUI.Hiding();
        }
        foreach (BaseUIForm staUI in _StaCurrentUIForms)
        {
            staUI.Hiding();
        }

        //把当前窗体加入到“正在显示窗体”集合中，且做显示处理。
        _DicALLUIForms.TryGetValue(strUIName, out baseUIFormFromALL);
        if (baseUIFormFromALL != null)
        {
            _DicCurrentShowUIForms.Add(strUIName, baseUIFormFromALL);
            //窗体显示
            baseUIFormFromALL.Display();

            baseUIFormFromALL.uiID = strUIName;
        }
    }

    /// <summary>
    /// (“隐藏其他”属性)关闭窗体，且显示其他窗体
    /// </summary>
    /// <param name="strUIName">打开的指定窗体名称</param>
    private void ExitUIFormsAndDisplayOther(string strUIName)
    {
        BaseUIForm baseUIForm;                          //UI窗体基类


        //参数检查
        if (string.IsNullOrEmpty(strUIName)) return;

        _DicCurrentShowUIForms.TryGetValue(strUIName, out baseUIForm);
        if (baseUIForm == null) return;

        //当前窗体隐藏状态，且“正在显示”集合中，移除本窗体
        baseUIForm.Hiding();
        _DicCurrentShowUIForms.Remove(strUIName);

        //把“正在显示集合”与“栈集合”中所有窗体都定义重新显示状态。
        foreach (BaseUIForm baseUI in _DicCurrentShowUIForms.Values)
        {
            baseUI.Redisplay();
        }
        foreach (BaseUIForm staUI in _StaCurrentUIForms)
        {
            staUI.Redisplay();
        }
    }

    public void ShowAll()
    {
        //把“正在显示集合”与“栈集合”中所有窗体都定义重新显示状态。
        foreach (BaseUIForm baseUI in _DicCurrentShowUIForms.Values)
        {
            baseUI.Redisplay();
        }
        foreach (BaseUIForm staUI in _StaCurrentUIForms)
        {
            staUI.Redisplay();
        }
    }

    public void HideAll()
    {
        //把“正在显示集合”与“栈集合”中所有窗体都定义重新显示状态。
        foreach (BaseUIForm baseUI in _DicCurrentShowUIForms.Values)
        {
            baseUI.Hiding();
        }
        foreach (BaseUIForm staUI in _StaCurrentUIForms)
        {
            staUI.Hiding();
        }
    }

    /// <summary>
    /// 是否清空“栈集合”中得数据
    /// </summary>
    /// <returns></returns>
    private bool ClearStackArray()
    {
        if (_StaCurrentUIForms != null && _StaCurrentUIForms.Count >= 1)
        {
            //清空栈集合
            _StaCurrentUIForms.Clear();
            return true;
        }

        return false;
    }
#endregion
}
