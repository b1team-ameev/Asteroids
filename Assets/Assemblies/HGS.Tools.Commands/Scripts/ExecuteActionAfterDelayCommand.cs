using System;

namespace HGS.Tools.Commands {

    public class ExecuteActionAfterDelayCommand: ActionCommand {
        
        public ExecuteActionAfterDelayCommand(Action action, float delay = 0.5f): base(action, delay) {

        }

        protected override void StartExecute() {

            if (actionHelper != null) {

                coroutine = actionHelper.StartCoroutine(actionHelper?.ActionAfterWaitTime(delay, () => {

                    action?.Invoke();

                    coroutine = null;

                }));

            }

        }

    }

}