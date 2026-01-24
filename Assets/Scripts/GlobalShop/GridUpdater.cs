using System;
using System.Security.Cryptography;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GlobalShop
{
    public class GridUpdater : MonoBehaviour
    {
        [SerializeField] private RectTransform Content;
        [SerializeField] private GridLayoutGroup GridLayoutGroup;
        [SerializeField] private float MaxCellSize;

        private void OnEnable()
        {
            Observable.NextFrame().Subscribe((_) => UpdateLayout()).AddTo(this);
        }

        private void UpdateLayout()
        {
            MaxCellSize = Content.rect.width / 3;
            float initWidth = 250;
            float initHigh = 300;
            float newWidthCell = GetCellWidth();
            float cof = newWidthCell / initWidth;
            float newHighCell = initHigh * cof;
            GridLayoutGroup.cellSize = new Vector2(newWidthCell, newHighCell);
            Destroy(this);
        }

        private float GetCellWidth()
        {
            int countCell = 1;
            float widthContent = Content.rect.width;
            float newWidthCell = (widthContent - 20) / countCell;

            while (newWidthCell > MaxCellSize)
            {
                countCell++;
                newWidthCell = ((widthContent - 20) - countCell * 20) / countCell;
            }

            return newWidthCell;
        }
    }
}