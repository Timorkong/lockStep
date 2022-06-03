#if UNITY_EDITOR

using System;

namespace AINodeCanvas
{

    //组合节点类型
    public enum EnumCompositesPriority
    {
        Sequencer = 100, //顺序节点
        Selector = 99,   //选择节点
        Parallel = 98, //平行节点
        PrioritySelector = 97, //优先选择
        ProbabilitySelector = 96,   //概率选择
        StepIterator = 95, //步骤迭代器
        Switch = 94,    //开关节点
    }

    //修饰节点类型
    public enum EnumDecoratorsPriority
    {
        Repeater = 100, //重复节点
    }


    /**
    * 行为树--节点类型
    */
    public enum EnumBTNodeType
    {
        /** 选择 */
        AINODE_TYPE_SELECT_CONTROL = 0,
        /** 顺序 */
        AINODE_TYPE_SEQUEUE_CONTROL = 1,
        /** 条件 */
        AINODE_TYPE_CONDITION = 2,
        /** 行为 */
        AINODE_TYPE_ACTION = 3,
        /** 装饰/限制 */
        AINODE_TYPE_DECORATOR = 4,
        /** 并行 */
        AINODE_TYPE_PARALLEL = 5,
        /** 并行 (且) */
        AINODE_TYPE_PARALLEL_AND_CONTROL = 6,
        /** 随机选择 */
        AINODE_TYPE_RANDOM_SELECT_CONTROL = 7,
        /** 二元左右选择 */
        AINODE_TYPE_BINARY_SELECT_CONTROL = 8
    }

    /// <summary>
    /// 条件
    /// </summary>
    public enum EnumBTNodeCondition
    {
        /** 概率 */
        AINODE_CONDITION_PROBABILITY = 0,
        /** 距离 */
        AINODE_CONDITION_DISTANCE = 1,
        /** 时间间隔 */
        AINODE_CONDITION_TIME = 2,
        /** 血量 */
        AINODE_CONDITION_HP = 3,
        /** 目标活着 */
        AINODE_CONDITION_TARGET_ALIVE = 4,
        /** 被击数 */
        AINODE_CONDITION_BEHIT_COUNT = 5,
        /** 有目标 */
        AINODE_CONDITION_HAVE_TARGET = 6,
        /** 移动时间 */
        AINODE_CONDITION_MOVE_TIME = 7,
        /** 已无效 */
        AINODE_CONDITION_IS_REST = 8,
        /** 被打倒地 */
        AINODE_CONDITION_BEATTACK_FALL = 9,
        /** 当前状态 */
        AINODE_CONDITION_NOW_STATE = 10,
        /** 上一次状态 */
        AINODE_CONDITION_LAST_STATE = 11,
        /** 所有目标死亡 */
        AINODE_CONDITION_ALL_TARGET_DEAD = 12,
        /** 正在攻击 */
        AINODE_CONDITION_IS_ATTACKING = 13,
        /** 目标倒地 */
        AINODE_CONDITION_TARGET_FALL = 14,
        /** 目标在攻击范围 */
        AINODE_CONDITION_TARGET_INRANGE = 15,
        /** 目标正在攻击 */
        AINODE_CONDITION_TARGET_ATTACKING = 16,
        /** AI时间 */
        AINODE_CONDITION_RUNNING_TIME = 17,
        /** 攻击列表为空 */
        AINODE_CONDITION_ATTACK_QUEUE_IS_EMPTY = 18,
        /**倒地起身*/
        AINODE_CONDITION_FALL_AND_GET_UP = 19,
        /**判断敌人周边的队友*/
        AINODE_CONDITION_FRIND_INTARGET_RANGE = 20,
        /**判断X轴和Z轴距离*/
        AINODE_CONDITION_DISTANCE_XZ = 21,
        /**是否面向目标*/
        AINODE_CONDITION_FACE_TARGET = 22,
        /**AI智力大于配置*/
        AINODE_CONDITION_INTELLIGENCE = 23,
        /**获取黑板整数值和指定值进行对比*/
        AINODE_CONDITION_CHECK_BB_INTEGER = 24,
        /**是否有指定buff*/
        AINODE_CONDITION_HAS_BUFF = 25,
        /**是否在使用技能*/
        AINODE_CONDITION_USING_SKILL = 26,
        /**是否可以释放技能*/
        AINODE_CONDITION_CANUSE_SKILL = 27,
        /**是否可在被控制状态*/
        AINODE_CONDITION_IN_CONTROL = 28,
        /**检测单位状态*/
        AINODE_CONDITION_UNIT_STATE = 29,
        /**判断伤害元素属性*/
        AINODE_CONDITION_ELEMENT_DAMAGE = 30,
        /**总是成功*/
        AINODE_CONDITION_ALWAYS_TRUE = 31,
        /**总是失败*/
        AINODE_CONDITION_ALWAYS_FALSE = 32,
    };

