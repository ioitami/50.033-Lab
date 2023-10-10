
using UnityEngine;
using System;
[CreateAssetMenu(menuName = "PluggableSM/Decisions/TransformStar")]
public class TransformDecisionStar : Decision
{
    public StateTransformMap1[] map1;

    public override bool Decide(StateController controller)
    {
        BuffStateController m = (BuffStateController)controller;
        // we assume that the state is named (string matched) after one of possible values in MarioState
        // convert between current state name into MarioState enum value using custom class EnumExtension
        // you are free to modify this to your own use
        MarioState toCompareState = EnumExtension.ParseEnum<MarioState>(m.currentState.name);

        // loop through state transform and see if it matches the current transformation we are looking for
        for (int i = 0; i < map1.Length; i++)
        {
            if (toCompareState == map1[i].fromState1 && m.currentPowerupType == map1[i].powerupCollected1)
            {
                return true;
            }
        }

        return false;

    }
}

[System.Serializable]
public struct StateTransformMap1
{
    public MarioState fromState1;
    public PowerupType powerupCollected1;
}
