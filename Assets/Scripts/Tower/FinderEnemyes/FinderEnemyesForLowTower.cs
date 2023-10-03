using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.FinderEnemyes
{
    public class FinderEnemyesForLowTower : AbsFinderEnemyes
    {
        public FinderEnemyesForLowTower(GameObject tower) : base(tower)
        {
        }

        protected override GameObject[] FindEnemyes()
        {
            Enemy[] enemyes = GameObject.FindObjectsOfType<Enemy>();
            GameObject[] result = new GameObject[enemyes.Length];
            for(int i = 0; i < result.Length; i++)
            {
                result[i] = enemyes[i].gameObject;
            }
            return result;
        }
    }
}