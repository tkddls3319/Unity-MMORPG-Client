using Google.Protobuf.Protocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    //기본적으로 서버에서 스텟을 주기때문에 클라에서는 불필요
    #region Stat
    //[Serializable]
    //public class Stat
    //{
    //    public int level;
    //    public int maxHp;
    //    public int attack;
    //    public int totalExp;
    //}

    //[Serializable]
    //public class StatData : ILoader<int, Stat>
    //{
    //    public List<Stat> stats = new List<Stat>();

    //    public Dictionary<int, Stat> MakeDict()
    //    {
    //        Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
    //        foreach (Stat stat in stats)
    //            dict.Add(stat.level, stat);
    //        return dict;
    //    }
    //}
    #endregion

    #region SKILL
    [Serializable]
    public class Skill
    {
        public int id;
        public string name;
        public float cooldown;
        public int damage;
        public SkillType skillType;
        public ProjectileInfo projectile;
    }
    public class ProjectileInfo
    {
        public string name;
        public float speed;
        public int range;
        public string prefeb;
    }

    [Serializable]
    class SkillData : ILoader<int, Skill>
    {
        public List<Skill> skills = new List<Skill>();

        public Dictionary<int, Skill> MakeDict()
        {
            Dictionary<int, Skill> dic = new Dictionary<int, Skill>();
            foreach (var skill in skills)
                dic.Add(skill.id, skill);
            return dic;
        }
    }
    #endregion

}