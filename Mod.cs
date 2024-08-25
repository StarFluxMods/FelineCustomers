using Kitchen;
using KitchenMods;
using Unity.Collections;
using Unity.Entities;

namespace FelineCustomers
{
    [UpdateInGroup(typeof(CustomerSchedulingGroup))]
    [UpdateAfter(typeof(CreateCustomerSchedule))]
    public class Mod : RestaurantSystem, IModSystem
    {
        public const string MOD_GUID = "com.starfluxgames.felinecustomers";
        public const string MOD_NAME = "Feline Customers";
        public const string MOD_VERSION = "0.1.0";
        public const string MOD_AUTHOR = "StarFluxGames";
        
        private EntityQuery ScheduledCustomers;

        protected override void Initialise()
        {
            LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
            base.Initialise();
            ScheduledCustomers = GetEntityQuery(typeof(CScheduledCustomer));
        }

        protected override void OnUpdate()
        {
            using (NativeArray<Entity> nativeArray = this.ScheduledCustomers.ToEntityArray(Allocator.Temp))
            {
                foreach (Entity entity in nativeArray)
                {
                    CScheduledCustomer cscheduledCustomer;
                    if (Require(entity, out cscheduledCustomer) && !cscheduledCustomer.IsCat)
                    {
                        cscheduledCustomer.IsCat = true;
                        Set(entity, cscheduledCustomer);
                    }
                }
            }
        }
        
        #region Logging
        internal static void LogInfo(string _log) { Debug.Log($"[{MOD_NAME}] " + _log); }
        internal static void LogWarning(string _log) { Debug.LogWarning($"[{MOD_NAME}] " + _log); }
        internal static void LogError(string _log) { Debug.LogError($"[{MOD_NAME}] " + _log); }
        #endregion
    }
}