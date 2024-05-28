namespace HGS.Tools.DI {

    public class DIContainerWrapper<TInterface, TType> where TType: TInterface {
        
        private DIContainer container;
        
        public DIContainerWrapper(DIContainer container) {

            this.container = container;

        }

        public DIContainerWrapper<TInterface, TType> AsSingle(bool isCreate = true) {
            
            container?.BindAsSingle<TInterface, TType>(isCreate);

            return this;

        }         

        public DIContainerWrapper<TInterface, TType> BindIsOver() {
            
            container?.BindIsOver<TInterface>();

            return this;

        } 

    }

}
