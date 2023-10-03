using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.FinderEnemyes
{
    public class FinderEnemyesForTower : AbsFinderEnemyes
    {
        public FinderEnemyesForTower(GameObject tower) : base(tower)
        {
        }

        protected override GameObject[] FindEnemyes()
        {
            return GameObject.FindGameObjectsWithTag("Enemy");
        }
    }
}