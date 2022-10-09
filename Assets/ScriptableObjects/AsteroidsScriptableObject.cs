using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AsteroidsScriptableObjects", order = 1)]
public class AsteroidsScriptableObject : ScriptableObject
{
    public enum Size
    {
        Huge,
        Big,
        Medium,
        Small
    }

    [System.Serializable]
    public struct Asteroids
    {
        public Size size;
        public GameObject[] prefabs;
    }

    public Asteroids[] asteroidsSizes;

    public GameObject GetRandomAsteroid(Size asteroidSize)
    {
        var asteroidChosenSize = Array.Find(asteroidsSizes, item => item.size == asteroidSize);
        var index = UnityEngine.Random.Range(0, asteroidChosenSize.prefabs.Length - 1);
        return asteroidChosenSize.prefabs[index];
    }
}
