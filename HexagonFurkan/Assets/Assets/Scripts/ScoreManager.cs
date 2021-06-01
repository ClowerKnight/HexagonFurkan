using Hexagon;
using TMPro;
using UnityEngine;


namespace UI
{
    
    public class ScoreManager : MonoBehaviour
    {
        #region Inspector
        public static ScoreManager instance;
       
        [SerializeField] 
        private TextMeshProUGUI currentScoreText, highestScoreText;
        [SerializeField] 
        public int scorePerHex = 5;
        [HideInInspector]
        public int currentScore { get; private set; }
        [HideInInspector]
        public readonly string highestScoreKey = "highestScore";
        [HideInInspector]
        private string _highScoreEmptyText;
        [HideInInspector]
        private int _highestScore;
        #endregion


        #region Awake-Start-OnScore-OnRestart-SetCurrentScore-OnGameOver
        private void Awake()
        {
            if (instance)
            {
                
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        void Start()
        {
            SetCurrentScore(0);
            _highScoreEmptyText = highestScoreText.text;
            _highestScore = PlayerPrefs.GetInt(highestScoreKey, -1);
            if (_highestScore == -1)
            {
                highestScoreText.SetText("");
            }

            GameManager.instance.onHexagonGroupExplode += OnScore;
            GameManager.instance.onGameOver += OnGameOver;
            GameManager.instance.onRestart += OnRestart;
        }

        public void OnScore(int count)
        {
            SetCurrentScore(currentScore + count * scorePerHex);
        }

        void OnRestart()
        {
            SetCurrentScore(0);
        }

        void SetCurrentScore(int score)
        {
            currentScore = score;
            currentScoreText.SetText(currentScore.ToString());
        }
        
        public void OnGameOver()
        {
            if (currentScore > _highestScore)
            {
                PlayerPrefs.SetFloat(highestScoreKey, currentScore);
                PlayerPrefs.Save();
                _highestScore = currentScore;
                highestScoreText.SetText(_highScoreEmptyText + _highestScore);;
            }
        }
        #endregion
    }

}
