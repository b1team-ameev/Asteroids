using System;
using System.Collections;

namespace HGS.Tools.Commands {

    public class CoroutineCommand: ActionCommand {
        
        private Func<IEnumerator> enumerator;

        public CoroutineCommand(IEnumerator enumerator, bool isActionDontDestroyOnLoad = false): base(isActionDontDestroyOnLoad) {

            this.enumerator = () => enumerator;

        }

        protected override void StartExecute() {

            if (actionHelper != null) {

                coroutine = actionHelper.StartCoroutine(WrapperCoroutine());

            }

        }

        public override void Stop() {

            base.Stop();

            enumerator = null;

        }

        private IEnumerator WrapperCoroutine() {
            
            if (enumerator != null) {

                yield return enumerator();

            }

            coroutine = null;

            DestroyActionHelper();

        }

    }

}