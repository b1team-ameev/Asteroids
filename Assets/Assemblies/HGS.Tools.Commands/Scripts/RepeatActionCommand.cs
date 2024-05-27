using System;

namespace HGS.Tools.Commands {

    public class RepeatActionCommand: ActionCommand {
        
        private bool isStop = false;
        
        public RepeatActionCommand(Action action, float delay = 0.5f): base(action, delay) {

        }

        protected override void StartExecute() {

            if (actionHelper != null && !isStop) {

                coroutine = actionHelper.StartCoroutine(actionHelper?.ActionAfterWaitTime(delay, () => {

                    action?.Invoke();

                    if (!isStop) {

                        StartExecute();

                    }
                    else {

                        coroutine = null;

                    }

                }));

            }

        }

        public override void Stop() {

            isStop = true;

            base.Stop();

        }

    }

}