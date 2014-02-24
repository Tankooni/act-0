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

    public static Vector3 rotateXZ(Vector3 vector, float rAngle) {
        Vector3 rVec = new Vector3(0, vector.y, 0);

        rVec.x = (float) (vector.x * Math.Sin(rAngle) - vector.z * Math.Cos(rAngle));
        rVec.y = (float) (vector.x * Math.Cos(rAngle) + vector.z * Math.Sin(rAngle));

        return rVec;
    }
}
