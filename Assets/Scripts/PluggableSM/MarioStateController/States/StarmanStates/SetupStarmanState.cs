using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/SetupStarmanState")]
public class SetupStarmanState : ActionCol
{
    public Color mariocol;
    public override void Act(Color col)
    {
        GameObject.Find("Mario").GetComponent<SpriteRenderer>().color = col;
    }
}
