using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetCode2.Client.Core
{
    public class Loading : MonoBehaviour
    {
        [SerializeField]
        private int mainSceneIndex;

        private void Start()
        {
            SceneManager.LoadScene(mainSceneIndex);
        }
    }
}