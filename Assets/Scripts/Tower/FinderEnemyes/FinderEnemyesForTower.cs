using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower.FinderEnemyes
{
    public class FinderEnemyesForTower : AbsFinderEnemyes
    {
        public override GameObject[] FindEnemyes()
        {
            return GameObject.FindGameObjectsWithTag("Enemy");
        }
    }
}