using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Game.Controllers;
using Random = UnityEngine.Random;


    public class Cell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _cellText;
        [SerializeField] private Transform _cellContent;
        [SerializeField] private ParticleSystem _starParticles;
        
        private Button _button;
        private int _cellIndex;
        private bool _isInteractable = true;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.image.color = GetRandomColor();
        }

        private Color GetRandomColor()
        {
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);

            return new Color(r, g, b, 1f); 
        }

        public void SetCell(int index, string text)
        {
            _starParticles.Stop();
            _cellIndex = index;

            if (_cellText != null)
                _cellText.text = text;

            _isInteractable = true;
        }

        public void OnCellClicked()
        {
            if (!_isInteractable) return;

            bool isCorrect = GameController.Instance.CheckCell(_cellIndex);
            
            if (isCorrect)
            {
                PlayCorrectAnimation();
                GameController.Instance.OnWinState();
            }
            else
            {
                PlayWrongAnimation();
            }
        }

        public void SetInteractable(bool state)
        {
            _isInteractable = state;
            _button.interactable = _isInteractable;
        }

        private void PlayCorrectAnimation()
        {
            _cellContent.DOKill();
            _cellContent.DOScale(Vector3.one * 1.2f, 0.2f)
                .SetEase(Ease.OutBounce)
                .OnComplete(() => _cellContent.DOScale(Vector3.one, 0.2f));

            if (_starParticles != null)
                _starParticles.Play();
        }

        private void PlayWrongAnimation()
        {
            _cellContent.DOKill();
            _cellContent.DOShakePosition(0.5f, new Vector3(10f, 0, 0), 20, 90, false, true)
                .SetEase(Ease.InBounce);
        }
    }

