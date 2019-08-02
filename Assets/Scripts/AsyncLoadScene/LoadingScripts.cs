using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*******************************************/
//该脚本用于处理异步加载逻辑
//通过AsyncLoadSceneManger的AsyncLoadSceneToAim方法来跳转到该场景
//如果需要修改加载场景UI，请进入Loading场景进行修改
//如果涉及修改Loading场景中的UI的逻辑，请在该脚本中进行修改
/*******************************************/
namespace FREEqinToolBox
{ 
    public class LoadingScripts : MonoBehaviour
    {
        //载入进度UI
        public Text loadingText;
        //加载进度条Slider类型
        public Slider progressSlider;
        //加载进度条Image类型
        public Image progressCircle;
        //加载速度
        private float loadingSpeed = 1.0f;
        //目标值，由于progress最大到0.9
        private float targetValue;
        //异步加载的名称
        private string aimSceneName;
        //异步加载操作类
        private AsyncOperation operation;
        //当前的进度值，该值旨在作为中间过渡值与界面元素连接
        private float currentProgress;
        private void Start()
        {
            Init();
            //开启协程
            if (SceneManager.GetActiveScene().name == "Loading")
            {
                StartCoroutine(AsyncLoading());
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            //将Slider进度条的值清零
            progressSlider.value = 0;
            //将Circle类型的进度条归零
            progressCircle.fillAmount = 0;
            //将目标场景赋值给aimSceneName
            aimSceneName = AsyncLoadSceneManager.instance.aimSceneName;
            //初始化是否显示加载进度
            if (AsyncLoadSceneManager.instance.isShowNum)
            {
                loadingText.gameObject.SetActive(true);
            }
            else
            {
                loadingText.gameObject.SetActive(false);
            }
            //根据前一个进度条的状态来进行进度条的显示
            switch (AsyncLoadSceneManager.instance.sliderMode)
            {
                case ProgressSliderMode.Image:
                    progressCircle.gameObject.SetActive(true);
                    progressSlider.gameObject.SetActive(false);
                    break;
                case ProgressSliderMode.Slider:
                    progressCircle.gameObject.SetActive(false);
                    progressSlider.gameObject.SetActive(true);
                    break;
                case ProgressSliderMode.Both:
                    progressCircle.gameObject.SetActive(true);
                    progressSlider.gameObject.SetActive(true);
                    break;
                case ProgressSliderMode.Nothing:
                    progressCircle.gameObject.SetActive(false);
                    progressSlider.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <returns></returns>
        IEnumerator AsyncLoading()
        {
            operation = SceneManager.LoadSceneAsync(aimSceneName);
            operation.allowSceneActivation = false;
            yield return operation;
        }

        private void Update()
        {
            //获取当前加载场景的进度
            targetValue = operation.progress;
            //由于进度AsyncOperation的对象的progress最大值为0.9
            //因此需要使其手动为1
            if (operation.progress >= 0.9f)
            {
                targetValue = 1.0f;
            }
            //给currentProgress赋值(状态机给progress赋值)
            switch (AsyncLoadSceneManager.instance.sliderMode)
            {
                case ProgressSliderMode.Image:
                    currentProgress = progressCircle.fillAmount;
                    break;
                case ProgressSliderMode.Slider:
                    currentProgress = progressSlider.value;
                    break;
                case ProgressSliderMode.Both:
                    currentProgress = progressSlider.value;
                    break;
                case ProgressSliderMode.Nothing:
                    break;
                default:
                    break;
            }
            //更新UI数值
            if (targetValue != currentProgress)
            {
                //实现平滑加载
                currentProgress = Mathf.Lerp(currentProgress,targetValue,Time.deltaTime * loadingSpeed);
                if (Mathf.Abs(currentProgress - targetValue) < 0.01f)
                {
                    currentProgress = 1.0f;
                }
                UpdateProgressUI(currentProgress);
            }
            //如果当前的进度值为1，说明加载场景已经完成，这时候将加载完的场景显示出来
            if (currentProgress == 1.0f)
            {
                //允许加载完毕进行自动切换场景
                operation.allowSceneActivation = true;
            }
        }
        /// <summary>
        /// 更新UI元素
        /// </summary>
        private void UpdateProgressUI(float sliderNum)
        {
            switch (AsyncLoadSceneManager.instance.sliderMode)
            {
                case ProgressSliderMode.Image:
                    progressCircle.fillAmount = sliderNum;
                    break;
                case ProgressSliderMode.Slider:
                    progressSlider.value = sliderNum;
                    break;
                case ProgressSliderMode.Both:
                    progressSlider.value = sliderNum;
                    progressCircle.fillAmount = sliderNum;
                    break;
                case ProgressSliderMode.Nothing:
                    break;
                default:
                    break;
            }
            if (AsyncLoadSceneManager.instance.isShowNum)
            {
                loadingText.text = ((int)(sliderNum* 100)).ToString() + "%";
            }     
        }
    }
}
