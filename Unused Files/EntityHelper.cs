using Mafi;
using Mafi.Core.Entities;
using Mafi.Core.Prototypes;




namespace COIExtended.EntityHelper
{
    [GlobalDependency(RegistrationMode.AsAllInterfaces, false, false)]
    public class EntityHelper
    {
        public EntityHelper(EntitiesManager entitiesManager, ProtosDb protosDb)
        {
            m_entitiesManager = entitiesManager;
            m_protosDb = protosDb;  
        }



        private EntitiesManager m_entitiesManager;
        private ProtosDb m_protosDb;
    }
}
