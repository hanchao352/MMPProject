﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Data;
using GameServer.Entities;
using GameServer.Managers;
using Network;
using SkillBridge.Message;
using GameServer.Services;
namespace GameServer.Managers
{
    class StatusManager
    {
        Character Owner;
        private List<NStatus> Status { get; set; }
        public bool HasStatus
        {
            get { return this.Status.Count > 0; }
        }
        public StatusManager(Character owner)
        {
            this.Owner = owner;
            this.Status = new List<NStatus>();
        }
        public void AddStatus(StatusType type,int id,int value,StatusAction action)
        {
            this.Status.Add(new NStatus()
            {
                Type = type,
                Id = id,
                Value=value,
                Action=action,
            }
            );
        }

        public void AddGoldChange(int goldDelta)
        {
            if (goldDelta>0)
            {
                this.AddStatus(StatusType.Money,0,goldDelta,StatusAction.Add);
            }
            if (goldDelta < 0)
            {
                this.AddStatus(StatusType.Money, 0, -goldDelta, StatusAction.Delete);
            }
        }

        internal void AddExpChange(int expDelta)
        {
            this.AddStatus(StatusType.Exp,0,expDelta,StatusAction.Add);
        }

        internal void AddLevelUp(int levelDelta)
        {
            this.AddStatus(StatusType.Level,0,levelDelta,StatusAction.Add);
        }

        public void AddItemChange(int id,int count,StatusAction action)
        {
            
            
                this.AddStatus(StatusType.Item, id, count, action);
            
        }


        public void PostProcess(NetMessageResponse message)
        {
            if (message.statusNotify==null)
            {
                message.statusNotify = new StatusNotify();
            }
            foreach (var status in this.Status)
            {
                message.statusNotify.Status.Add(status);
            }
            this.Status.Clear();
            
        }

      
    }
}
