using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/SetupSTARInvincibility")]
public class StarInvincibleAction : Action
{

    public Color colchange;
    public override void Act(StateController controller)
    {
        BuffStateController m = (BuffStateController)controller;

        m.gameObject.GetComponent<SpriteRenderer>().color = colchange;
    }
}
