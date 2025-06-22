using _Game.Clipboard;
using _Game.Crime;
using _Game.Family.ExecutionerSim.Core;
using _Game.Handle;
using UnityEngine;

namespace _Game
{
    public static class G
    {
        public static UI ui;
        public static Main Main;
        public static Feel feel;
        
        public static CrimeInitializator crimeInitializator;
        public static RunState RunState;

        public static Door Door;
        public static DisplayFolder Clipboard;
        public static FolderPickup FolderPickup;

        public static DayEndView DayEndView;
        public static FamilyView FamilyView;
        public static FamilySystem FamilySystem;

        public static GameProgressTracker ProgressTracker;

        public static Transform BribeHand;

        public static BribeEvent BribeEvent;
        public static LeverSwitch LeverSwitch;
    }
}