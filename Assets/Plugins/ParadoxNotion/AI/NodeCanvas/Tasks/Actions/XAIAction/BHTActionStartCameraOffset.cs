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
    [Name("相机移动")]
    [Description("相机移动")]
    [Category("剧情")]
    [System.Serializable]
    public class BHTActionStartCameraOffset : ActionTask
    {
        public BHTActionStartCameraOffset()
        {
            detailType = EnumBTNodeAction.AINODE_START_CAMERA_OFFSET;
        }

        [Name("参数")]
        public XBHTStartCameraOffsetConfig cameraConfig = new XBHTStartCameraOffsetConfig();

        protected override string info
        {
            get
            {
                string tempString;
                    tempString = string.Format("相机偏移:x[:" + cameraConfig.xOffset + "] z: " + "[" +
                    cameraConfig.zOffset +
                    "] 缓动帧数:[" + cameraConfig.smoothMsTime + "]");
              
                return tempString;
            }
        }

        public Pbe.AIActionStartCameraOffsetConfig ToProto()
        {
            Pbe.AIActionStartCameraOffsetConfig cfg = new Pbe.AIActionStartCameraOffsetConfig()
            {
                XOffset = cameraConfig.xOffset,
                ZOffset = cameraConfig.zOffset,
                SmoothMsTime = cameraConfig.smoothMsTime,
            };
            return cfg;
        }
    }


    [System.Serializable]
    public class XBHTStartCameraOffsetConfig
    {

        [Name("相机X偏移")]
        [SerializeField]
        public float xOffset = 0;

        [Name("相机Z偏移")]
        [SerializeField]
        public float zOffset = 0;

        [Name("缓动时间(ms)")]
        [SerializeField]
        public int smoothMsTime = 0;

    }
}