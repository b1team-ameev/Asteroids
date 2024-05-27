using System;
using System.Collections;
using UnityEngine.SceneManagement;
using HGS.Enums;
using HGS.Tools.Services.ServiceEvents;
using HGS.Tools.Commands;

using Scene = HGS.Enums.Scene;

namespace HGS.Asteroids.Tools.Commands {

    public class SceneLoadCommand: ActionCommand {

        private readonly Scene scene;

        public SceneLoadCommand(Scene scene, Action action = null): base(action, true) {

            this.scene = scene;

        }

        protected override void StartExecute() {

            if (actionHelper != null) {

                actionHelper.StartCoroutine(LoadSceneAsync(scene, action));

            }

        }

        private IEnumerator LoadSceneAsync(Scene scene, Action action = null) {
            
            yield return SceneManager.LoadSceneAsync((int)scene);

            try {

                if (action != null) {

                    action();

                }

                new EventsProxy().Events?.Raise(EventKey.OnSceneLoaded, new UniversalEventArgs<Scene>(scene));

            }
            finally {

                DestroyActionHelper();

            }

        }

    }

}