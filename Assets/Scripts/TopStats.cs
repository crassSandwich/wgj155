﻿using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTopStats.asset", menuName = "Top Stats")]
public class TopStats : ScriptableObject
{
    public float Acceleration, MaxSpeed, KnockbackTime;
    public Spin InitialSpin;
    public float SpinAcceleration, SpinDeceleration, MaxSpinDelta;
    public float SpinClutchPoint; // if the top is losing spin at a higher speed than this value and the player does the acceleration input, start accelerating at this speed
}
