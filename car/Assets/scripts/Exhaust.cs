﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exhaust : MonoBehaviour
{
    [SerializeField] private ParticleSystem left;
    [SerializeField] private ParticleSystem right;
    [SerializeField] private CarControl1 control;

    [SerializeField] private float maxSpeed = 120f;
    [SerializeField] private float minROT = 5f;
    [SerializeField] private float maxROT = 15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ExhaustEmission(left); ExhaustEmission(right);
    }

    void ExhaustEmission(ParticleSystem exhaust)
    {
        float ROTrange = maxROT - minROT;
        float absoluteSpeed = Mathf.Abs(control.speedOnKm);
        float normalizedSpeed = absoluteSpeed / maxSpeed;

        var emission = exhaust.emission;
        emission.rateOverTime = minROT + ROTrange * normalizedSpeed;
    }
}
