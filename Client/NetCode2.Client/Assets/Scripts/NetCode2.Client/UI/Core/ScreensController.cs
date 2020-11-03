using System.Collections.Generic;
using UnityEngine;

namespace NetCode2.Client.UI.Core
{
    public class ScreensController : MonoBehaviour
    {
        [SerializeField]
        private List<BaseScreen> screens = null;

        private BaseScreen currentScreen;
        
        public TScreen ShowScreen<TScreen>() where TScreen : BaseScreen
        {
            CloseCurrentScreen();

            var screenPrefab = screens.Find(bs => (bs.GetType() == typeof(TScreen)));

            currentScreen = Instantiate(screenPrefab, transform);
            currentScreen.transform.SetAsFirstSibling();

            return currentScreen.GetComponent<TScreen>();
        }

        private void CloseCurrentScreen()
        {
            if (currentScreen != null)
            {
                currentScreen.Close();
                currentScreen = null;
            }
        }
    }
}