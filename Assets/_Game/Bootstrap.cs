using _Game.Clipboard;
using _Game.Crime;
using _Game.Family.ExecutionerSim.Core;
using _Game.Handle;
using UnityEngine;

namespace _Game
{
    [DefaultExecutionOrder(-100)]
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Transform bribeHand;
        private void Awake()
        {
            G.BribeHand = bribeHand;
            G.ui   = FindFirstObjectByType<UI>(); 
            G.Main = FindFirstObjectByType<Main>();
            G.crimeInitializator = FindFirstObjectByType<CrimeInitializator>();
            G.RunState = FindFirstObjectByType<RunState>();
            G.Door = FindFirstObjectByType<Door>();
            G.feel = FindFirstObjectByType<Feel>();
            G.Clipboard = FindFirstObjectByType<DisplayFolder>();
            G.DayEndView = FindFirstObjectByType<DayEndView>();
            G.FamilySystem = FindFirstObjectByType<FamilySystem>();
            G.FamilyView = FindFirstObjectByType<FamilyView>();
            G.FolderPickup = FindFirstObjectByType<FolderPickup>();
            G.ProgressTracker = FindFirstObjectByType<GameProgressTracker>();
            G.BribeEvent = FindFirstObjectByType<BribeEvent>();
            G.LeverSwitch = FindFirstObjectByType<LeverSwitch>();
            G.Micro = FindFirstObjectByType<Micro>();
            
            DontDestroyOnLoad(gameObject);      
        }

        /*private void OnDestroy()     
        {
            G.ui   = null;
            G.Main = null;
            G.crimeInitializator = null;
            G.Door = null;
            G.RunState = null;
            G.feel = null;
            G.Clipboard = null;
            G.FamilySystem = null;
            G.DayEndView = null;
            G.FamilyView = null;
            G.FolderPickup = null;
            G.ProgressTracker = null;
            G.BribeEvent = null;
            G.LeverSwitch = null;
        }*/
    }

}