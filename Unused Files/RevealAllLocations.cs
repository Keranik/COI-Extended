using Mafi.Core.Entities;
using Mafi;

namespace COIExtended.DebugCommandsProcessor
{
    // Token: 0x02000AFF RID: 2815
    [GlobalDependency(RegistrationMode.AsAllInterfaces, false, false)]
    internal class DebugCommandsProcessor
    {
        private readonly EntitiesManager m_entitiesManager;
        // Token: 0x06005CB3 RID: 23731 RVA: 0x0011F464 File Offset: 0x0011D664
        public DebugCommandsProcessor(EntitiesManager entitiesManager)
        {
            m_entitiesManager = entitiesManager;
        }

        public static void RevealAllVillages(EntitiesManager entityManager)
        {
            m_entitiesManager = entityManager;
        }

        // Token: 0x06005CB4 RID: 23732 RVA: 0x0011F478 File Offset: 0x0011D678
        

        // Token: 0x04002A63 RID: 10851
        
    }
}
