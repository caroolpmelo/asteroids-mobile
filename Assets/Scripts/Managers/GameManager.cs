using Game.Common;
using UnityEngine;

namespace Game.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private GameObject shipAmmunition;
        public GameObject ShipAmmunation
        {
            get { return shipAmmunition; }
        }

        [SerializeField]
        private GameObject asteroid;
        public GameObject Asteroid
        {
            get { return asteroid; }
        }

        [SerializeField]
        private int extraAsteroidsPerWave = 0;
        [SerializeField]
        private AsteroidsScriptableObject asteroidsSO;

        private int asteroidsWave = 0;

        private void Start()
        {
            StartNewWave();
        }

        private void StartNewWave()
        {
            asteroidsWave += 1;

            if (extraAsteroidsPerWave < 1)
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
                var randomAsteroid = asteroidsSO.GetRandomAsteroid(AsteroidsScriptableObject.Size.Small);
                randomAsteroid.AddComponent<AsteroidController>();
                Instantiate(randomAsteroid,
                    new Vector3(Random.Range(-9.0f, 9.0f), Random.Range(-6.0f, 6.0f), 0),
                    Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));
            }
        }
    }
}
