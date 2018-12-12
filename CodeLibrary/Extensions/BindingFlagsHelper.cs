

namespace ZacksSampleCode.Extensions
{
    using System.Reflection;
    public static class BindingFlagsHelper
    {
        public const BindingFlags DefaultBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        public static BindingFlags GetFinalBindingFlags(bool flattenHierarchy, bool allowStaticMembers, bool? allowNonPublicMembers = null)
        {
            var bindingFlags = DefaultBindingFlags;

            if (allowNonPublicMembers.HasValue)
            {
                if (allowNonPublicMembers.Value)
                {
                    bindingFlags = Enum<BindingFlags>.Flags.SetFlag(bindingFlags, BindingFlags.NonPublic);
                }
                else
                {
                    bindingFlags = Enum<BindingFlags>.Flags.ClearFlag(bindingFlags, BindingFlags.NonPublic);
                }
            }

            if (flattenHierarchy)
            {
                bindingFlags = Enum<BindingFlags>.Flags.SetFlag(bindingFlags, BindingFlags.FlattenHierarchy);
            }

            if (allowStaticMembers)
            {
                bindingFlags = Enum<BindingFlags>.Flags.SetFlag(bindingFlags, BindingFlags.Static);
            }

            return bindingFlags;
        }
    }
}
