using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
namespace ExcelToolNS
{
    public class ExcelDataManager
    {
        public static string defaultCommonPath = Path.GetFullPath(Path.Combine(Application.dataPath, "../../../config/Config/Common"));

        public static string currentPath = Path.GetFullPath(Path.Combine(Application.dataPath, "../../../config/Config/Common"));

        private Dictionary<int, AudioBattle> _AudioBattle;
        public Dictionary<int, AudioBattle> AudioBattle { get { if (_AudioBattle == null) LoadAudioBattle(); return _AudioBattle; } }
        private Dictionary<int, Buff> _Buff;
        public Dictionary<int, Buff> Buff { get { if (_Buff == null) LoadBuff(); return _Buff; } }
        private Dictionary<int, BuffInfo> _BuffInfo;
        public Dictionary<int, BuffInfo> BuffInfo { get { if (_BuffInfo == null) LoadBuffInfo(); return _BuffInfo; } }
        private Dictionary<int, CameraShake> _CameraShake;
        public Dictionary<int, CameraShake> CameraShake { get { if (_CameraShake == null) LoadCameraShake(); return _CameraShake; } }
        private Dictionary<int, DestructibleProp> _DestructibleProp;
        public Dictionary<int, DestructibleProp> DestructibleProp { get { if (_DestructibleProp == null) LoadDestructibleProp(); return _DestructibleProp; } }
        private Dictionary<int, Effect> _Effect;
        public Dictionary<int, Effect> Effect { get { if (_Effect == null) LoadEffect(); return _Effect; } }
        private Dictionary<int, Fashion> _Fashion;
        public Dictionary<int, Fashion> Fashion { get { if (_Fashion == null) LoadFashion(); return _Fashion; } }
        private Dictionary<int, FashionAttr> _FashionAttr;
        public Dictionary<int, FashionAttr> FashionAttr { get { if (_FashionAttr == null) LoadFashionAttr(); return _FashionAttr; } }
        private Dictionary<int, FashionSuit> _FashionSuit;
        public Dictionary<int, FashionSuit> FashionSuit { get { if (_FashionSuit == null) LoadFashionSuit(); return _FashionSuit; } }
        private Dictionary<int, FashionProp> _FashionProp;
        public Dictionary<int, FashionProp> FashionProp { get { if (_FashionProp == null) LoadFashionProp(); return _FashionProp; } }
        private Dictionary<int, FashionTitle> _FashionTitle;
        public Dictionary<int, FashionTitle> FashionTitle { get { if (_FashionTitle == null) LoadFashionTitle(); return _FashionTitle; } }
        private Dictionary<int, FashionDecompose> _FashionDecompose;
        public Dictionary<int, FashionDecompose> FashionDecompose { get { if (_FashionDecompose == null) LoadFashionDecompose(); return _FashionDecompose; } }
        private Dictionary<int, Item> _Item;
        public Dictionary<int, Item> Item { get { if (_Item == null) LoadItem(); return _Item; } }
        private Dictionary<int, Monster> _Monster;
        public Dictionary<int, Monster> Monster { get { if (_Monster == null) LoadMonster(); return _Monster; } }
        private Dictionary<int, Skillinfo> _Skillinfo;
        public Dictionary<int, Skillinfo> Skillinfo { get { if (_Skillinfo == null) LoadSkillinfo(); return _Skillinfo; } }
        private Dictionary<int, WeaponRes> _WeaponRes;
        public Dictionary<int, WeaponRes> WeaponRes { get { if (_WeaponRes == null) LoadWeaponRes(); return _WeaponRes; } }
        public void LoadAll(string path = "")
        {
            if (string.IsNullOrEmpty(path))
            {
                path = defaultCommonPath;
            }
            currentPath = path;
            LoadAudioBattle();
            LoadBuff();
            LoadBuffInfo();
            LoadCameraShake();
            LoadDestructibleProp();
            LoadEffect();
            LoadFashion();
            LoadFashionAttr();
            LoadFashionSuit();
            LoadFashionProp();
            LoadFashionTitle();
            LoadFashionDecompose();
            LoadItem();
            LoadMonster();
            LoadSkillinfo();
            LoadWeaponRes();
        }

