/*
 * @Descripttion: AI行为-巡逻
 * @Author: colecai
 * @Date: 2019-11-28 10:58:21
 */
using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;
using AINodeCanvas.Tasks.Conditions;


namespace AINodeCanvas.Tasks.Actions
{
    [SceneNode]
    [ActorNode]
    [Name("相机恢复")]
    [Description("相机恢复")]
    [Category("剧情")]
    [System.Serializable]
    public class BHTActionCameraReset : ActionTask
    {
        public BHTActionCameraReset()
        {
            detailType = EnumBTNodeAction.AINODE_CAMERA_RESET;
        }

        [Name("参数")]
        public XBHTCameraResetConfig cameraConfig = new XBHTCameraResetConfig();

        protected override string info
        {
            get
            {
                string tempString;
                tempString = string.Format("相机恢复 缓动帧数:[" + cameraConfig.smoothTime + "]");

                return tempString;
            }
        }

        public Pbe.AIActionCameraResetConfig ToProto()
        {
            Pbe.AIActionCameraResetConfig cfg = new Pbe.AIActionCameraResetConfig()
            {
                SmoothMsTime = cameraConfig.smoothTime,
            };
            return cfg;
        }
    }


    [System.Serializable]
    public class XBHTCameraResetConfig
    {
        [Name("缓动时间(ms)")]
        [SerializeField]
        public int smoothTime = 0;
    }
}