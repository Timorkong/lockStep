using AINodeCanvas.Framework;
using AIParadoxNotion.Design;

namespace AINodeCanvas.Tasks.Actions
{
    [SceneNode]
    [Name("开启剧情对话")]
    [Category("剧情")]
    [System.Serializable]
    public class BHTActionScenario : ActionTask
    {
        public BHTActionScenario()
        {
            detailType = EnumBTNodeAction.AINODE_SCENARIO;
        }

        [Name("对话ID")]
        public int scenarioID = 0;

        protected override string info
        {
            get
            {
                return $"开启剧情对话: {scenarioID}";
            }
        }

        public Pbe.AIActionScenarioConfig ToProto()
        {
            Pbe.AIActionScenarioConfig cfg = new Pbe.AIActionScenarioConfig()
            {
                ScenarioID = scenarioID,
            };
            return cfg;
        }
    }

}
