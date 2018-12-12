
namespace ZacksSampleCode.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;


    public static class Enum<TEnum>
        where TEnum : struct, IComparable, IFormattable
    {

        public static string GetName(int value)
        {
            return Enum.GetName(typeof(TEnum), value);
        }
        public static string GetName(long value)
        {
            return Enum.GetName(typeof(TEnum), value);
        }
        public static List<string> GetNames()
        {
            return GetFields(typeof(TEnum)).Select(f => f.Name).ToList();
        }
        public static List<TEnum> GetValues()
        {
            var enumType = typeof(TEnum);
            return GetFields(enumType).Select(f => f.GetValue(enumType)).Cast<TEnum>().ToList();
        }

        private static List<FieldInfo> GetFields(Type enumType)
        {
            return enumType.GetFieldsEx(BindingFlagsHelper.GetFinalBindingFlags(true, true)).Where(f => f.IsLiteral && f.IsPublic).ToList();
        }
        public static TEnum Parse(string input, bool ignoreCase = false)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), input, ignoreCase);
        }
        public static bool TryParse(string input, out TEnum? result)
        {
            return TryParse(input, true, out result);
        }
        public static bool TryParse(string input, out TEnum result)
        {
            return TryParse(input, true, out result);
        }

        public static bool TryParse(string input, bool ignoreCase, out TEnum? result)
        {
            result = null;
            if (!Enum.IsDefined(typeof(TEnum), input))
            {
                return false;
            }
            FieldInfo fInfo = GetFields(typeof(TEnum)).FirstOrDefault(f => String.Compare(f.Name, input, ignoreCase) == 0);
            if (fInfo != null)
            {
                result = (TEnum)Enum.Parse(typeof(TEnum), input, ignoreCase);
                return true;
            }
            else
                return false;
        }
        public static bool TryParse(string input, bool ignoreCase, out TEnum result)
        {
            TEnum? temp;
            if (!TryParse(input, ignoreCase, out temp))
            {
                // input not found in the Enum, fill the out parameter with the first item from the enum
                var values = GetValues().ToArray();

                result = (TEnum)values.GetValue(values.GetLowerBound(0));
                return false;
            }

            result = temp.Value;
            return true;
        }

        private static string GetName(TEnum value)
        {
            return Enum.GetName(typeof(TEnum), value);
        }


        #region Flags Nested Type

        public static class Flags
        {
            #region Clear Flags
            public static TEnum ClearFlag(TEnum flags, TEnum flagToClear)
            {
                return ClearFlag(Convert.ToInt32(flags, CultureInfo.CurrentCulture), Convert.ToInt32(flagToClear, CultureInfo.CurrentCulture));
            }
            public static TEnum ClearFlag(int flags, TEnum flagToClear)
            {
                return ClearFlag(flags, Convert.ToInt32(flagToClear, CultureInfo.CurrentCulture));
            }
            public static TEnum ClearFlag(long flags, TEnum flagToClear)
            {
                return ClearFlag(flags, Convert.ToInt64(flagToClear, CultureInfo.CurrentCulture));
            }
            public static TEnum ClearFlag(int flags, int flagToClear)
            {
                // ReSharper disable RedundantCast
                return ClearFlag((long)flags, (long)flagToClear);
                // ReSharper restore RedundantCast
            }
            public static TEnum ClearFlag(long flags, long flagToClear)
            {
                if (IsFlagSet(flags, flagToClear))
                {
                    flags &= ~flagToClear;
                }
                return (TEnum)Enum.ToObject(typeof(TEnum), flags);
            }

            #endregion Clear Flags

            #region IsFlagSet

            public static bool IsFlagSet(TEnum flags, TEnum flagToFind)
            {
                return IsFlagSet(Convert.ToInt32(flags, CultureInfo.CurrentCulture), Convert.ToInt32(flagToFind, CultureInfo.CurrentCulture));
            }
            public static bool IsFlagSet(int flags, TEnum flagToFind)
            {
                return IsFlagSet(Convert.ToInt32(flags, CultureInfo.CurrentCulture), Convert.ToInt32(flagToFind, CultureInfo.CurrentCulture));
            }
            public static bool IsFlagSet(long flags, TEnum flagToFind)
            {
                return IsFlagSet(Convert.ToInt64(flags, CultureInfo.CurrentCulture), Convert.ToInt32(flagToFind, CultureInfo.CurrentCulture));
            }
            public static bool IsFlagSet(int flags, int flagToFind)
            {
                return (flags & flagToFind) == flagToFind;
            }
            public static bool IsFlagSet(long flags, long flagToFind)
            {
                return (flags & flagToFind) == flagToFind;
            }

            #endregion IsFlagSet


            #region SetFlags

            public static TEnum SetFlag(TEnum flags, TEnum flagToSet)
            {
                return SetFlag(Convert.ToInt32(flags, CultureInfo.CurrentCulture), Convert.ToInt32(flagToSet, CultureInfo.CurrentCulture));
            }
            public static TEnum SetFlag(int flags, TEnum flagToSet)
            {
                return SetFlag(flags, Convert.ToInt32(flagToSet, CultureInfo.CurrentCulture));
            }
            public static TEnum SetFlag(long flags, TEnum flagToSet)
            {
                return SetFlag(flags, Convert.ToInt64(flagToSet, CultureInfo.CurrentCulture));
            }
            public static TEnum SetFlag(int flags, int flagToSet)
            {
                // ReSharper disable RedundantCast
                return SetFlag((long)flags, (long)flagToSet);
                // ReSharper restore RedundantCast
            }
            public static TEnum SetFlag(long flags, long flagToSet)
            {
                if (!IsFlagSet(flags, flagToSet))
                {
                    flags |= flagToSet;
                }

                return (TEnum)Enum.ToObject(typeof(TEnum), flags);
            }
            #endregion SetFlags
        }
        #endregion Flags Nested Type
    }
}
