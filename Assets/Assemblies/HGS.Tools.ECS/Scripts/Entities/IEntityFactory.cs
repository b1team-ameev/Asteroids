namespace HGS.Tools.ECS.Entities {

    public interface IEntityFactory {
        
        public IEntity Create();
        public void Destroy();

    }

}
