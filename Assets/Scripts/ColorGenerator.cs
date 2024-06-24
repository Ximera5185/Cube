using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
    private List<Color> _colors = new ()
        {
            Color.blue,
            Color.green,
            Color.red,
            Color.yellow,
            Color.cyan,
            Color.magenta
        };
        
    public Color GetRandomColor()
    {
         return _colors [Random.Range(0, _colors.Count)];
    }

}