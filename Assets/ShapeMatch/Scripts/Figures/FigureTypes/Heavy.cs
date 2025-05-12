using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavy : FigureType
{
    public float mass;
    public override FigureFactory.FigureTypeEnum FeatureType => FigureFactory.FigureTypeEnum.Heavy;
    protected override void TypeFeatures()
    {
        rb.gravityScale += mass;
    }
}
