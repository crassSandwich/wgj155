﻿using UnityEngine;
using UnityAtoms.Tags;
using UnityAtoms.BaseAtoms;
using crass;

public class TopPhysics : MonoBehaviour
{
    public Spin CurrentSpin;

    public TopStats Stats;
    public Rigidbody Rigidbody;
    public StringConstant GroundTag;

    float knockbackTimer;
    Vector3 input;
    bool collisionLock;

    void Start ()
    {
        knockbackTimer = Stats.KnockbackTime;
        CurrentSpin = Stats.InitialSpin;
    }

    void Update ()
    {
        knockbackTimer += Time.deltaTime;
    }

    void FixedUpdate ()
    {
        if (knockbackTimer >= Stats.KnockbackTime)
            Rigidbody.AddForce(input * Stats.Acceleration, ForceMode.Acceleration);

        Rigidbody.velocity = Vector3.ClampMagnitude(Rigidbody.velocity, Stats.MaxSpeed);
    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.HasTag(GroundTag)) return;

        knockbackTimer = 0;

        TopPhysics otherTop = collision.gameObject.GetComponent<TopPhysics>();

        if (otherTop == null)
        {
            CurrentSpin /= 2;
        }
        else if (!collisionLock && !otherTop.collisionLock)
        {
            redistributeSpinWith(otherTop);
        }
    }

    void OnCollisionExit (Collision collision)
    {
        if (collision.gameObject.HasTag(GroundTag)) return;

        collisionLock = false;
    }

    public void SetInput (Vector3 worldDirection)
    {
        input = worldDirection.normalized;
    }

    void redistributeSpinWith (TopPhysics otherTop)
    {
        collisionLock = true;
        otherTop.collisionLock = true;

        bool thisSpunFaster = CurrentSpin == otherTop.CurrentSpin
            ? RandomExtra.Chance(.5f) // ties are broken randomly
            : CurrentSpin > otherTop.CurrentSpin;

        TopPhysics fasterSpinningTop = thisSpunFaster ? this : otherTop;
        TopPhysics slowerSpinningTop = thisSpunFaster ? otherTop : this;
 
        Spin differential = Mathf.Min(fasterSpinningTop.CurrentSpin, Spin.MAX - slowerSpinningTop.CurrentSpin);

        fasterSpinningTop.CurrentSpin -= differential;
        slowerSpinningTop.CurrentSpin += differential;
    }
}
