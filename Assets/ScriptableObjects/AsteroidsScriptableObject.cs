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
}
