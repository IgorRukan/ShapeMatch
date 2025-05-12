using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FigureData", menuName = "ScriptableObjects/FigureData", order = 1)]
public class FigureData : ScriptableObject
{
    public List<Sprite> shape;
    public List<Sprite> shapeMask;
    public List<Color> color;
    public List<Sprite> icon;
}
