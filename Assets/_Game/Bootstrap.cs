using UnityEngine;

namespace _Game
{
    [DefaultExecutionOrder(-100)]
    public class Bootstrap : MonoBehaviour
    {
        private void Awake()
        {
            G.ui   = FindFirstObjectByType<UI>(); 
            G.Main = FindFirstObjectByType<Main>();
            DontDestroyOnLoad(gameObject);      
        }

        private void OnDestroy()     
        {
            G.ui   = null;
            G.Main = null;
        }
    }

}