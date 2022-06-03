using AINodeCanvas.Framework;
using AIParadoxNotion.Design;
using UnityEngine;

namespace AINodeCanvas.Tasks.Actions
{
    [ActorNode, SceneNode]
    [Name("移除场景装饰物")]
    [Category("指令")]
    [Description("移除场景装饰物")]
    public class BHTActionRemoveSceneItem : ActionTask
    {
        public BHTActionRemoveSceneItem()
        {
            detailType = EnumBTNodeAction.AINODE_REMOVE_SCENE_ITEM;
        }

        [Name("装饰物层级")]
        public EnumSceneLayer sceneLayer = EnumSceneLayer.ForeLayer;

        [Name("装饰物名字")]
        public string decoratorName = "";

        protected override string info
        {
            get
            {
                return $"删除 {sceneLayer}层的装饰物'{decoratorName}'" ;
            }
        }

        public Pbe.AIActionRemoveSceneItem ToProto()
        {
            string removeDecoratorName = "Map/";
            switch (sceneLayer)
            {
                case EnumSceneLayer.ForeLayer:
                    removeDecoratorName += "ForeLayerRoot/";
                    break;
                case EnumSceneLayer.CloseLayer:
                    removeDecoratorName += "CloseLayerRoot/";
                    break;
                case EnumSceneLayer.GroundLayer:
                    removeDecoratorName += "GroundLayerRoot/";
                    break;
                case EnumSceneLayer.MiddleLayer:
                    removeDecoratorName += "MiddleLayerRoot/";
                    break;
                case EnumSceneLayer.FarLayer:
                    removeDecoratorName += "FarLayerRoot/";
                    break;
                default:
                    break;
            }
            removeDecoratorName += decoratorName;
            var cfg = new Pbe.AIActionRemoveSceneItem()
            {
                RemoveDecoratorName = removeDecoratorName,
            };
            return cfg;
        }
    }

    public enum EnumSceneLayer
    {
        //前景
        ForeLayer,
        //近景
        CloseLayer,
        //地面
        GroundLayer,
        //中景
        MiddleLayer,
        //远景
        FarLayer,
    }
}