    /// <summary>
    /// 行为节点
    /// </summary>
    public enum EnumBTNodeAction
    {
        /*多节点合一*/
        AINODE_COMBINE = 0,
        /** 巡逻 */
        AINODE_ACTION_PATROL = 1,
        /** 找一个目标 */
        AINODE_ACTION_FIND_TARGET = 2,
        /** 靠近目标 */
        AINODE_ACTION_CLOST_TARGET = 3,
        /** 飞行道具 */
        AINODE_ACTION_SPRITE = 4,
        /** 攻击 */
        AINODE_ACTION_SKILL = 5,
        /** 进入状态 */
        AINODE_ACTION_ENTER_STATE = 6,
        /** 进入巡逻状态 */
        AINODE_ACTION_ENTER_PATROL_STATE = 7,
        /** 发起攻击 */
        AINODE_ACTION_TRIGGLE_ATTACK = 8,
        /** 思考 */
        AINODE_ACTION_THINK = 9,
        /** 清除之前攻击 */
        AINODE_ACTION_CLEAR_ATTACK = 10,
        /** 设置朝向 */
        AINODE_ACTION_SET_FACE = 11,
        /** 随机一招 */
        AINODE_ACTION_RANDOM_ATTACK = 12,
        /** 随机一个技能 */
        AINODE_ACTION_RANDOM_SILL = 13,
        /** 躲避 */
        AINODE_ACTION_DODGE = 14,
        /** 移动 */
        AINODE_ACTION_MOVE = 15,
        /** 跟随队长 */
        AINODE_ACTION_FOLLOW_CAPTAIN = 16,
        /** Buff */
        AINODE_ACTION_BUFF = 17,
        /** AI触发事件 */
        AINODE_ACTION_TRIGGER_EVENT = 18,
        /** 进入自定义状态 */
        AINODE_ACTION_ENTER_CUSTOM_STATE = 19,
        /**待机*/
        AINODE_ACTION_IDLE = 20,
        /**时间停止*/
        AINODE_TIME_STOP = 21,
        /**保持距离跟随敌人*/
        AINODE_FOLLOW_TARGET = 22,
        /**进入迂回靠近敌人状态*/
        AINODE_ACTION_CYCLE_CLOSE = 23,
        /**迂回靠近*/
        AINODE_ACTION_CLOSE = 24,
        /**随机技能可配置*/
        AINODE_ACTION_RANDOM_SKILL_SET = 25,
        /**移动方向锁定*/
        AINODE_MOVE_LOCK = 26,
        /**更换目标*/
        AINODE_CHANGE_TARGET = 27,
        /**文字框*/
        AINODE_TEXT_BUBBLE = 28,
        /**直接靠近*/
        AINODE_DIRECTLY_APPROACH = 29,
        /**远离目标*/
        AINODE_AWAY_TARGET = 30,
        /**游走*/
        AINODE_WANDER = 31,
        /**Z字靠近*/
        AINODE_ZIG_APPROACH = 32,
        /**获取唯一ID*/
        AINODE_GET_OWNER_ID = 33,
        /**获取目标唯一ID*/
        AINODE_GET_TARGET_ID = 34,
        /**执行节点树*/
        AINODE_ACTION_EXECUTE_TREE = 35,
        /**等待*/
        AINODE_ACTION_WAIT = 36,
        /**设置黑板整数值*/
        AINODE_ACTION_SET_BB_INTEGER = 37,
        /**释放技能*/
        AINODE_ACTION_DO_SKILL = 38,
        /**执行动作*/
        AINODE_ACTION_DO_ACTION = 39,
        /**添加Buff Info */
        AINODE_ACTION_ADD_BUFF_INFO = 40,
        /**删除buff*/
        AINODE_ACTION_REMOVE_BUFF = 41,
        /**删除buffinfo*/
        AINODE_ACTION_REMOVE_BUFF_INFO = 42,
        /**获取场景中单位的唯一ID*/
        AINODE_GET_UNIT_ID = 43,
        /**获取范围内怪物数量*/
        AINODE_ACTION_GET_MONSTER_COUNT_IN_RANGE = 44,
        /**剧情游荡*/
        AINODE_ACTION_LOAF = 45,
        /**自杀*/
        AINODE_ACTION_SUICIDE = 46,
        /**开启黑幕*/
        AINODE_BLACK_BAR_SHOW = 47,
        /**关闭黑幕*/
        AINODE_BLACK_BAR_HIDE = 48,
        /**停止剧情*/
        AINODE_END_STORY = 49,
        /**获取玩家唯一ID*/
        AINODE_GET_PLAYER_UNIT_ID = 50,
        /**找背靠近*/
        AINODE_FINDBACK_APPROACH = 51,
        /**开启战斗引导*/
        AINODE_START_BATTLE_TIPS = 52,
        /**开启剧情对话*/
        AINODE_SCENARIO = 53,
        /**设置按键状态*/
        AINODE_BUTTON_STATE = 54,
        /**开始相机移动*/
        AINODE_START_CAMERA_OFFSET = 55,
        /**相机恢复*/
        AINODE_CAMERA_RESET = 56,
        /**设置位置*/
        AINODE_ACTION_SET_POSITION = 57,
        /**获取怪物唯一ID*/
        AINODE_GET_MONSTER_UNIT_ID = 58,
        /**攻击列表*/
        AINODE_ATTACK_LIST = 59,
        /**获取关卡难度*/
        AINODE_GET_SECTION_DIF = 60,
        /**移除场景装饰物*/
        AINODE_REMOVE_SCENE_ITEM = 61,
        /**开启黑幕*/
        AINODE_FULL_SCREEN_MASK_SHOW = 62,
    };

