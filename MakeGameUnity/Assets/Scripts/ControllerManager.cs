using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletPattern;

public class ControllerManager
{
    private static ControllerManager Instance = null;

    public static ControllerManager GetInstance()
    {
        if (Instance == null)
            Instance = new ControllerManager();
        return Instance;
    }

    public bool DirLeft;
    public bool DirRight;

    public float PlayerSpeed = 5.0f;
    public int EXP = 0;
    
    public int SkillPoint = 0; // 스킬 포인트

    // 스킬
    public float BulletSpeed = 10.0f;
    public float BulletDamage = 1;

    public int EnemyDamage = 3;
    public int BossDamage = 5;

    public int Player_HP = 10;

    public int ToBossDamage = 10;
}


/*
    ControllerManager.GetInstance().
*/