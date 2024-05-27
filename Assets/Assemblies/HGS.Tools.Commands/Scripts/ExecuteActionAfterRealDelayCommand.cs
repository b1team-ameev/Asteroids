using System;

namespace HGS.Tools.Commands {

    public class ExecuteActionAfterRealDelayCommand: ActionCommand {    
        
        public ExecuteActionAfterRealDelayCommand(Action action, float delay = 0.5f): base(action, delay) {

        }

        protected override void StartExecute() {

            if (actionHelper != null) {

                coroutine = actionHelper.StartCoroutine(actionHelper?.ActionAfterWaitRealTime(delay, () => {

                    action?.Invoke();

                    coroutine = null;

                }));

            }

        }

    }

}