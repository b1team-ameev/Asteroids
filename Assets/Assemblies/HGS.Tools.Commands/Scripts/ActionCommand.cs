using System;
using UnityEngine;

namespace HGS.Tools.Commands {

    public abstract class ActionCommand: ICommand {    
        
        protected static ActionHelper globalActionHelper;
        protected ActionHelper actionHelper;

        protected readonly bool isActionDontDestroyOnLoad;

        protected Action action;
        protected readonly float delay;

        protected Coroutine coroutine;

        public ActionCommand(Action action = null, float delay = 0.5f, bool isActionDontDestroyOnLoad = false) {

            this.action = action;
            this.delay = delay;

            this.isActionDontDestroyOnLoad = isActionDontDestroyOnLoad;

        }

        public ActionCommand(Action action, bool isActionDontDestroyOnLoad): this(action, 0f, isActionDontDestroyOnLoad) { }
        public ActionCommand(bool isActionDontDestroyOnLoad): this(null, 0f, isActionDontDestroyOnLoad) { }
        public ActionCommand(Action action): this(action, 0f) { }

        public void Execute() {

            if (actionHelper == null) {

                actionHelper = CreateActionHelper();

            }
            
            StartExecute();

        }

        protected virtual ActionHelper CreateActionHelper() {

            // если не предполагается существование объекта между загрузками сцен,
            // то используется локальная переменная; иначе статика globalActionHelper
            if (isActionDontDestroyOnLoad) {

                if (actionHelper == null) {

                    actionHelper = ActionHelper.CreateActionHelper(true);

                }
            
                return actionHelper;

            }
            else {

                if (globalActionHelper == null) {

                    globalActionHelper = ActionHelper.CreateActionHelper();

                }

                return globalActionHelper;

            }   

        }

        public virtual void Stop() {

            if (coroutine != null && actionHelper != null) {

                actionHelper.StopCoroutine(coroutine);
                coroutine = null;

            }

            DestroyActionHelper();

        }

        protected abstract void StartExecute();    

        protected void DestroyActionHelper() {

            if (isActionDontDestroyOnLoad) {

                ActionHelper.DestroyActionHelper(actionHelper);
                actionHelper = null;

            }

        }

    }

}