    /// <summary>
    /// 修饰节点
    /// </summary>
    public enum EnumBTNodeDecorator
    {
        /** 结果取反 */
        AINODE_DECORATOR_REVERSAL,
        /** 触发次数 */
        AINODE_DECORATOR_REPEAT,
        /** 时间间隔(帧) */
        AINODE_DECORATOR_TIME_INTERVAL,
    };

    /// <summary>
    /// 对比
    /// </summary>
    public enum EnumBTNodeCompareSign
    {
        等于 = 0,
        不等于 = 1,
        大于 = 2,
        小于 = 3,
        大于等于 = 4,
        小于等于 = 5,
    };

    public enum EnumBTNodeValueRefer
    {
        具体数值 = 0,
        百分比 = 1,
    }

    //多条件逻辑关系
    public enum EnumBTNodeCoditionCal
    {
        且 = 1,
        或 = 2,
    };


    //方向
    public enum EnumBTNodeDirction
    {
        向左 = 0,
        左下 = 1,
        向下 = 2,
        右下 = 3,
        向右 = 4,
        右上 = 5,
        向上 = 6,
        左上 = 7,
    };

    // public enum EnumAIState
    // {
    //     STATE_STOP = -1,    //停止
    //     STATE_NONE = 0,//未启动
    //     STATE_ATTACK = 1,//移动
    //     STATE_MOVE = 2,//攻击
    //     STATE_PATROL = 3,//游荡
    // };

    public enum EnumAIState
    {
        停止 = -1,    //停止
        未启动 = 0,//未启动
        移动 = 1,//移动
        攻击 = 2,//攻击
        巡逻 = 3,//巡逻
        被攻击 = 4,       //被攻击
    };

    public enum EnumBTNodeTarget
    {
        AINODE_TARGET_SELF = 0,
        AINODE_TARGET_TARGET = 1,
    };

    public enum EnumBTNodeCloseAction
    {
        远离 = 0,
        靠近 = 1,
    };

    //单位攻击类型
    public enum EnumSkillActionType
    {
        BeBreak = -1, //被动打断技能
        NONE = 0,
        // 1-8 待机，休闲待机，切换动作
        STANDBY = 1,            // 战斗待机
        IDLE_01 = 2,            // 休闲待机
        IDLE_02 = 3,            // 休闲动作
        IDLE_03 = 4,            // 休闲待机
        IDLE_04 = 5,
        IDLE_05 = 6,
        IDLE_06 = 7,
        IDLE_MAX = 10,

        // 10-19 移动相关
        WALK = 10,              // 走
        RUN = 11,               // 跑
        MOVE_MAX = 13,

