using HGS.Enums;
using HGS.Tools.Services.ServiceEvents;
using UnityEditor;

namespace HGS.Asteroids.Editor {

    [InitializeOnLoad]
    public static class PauseStateChanged {
        
        static PauseStateChanged() {

            EditorApplication.pauseStateChanged += LogPauseState;

        }

        private static void LogPauseState(PauseState state) {

            new EventsProxy().Events?.Raise(EventKey.OnAppPause, new UniversalEventArgs<bool>(state == PauseState.Paused));

        }
        
    }

}