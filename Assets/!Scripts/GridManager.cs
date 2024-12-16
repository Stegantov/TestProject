using Game.Controllers;
using UnityEngine;


    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Transform _gridParent;
        [SerializeField] private GridSpawner _gridSpawner;
        [SerializeField] private LevelData[] _levelDataArray;
        [SerializeField] private GameObject _restartButton;

        private LevelData _currentLevelData;
        private int _currentLevelIndex;

        private void Start()
        {
            GenerateLevel();
        }

        public void Restart()
        {
            _currentLevelIndex = 0;
            GenerateLevel();
            _restartButton.SetActive(false);
        }

        public void ShowRestartButton()
        {
            _restartButton.SetActive(true);
        }

        public bool IsLevelComplete()
        {
            return _currentLevelIndex >= _levelDataArray.Length;
        }

        public void LockAllCells()
        {
            foreach (Transform child in _gridParent)
            {
                if (child.TryGetComponent(out Cell cell))
                {
                    cell.SetInteractable(false);
                }
            }
        }

        public void GenerateLevel()
        {
            _currentLevelData = _levelDataArray[_currentLevelIndex++];
            GenerateGrid(_currentLevelData);
        }

        private void GenerateGrid(LevelData levelData)
        {
            ClearGrid();
            GameController.Instance.SetTargetIndex(levelData.items.Count, levelData);
            _gridSpawner.SpawnGrid(levelData, _gridParent);
        }

        private void ClearGrid()
        {
            foreach (Transform child in _gridParent)
            {
                Destroy(child.gameObject);
            }
        }
    }
