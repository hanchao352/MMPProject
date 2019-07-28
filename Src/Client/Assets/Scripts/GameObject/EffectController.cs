﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


 class EffectController:MonoBehaviour
 {
    public float lifeTime = 1;
    float time = 0;

    EffectType type;
    Transform target;
    Vector3 targetPos;
    Vector3 startPos;
    Vector3 offset;

    private void OnEnable()
    {
        if (type!=EffectType.Bullet)
        {
            StartCoroutine(Run());
        }
    }

    IEnumerator Run()
    {
        yield return new WaitForSeconds(this.lifeTime);
        this.gameObject.SetActive(false);
    }

    internal void Init(EffectType type,Transform source,Transform target,Vector3 offset,float duration)
    {
        this.type = type;
        this.target = target;
        if(duration>0)
            this.lifeTime = duration;
        this.time = 0;
        if (type==EffectType.Bullet)
        {
            this.startPos = this.transform.position;
            this.offset = offset;
            this.targetPos = target.position + offset;
        }
        else if(type==EffectType.Hit)
        {
            this.transform.position = target.position + offset;
        }
    }

    private void Update()
    {
        if (type==EffectType.Bullet)
        {
            this.time += Time.deltaTime;
            if (this.target!=null)
            {
                this.targetPos = this.target.position + offset;
            }
            this.transform.LookAt(this.targetPos);

            if (Vector3.Distance(this.targetPos,this.transform.position)<0.5f)
            {
                Destroy(this.gameObject);
                return;
            }
            if (this.lifeTime>0&&this.time>=this.lifeTime)
            {
                Destroy(this.gameObject);
                return;
            }
            this.transform.position = Vector3.Lerp(this.transform.position,this.targetPos,Time.deltaTime/(this.lifeTime-this.time));
        }
    }
}