        // 40-59 受击动作
        BEHIT_01 = 40,         // 地面受击1
        BEHIT_02 = 41,         // 地面受击2
        BEHIT_UP03 = 42,       // 击飞上升（浮空通用动作）
        BEHIT_DOWN = 43,       // 击飞下落（浮空通用动作）
        BEHIT_TAN = 44,        // 倒地受击
        BEHIT_LIE = 45,        // 倒地
        BOUNCE_UP = 46,        // 弹地上升
        BOUNCE_DOWN = 47,      // 弹地下落
        BEHIT_UP01 = 48,       // 浮空通用动作1
        BEHIT_UP02 = 49,       // 浮空通用动作2
        FALL = 50,  // 地面受击5(暂时不用)
        BEHIT_MAX = 51,

        // 60-99 通用动作
        UPLEVEL = 59,          // 升级
        JUMP = 60,
        JUMP_BACK = 61,        // 后跳
        DIE = 62,              // 死亡
        GET_UP = 63,           // 起身
        STUN = 64,             // 眩晕
        LEAVE = 65,            // 离场
        WIN = 66,              // 胜利
        LOSE = 67,             // 失败
        REVIVE = 68,           // 复活
        SQUAT = 69,            // 蹲伏
        COMMON_MAX = 70,

        // 71 - 89 出场动作
        BORN_01 = 71,
        BORN_MAX = 90,

        // 100 - 199 技能
        SKILL = 100,        // 技能1
        SKILL_MAX = 1000000,    // 技能最大值
    }

    public enum EnumXAIMoveType
    {
        WALK = EnumSkillActionType.WALK,
        RUN = EnumSkillActionType.RUN,
    }

    public enum EnumXAIThinkType
    {
        DEFAULT = 1,
        RANDOM = 2,
    }

    public enum EnumXAIMoveAction
    {
        移动固定距离 = 1,  //超目标移动固定距离
        后退固定距离 = 2,   //
        后退至目标固定距离 = 3, //朝目标后退，移动距离目标固定距离
        始终面向目标移动固定距离 = 4,
    }

    public enum EnumXAIDistanceType
    {
        在自己攻击范围 = 1,
        距离固定值 = 2,
    }

    //与目标进行判定
    public enum EnumInTargetRange
    {
        在目标身前 = 0, //前
        在目标身后 = 1, //后
        目标全范围 = 2,
    }

    //攻击状态
    public enum EnumAttackingState
    {
        在攻击 = 0,
        不在攻击 = 1,
    }

    public enum EnumBreakType
    {
        绝对距离 = 0,
        相对距离 = 1,
    }

    public enum EnumFaceTo
    {
        面向目标 = 0,
        背向目标 = 1,
    }

    public enum EnumFace
    {
        左 = 0,
        右 = 1,
    }

    public enum EnumCloseType
    {
        直接靠近 = 1,
        Z字靠近 = 2,
    }

    public enum EnumDirectionFirst
    {
        X轴优先 = 1,
        Y轴优先 = 2,
    }

    public enum EnumWanderTarget
    {
        自身游走 = 1,
        目标游走 = 2,
    }

    public enum EnumAxis
    {
        X轴 = 1,
        Y轴 = 2,
    }

    public enum EnumUnitState
    {
        死亡 = 1,
        受击 = 2,
        倒地 = 3,
        浮空 = 4,
    }

    public enum EnumSkillPos
    {
        Joystick = 0,
        Skill1 = 1,
        Skill2 = 2,
        Skill3 = 3,
        Skill4 = 4,
        Skill5 = 5,
        Skill6 = 6,
        Skill7 = 7,
        Skill8 = 8,
        Skill9 = 9,
        Skill10 = 10,
        Skill11 = 11,
        Skill12 = 12,
        Skill13 = 13,
        Skill14 = 14,
        Skill15 = 15,
        Attack = 100,       // 普攻
        Squat = 101,        //蹲伏
        Big = 201,          // 大招
        Jump = 301,         // 跳跃
        JumpBack = 302,     // 后跳
        Buff1 = 401,        // 主动buff技能
        Buff2 = 402,
        Buff3 = 403,
        Pill1 = 501,        // 药品
        Pill2 = 502,
        Pill3 = 503,
        Weapon = 600,       // 武器
        SPE = 701,         // 特殊技能
    }

    public enum EnumElementType
    {
        无属性 = 0,
        风 = 23,
        雷,
        水,
        火,
        土,
    }

    public static class BehaviourTreeDefine
    {
        /**游戏逻辑1秒30帧*/
        public static int TimeFrame = 30;
    }

    [System.Serializable]
    public class EnterStateParam
    {
        [UnityEngine.HideInInspector]
        public EnumAIState state = EnumAIState.未启动;

        public EnterStateParam(EnumAIState _state)
        {
            state = _state;
        }

    }

}

#endif