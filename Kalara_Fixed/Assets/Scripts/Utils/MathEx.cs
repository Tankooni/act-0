using UnityEngine;
using System;

public static class MathEx
{
    public static Vector2 GenUnitVec2() {
        //get a random value for the x position then find its complement
        //for y
        Vector2 rVec = new Vector2();
        rVec.x = (UnityEngine.Random.value * 2) - 1;
        rVec.y = (float)Math.Sqrt(1 - (rVec.x * rVec.x));

        return rVec;
    }
}
