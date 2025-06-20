using _Game.Clipboard;
using _Game.Crime;
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
            G.crimeInitializator = FindFirstObjectByType<CrimeInitializator>();
            G.RunState = FindFirstObjectByType<RunState>();
            G.Door = FindFirstObjectByType<Door>();
            G.feel = FindFirstObjectByType<Feel>();
            G.Clipboard = FindFirstObjectByType<DisplayFolder>();
            DontDestroyOnLoad(gameObject);      
        }

        private void OnDestroy()     
        {
            G.ui   = null;
            G.Main = null;
            G.crimeInitializator = null;
            G.Door = null;
            G.RunState = null;
            G.feel = null;
            G.Clipboard = null;
        }
    }

}