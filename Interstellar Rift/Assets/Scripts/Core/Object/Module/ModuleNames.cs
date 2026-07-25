namespace Core.Object.Module
{
    /// <summary>
    /// Defines the display names of all modules, grouped by module category.
    /// </summary>
    public static class ModuleNames
    {
        /// <summary>
        /// Display names of structure modules — the body a module can be attached to.
        /// </summary>
        public static class Structure
        {
            public const string CoreModule = "Core Module";
            public const string ConnectionModule = "Connection Module";
        }

        /// <summary>
        /// Display names of booster modules — modules that provide propulsion.
        /// </summary>
        public static class Booster
        {
            public const string BuiltInBooster = "Built In Booster";
        }
    }
}
