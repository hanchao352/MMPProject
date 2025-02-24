﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Managers;

class UIPopCharMenu : UIWindow, IDeselectHandler
{
    public int targetId;
    public string targetName;
    public void OnDeselect(BaseEventData eventData)
    {
        var ed = eventData as PointerEventData;
        if (ed.hovered.Contains(this.gameObject))
        {
            return;
        }
        this.Close(WindowResult.None) ;
    }
    public void OnEnable()
    {
        this.GetComponent<Selectable>().Select();
        this.Root.transform.position = Input.mousePosition + new Vector3(80,0,0);
    }

    public void OnChat()
    {
        ChatManager.Instance.StartPrivateChat(targetId,targetName);
        this.Close(WindowResult.No);
    }

    public void OnAddFriend()
    {
        this.Close(WindowResult.No);
    }
    public void OnInviteTeam()
    {
        this.Close(WindowResult.No);
    }
}

