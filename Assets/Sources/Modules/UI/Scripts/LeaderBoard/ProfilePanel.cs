using TMPro;
using UnityEngine;

namespace Sources.Modules.UI.Scripts.LeaderBoard
{
    public class ProfilePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;

        public void SetParams(string userName, int score)
        {
            if (string.IsNullOrEmpty(userName) == false)
                _name.text = userName;

            _score.text = score.ToString();
        }
    }
}
