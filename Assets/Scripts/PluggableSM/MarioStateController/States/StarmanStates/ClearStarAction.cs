using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/ClearStar")]
public class ClearStarAction : Action
{
    public override void Act(StateController controller)
    {
        BuffStateController m = (BuffStateController)controller;
        m.currentPowerupType = PowerupType.Default;
    }
}