        public void LoadAudioBattle()
        {
            _AudioBattle = AudioBattleRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"AudioBattle.xlsx"), "AudioBattle"));
        }
        public void LoadBuff()
        {
            _Buff = BuffRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"Buff.xlsx"), "Buff"));
        }
        public void LoadBuffInfo()
        {
            _BuffInfo = BuffInfoRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"Buff.xlsx"), "BuffInfo"));
        }
        public void LoadCameraShake()
        {
            _CameraShake = CameraShakeRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"CameraShake.xlsx"), "CameraShake"));
        }
        public void LoadDestructibleProp()
        {
            _DestructibleProp = DestructiblePropRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"Destructible.xlsx"), "DestructibleProp"));
        }
        public void LoadEffect()
        {
            _Effect = EffectRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"Effect.xlsx"), "Effect"));
        }
        public void LoadFashion()
        {
            _Fashion = FashionRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"Fashion.xlsx"), "Fashion"));
        }
        public void LoadFashionAttr()
        {
            _FashionAttr = FashionAttrRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"Fashion.xlsx"), "FashionAttr"));
        }
        public void LoadFashionSuit()
        {
            _FashionSuit = FashionSuitRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"Fashion.xlsx"), "FashionSuit"));
        }
        public void LoadFashionProp()
        {
            _FashionProp = FashionPropRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"Fashion.xlsx"), "FashionProp"));
        }
        public void LoadFashionTitle()
        {
            _FashionTitle = FashionTitleRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"Fashion.xlsx"), "FashionTitle"));
        }
        public void LoadFashionDecompose()
        {
            _FashionDecompose = FashionDecomposeRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"Fashion.xlsx"), "FashionDecompose"));
        }
        public void LoadItem()
        {
            _Item = ItemRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"Item.xlsx"), "Item"));
        }
        public void LoadMonster()
        {
            _Monster = MonsterRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"Monster.xlsx"), "Monster"));
        }
        public void LoadSkillinfo()
        {
            _Skillinfo = SkillinfoRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"Skill.xlsx"), "Skillinfo"));
        }
        public void LoadWeaponRes()
        {
            _WeaponRes = WeaponResRW.LoadDict(ExcelUtils.ReadExcelSheetData(Path.Combine(currentPath, @"Weapon.xlsx"), "WeaponRes"));
        }
    }

}
namespace ExcelToolNS
{
    public class DTItemNum
    {
        public int itemId;
        public int itemNum;
        public static DTItemNum Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTItemNum();
            data.itemId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.itemNum = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTItemNum[] Unpack(string[] strArr)
        {
            var arr = new List<DTItemNum>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTRewardRandom
    {
        public int weight;
        public int bindStatus;
        public int itemId;
        public int itemNum;
        public static DTRewardRandom Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTRewardRandom();
            data.weight = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.bindStatus = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.itemId = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            data.itemNum = ExcelUtils.ParseInt(arr?.Length > 3 ? arr[3] : "");
            return data;
        }
        public static DTRewardRandom[] Unpack(string[] strArr)
        {
            var arr = new List<DTRewardRandom>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTStoryNum
    {
        public int star;
        public int num;
        public static DTStoryNum Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTStoryNum();
            data.star = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.num = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTStoryNum[] Unpack(string[] strArr)
        {
            var arr = new List<DTStoryNum>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTVector2
    {
        public float x;
        public float y;
        public static DTVector2 Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTVector2();
            data.x = ExcelUtils.ParseFloat(arr?.Length > 0 ? arr[0] : "");
            data.y = ExcelUtils.ParseFloat(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTVector2[] Unpack(string[] strArr)
        {
            var arr = new List<DTVector2>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTActorSkill
    {
        public int groupId;
        public int lv;
        public static DTActorSkill Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTActorSkill();
            data.groupId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.lv = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTActorSkill[] Unpack(string[] strArr)
        {
            var arr = new List<DTActorSkill>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTActorStory
    {
        public int story;
        public int lv;
        public int property;
        public static DTActorStory Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTActorStory();
            data.story = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.lv = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.property = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTActorStory[] Unpack(string[] strArr)
        {
            var arr = new List<DTActorStory>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTActorFeature
    {
        public int feature;
        public int lv;
        public int property;
        public static DTActorFeature Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTActorFeature();
            data.feature = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.lv = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.property = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTActorFeature[] Unpack(string[] strArr)
        {
            var arr = new List<DTActorFeature>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTActorCharacter
    {
        public int character;
        public int lv;
        public int property;
        public static DTActorCharacter Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTActorCharacter();
            data.character = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.lv = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.property = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTActorCharacter[] Unpack(string[] strArr)
        {
            var arr = new List<DTActorCharacter>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTRandomInterval
    {
        public int num1;
        public int num2;
        public static DTRandomInterval Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTRandomInterval();
            data.num1 = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.num2 = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTRandomInterval[] Unpack(string[] strArr)
        {
            var arr = new List<DTRandomInterval>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTShootType
    {
        public int shoottypeid;
        public int rate;
        public static DTShootType Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTShootType();
            data.shoottypeid = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.rate = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTShootType[] Unpack(string[] strArr)
        {
            var arr = new List<DTShootType>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTStoryEffect
    {
        public int storyeffect;
        public int rate;
        public static DTStoryEffect Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTStoryEffect();
            data.storyeffect = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.rate = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTStoryEffect[] Unpack(string[] strArr)
        {
            var arr = new List<DTStoryEffect>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTBuildingReq
    {
        public int type;
        public int level;
        public static DTBuildingReq Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTBuildingReq();
            data.type = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.level = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTBuildingReq[] Unpack(string[] strArr)
        {
            var arr = new List<DTBuildingReq>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTValue2
    {
        public int v1;
        public int v2;
        public static DTValue2 Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTValue2();
            data.v1 = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.v2 = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTValue2[] Unpack(string[] strArr)
        {
            var arr = new List<DTValue2>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTValue3
    {
        public int v1;
        public int v2;
        public int v3;
        public static DTValue3 Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTValue3();
            data.v1 = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.v2 = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.v3 = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTValue3[] Unpack(string[] strArr)
        {
            var arr = new List<DTValue3>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTValue4
    {
        public int v1;
        public int v2;
        public int v3;
        public int v4;
        public static DTValue4 Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTValue4();
            data.v1 = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.v2 = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.v3 = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            data.v4 = ExcelUtils.ParseInt(arr?.Length > 3 ? arr[3] : "");
            return data;
        }
        public static DTValue4[] Unpack(string[] strArr)
        {
            var arr = new List<DTValue4>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTDrop
    {
        public int x;
        public float y;
        public static DTDrop Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTDrop();
            data.x = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.y = ExcelUtils.ParseFloat(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTDrop[] Unpack(string[] strArr)
        {
            var arr = new List<DTDrop>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTPerformActoin
    {
        public int v1;
        public int v2;
        public int v3;
        public static DTPerformActoin Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTPerformActoin();
            data.v1 = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.v2 = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.v3 = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTPerformActoin[] Unpack(string[] strArr)
        {
            var arr = new List<DTPerformActoin>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTPerformSpeak
    {
        public int v1;
        public int v2;
        public int v3;
        public static DTPerformSpeak Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTPerformSpeak();
            data.v1 = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.v2 = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.v3 = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTPerformSpeak[] Unpack(string[] strArr)
        {
            var arr = new List<DTPerformSpeak>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTParts
    {
        public string v1;
        public string v2;
        public static DTParts Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTParts();
            data.v1 = arr?.Length > 0 ? arr[0] : "";
            data.v2 = arr?.Length > 1 ? arr[1] : "";
            return data;
        }
        public static DTParts[] Unpack(string[] strArr)
        {
            var arr = new List<DTParts>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTProficiency
    {
        public int v1;
        public int v2;
        public static DTProficiency Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTProficiency();
            data.v1 = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.v2 = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTProficiency[] Unpack(string[] strArr)
        {
            var arr = new List<DTProficiency>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTDate
    {
        public int y;
        public int m;
        public int d;
        public static DTDate Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTDate();
            data.y = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.m = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.d = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTDate[] Unpack(string[] strArr)
        {
            var arr = new List<DTDate>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTTime
    {
        public int h;
        public int mm;
        public int s;
        public static DTTime Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTTime();
            data.h = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.mm = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.s = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTTime[] Unpack(string[] strArr)
        {
            var arr = new List<DTTime>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTDateTime
    {
        public int y;
        public int m;
        public int d;
        public int h;
        public int mm;
        public int s;
        public static DTDateTime Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTDateTime();
            data.y = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.m = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.d = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            data.h = ExcelUtils.ParseInt(arr?.Length > 3 ? arr[3] : "");
            data.mm = ExcelUtils.ParseInt(arr?.Length > 4 ? arr[4] : "");
            data.s = ExcelUtils.ParseInt(arr?.Length > 5 ? arr[5] : "");
            return data;
        }
        public static DTDateTime[] Unpack(string[] strArr)
        {
            var arr = new List<DTDateTime>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTFixItemNum
    {
        public int bindStatus;
        public int itemId;
        public int itemNum;
        public static DTFixItemNum Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTFixItemNum();
            data.bindStatus = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.itemId = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.itemNum = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTFixItemNum[] Unpack(string[] strArr)
        {
            var arr = new List<DTFixItemNum>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTGiftNum
    {
        public int itemId;
        public int itemNum;
        public int property;
        public static DTGiftNum Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTGiftNum();
            data.itemId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.itemNum = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.property = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTGiftNum[] Unpack(string[] strArr)
        {
            var arr = new List<DTGiftNum>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTLabelProperty
    {
        public int storyTag;
        public int property;
        public static DTLabelProperty Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTLabelProperty();
            data.storyTag = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.property = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTLabelProperty[] Unpack(string[] strArr)
        {
            var arr = new List<DTLabelProperty>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTInitialLabel
    {
        public int storyTag;
        public int level;
        public int property;
        public static DTInitialLabel Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTInitialLabel();
            data.storyTag = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.level = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.property = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTInitialLabel[] Unpack(string[] strArr)
        {
            var arr = new List<DTInitialLabel>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTPerformanceCondition
    {
        public int lv;
        public int property;
        public int num;
        public static DTPerformanceCondition Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTPerformanceCondition();
            data.lv = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.property = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.num = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTPerformanceCondition[] Unpack(string[] strArr)
        {
            var arr = new List<DTPerformanceCondition>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTSkillTiggerCondition
    {
        public int tiggerConditon;
        public int tiggerValue;
        public static DTSkillTiggerCondition Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTSkillTiggerCondition();
            data.tiggerConditon = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.tiggerValue = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTSkillTiggerCondition[] Unpack(string[] strArr)
        {
            var arr = new List<DTSkillTiggerCondition>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTburstlibrary
    {
        public int itemId;
        public int itemNum;
        public int rate;
        public static DTburstlibrary Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTburstlibrary();
            data.itemId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.itemNum = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.rate = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTburstlibrary[] Unpack(string[] strArr)
        {
            var arr = new List<DTburstlibrary>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTProp
    {
        public int PropID;
        public int Rate;
        public static DTProp Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTProp();
            data.PropID = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.Rate = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTProp[] Unpack(string[] strArr)
        {
            var arr = new List<DTProp>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTRandNum
    {
        public int Num;
        public int Rate;
        public static DTRandNum Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTRandNum();
            data.Num = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.Rate = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTRandNum[] Unpack(string[] strArr)
        {
            var arr = new List<DTRandNum>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTPropGrowupStep
    {
        public int level;
        public int step;
        public static DTPropGrowupStep Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTPropGrowupStep();
            data.level = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.step = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTPropGrowupStep[] Unpack(string[] strArr)
        {
            var arr = new List<DTPropGrowupStep>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTDecompose
    {
        public int percent;
        public int rewardid;
        public static DTDecompose Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTDecompose();
            data.percent = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.rewardid = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTDecompose[] Unpack(string[] strArr)
        {
            var arr = new List<DTDecompose>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTStarReward
    {
        public int level;
        public int starnum;
        public int rewardid;
        public static DTStarReward Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTStarReward();
            data.level = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.starnum = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.rewardid = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTStarReward[] Unpack(string[] strArr)
        {
            var arr = new List<DTStarReward>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTBuffId
    {
        public int buffID1;
        public int buffID2;
        public int buffID3;
        public int buffID4;
        public int buffID5;
        public int buffID6;
        public static DTBuffId Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTBuffId();
            data.buffID1 = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.buffID2 = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.buffID3 = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            data.buffID4 = ExcelUtils.ParseInt(arr?.Length > 3 ? arr[3] : "");
            data.buffID5 = ExcelUtils.ParseInt(arr?.Length > 4 ? arr[4] : "");
            data.buffID6 = ExcelUtils.ParseInt(arr?.Length > 5 ? arr[5] : "");
            return data;
        }
        public static DTBuffId[] Unpack(string[] strArr)
        {
            var arr = new List<DTBuffId>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTPropValue
    {
        public int PropID;
        public int value;
        public static DTPropValue Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTPropValue();
            data.PropID = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.value = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTPropValue[] Unpack(string[] strArr)
        {
            var arr = new List<DTPropValue>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTMapId
    {
        public int chapterId;
        public int mapId;
        public static DTMapId Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTMapId();
            data.chapterId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.mapId = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTMapId[] Unpack(string[] strArr)
        {
            var arr = new List<DTMapId>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTRandomMap
    {
        public int part;
        public int mapNum;
        public static DTRandomMap Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTRandomMap();
            data.part = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.mapNum = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTRandomMap[] Unpack(string[] strArr)
        {
            var arr = new List<DTRandomMap>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTRewardRate
    {
        public int rate;
        public int rewardid;
        public static DTRewardRate Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTRewardRate();
            data.rate = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.rewardid = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTRewardRate[] Unpack(string[] strArr)
        {
            var arr = new List<DTRewardRate>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTGashaponReward
    {
        public int Gashaponid;
        public int times;
        public int rewardid;
        public static DTGashaponReward Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTGashaponReward();
            data.Gashaponid = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.times = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.rewardid = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTGashaponReward[] Unpack(string[] strArr)
        {
            var arr = new List<DTGashaponReward>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTReward
    {
        public int itemid;
        public int itemNum;
        public int rate;
        public static DTReward Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTReward();
            data.itemid = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.itemNum = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.rate = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTReward[] Unpack(string[] strArr)
        {
            var arr = new List<DTReward>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTAddSkillLevel
    {
        public int skillId;
        public int addLevel;
        public static DTAddSkillLevel Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTAddSkillLevel();
            data.skillId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.addLevel = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTAddSkillLevel[] Unpack(string[] strArr)
        {
            var arr = new List<DTAddSkillLevel>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTAmountOfMoney
    {
        public int moneyType;
        public int moneyNum;
        public static DTAmountOfMoney Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTAmountOfMoney();
            data.moneyType = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.moneyNum = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTAmountOfMoney[] Unpack(string[] strArr)
        {
            var arr = new List<DTAmountOfMoney>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTStrengthenPara
    {
        public int strengthenLevel;
        public int para;
        public static DTStrengthenPara Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTStrengthenPara();
            data.strengthenLevel = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.para = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTStrengthenPara[] Unpack(string[] strArr)
        {
            var arr = new List<DTStrengthenPara>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTReleaseType
    {
        public int release;
        public float time;
        public static DTReleaseType Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTReleaseType();
            data.release = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.time = ExcelUtils.ParseFloat(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTReleaseType[] Unpack(string[] strArr)
        {
            var arr = new List<DTReleaseType>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTWeight
    {
        public int sectionId;
        public int weight;
        public static DTWeight Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTWeight();
            data.sectionId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.weight = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTWeight[] Unpack(string[] strArr)
        {
            var arr = new List<DTWeight>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTTrySkill
    {
        public int heroId;
        public int skillId;
        public static DTTrySkill Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTTrySkill();
            data.heroId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.skillId = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTTrySkill[] Unpack(string[] strArr)
        {
            var arr = new List<DTTrySkill>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTGroupRewardRandom
    {
        public int group;
        public int weight;
        public int bindStatus;
        public int itemId;
        public int itemNum;
        public static DTGroupRewardRandom Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTGroupRewardRandom();
            data.group = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.weight = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.bindStatus = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            data.itemId = ExcelUtils.ParseInt(arr?.Length > 3 ? arr[3] : "");
            data.itemNum = ExcelUtils.ParseInt(arr?.Length > 4 ? arr[4] : "");
            return data;
        }
        public static DTGroupRewardRandom[] Unpack(string[] strArr)
        {
            var arr = new List<DTGroupRewardRandom>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTTaskByHeroType
    {
        public int heroId;
        public int taskId;
        public static DTTaskByHeroType Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTTaskByHeroType();
            data.heroId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.taskId = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTTaskByHeroType[] Unpack(string[] strArr)
        {
            var arr = new List<DTTaskByHeroType>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTRewardTask
    {
        public int bindStatus;
        public int itemId;
        public int itemNum;
        public static DTRewardTask Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTRewardTask();
            data.bindStatus = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.itemId = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.itemNum = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTRewardTask[] Unpack(string[] strArr)
        {
            var arr = new List<DTRewardTask>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTJobRewardTask
    {
        public int itemId;
        public int itemNum;
        public int jobId;
        public static DTJobRewardTask Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTJobRewardTask();
            data.itemId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.itemNum = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.jobId = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTJobRewardTask[] Unpack(string[] strArr)
        {
            var arr = new List<DTJobRewardTask>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTItemRewardTask
    {
        public int itemId;
        public int itemNum;
        public static DTItemRewardTask Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTItemRewardTask();
            data.itemId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.itemNum = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTItemRewardTask[] Unpack(string[] strArr)
        {
            var arr = new List<DTItemRewardTask>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTFriendGiveRewards
    {
        public int moneyId;
        public int moneyNum;
        public static DTFriendGiveRewards Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTFriendGiveRewards();
            data.moneyId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.moneyNum = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTFriendGiveRewards[] Unpack(string[] strArr)
        {
            var arr = new List<DTFriendGiveRewards>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTRandomProp
    {
        public int weight;
        public float minRatio;
        public float maxRatio;
        public static DTRandomProp Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTRandomProp();
            data.weight = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.minRatio = ExcelUtils.ParseFloat(arr?.Length > 1 ? arr[1] : "");
            data.maxRatio = ExcelUtils.ParseFloat(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTRandomProp[] Unpack(string[] strArr)
        {
            var arr = new List<DTRandomProp>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTAttribute
    {
        public int pvpSceneid;
        public int balanceBag;
        public static DTAttribute Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTAttribute();
            data.pvpSceneid = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.balanceBag = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTAttribute[] Unpack(string[] strArr)
        {
            var arr = new List<DTAttribute>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTFixed
    {
        public int PropID;
        public int value;
        public static DTFixed Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTFixed();
            data.PropID = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.value = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTFixed[] Unpack(string[] strArr)
        {
            var arr = new List<DTFixed>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTPercentage
    {
        public int PropID;
        public int rate;
        public static DTPercentage Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTPercentage();
            data.PropID = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.rate = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTPercentage[] Unpack(string[] strArr)
        {
            var arr = new List<DTPercentage>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTShopLimit
    {
        public int limitType;
        public int limitNum;
        public static DTShopLimit Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTShopLimit();
            data.limitType = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.limitNum = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTShopLimit[] Unpack(string[] strArr)
        {
            var arr = new List<DTShopLimit>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTShopItem
    {
        public int bindStatus;
        public int itemId;
        public static DTShopItem Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTShopItem();
            data.bindStatus = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.itemId = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTShopItem[] Unpack(string[] strArr)
        {
            var arr = new List<DTShopItem>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTStrengthenScore
    {
        public int strengthenLevel;
        public int strengthenScore;
        public static DTStrengthenScore Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTStrengthenScore();
            data.strengthenLevel = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.strengthenScore = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTStrengthenScore[] Unpack(string[] strArr)
        {
            var arr = new List<DTStrengthenScore>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTAutomaticCharging
    {
        public int initialfrequency;
        public int Chargingtime;
        public static DTAutomaticCharging Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTAutomaticCharging();
            data.initialfrequency = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.Chargingtime = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTAutomaticCharging[] Unpack(string[] strArr)
        {
            var arr = new List<DTAutomaticCharging>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTSkillSetting
    {
        public int Prop;
        public int skillId;
        public int front;
        public int back;
        public int top;
        public int down;
        public int priority;
        public static DTSkillSetting Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTSkillSetting();
            data.Prop = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.skillId = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.front = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            data.back = ExcelUtils.ParseInt(arr?.Length > 3 ? arr[3] : "");
            data.top = ExcelUtils.ParseInt(arr?.Length > 4 ? arr[4] : "");
            data.down = ExcelUtils.ParseInt(arr?.Length > 5 ? arr[5] : "");
            data.priority = ExcelUtils.ParseInt(arr?.Length > 6 ? arr[6] : "");
            return data;
        }
        public static DTSkillSetting[] Unpack(string[] strArr)
        {
            var arr = new List<DTSkillSetting>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTUnlock
    {
        public int Unlockype;
        public int UnlockValue;
        public static DTUnlock Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTUnlock();
            data.Unlockype = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.UnlockValue = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTUnlock[] Unpack(string[] strArr)
        {
            var arr = new List<DTUnlock>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTPropDamegeGrowth
    {
        public string PropDamegeGrowth;
        public static DTPropDamegeGrowth Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTPropDamegeGrowth();
            data.PropDamegeGrowth = arr?.Length > 0 ? arr[0] : "";
            return data;
        }
        public static DTPropDamegeGrowth[] Unpack(string[] strArr)
        {
            var arr = new List<DTPropDamegeGrowth>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTDungeonCost
    {
        public int itemId;
        public int itemNum;
        public static DTDungeonCost Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTDungeonCost();
            data.itemId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.itemNum = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTDungeonCost[] Unpack(string[] strArr)
        {
            var arr = new List<DTDungeonCost>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTSetlist
    {
        public int ValueMin;
        public int ValueMax;
        public static DTSetlist Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTSetlist();
            data.ValueMin = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.ValueMax = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTSetlist[] Unpack(string[] strArr)
        {
            var arr = new List<DTSetlist>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTPerChapterId
    {
        public int ChapterId;
        public int SectionListId;
        public int Deefid;
        public static DTPerChapterId Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTPerChapterId();
            data.ChapterId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.SectionListId = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.Deefid = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTPerChapterId[] Unpack(string[] strArr)
        {
            var arr = new List<DTPerChapterId>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTPropGrowth
    {
        public string GrowthParamString;
        public static DTPropGrowth Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTPropGrowth();
            data.GrowthParamString = arr?.Length > 0 ? arr[0] : "";
            return data;
        }
        public static DTPropGrowth[] Unpack(string[] strArr)
        {
            var arr = new List<DTPropGrowth>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTAuction
    {
        public string itemType;
        public int value;
        public static DTAuction Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTAuction();
            data.itemType = arr?.Length > 0 ? arr[0] : "";
            data.value = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTAuction[] Unpack(string[] strArr)
        {
            var arr = new List<DTAuction>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DtBuffAudio
    {
        public int Birth;
        public int ID;
        public int Loop;
        public static DtBuffAudio Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DtBuffAudio();
            data.Birth = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.ID = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.Loop = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DtBuffAudio[] Unpack(string[] strArr)
        {
            var arr = new List<DtBuffAudio>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DtBuffEffect
    {
        public int Birth;
        public string ID;
        public int Loop;
        public static DtBuffEffect Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DtBuffEffect();
            data.Birth = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.ID = arr?.Length > 1 ? arr[1] : "";
            data.Loop = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DtBuffEffect[] Unpack(string[] strArr)
        {
            var arr = new List<DtBuffEffect>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTSuitProp
    {
        public int num;
        public int propId;
        public static DTSuitProp Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTSuitProp();
            data.num = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.propId = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTSuitProp[] Unpack(string[] strArr)
        {
            var arr = new List<DTSuitProp>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTEquipMaster
    {
        public float quality;
        public float partGrowth;
        public float partAmend;
        public static DTEquipMaster Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTEquipMaster();
            data.quality = ExcelUtils.ParseFloat(arr?.Length > 0 ? arr[0] : "");
            data.partGrowth = ExcelUtils.ParseFloat(arr?.Length > 1 ? arr[1] : "");
            data.partAmend = ExcelUtils.ParseFloat(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTEquipMaster[] Unpack(string[] strArr)
        {
            var arr = new List<DTEquipMaster>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTItemUse
    {
        public int type;
        public int param;
        public static DTItemUse Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTItemUse();
            data.type = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.param = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTItemUse[] Unpack(string[] strArr)
        {
            var arr = new List<DTItemUse>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTWay
    {
        public int module;
        public int id;
        public static DTWay Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTWay();
            data.module = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.id = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTWay[] Unpack(string[] strArr)
        {
            var arr = new List<DTWay>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTChanceRange
    {
        public int chance;
        public int downinterval;
        public int upinterval;
        public static DTChanceRange Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTChanceRange();
            data.chance = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.downinterval = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.upinterval = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTChanceRange[] Unpack(string[] strArr)
        {
            var arr = new List<DTChanceRange>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTRefineProperty
    {
        public int basePercent;
        public int perfectPercent;
        public static DTRefineProperty Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTRefineProperty();
            data.basePercent = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.perfectPercent = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTRefineProperty[] Unpack(string[] strArr)
        {
            var arr = new List<DTRefineProperty>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTDropWeight
    {
        public int dropId;
        public int weight;
        public static DTDropWeight Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTDropWeight();
            data.dropId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.weight = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTDropWeight[] Unpack(string[] strArr)
        {
            var arr = new List<DTDropWeight>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTMonsterGroupWeight
    {
        public int monsterGroupId;
        public int weight;
        public static DTMonsterGroupWeight Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTMonsterGroupWeight();
            data.monsterGroupId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.weight = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTMonsterGroupWeight[] Unpack(string[] strArr)
        {
            var arr = new List<DTMonsterGroupWeight>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTDropIdFixWeight
    {
        public int monsterGroupId;
        public int itemId;
        public int weight;
        public static DTDropIdFixWeight Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTDropIdFixWeight();
            data.monsterGroupId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.itemId = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.weight = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTDropIdFixWeight[] Unpack(string[] strArr)
        {
            var arr = new List<DTDropIdFixWeight>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTDropQualityFixWeight
    {
        public int monsterGroupId;
        public int quality;
        public int weight;
        public static DTDropQualityFixWeight Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTDropQualityFixWeight();
            data.monsterGroupId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.quality = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.weight = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTDropQualityFixWeight[] Unpack(string[] strArr)
        {
            var arr = new List<DTDropQualityFixWeight>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTSpillarWeight
    {
        public int SpillarId;
        public int weight;
        public static DTSpillarWeight Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTSpillarWeight();
            data.SpillarId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.weight = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTSpillarWeight[] Unpack(string[] strArr)
        {
            var arr = new List<DTSpillarWeight>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTJobDropWeight
    {
        public int JobId;
        public int weight;
        public static DTJobDropWeight Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTJobDropWeight();
            data.JobId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.weight = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTJobDropWeight[] Unpack(string[] strArr)
        {
            var arr = new List<DTJobDropWeight>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTJTaskDropWeight
    {
        public int taskId;
        public int weight;
        public static DTJTaskDropWeight Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTJTaskDropWeight();
            data.taskId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.weight = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTJTaskDropWeight[] Unpack(string[] strArr)
        {
            var arr = new List<DTJTaskDropWeight>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTMenuArea
    {
        public int menuId;
        public int area;
        public static DTMenuArea Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTMenuArea();
            data.menuId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.area = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTMenuArea[] Unpack(string[] strArr)
        {
            var arr = new List<DTMenuArea>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTPasswordWrong
    {
        public int totalTimes;
        public int tipTimes;
        public static DTPasswordWrong Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTPasswordWrong();
            data.totalTimes = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.tipTimes = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTPasswordWrong[] Unpack(string[] strArr)
        {
            var arr = new List<DTPasswordWrong>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTShopRefresh
    {
        public int itemId;
        public int itemNum;
        public static DTShopRefresh Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTShopRefresh();
            data.itemId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.itemNum = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTShopRefresh[] Unpack(string[] strArr)
        {
            var arr = new List<DTShopRefresh>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTSkillList
    {
        public int skillId;
        public int level;
        public int pos;
        public static DTSkillList Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTSkillList();
            data.skillId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.level = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.pos = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTSkillList[] Unpack(string[] strArr)
        {
            var arr = new List<DTSkillList>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTStrengthenedEquip
    {
        public int itemId;
        public int strengthLevel;
        public static DTStrengthenedEquip Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTStrengthenedEquip();
            data.itemId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.strengthLevel = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTStrengthenedEquip[] Unpack(string[] strArr)
        {
            var arr = new List<DTStrengthenedEquip>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTAffixProb
    {
        public int num;
        public int prob;
        public static DTAffixProb Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTAffixProb();
            data.num = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.prob = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTAffixProb[] Unpack(string[] strArr)
        {
            var arr = new List<DTAffixProb>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTItemExchange
    {
        public int originId;
        public int newId;
        public int num;
        public static DTItemExchange Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTItemExchange();
            data.originId = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.newId = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            data.num = ExcelUtils.ParseInt(arr?.Length > 2 ? arr[2] : "");
            return data;
        }
        public static DTItemExchange[] Unpack(string[] strArr)
        {
            var arr = new List<DTItemExchange>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class DTHelpLimit
    {
        public int type;
        public int id;
        public static DTHelpLimit Unpack(string str)
        {
            var arr = string.IsNullOrEmpty(str) ? null : str.Split('&');
            var data = new DTHelpLimit();
            data.type = ExcelUtils.ParseInt(arr?.Length > 0 ? arr[0] : "");
            data.id = ExcelUtils.ParseInt(arr?.Length > 1 ? arr[1] : "");
            return data;
        }
        public static DTHelpLimit[] Unpack(string[] strArr)
        {
            var arr = new List<DTHelpLimit>();
            if (strArr != null)
            {
                foreach (var item in strArr)
                {
                    arr.Add(Unpack(item));
                }
            }
            return arr.ToArray();
        }
    }

    public class AudioBattle
    {
        public int id;
        public int group;
        public string name;
        public string[] path;
        public int prio;
        public float volume;
        public bool randomCull;
        public int rand;
        public int loop;
        public int Duration;
    }
    public class AudioBattleRW
    {
        public const string excelPath = @"AudioBattle.xlsx";
        public static Dictionary<int, AudioBattle> LoadDict(string[][] strArr)
        {
            Dictionary<int, AudioBattle> dict = new Dictionary<int, AudioBattle>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static AudioBattle Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            AudioBattle data = new AudioBattle();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.group = ExcelUtils.ParseInt(strArr[1]);
            data.name = strArr[2];
            data.path = ExcelUtils.ParseStringArr(strArr[3] ,';');
            data.prio = ExcelUtils.ParseInt(strArr[4]);
            data.volume = ExcelUtils.ParseFloat(strArr[5]);
            data.randomCull = ExcelUtils.ParseBool(strArr[6]);
            data.rand = ExcelUtils.ParseInt(strArr[7]);
            data.loop = ExcelUtils.ParseInt(strArr[8]);
            data.Duration = ExcelUtils.ParseInt(strArr[9]);
            return data;
        }
    }

    public class Buff
    {
        public int id;
        public string Name;
        public string Description;
        public int Type;
        public int WorkType;
        public bool DispelType;
        public int[] DispelWorkType;
        public int BuffShaderType;
        public int BuffClip;
        public string HeadName;
        public int[] BirthAudio;
        public int[] BirthEffect;
        public int[] StateChange;
        public int[] AbilityChange;
        public int TargetState;
        public bool IsClipBUFF;
        public int TimeInterval;
        public int Overlay;
        public int OverlayLimit;
        public int IsQuickPressSupport;
        public int[] UseSkillIDs;
        public int[] TriggerBuffInfoIDs;
        public int[] MechanismID;
        public int[] SummonID;
        public int[] EntityId;
        public int BuffSpeech;
        public bool NotShowTopText;
        public int ElementType;
        public DTPropDamegeGrowth EnhanceWind;
        public DTPropDamegeGrowth EnhanceThunder;
        public DTPropDamegeGrowth EnhanceWater;
        public DTPropDamegeGrowth EnhanceFire;
        public DTPropDamegeGrowth EnhanceSoil;
        public DTPropDamegeGrowth DefWind;
        public DTPropDamegeGrowth DefThunder;
        public DTPropDamegeGrowth DefWater;
        public DTPropDamegeGrowth DefFire;
        public DTPropDamegeGrowth DefSoil;
        public DTPropDamegeGrowth RecoverMaxHp;
        public DTPropDamegeGrowth RecoverMaxMp;
        public DTPropDamegeGrowth RecoverMaxHpPer;
        public DTPropDamegeGrowth RecoverMaxMpPer;
        public DTPropDamegeGrowth RecoverCurrentHpPer;
        public DTPropDamegeGrowth RecoverCurrentMpPer;
        public DTPropDamegeGrowth Strength;
        public DTPropDamegeGrowth Intelligence;
        public DTPropDamegeGrowth Power;
        public DTPropDamegeGrowth Spirit;
        public DTPropDamegeGrowth StrengthP;
        public DTPropDamegeGrowth IntelligenceP;
        public DTPropDamegeGrowth PowerP;
        public DTPropDamegeGrowth SpiritP;
        public DTPropDamegeGrowth MaxHp;
        public DTPropDamegeGrowth MaxMp;
        public DTPropDamegeGrowth MaxHpP;
        public DTPropDamegeGrowth MaxMpP;
        public DTPropDamegeGrowth HpAddTime;
        public DTPropDamegeGrowth MpAddTime;
        public DTPropDamegeGrowth Patt;
        public DTPropDamegeGrowth Matt;
        public DTPropDamegeGrowth FixAtt;
        public DTPropDamegeGrowth Pdef;
        public DTPropDamegeGrowth Mdef;
        public DTPropDamegeGrowth AttSpeedP;
        public DTPropDamegeGrowth CastSpeedP;
        public DTPropDamegeGrowth MoveSpeedP;
        public DTPropDamegeGrowth Pcrit;
        public DTPropDamegeGrowth Mcrit;
        public DTPropDamegeGrowth Hit;
        public DTPropDamegeGrowth Dodge;
        public DTPropDamegeGrowth Rigidity;
        public DTPropDamegeGrowth Stiff;
        public DTPropDamegeGrowth DefElectricity;
        public DTPropDamegeGrowth DefBlood;
        public DTPropDamegeGrowth DefBurn;
        public DTPropDamegeGrowth DefPoision;
        public DTPropDamegeGrowth DefBlind;
        public DTPropDamegeGrowth DefChaos;
        public DTPropDamegeGrowth DefSlow;
        public DTPropDamegeGrowth DefCurse;
        public DTPropDamegeGrowth DefShackles;
        public DTPropDamegeGrowth DefDizziness;
        public DTPropDamegeGrowth DefStone;
        public DTPropDamegeGrowth DefFrozen;
        public DTPropDamegeGrowth DefSleep;
        public DTPropDamegeGrowth PcritPer;
        public DTPropDamegeGrowth McritPer;
        public DTPropDamegeGrowth PcritDamage;
        public DTPropDamegeGrowth McritDamage;
        public DTPropDamegeGrowth PattP;
        public DTPropDamegeGrowth MattP;
        public DTPropDamegeGrowth FixAttP;
        public DTPropDamegeGrowth PDefendPer;
        public DTPropDamegeGrowth MDefendPer;
        public DTPropDamegeGrowth PattNodefP;
        public DTPropDamegeGrowth MattNodefP;
        public DTPropDamegeGrowth FixedExtraDamag;
        public DTPropDamegeGrowth ExtraDamag;
        public DTPropDamegeGrowth FixedDamagAdd;
        public DTPropDamegeGrowth DamagAdd;
        public DTPropDamegeGrowth FixedDamageReduce;
        public DTPropDamegeGrowth DamageReduce;
        public DTPropDamegeGrowth PDamageReduce;
        public DTPropDamegeGrowth MDamageReduce;
        public DTPropDamegeGrowth ReflectDamage;
        public DTPropDamegeGrowth FixedReflectDamage;
        public DTPropDamegeGrowth GrabDefense;
        public DTPropDamegeGrowth SkillLevel;
        public DTPropDamegeGrowth SkillHpCostAdd;
        public DTPropDamegeGrowth SkillMpCostAdd;
        public DTPropDamegeGrowth SkillMpCostAddP;
        public DTPropDamegeGrowth SkillItemCostAdd;
        public DTPropDamegeGrowth SkillCooldownAdd;
        public DTPropDamegeGrowth SkillCooldownAddP;
        public DTPropDamegeGrowth SkillSpeedAddFactor;
        public DTPropDamegeGrowth SkillHitRateAdd;
        public DTPropDamegeGrowth SkillCriticalHitRateAdd;
        public DTPropDamegeGrowth SkillAttackAddRate;
        public DTPropDamegeGrowth SkillAttackAdd;
        public DTPropDamegeGrowth SkillAttackAddFix;
        public DTPropDamegeGrowth SkillHardAddRate;
        public DTPropDamegeGrowth SkillChargeReduceRate;
        public int needShowUIBar;
    }
    public class BuffRW
    {
        public const string excelPath = @"Buff.xlsx";
        public static Dictionary<int, Buff> LoadDict(string[][] strArr)
        {
            Dictionary<int, Buff> dict = new Dictionary<int, Buff>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static Buff Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            Buff data = new Buff();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.Name = strArr[1];
            data.Description = strArr[2];
            data.Type = ExcelUtils.ParseInt(strArr[3]);
            data.WorkType = ExcelUtils.ParseInt(strArr[4]);
            data.DispelType = ExcelUtils.ParseBool(strArr[5]);
            data.DispelWorkType = ExcelUtils.ParseIntArr(strArr[6] ,';');
            data.BuffShaderType = ExcelUtils.ParseInt(strArr[7]);
            data.BuffClip = ExcelUtils.ParseInt(strArr[8]);
            data.HeadName = strArr[9];
            data.BirthAudio = ExcelUtils.ParseIntArr(strArr[10] ,';');
            data.BirthEffect = ExcelUtils.ParseIntArr(strArr[11] ,';');
            data.StateChange = ExcelUtils.ParseIntArr(strArr[12] ,';');
            data.AbilityChange = ExcelUtils.ParseIntArr(strArr[13] ,';');
            data.TargetState = ExcelUtils.ParseInt(strArr[14]);
            data.IsClipBUFF = ExcelUtils.ParseBool(strArr[15]);
            data.TimeInterval = ExcelUtils.ParseInt(strArr[16]);
            data.Overlay = ExcelUtils.ParseInt(strArr[17]);
            data.OverlayLimit = ExcelUtils.ParseInt(strArr[18]);
            data.IsQuickPressSupport = ExcelUtils.ParseInt(strArr[19]);
            data.UseSkillIDs = ExcelUtils.ParseIntArr(strArr[20] ,';');
            data.TriggerBuffInfoIDs = ExcelUtils.ParseIntArr(strArr[21] ,';');
            data.MechanismID = ExcelUtils.ParseIntArr(strArr[22] ,';');
            data.SummonID = ExcelUtils.ParseIntArr(strArr[23] ,';');
            data.EntityId = ExcelUtils.ParseIntArr(strArr[24] ,';');
            data.BuffSpeech = ExcelUtils.ParseInt(strArr[25]);
            data.NotShowTopText = ExcelUtils.ParseBool(strArr[26]);
            data.ElementType = ExcelUtils.ParseInt(strArr[27]);
            data.EnhanceWind = DTPropDamegeGrowth.Unpack(strArr[28]);
            data.EnhanceThunder = DTPropDamegeGrowth.Unpack(strArr[29]);
            data.EnhanceWater = DTPropDamegeGrowth.Unpack(strArr[30]);
            data.EnhanceFire = DTPropDamegeGrowth.Unpack(strArr[31]);
            data.EnhanceSoil = DTPropDamegeGrowth.Unpack(strArr[32]);
            data.DefWind = DTPropDamegeGrowth.Unpack(strArr[33]);
            data.DefThunder = DTPropDamegeGrowth.Unpack(strArr[34]);
            data.DefWater = DTPropDamegeGrowth.Unpack(strArr[35]);
            data.DefFire = DTPropDamegeGrowth.Unpack(strArr[36]);
            data.DefSoil = DTPropDamegeGrowth.Unpack(strArr[37]);
            data.RecoverMaxHp = DTPropDamegeGrowth.Unpack(strArr[38]);
            data.RecoverMaxMp = DTPropDamegeGrowth.Unpack(strArr[39]);
            data.RecoverMaxHpPer = DTPropDamegeGrowth.Unpack(strArr[40]);
            data.RecoverMaxMpPer = DTPropDamegeGrowth.Unpack(strArr[41]);
            data.RecoverCurrentHpPer = DTPropDamegeGrowth.Unpack(strArr[42]);
            data.RecoverCurrentMpPer = DTPropDamegeGrowth.Unpack(strArr[43]);
            data.Strength = DTPropDamegeGrowth.Unpack(strArr[44]);
            data.Intelligence = DTPropDamegeGrowth.Unpack(strArr[45]);
            data.Power = DTPropDamegeGrowth.Unpack(strArr[46]);
            data.Spirit = DTPropDamegeGrowth.Unpack(strArr[47]);
            data.StrengthP = DTPropDamegeGrowth.Unpack(strArr[48]);
            data.IntelligenceP = DTPropDamegeGrowth.Unpack(strArr[49]);
            data.PowerP = DTPropDamegeGrowth.Unpack(strArr[50]);
            data.SpiritP = DTPropDamegeGrowth.Unpack(strArr[51]);
            data.MaxHp = DTPropDamegeGrowth.Unpack(strArr[52]);
            data.MaxMp = DTPropDamegeGrowth.Unpack(strArr[53]);
            data.MaxHpP = DTPropDamegeGrowth.Unpack(strArr[54]);
            data.MaxMpP = DTPropDamegeGrowth.Unpack(strArr[55]);
            data.HpAddTime = DTPropDamegeGrowth.Unpack(strArr[56]);
            data.MpAddTime = DTPropDamegeGrowth.Unpack(strArr[57]);
            data.Patt = DTPropDamegeGrowth.Unpack(strArr[58]);
            data.Matt = DTPropDamegeGrowth.Unpack(strArr[59]);
            data.FixAtt = DTPropDamegeGrowth.Unpack(strArr[60]);
            data.Pdef = DTPropDamegeGrowth.Unpack(strArr[61]);
            data.Mdef = DTPropDamegeGrowth.Unpack(strArr[62]);
            data.AttSpeedP = DTPropDamegeGrowth.Unpack(strArr[63]);
            data.CastSpeedP = DTPropDamegeGrowth.Unpack(strArr[64]);
            data.MoveSpeedP = DTPropDamegeGrowth.Unpack(strArr[65]);
            data.Pcrit = DTPropDamegeGrowth.Unpack(strArr[66]);
            data.Mcrit = DTPropDamegeGrowth.Unpack(strArr[67]);
            data.Hit = DTPropDamegeGrowth.Unpack(strArr[68]);
            data.Dodge = DTPropDamegeGrowth.Unpack(strArr[69]);
            data.Rigidity = DTPropDamegeGrowth.Unpack(strArr[70]);
            data.Stiff = DTPropDamegeGrowth.Unpack(strArr[71]);
            data.DefElectricity = DTPropDamegeGrowth.Unpack(strArr[72]);
            data.DefBlood = DTPropDamegeGrowth.Unpack(strArr[73]);
            data.DefBurn = DTPropDamegeGrowth.Unpack(strArr[74]);
            data.DefPoision = DTPropDamegeGrowth.Unpack(strArr[75]);
            data.DefBlind = DTPropDamegeGrowth.Unpack(strArr[76]);
            data.DefChaos = DTPropDamegeGrowth.Unpack(strArr[77]);
            data.DefSlow = DTPropDamegeGrowth.Unpack(strArr[78]);
            data.DefCurse = DTPropDamegeGrowth.Unpack(strArr[79]);
            data.DefShackles = DTPropDamegeGrowth.Unpack(strArr[80]);
            data.DefDizziness = DTPropDamegeGrowth.Unpack(strArr[81]);
            data.DefStone = DTPropDamegeGrowth.Unpack(strArr[82]);
            data.DefFrozen = DTPropDamegeGrowth.Unpack(strArr[83]);
            data.DefSleep = DTPropDamegeGrowth.Unpack(strArr[84]);
            data.PcritPer = DTPropDamegeGrowth.Unpack(strArr[85]);
            data.McritPer = DTPropDamegeGrowth.Unpack(strArr[86]);
            data.PcritDamage = DTPropDamegeGrowth.Unpack(strArr[87]);
            data.McritDamage = DTPropDamegeGrowth.Unpack(strArr[88]);
            data.PattP = DTPropDamegeGrowth.Unpack(strArr[89]);
            data.MattP = DTPropDamegeGrowth.Unpack(strArr[90]);
            data.FixAttP = DTPropDamegeGrowth.Unpack(strArr[91]);
            data.PDefendPer = DTPropDamegeGrowth.Unpack(strArr[92]);
            data.MDefendPer = DTPropDamegeGrowth.Unpack(strArr[93]);
            data.PattNodefP = DTPropDamegeGrowth.Unpack(strArr[94]);
            data.MattNodefP = DTPropDamegeGrowth.Unpack(strArr[95]);
            data.FixedExtraDamag = DTPropDamegeGrowth.Unpack(strArr[96]);
            data.ExtraDamag = DTPropDamegeGrowth.Unpack(strArr[97]);
            data.FixedDamagAdd = DTPropDamegeGrowth.Unpack(strArr[98]);
            data.DamagAdd = DTPropDamegeGrowth.Unpack(strArr[99]);
            data.FixedDamageReduce = DTPropDamegeGrowth.Unpack(strArr[100]);
            data.DamageReduce = DTPropDamegeGrowth.Unpack(strArr[101]);
            data.PDamageReduce = DTPropDamegeGrowth.Unpack(strArr[102]);
            data.MDamageReduce = DTPropDamegeGrowth.Unpack(strArr[103]);
            data.ReflectDamage = DTPropDamegeGrowth.Unpack(strArr[104]);
            data.FixedReflectDamage = DTPropDamegeGrowth.Unpack(strArr[105]);
            data.GrabDefense = DTPropDamegeGrowth.Unpack(strArr[106]);
            data.SkillLevel = DTPropDamegeGrowth.Unpack(strArr[107]);
            data.SkillHpCostAdd = DTPropDamegeGrowth.Unpack(strArr[108]);
            data.SkillMpCostAdd = DTPropDamegeGrowth.Unpack(strArr[109]);
            data.SkillMpCostAddP = DTPropDamegeGrowth.Unpack(strArr[110]);
            data.SkillItemCostAdd = DTPropDamegeGrowth.Unpack(strArr[111]);
            data.SkillCooldownAdd = DTPropDamegeGrowth.Unpack(strArr[112]);
            data.SkillCooldownAddP = DTPropDamegeGrowth.Unpack(strArr[113]);
            data.SkillSpeedAddFactor = DTPropDamegeGrowth.Unpack(strArr[114]);
            data.SkillHitRateAdd = DTPropDamegeGrowth.Unpack(strArr[115]);
            data.SkillCriticalHitRateAdd = DTPropDamegeGrowth.Unpack(strArr[116]);
            data.SkillAttackAddRate = DTPropDamegeGrowth.Unpack(strArr[117]);
            data.SkillAttackAdd = DTPropDamegeGrowth.Unpack(strArr[118]);
            data.SkillAttackAddFix = DTPropDamegeGrowth.Unpack(strArr[119]);
            data.SkillHardAddRate = DTPropDamegeGrowth.Unpack(strArr[120]);
            data.SkillChargeReduceRate = DTPropDamegeGrowth.Unpack(strArr[121]);
            data.needShowUIBar = ExcelUtils.ParseInt(strArr[122]);
            return data;
        }
    }

    public class BuffInfo
    {
        public int id;
        public string Description;
        public int[] BuffID;
        public DTPropDamegeGrowth Buffprobability;
        public DTPropDamegeGrowth Bufftime;
        public DTPropDamegeGrowth Bufflevel;
        public DTPropDamegeGrowth BuffCD;
        public int TriggerEffectTime;
        public int BuffTarget;
        public int BuffTargetRadius;
        public int[] SkillType;
        public int[] SkillID;
        public int[] BuffTargetMonsterID;
        public int BuffCondition;
        public int BuffDelay;
        public int[] ConditionEffect;
        public int[] ConditionSkillType;
        public int[] ConditionSkillID;
        public int DoAttackTimeRate;
        public DTPropDamegeGrowth DefAttack;
    }
    public class BuffInfoRW
    {
        public const string excelPath = @"Buff.xlsx";
        public static Dictionary<int, BuffInfo> LoadDict(string[][] strArr)
        {
            Dictionary<int, BuffInfo> dict = new Dictionary<int, BuffInfo>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static BuffInfo Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            BuffInfo data = new BuffInfo();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.Description = strArr[1];
            data.BuffID = ExcelUtils.ParseIntArr(strArr[2] ,';');
            data.Buffprobability = DTPropDamegeGrowth.Unpack(strArr[3]);
            data.Bufftime = DTPropDamegeGrowth.Unpack(strArr[4]);
            data.Bufflevel = DTPropDamegeGrowth.Unpack(strArr[5]);
            data.BuffCD = DTPropDamegeGrowth.Unpack(strArr[6]);
            data.TriggerEffectTime = ExcelUtils.ParseInt(strArr[7]);
            data.BuffTarget = ExcelUtils.ParseInt(strArr[8]);
            data.BuffTargetRadius = ExcelUtils.ParseInt(strArr[9]);
            data.SkillType = ExcelUtils.ParseIntArr(strArr[10] ,';');
            data.SkillID = ExcelUtils.ParseIntArr(strArr[11] ,';');
            data.BuffTargetMonsterID = ExcelUtils.ParseIntArr(strArr[12] ,';');
            data.BuffCondition = ExcelUtils.ParseInt(strArr[13]);
            data.BuffDelay = ExcelUtils.ParseInt(strArr[14]);
            data.ConditionEffect = ExcelUtils.ParseIntArr(strArr[15] ,';');
            data.ConditionSkillType = ExcelUtils.ParseIntArr(strArr[16] ,';');
            data.ConditionSkillID = ExcelUtils.ParseIntArr(strArr[17] ,';');
            data.DoAttackTimeRate = ExcelUtils.ParseInt(strArr[18]);
            data.DefAttack = DTPropDamegeGrowth.Unpack(strArr[19]);
            return data;
        }
    }

    public class CameraShake
    {
        public int id;
        public string Des;
        public float XScale;
        public float Yscale;
        public int AllTime;
        public int TimeOneTimes;
        public float Reduction;
    }
    public class CameraShakeRW
    {
        public const string excelPath = @"CameraShake.xlsx";
        public static Dictionary<int, CameraShake> LoadDict(string[][] strArr)
        {
            Dictionary<int, CameraShake> dict = new Dictionary<int, CameraShake>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static CameraShake Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            CameraShake data = new CameraShake();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.Des = strArr[1];
            data.XScale = ExcelUtils.ParseFloat(strArr[2]);
            data.Yscale = ExcelUtils.ParseFloat(strArr[3]);
            data.AllTime = ExcelUtils.ParseInt(strArr[4]);
            data.TimeOneTimes = ExcelUtils.ParseInt(strArr[5]);
            data.Reduction = ExcelUtils.ParseFloat(strArr[6]);
            return data;
        }
    }

    public class DestructibleProp
    {
        public int id;
        public int destructibleId;
        public string name;
        public string desc;
        public int Camp;
        public int modeId;
        public int avatarid;
        public int[] beHitType;
        public int idleCount;
        public float defendboxy;
    }
    public class DestructiblePropRW
    {
        public const string excelPath = @"Destructible.xlsx";
        public static Dictionary<int, DestructibleProp> LoadDict(string[][] strArr)
        {
            Dictionary<int, DestructibleProp> dict = new Dictionary<int, DestructibleProp>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static DestructibleProp Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            DestructibleProp data = new DestructibleProp();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.destructibleId = ExcelUtils.ParseInt(strArr[1]);
            data.name = strArr[2];
            data.desc = strArr[3];
            data.Camp = ExcelUtils.ParseInt(strArr[4]);
            data.modeId = ExcelUtils.ParseInt(strArr[5]);
            data.avatarid = ExcelUtils.ParseInt(strArr[6]);
            data.beHitType = ExcelUtils.ParseIntArr(strArr[7] ,';');
            data.idleCount = ExcelUtils.ParseInt(strArr[8]);
            data.defendboxy = ExcelUtils.ParseFloat(strArr[9]);
            return data;
        }
    }

    public class Effect
    {
        public int id;
        public string Description;
        public string Path;
        public int JoinId;
        public bool Loop;
        public int Duration;
        public int Scale;
        public int[] Offset;
    }
    public class EffectRW
    {
        public const string excelPath = @"Effect.xlsx";
        public static Dictionary<int, Effect> LoadDict(string[][] strArr)
        {
            Dictionary<int, Effect> dict = new Dictionary<int, Effect>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static Effect Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            Effect data = new Effect();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.Description = strArr[1];
            data.Path = strArr[2];
            data.JoinId = ExcelUtils.ParseInt(strArr[3]);
            data.Loop = ExcelUtils.ParseBool(strArr[4]);
            data.Duration = ExcelUtils.ParseInt(strArr[5]);
            data.Scale = ExcelUtils.ParseInt(strArr[6]);
            data.Offset = ExcelUtils.ParseIntArr(strArr[7] ,';');
            return data;
        }
    }

    public class Fashion
    {
        public int id;
        public string desc;
        public int fashionPart;
        public int fashionType;
        public int[] hangPoint;
        public DTValue3[] offset;
        public int[] scale;
        public int fashionPropertyID;
        public int equipSuitID;
        public int resType;
        public string ResModel;
        public string ResMatrix;
        public int ModelResourceID;
    }
    public class FashionRW
    {
        public const string excelPath = @"Fashion.xlsx";
        public static Dictionary<int, Fashion> LoadDict(string[][] strArr)
        {
            Dictionary<int, Fashion> dict = new Dictionary<int, Fashion>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static Fashion Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            Fashion data = new Fashion();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.desc = strArr[1];
            data.fashionPart = ExcelUtils.ParseInt(strArr[2]);
            data.fashionType = ExcelUtils.ParseInt(strArr[3]);
            data.hangPoint = ExcelUtils.ParseIntArr(strArr[4] ,';');
            data.offset = DTValue3.Unpack(ExcelUtils.ParseStringArr(strArr[5] ,';'));
            data.scale = ExcelUtils.ParseIntArr(strArr[6] ,';');
            data.fashionPropertyID = ExcelUtils.ParseInt(strArr[7]);
            data.equipSuitID = ExcelUtils.ParseInt(strArr[8]);
            data.resType = ExcelUtils.ParseInt(strArr[9]);
            data.ResModel = strArr[10];
            data.ResMatrix = strArr[11];
            data.ModelResourceID = ExcelUtils.ParseInt(strArr[12]);
            return data;
        }
    }

    public class FashionAttr
    {
        public int id;
        public string desc;
        public int BaseFashionPropID;
        public int[] FashionPropID;
        public int DefaultFashionPropID;
    }
    public class FashionAttrRW
    {
        public const string excelPath = @"Fashion.xlsx";
        public static Dictionary<int, FashionAttr> LoadDict(string[][] strArr)
        {
            Dictionary<int, FashionAttr> dict = new Dictionary<int, FashionAttr>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static FashionAttr Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            FashionAttr data = new FashionAttr();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.desc = strArr[1];
            data.BaseFashionPropID = ExcelUtils.ParseInt(strArr[2]);
            data.FashionPropID = ExcelUtils.ParseIntArr(strArr[3] ,';');
            data.DefaultFashionPropID = ExcelUtils.ParseInt(strArr[4]);
            return data;
        }
    }

    public class FashionSuit
    {
        public int id;
        public string desc;
        public int[] suitList;
        public DTSuitProp[] suitProp;
        public string url;
    }
    public class FashionSuitRW
    {
        public const string excelPath = @"Fashion.xlsx";
        public static Dictionary<int, FashionSuit> LoadDict(string[][] strArr)
        {
            Dictionary<int, FashionSuit> dict = new Dictionary<int, FashionSuit>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static FashionSuit Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            FashionSuit data = new FashionSuit();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.desc = strArr[1];
            data.suitList = ExcelUtils.ParseIntArr(strArr[2] ,';');
            data.suitProp = DTSuitProp.Unpack(ExcelUtils.ParseStringArr(strArr[3] ,';'));
            data.url = strArr[4];
            return data;
        }
    }

    public class FashionProp
    {
        public int id;
        public string zhCnName;
        public int[] PvebuffinfoID;
        public int[] PveMechanismID;
        public int[] PvpbuffinfoID;
        public int[] PvpMechanismID;
        public int scoreadd;
        public string discrepadd;
        public int Patt;
        public int Matt;
        public int FixAtt;
        public int Pdef;
        public int Mdef;
        public float Strength;
        public float Intelligence;
        public float Power;
        public float Spirit;
        public int MaxHp;
        public int MaxMp;
        public int HpAddTime;
        public int MpAddTime;
        public int AttSpeedPer;
        public int CastSpeedPer;
        public int MoveSpeedPer;
        public int Pcrit;
        public int Mcrit;
        public int PcritPer;
        public int McritPer;
        public int Hit;
        public int Dodge;
        public int ElementType;
        public int EnhanceWind;
        public int EnhanceThunder;
        public int EnhanceWater;
        public int EnhanceFire;
        public int EnhanceSoil;
        public int DefWind;
        public int DefThunder;
        public int DefWater;
        public int DefFire;
        public int DefSoil;
        public int DefElectricity;
        public int DefBlood;
        public int DefBurn;
        public int DefPoision;
        public int DefBlind;
        public int DefChaos;
        public int DefSlow;
        public int DefCurse;
        public int DefShackles;
        public int DefDizziness;
        public int DefStone;
        public int DefFrozen;
        public int DefSleep;
        public int AllDef;
        public int DefMonster;
        public int Stiff;
        public int CityMoveSpeedPer;
    }
    public class FashionPropRW
    {
        public const string excelPath = @"Fashion.xlsx";
        public static Dictionary<int, FashionProp> LoadDict(string[][] strArr)
        {
            Dictionary<int, FashionProp> dict = new Dictionary<int, FashionProp>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static FashionProp Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            FashionProp data = new FashionProp();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.zhCnName = strArr[1];
            data.PvebuffinfoID = ExcelUtils.ParseIntArr(strArr[2] ,';');
            data.PveMechanismID = ExcelUtils.ParseIntArr(strArr[3] ,';');
            data.PvpbuffinfoID = ExcelUtils.ParseIntArr(strArr[4] ,';');
            data.PvpMechanismID = ExcelUtils.ParseIntArr(strArr[5] ,';');
            data.scoreadd = ExcelUtils.ParseInt(strArr[6]);
            data.discrepadd = strArr[7];
            data.Patt = ExcelUtils.ParseInt(strArr[8]);
            data.Matt = ExcelUtils.ParseInt(strArr[9]);
            data.FixAtt = ExcelUtils.ParseInt(strArr[10]);
            data.Pdef = ExcelUtils.ParseInt(strArr[11]);
            data.Mdef = ExcelUtils.ParseInt(strArr[12]);
            data.Strength = ExcelUtils.ParseFloat(strArr[13]);
            data.Intelligence = ExcelUtils.ParseFloat(strArr[14]);
            data.Power = ExcelUtils.ParseFloat(strArr[15]);
            data.Spirit = ExcelUtils.ParseFloat(strArr[16]);
            data.MaxHp = ExcelUtils.ParseInt(strArr[17]);
            data.MaxMp = ExcelUtils.ParseInt(strArr[18]);
            data.HpAddTime = ExcelUtils.ParseInt(strArr[19]);
            data.MpAddTime = ExcelUtils.ParseInt(strArr[20]);
            data.AttSpeedPer = ExcelUtils.ParseInt(strArr[21]);
            data.CastSpeedPer = ExcelUtils.ParseInt(strArr[22]);
            data.MoveSpeedPer = ExcelUtils.ParseInt(strArr[23]);
            data.Pcrit = ExcelUtils.ParseInt(strArr[24]);
            data.Mcrit = ExcelUtils.ParseInt(strArr[25]);
            data.PcritPer = ExcelUtils.ParseInt(strArr[26]);
            data.McritPer = ExcelUtils.ParseInt(strArr[27]);
            data.Hit = ExcelUtils.ParseInt(strArr[28]);
            data.Dodge = ExcelUtils.ParseInt(strArr[29]);
            data.ElementType = ExcelUtils.ParseInt(strArr[30]);
            data.EnhanceWind = ExcelUtils.ParseInt(strArr[31]);
            data.EnhanceThunder = ExcelUtils.ParseInt(strArr[32]);
            data.EnhanceWater = ExcelUtils.ParseInt(strArr[33]);
            data.EnhanceFire = ExcelUtils.ParseInt(strArr[34]);
            data.EnhanceSoil = ExcelUtils.ParseInt(strArr[35]);
            data.DefWind = ExcelUtils.ParseInt(strArr[36]);
            data.DefThunder = ExcelUtils.ParseInt(strArr[37]);
            data.DefWater = ExcelUtils.ParseInt(strArr[38]);
            data.DefFire = ExcelUtils.ParseInt(strArr[39]);
            data.DefSoil = ExcelUtils.ParseInt(strArr[40]);
            data.DefElectricity = ExcelUtils.ParseInt(strArr[41]);
            data.DefBlood = ExcelUtils.ParseInt(strArr[42]);
            data.DefBurn = ExcelUtils.ParseInt(strArr[43]);
            data.DefPoision = ExcelUtils.ParseInt(strArr[44]);
            data.DefBlind = ExcelUtils.ParseInt(strArr[45]);
            data.DefChaos = ExcelUtils.ParseInt(strArr[46]);
            data.DefSlow = ExcelUtils.ParseInt(strArr[47]);
            data.DefCurse = ExcelUtils.ParseInt(strArr[48]);
            data.DefShackles = ExcelUtils.ParseInt(strArr[49]);
            data.DefDizziness = ExcelUtils.ParseInt(strArr[50]);
            data.DefStone = ExcelUtils.ParseInt(strArr[51]);
            data.DefFrozen = ExcelUtils.ParseInt(strArr[52]);
            data.DefSleep = ExcelUtils.ParseInt(strArr[53]);
            data.AllDef = ExcelUtils.ParseInt(strArr[54]);
            data.DefMonster = ExcelUtils.ParseInt(strArr[55]);
            data.Stiff = ExcelUtils.ParseInt(strArr[56]);
            data.CityMoveSpeedPer = ExcelUtils.ParseInt(strArr[57]);
            return data;
        }
    }

    public class FashionTitle
    {
        public int id;
        public string zhCnName;
        public string pkgName;
        public string resName;
        public int numFrames;
    }
    public class FashionTitleRW
    {
        public const string excelPath = @"Fashion.xlsx";
        public static Dictionary<int, FashionTitle> LoadDict(string[][] strArr)
        {
            Dictionary<int, FashionTitle> dict = new Dictionary<int, FashionTitle>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static FashionTitle Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            FashionTitle data = new FashionTitle();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.zhCnName = strArr[1];
            data.pkgName = strArr[2];
            data.resName = strArr[3];
            data.numFrames = ExcelUtils.ParseInt(strArr[4]);
            return data;
        }
    }

    public class FashionDecompose
    {
        public int id;
        public int fashionPart;
        public int fashionType;
        public int quality;
        public int dropBagID;
        public DTItemNum[] show;
    }
    public class FashionDecomposeRW
    {
        public const string excelPath = @"Fashion.xlsx";
        public static Dictionary<int, FashionDecompose> LoadDict(string[][] strArr)
        {
            Dictionary<int, FashionDecompose> dict = new Dictionary<int, FashionDecompose>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static FashionDecompose Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            FashionDecompose data = new FashionDecompose();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.fashionPart = ExcelUtils.ParseInt(strArr[1]);
            data.fashionType = ExcelUtils.ParseInt(strArr[2]);
            data.quality = ExcelUtils.ParseInt(strArr[3]);
            data.dropBagID = ExcelUtils.ParseInt(strArr[4]);
            data.show = DTItemNum.Unpack(ExcelUtils.ParseStringArr(strArr[5] ,';'));
            return data;
        }
    }

    public class Item
    {
        public int id;
        public string zhCnName;
        public string zhCnItemDes;
        public int type;
        public int smallType;
        public int awardNumber;
        public int equipPart;
        public int equipType;
        public int avatarid;
        public string avataridColour;
        public int quality;
        public int dropItemEffectAndAudio;
        public DTItemUse itemUse;
        public int level;
        public int useLimit;
        public int maxNum;
        public int maxLimit;
        public bool isAbandon;
        public int[] getWay;
        public int bagPlace;
        public int showSort;
        public int bindType;
        public int bindStatus;
        public bool canTrade;
        public DTTime limitTime;
        public DTDateTime periodTime;
        public bool canSeal;
        public int sealNum;
        public bool canStren;
        public int strenLv;
        public bool canEnchant;
        public bool canInsertSoul;
        public bool canInsert;
        public bool decompose;
        public bool canStore;
        public int refineLv;
        public int[] hero;
        public string showHero;
        public int[] recommendHero;
        public int equipProp;
        public int equipSuit;
        public int[] equipPlace;
        public int chestID;
        public DTItemNum chestCost;
        public int strenTicketID;
        public int strenTicketLv;
        public int priceRec;
        public int priceMin;
        public int priceMax;
        public bool isTreasure;
        public int tradeCD;
        public int tradeNum;
        public int AcutionNum;
        public bool canCopy;
        public bool canHandsel;
        public int popupType;
        public int value;
        public int tipsButton;
        public int cdType;
        public int cdTime;
        public string zhCnShowSmall;
        public DTItemNum price;
    }
    public class ItemRW
    {
        public const string excelPath = @"Item.xlsx";
        public static Dictionary<int, Item> LoadDict(string[][] strArr)
        {
            Dictionary<int, Item> dict = new Dictionary<int, Item>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static Item Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            Item data = new Item();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.zhCnName = strArr[1];
            data.zhCnItemDes = strArr[2];
            data.type = ExcelUtils.ParseInt(strArr[3]);
            data.smallType = ExcelUtils.ParseInt(strArr[4]);
            data.awardNumber = ExcelUtils.ParseInt(strArr[5]);
            data.equipPart = ExcelUtils.ParseInt(strArr[6]);
            data.equipType = ExcelUtils.ParseInt(strArr[7]);
            data.avatarid = ExcelUtils.ParseInt(strArr[8]);
            data.avataridColour = strArr[9];
            data.quality = ExcelUtils.ParseInt(strArr[10]);
            data.dropItemEffectAndAudio = ExcelUtils.ParseInt(strArr[11]);
            data.itemUse = DTItemUse.Unpack(strArr[12]);
            data.level = ExcelUtils.ParseInt(strArr[13]);
            data.useLimit = ExcelUtils.ParseInt(strArr[14]);
            data.maxNum = ExcelUtils.ParseInt(strArr[15]);
            data.maxLimit = ExcelUtils.ParseInt(strArr[16]);
            data.isAbandon = ExcelUtils.ParseBool(strArr[17]);
            data.getWay = ExcelUtils.ParseIntArr(strArr[18] ,';');
            data.bagPlace = ExcelUtils.ParseInt(strArr[19]);
            data.showSort = ExcelUtils.ParseInt(strArr[20]);
            data.bindType = ExcelUtils.ParseInt(strArr[21]);
            data.bindStatus = ExcelUtils.ParseInt(strArr[22]);
            data.canTrade = ExcelUtils.ParseBool(strArr[23]);
            data.limitTime = DTTime.Unpack(strArr[24]);
            data.periodTime = DTDateTime.Unpack(strArr[25]);
            data.canSeal = ExcelUtils.ParseBool(strArr[26]);
            data.sealNum = ExcelUtils.ParseInt(strArr[27]);
            data.canStren = ExcelUtils.ParseBool(strArr[28]);
            data.strenLv = ExcelUtils.ParseInt(strArr[29]);
            data.canEnchant = ExcelUtils.ParseBool(strArr[30]);
            data.canInsertSoul = ExcelUtils.ParseBool(strArr[31]);
            data.canInsert = ExcelUtils.ParseBool(strArr[32]);
            data.decompose = ExcelUtils.ParseBool(strArr[33]);
            data.canStore = ExcelUtils.ParseBool(strArr[34]);
            data.refineLv = ExcelUtils.ParseInt(strArr[35]);
            data.hero = ExcelUtils.ParseIntArr(strArr[36] ,';');
            data.showHero = strArr[37];
            data.recommendHero = ExcelUtils.ParseIntArr(strArr[38] ,';');
            data.equipProp = ExcelUtils.ParseInt(strArr[39]);
            data.equipSuit = ExcelUtils.ParseInt(strArr[40]);
            data.equipPlace = ExcelUtils.ParseIntArr(strArr[41] ,';');
            data.chestID = ExcelUtils.ParseInt(strArr[42]);
            data.chestCost = DTItemNum.Unpack(strArr[43]);
            data.strenTicketID = ExcelUtils.ParseInt(strArr[44]);
            data.strenTicketLv = ExcelUtils.ParseInt(strArr[45]);
            data.priceRec = ExcelUtils.ParseInt(strArr[46]);
            data.priceMin = ExcelUtils.ParseInt(strArr[47]);
            data.priceMax = ExcelUtils.ParseInt(strArr[48]);
            data.isTreasure = ExcelUtils.ParseBool(strArr[49]);
            data.tradeCD = ExcelUtils.ParseInt(strArr[50]);
            data.tradeNum = ExcelUtils.ParseInt(strArr[51]);
            data.AcutionNum = ExcelUtils.ParseInt(strArr[52]);
            data.canCopy = ExcelUtils.ParseBool(strArr[53]);
            data.canHandsel = ExcelUtils.ParseBool(strArr[54]);
            data.popupType = ExcelUtils.ParseInt(strArr[55]);
            data.value = ExcelUtils.ParseInt(strArr[56]);
            data.tipsButton = ExcelUtils.ParseInt(strArr[57]);
            data.cdType = ExcelUtils.ParseInt(strArr[58]);
            data.cdTime = ExcelUtils.ParseInt(strArr[59]);
            data.zhCnShowSmall = strArr[60];
            data.price = DTItemNum.Unpack(strArr[61]);
            return data;
        }
    }

    public class Monster
    {
        public int id;
        public int MonsterID;
        public string Name;
        public string Desc;
        public int MonsterMode;
        public int Type;
        public int IsShowPortrait;
        public int Camp;
        public int MonsterRace;
        public int Mode;
        public int avatarid;
        public int Weight;
        public int Scale;
        public int ColliderRadius;
        public float defendboxy;
        public int overHeadHeight;
        public int buffOriginHeight;
        public int MoveSpeedRatio;
        public int WalkAnimationSpeedPerent;
        public int IsNeedClear;
        public int FloatValue;
        public int ShowName;
        public int ShowHPBar;
        public int ShowFootBar;
        public int[] AbilityChange;
        public int[] BornBuff;
        public int[] BornMechanism;
        public int Height;
        public int KeepBody;
        public int[] shadowScale;
        public int BehitAudioTypeId;
        public int BehitFightBackProb;
        public int BehitFightBackSkillID;
        public int AutoFightNeedAttackFirst;
        public int GetupBati;
        public int GetupSkillRand;
        public int GetupSkillID;
        public int GetupEnityRand;
        public int GetupEntityID;
        public int[] SkillIDs;
        public int[] WalkSpeech;
        public int AttackSpeech;
        public int BirthSpeech;
        public int AIType;
        public int AIActionPath;
        public int AIDestinationSelectPath;
        public int AIEventPath;
        public int AIAttackDelay;
        public int AIDestinationChangeTerm;
        public int AIThinkTargetTerm;
        public DTSkillSetting[] AIAttackKind;
        public int DazeTime;
        public int AIWarlike;
        public int maxFixHp;
        public int blockGroundFriction;
        public int blockAttackX;
        public int IdleProtectHpPer;
        public int AirProtectHpPer;
        public int LieProtectHpPer;
        public int ModelResourceID;
        public int WeaponResSetID;
        public int ShowZPos;
    }
    public class MonsterRW
    {
        public const string excelPath = @"Monster.xlsx";
        public static Dictionary<int, Monster> LoadDict(string[][] strArr)
        {
            Dictionary<int, Monster> dict = new Dictionary<int, Monster>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static Monster Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            Monster data = new Monster();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.MonsterID = ExcelUtils.ParseInt(strArr[1]);
            data.Name = strArr[2];
            data.Desc = strArr[3];
            data.MonsterMode = ExcelUtils.ParseInt(strArr[4]);
            data.Type = ExcelUtils.ParseInt(strArr[5]);
            data.IsShowPortrait = ExcelUtils.ParseInt(strArr[6]);
            data.Camp = ExcelUtils.ParseInt(strArr[7]);
            data.MonsterRace = ExcelUtils.ParseInt(strArr[8]);
            data.Mode = ExcelUtils.ParseInt(strArr[9]);
            data.avatarid = ExcelUtils.ParseInt(strArr[10]);
            data.Weight = ExcelUtils.ParseInt(strArr[11]);
            data.Scale = ExcelUtils.ParseInt(strArr[12]);
            data.ColliderRadius = ExcelUtils.ParseInt(strArr[13]);
            data.defendboxy = ExcelUtils.ParseFloat(strArr[14]);
            data.overHeadHeight = ExcelUtils.ParseInt(strArr[15]);
            data.buffOriginHeight = ExcelUtils.ParseInt(strArr[16]);
            data.MoveSpeedRatio = ExcelUtils.ParseInt(strArr[17]);
            data.WalkAnimationSpeedPerent = ExcelUtils.ParseInt(strArr[18]);
            data.IsNeedClear = ExcelUtils.ParseInt(strArr[19]);
            data.FloatValue = ExcelUtils.ParseInt(strArr[20]);
            data.ShowName = ExcelUtils.ParseInt(strArr[21]);
            data.ShowHPBar = ExcelUtils.ParseInt(strArr[22]);
            data.ShowFootBar = ExcelUtils.ParseInt(strArr[23]);
            data.AbilityChange = ExcelUtils.ParseIntArr(strArr[24] ,';');
            data.BornBuff = ExcelUtils.ParseIntArr(strArr[25] ,';');
            data.BornMechanism = ExcelUtils.ParseIntArr(strArr[26] ,';');
            data.Height = ExcelUtils.ParseInt(strArr[27]);
            data.KeepBody = ExcelUtils.ParseInt(strArr[28]);
            data.shadowScale = ExcelUtils.ParseIntArr(strArr[29] ,';');
            data.BehitAudioTypeId = ExcelUtils.ParseInt(strArr[30]);
            data.BehitFightBackProb = ExcelUtils.ParseInt(strArr[31]);
            data.BehitFightBackSkillID = ExcelUtils.ParseInt(strArr[32]);
            data.AutoFightNeedAttackFirst = ExcelUtils.ParseInt(strArr[33]);
            data.GetupBati = ExcelUtils.ParseInt(strArr[34]);
            data.GetupSkillRand = ExcelUtils.ParseInt(strArr[35]);
            data.GetupSkillID = ExcelUtils.ParseInt(strArr[36]);
            data.GetupEnityRand = ExcelUtils.ParseInt(strArr[37]);
            data.GetupEntityID = ExcelUtils.ParseInt(strArr[38]);
            data.SkillIDs = ExcelUtils.ParseIntArr(strArr[39] ,';');
            data.WalkSpeech = ExcelUtils.ParseIntArr(strArr[40] ,';');
            data.AttackSpeech = ExcelUtils.ParseInt(strArr[41]);
            data.BirthSpeech = ExcelUtils.ParseInt(strArr[42]);
            data.AIType = ExcelUtils.ParseInt(strArr[43]);
            data.AIActionPath = ExcelUtils.ParseInt(strArr[44]);
            data.AIDestinationSelectPath = ExcelUtils.ParseInt(strArr[45]);
            data.AIEventPath = ExcelUtils.ParseInt(strArr[46]);
            data.AIAttackDelay = ExcelUtils.ParseInt(strArr[47]);
            data.AIDestinationChangeTerm = ExcelUtils.ParseInt(strArr[48]);
            data.AIThinkTargetTerm = ExcelUtils.ParseInt(strArr[49]);
            data.AIAttackKind = DTSkillSetting.Unpack(ExcelUtils.ParseStringArr(strArr[50] ,';'));
            data.DazeTime = ExcelUtils.ParseInt(strArr[51]);
            data.AIWarlike = ExcelUtils.ParseInt(strArr[52]);
            data.maxFixHp = ExcelUtils.ParseInt(strArr[53]);
            data.blockGroundFriction = ExcelUtils.ParseInt(strArr[54]);
            data.blockAttackX = ExcelUtils.ParseInt(strArr[55]);
            data.IdleProtectHpPer = ExcelUtils.ParseInt(strArr[56]);
            data.AirProtectHpPer = ExcelUtils.ParseInt(strArr[57]);
            data.LieProtectHpPer = ExcelUtils.ParseInt(strArr[58]);
            data.ModelResourceID = ExcelUtils.ParseInt(strArr[59]);
            data.WeaponResSetID = ExcelUtils.ParseInt(strArr[60]);
            data.ShowZPos = ExcelUtils.ParseInt(strArr[61]);
            return data;
        }
    }

    public class Skillinfo
    {
        public int id;
        public int SkillID;
        public string name;
        public int EffectTargetType;
        public int HasDamage;
        public int DamageType;
        public int DistanceType;
        public bool IsCanMiss;
        public bool HitGrab;
        public bool BreakSuperArmor;
        public bool BreakBlock;
        public int ClearTargetState;
        public DTPropDamegeGrowth DamageMaxCount;
        public DTPropDamegeGrowth RepeatAttackInterval;
        public int[] AttachTargetEntity;
        public int[] AttachEntity;
        public bool CanBreakUp;
        public int ElementType;
        public bool UseOtherElementType;
        public DTPropDamegeGrowth GroundFriction;
        public DTPropDamegeGrowth Attack;
        public DTPropDamegeGrowth FloatingRate;
        public DTPropDamegeGrowth HitFloatXForce;
        public DTPropDamegeGrowth HitFloatYForce;
        public bool IgnoreComboFlag;
        public int SkillBlockID;
        public int SkillShakeID;
        public DTPropDamegeGrowth Rigidity;
        public DTPropDamegeGrowth MaxHurtDistance;
        public bool CanAvoidBackAttack;
        public int BouncedFloatPer;
        public int EffectId;
        public int SoundId;
        public DTPropDamegeGrowth Penetrate;
    }
    public class SkillinfoRW
    {
        public const string excelPath = @"Skill.xlsx";
        public static Dictionary<int, Skillinfo> LoadDict(string[][] strArr)
        {
            Dictionary<int, Skillinfo> dict = new Dictionary<int, Skillinfo>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static Skillinfo Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            Skillinfo data = new Skillinfo();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.SkillID = ExcelUtils.ParseInt(strArr[1]);
            data.name = strArr[2];
            data.EffectTargetType = ExcelUtils.ParseInt(strArr[3]);
            data.HasDamage = ExcelUtils.ParseInt(strArr[4]);
            data.DamageType = ExcelUtils.ParseInt(strArr[5]);
            data.DistanceType = ExcelUtils.ParseInt(strArr[6]);
            data.IsCanMiss = ExcelUtils.ParseBool(strArr[7]);
            data.HitGrab = ExcelUtils.ParseBool(strArr[8]);
            data.BreakSuperArmor = ExcelUtils.ParseBool(strArr[9]);
            data.BreakBlock = ExcelUtils.ParseBool(strArr[10]);
            data.ClearTargetState = ExcelUtils.ParseInt(strArr[11]);
            data.DamageMaxCount = DTPropDamegeGrowth.Unpack(strArr[12]);
            data.RepeatAttackInterval = DTPropDamegeGrowth.Unpack(strArr[13]);
            data.AttachTargetEntity = ExcelUtils.ParseIntArr(strArr[14] ,';');
            data.AttachEntity = ExcelUtils.ParseIntArr(strArr[15] ,';');
            data.CanBreakUp = ExcelUtils.ParseBool(strArr[16]);
            data.ElementType = ExcelUtils.ParseInt(strArr[17]);
            data.UseOtherElementType = ExcelUtils.ParseBool(strArr[18]);
            data.GroundFriction = DTPropDamegeGrowth.Unpack(strArr[19]);
            data.Attack = DTPropDamegeGrowth.Unpack(strArr[20]);
            data.FloatingRate = DTPropDamegeGrowth.Unpack(strArr[21]);
            data.HitFloatXForce = DTPropDamegeGrowth.Unpack(strArr[22]);
            data.HitFloatYForce = DTPropDamegeGrowth.Unpack(strArr[23]);
            data.IgnoreComboFlag = ExcelUtils.ParseBool(strArr[24]);
            data.SkillBlockID = ExcelUtils.ParseInt(strArr[25]);
            data.SkillShakeID = ExcelUtils.ParseInt(strArr[26]);
            data.Rigidity = DTPropDamegeGrowth.Unpack(strArr[27]);
            data.MaxHurtDistance = DTPropDamegeGrowth.Unpack(strArr[28]);
            data.CanAvoidBackAttack = ExcelUtils.ParseBool(strArr[29]);
            data.BouncedFloatPer = ExcelUtils.ParseInt(strArr[30]);
            data.EffectId = ExcelUtils.ParseInt(strArr[31]);
            data.SoundId = ExcelUtils.ParseInt(strArr[32]);
            data.Penetrate = DTPropDamegeGrowth.Unpack(strArr[33]);
            return data;
        }
    }

    public class WeaponRes
    {
        public int id;
        public string Name;
        public int Tag;
        public string HitEffect;
        public int WavingSound;
        public int EffectId;
        public int HitSound;
        public int MissSound;
        public int SwitchSound;
        public int SwitchEffect;
        public string ResMatrix;
        public string ResSkin;
        public string ResTexture;
        public int Effect;
        public int ReinforcementEffectID;
        public int ModelResourceID;
    }
    public class WeaponResRW
    {
        public const string excelPath = @"Weapon.xlsx";
        public static Dictionary<int, WeaponRes> LoadDict(string[][] strArr)
        {
            Dictionary<int, WeaponRes> dict = new Dictionary<int, WeaponRes>();
            for (int i = 4; i < strArr.Length; i++)
            {
                var data = Load(strArr[i]);
                if (data != null)
                {
                    dict.Add(data.id, data);
                }
            }
            return dict;
        }
        public static WeaponRes Load(string[] strArr)
        {
            if (string.IsNullOrEmpty(strArr[0]))
            {
                return null;
            }
            WeaponRes data = new WeaponRes();
            data.id = ExcelUtils.ParseInt(strArr[0]);
            data.Name = strArr[1];
            data.Tag = ExcelUtils.ParseInt(strArr[2]);
            data.HitEffect = strArr[3];
            data.WavingSound = ExcelUtils.ParseInt(strArr[4]);
            data.EffectId = ExcelUtils.ParseInt(strArr[5]);
            data.HitSound = ExcelUtils.ParseInt(strArr[6]);
            data.MissSound = ExcelUtils.ParseInt(strArr[7]);
            data.SwitchSound = ExcelUtils.ParseInt(strArr[8]);
            data.SwitchEffect = ExcelUtils.ParseInt(strArr[9]);
            data.ResMatrix = strArr[10];
            data.ResSkin = strArr[11];
            data.ResTexture = strArr[12];
            data.Effect = ExcelUtils.ParseInt(strArr[13]);
            data.ReinforcementEffectID = ExcelUtils.ParseInt(strArr[14]);
            data.ModelResourceID = ExcelUtils.ParseInt(strArr[15]);
            return data;
        }
    }

}
