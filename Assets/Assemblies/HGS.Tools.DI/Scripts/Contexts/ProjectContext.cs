namespace HGS.Tools.DI.Contexts {

    public class ProjectContext: Context {

        #region Awake/Start/Update/FixedUpdate

        protected new void Awake() {

            base.Awake();

            Init();

            Bind();

            DontDestroyOnLoad(gameObject);

        }

        #endregion

        private void Init()  {

            Container?.BindFromInstance<IContext, ProjectContext>(this);

        }

    }

}
