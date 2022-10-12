using Game.Common;
using UnityEngine;
using UnityEngine.UI;

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
        private GameObject sceneObjects;
        public GameObject SceneObjects { get { return sceneObjects; } }

        [SerializeField]
        private int extraAsteroidsPerWave = 4;
        [SerializeField]
        private AsteroidsScriptableObject asteroidsSO;

        [SerializeField]
        private Transform canvasTransform;
        public Transform CanvasTransform { get { return canvasTransform; } }

        private int score;
        private int hiscore;
        private int asteroidsRemaining;
        private int lives;
        private int wave;

        [SerializeField]
        private Text scoreText;
        [SerializeField]
        private Text livesText;
        [SerializeField]
        private Text waveText;
        [SerializeField]
        private Text hiscoreText;

        private int asteroidsWave = 0;
        private int currentNumOfAsteroids = 0;

        private void Start()
        {
            hiscore = PlayerPrefs.GetInt("hiscore", 0);
            BeginGame();
            //StartNewWave();
        }

        private void BeginGame()
        {
            score = 0;
            lives = 3;
            wave = 1;

            scoreText.text = "SCORE:" + score;
            hiscoreText.text = "HISCORE:" + hiscore;
            livesText.text = "LIVES:" + lives;
            waveText.text = "WAVE:" + wave;

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
            DestroyExistingAsteroids();

            if (currentNumOfAsteroids > 24)
            {
                DestroyExistingAsteroids();
                return;
            }

            for (int i = 0; i < asteroidsCount; i++)
            {
                // TODO change size at every wave
                var asteroidSize = asteroidsWave < 4 ? AsteroidsScriptableObject.Size.Small : AsteroidsScriptableObject.Size.Medium;
                var randomAsteroid = asteroidsSO.GetRandomAsteroid(asteroidSize);

                var newAsteroid = Instantiate(randomAsteroid,
                    new Vector3(Random.Range(-9.0f, 9.0f), Random.Range(-6.0f, 6.0f), 0),
                    Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)),
                    sceneObjects.transform);
                newAsteroid.AddComponent<AsteroidController>();
                newAsteroid.AddComponent<EuclideanTorusController>();
                newAsteroid.AddComponent<PolygonCollider2D>();
                newAsteroid.tag = "Enemy";

                if (asteroidSize == AsteroidsScriptableObject.Size.Medium)
                {
                    newAsteroid.GetComponent<AsteroidController>().isBig = true;
                }

                currentNumOfAsteroids++;
            }
        }

        public void SpawnSmallerAsteroids(Vector3 position)
        {
            var smallAsteroid = 
                asteroidsSO.GetRandomAsteroid(AsteroidsScriptableObject.Size.Small);
            // spawn small asteroids
            var asteroidOne = Instantiate(
                smallAsteroid,
                new Vector3(
                    position.x - .5f,
                    position.y - .5f, 0
                ),
                Quaternion.Euler(0, 0, 90)
            );
            var asteroidTwo = Instantiate(
                smallAsteroid,
                new Vector3(
                    position.x + .5f,
                    position.y + .0f, 0
                ),
                Quaternion.Euler(0, 0, 0)
            );
            var asteroidThree = Instantiate(
                smallAsteroid,
                new Vector3(
                    position.x + .5f,
                    position.y - .5f, 0
                ),
                Quaternion.Euler(0, 0, 270)
            );
            asteroidOne.tag = "Enemy";
            asteroidTwo.tag = "Enemy";
            asteroidThree.tag = "Enemy";
        }

        void DestroyExistingAsteroids()
        {
            GameObject[] asteroids =
                GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject current in asteroids)
                Destroy(current);
        }


        public void DecrementLives()
        {
            lives--;
            livesText.text = "LIVES:" + lives;

            if (lives < 1) // run out of lives
                BeginGame(); // restart
        }

        public void IncrementScore()
        {
            score++;

            scoreText.text = "SCORE:" + score;

            if (score > hiscore)
            {
                hiscore = score;
                hiscoreText.text = "HISCORE:" + hiscore;

                // save new hiscore
                PlayerPrefs.SetInt("hiscore", hiscore);
            }

            if (asteroidsRemaining < 1)
            {
                //wave++; // new wave
                //SpawnAsteroids();
                StartNewWave();
            }
        }
    }
}
