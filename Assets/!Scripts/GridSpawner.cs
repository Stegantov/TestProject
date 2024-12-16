using UnityEngine;
using DG.Tweening;


    public class GridSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private float _cellSize = 50f;
        [SerializeField] private float _spawnAnimationDuration = 1f;
        [SerializeField] private float _spawnDelay = 0.1f;

        public void SpawnGrid(LevelData levelData, Transform parent)
        {
            for (int row = 0; row < levelData.rows; row++)
            {
                for (int col = 0; col < levelData.columns; col++)
                {
                    GameObject cell = InstantiateCell(parent, row, col, levelData);
                    AnimateCellSpawn(cell, row, col, levelData.columns);
                }
            }
        }

        private GameObject InstantiateCell(Transform parent, int row, int col, LevelData levelData)
        {
            GameObject cell = Instantiate(_cellPrefab, parent);
            cell.transform.localPosition = new Vector3(col * _cellSize, -row * _cellSize, 0);
            cell.transform.localScale = Vector3.zero;

            InitializeCellContent(cell, row, col, levelData);
            return cell;
        }

        private void InitializeCellContent(GameObject cell, int row, int col, LevelData levelData)
        {
            int index = row * levelData.columns + col;
            if (index >= levelData.items.Count) return;

            if (cell.TryGetComponent(out Cell cellComponent))
            {
                cellComponent.SetCell(index, levelData.items[index]);
            }
        }

        private void AnimateCellSpawn(GameObject cell, int row, int col, int columns)
        {
            float delay = (row * columns + col) * _spawnDelay;

            cell.transform.DOScale(Vector3.one, _spawnAnimationDuration)
                .SetEase(Ease.OutBounce)
                .SetDelay(delay);
        }
    }
