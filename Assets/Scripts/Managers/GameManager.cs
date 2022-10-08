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
    }
}
