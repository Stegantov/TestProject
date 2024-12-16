using UnityEngine;
using TMPro;

namespace Game.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        public int TargetIndex { get; private set; }
 
        [SerializeField] private GridManager _gridManager;
        [SerializeField] private TextMeshProUGUI _targetText;

        private LevelData _currentLevelData;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetTargetIndex(int maxIndex, LevelData levelData)
        {
            TargetIndex = Random.Range(0, maxIndex);
            _currentLevelData = levelData;

            string targetValue = levelData.items[TargetIndex];
            _targetText.text = $"Find {targetValue}";
            
        }

        public void OnWinState()
        {
            if (_gridManager.IsLevelComplete())
            {
                Debug.Log("All levels are complete!");
                _gridManager.LockAllCells();
                _gridManager.ShowRestartButton();
            }
            else
            {
                _gridManager.GenerateLevel();
            }
        }

        public bool CheckCell(int index)
        {
            return index == TargetIndex;
        }
    }
}