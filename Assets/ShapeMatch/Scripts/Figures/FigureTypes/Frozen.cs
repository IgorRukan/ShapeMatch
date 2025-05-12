using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Frozen : FigureType
{
    public int numCountToUnfreeze;

    public override FigureFactory.FigureTypeEnum FeatureType => FigureFactory.FigureTypeEnum.Frozen;
    protected override void OnEnable()
    {
        base.OnEnable();
        gameManager.Match += UnfreezeCheck;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        gameManager.Match -= UnfreezeCheck;
    }
    
    protected override void TypeFeatures()
    {
        base.TypeFeatures();
        
        figure.isFrozen = true;
    }

    private void UnfreezeCheck(int matchedCount)
    {
        if (matchedCount >= numCountToUnfreeze)
        {
            figure.isFrozen = false;
        }
    }
    
}
