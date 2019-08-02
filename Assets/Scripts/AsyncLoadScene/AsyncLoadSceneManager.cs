using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*******************************************/
//该脚本用于异步加载场景的调用,该脚本需要挂载到一个不被销毁的物体上
//调用时使用单例进行调用
//该脚本提供了三种方法
/*******************************************/
namespace FREEqinToolBox
{
    public enum ProgressSliderMode
    {
        Image,
        Slider,
        Both,
        Nothing
    }

    public class AsyncLoadSceneManager : MonoBehaviour
    {
        #region 单例
        public static AsyncLoadSceneManager instance;
        #endregion
        [HideInInspector]
        //目标场景名称
        public string aimSceneName;
        [HideInInspector]
        public float loadingSceneSpeed;
        [Header("选择进度条模式，Image为图片模式，Slider为滑动条模式，Nothing不显示滑动条")]
        public ProgressSliderMode sliderMode;
        [Header("该选项控制是否显示加载进程的数字")]
        public bool isShowNum = false;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                return;
            }
            //加载场景时不销毁该物体
            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// 该方法实现场景异步加载
        /// </summary>
        /// <param name="aimScene">这里输入跳转目标场景名称</param>
        public void AsyncLoadSceneToAim(string aimScene)
        {
            loadingSceneSpeed = 1.0f;
            aimSceneName = aimScene;
            SceneManager.LoadScene("Loading");
        }
        public void AsyncLoadSceneToAim(string aimScene,float loadingSpeed)
        {
            loadingSceneSpeed = loadingSpeed;
            aimSceneName = aimScene;
            SceneManager.LoadScene("Loading");
        }
        public void AsyncLoadSceneToAim(string aimScene, float loadingSpeed, ProgressSliderMode loadMode)
        {
            loadingSceneSpeed = loadingSpeed;
            sliderMode = loadMode;
            aimSceneName = aimScene;
            SceneManager.LoadScene("Loading");
        }

    }
}
