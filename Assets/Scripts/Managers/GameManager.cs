using Game.Common;
using Game.Controllers;
using UnityEngine;

namespace Game.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private GameObject sceneObjects;
        [SerializeField]
        private int extraAsteroidsPerWave = 0;
        [SerializeField]
        private AsteroidsScriptableObject asteroidsSO;

        [SerializeField]
        private Transform canvasTransform;
        public Transform CanvasTransform { get { return canvasTransform; } }

        private int asteroidsWave = 0;

        private void Start()
        {
            StartNewWave();
        }

        private void StartNewWave()
        {
            asteroidsWave += 1;

            if (extraAsteroidsPerWave <= 1)
            {
                // if not set, follow wave number
                extraAsteroidsPerWave = asteroidsWave;
            }

            SpawnAsteroids(asteroidsWave * extraAsteroidsPerWave);
        }

        private void SpawnAsteroids(int asteroidsCount)
        {
            // destroy existing asteroids; hasPlayerLost
            for (int i = 0; i < asteroidsCount; i++)
            {
                // TODO change size at every wave
                var randomAsteroidSizeIndex = Random.Range(0, System.Enum.GetValues(typeof(AsteroidsScriptableObject.Size)).Length - 1);
                // not working
                var randomAsteroid = asteroidsSO.GetRandomAsteroid(
                    (AsteroidsScriptableObject.Size)System.Enum.GetValues(typeof(AsteroidsScriptableObject.Size)).GetValue(randomAsteroidSizeIndex));
                var newAsteroid = Instantiate(randomAsteroid,
                    new Vector3(Random.Range(-9.0f, 9.0f), Random.Range(-6.0f, 6.0f), 0),
                    Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)),
                    sceneObjects.transform);

                newAsteroid.AddComponent<AsteroidController>();
                newAsteroid.AddComponent<EuclideanTorusController>();
            }
        }
    }
}
