using UnityEngine;

public class RandomNumberGenerator 
{
    public int GetRandomNamber(int minVAlue,int maxValue) 
    {
        return Random.Range(minVAlue, maxValue);
    }

    public float GetRandomNamber(float minVAlue, float maxValue)
    {
        return Random.Range(minVAlue, maxValue);
    }
}