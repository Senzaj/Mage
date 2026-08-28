using System.Collections.Generic;
using Sources.Modules.YandexSDK.Scripts;
using UnityEngine;

namespace Sources.Modules.UI.Scripts.LeaderBoard
{
    public class LeaderList : MonoBehaviour
    {
        [SerializeField] private YandexSdk _yandex;
        [SerializeField] private string _leaderboardName = "Leaderboard";
        [SerializeField] private ProfilePanel _resultTemplate;
        [SerializeField] private GameObject _content;
        [SerializeField] private int _minPlayersCount = 1;
        [SerializeField] private int _maxPlayersCount;

        private List<ProfilePanel> _results = new();

        public void SetLeaderboardScore(int score)
        {
        }

        public void ShowResults()
        {
        }

        public void Clear()
        {
            if (_results.Count > 0)
            {
                while (_results.Count > 0)
                {
                    Destroy(_results[0].gameObject);
                    _results.Remove(_results[0]);
                }
            }
        }

    }
}
