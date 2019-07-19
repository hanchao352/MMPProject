﻿using Managers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

 public class UISkillSlots:MonoBehaviour
 {
    public UISkillSlot[] slots;
    private void Start()
    {
       
    }

   

    public void UpdateSkills()
    {
        if (User.Instance.CurrentCharacter==null) 
        {
            return;
        }
        var Skills = User.Instance.CurrentCharacter.SkillMgr.Skills;
        int skillIdx = 0;
        foreach (var skill in Skills)
        {
            slots[skillIdx].SetSkill(skill);
            skillIdx++;
        }
    }
}

