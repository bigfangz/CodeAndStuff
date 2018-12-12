
namespace ZacksSampleCode.Extensions
{
    using System;
    using System.Reflection;
    public static class ReflectionExtensions
    {
        public static FieldInfo[] GetFieldsEx(this Type type, BindingFlags bindingFlags)
        {
            return type.GetTypeInfo().GetFields(bindingFlags);
        }

        public static T GetPropertyValue<T>(this Type type, object obj,string propertyName)
        {
            var propInfo = type.GetProperty(propertyName);
            if (propInfo == null)
                throw new ArgumentException(string.Format("Property {0} does not exist on {1}", propertyName, type.Name), "propertyName");
            object propValue = propInfo.GetValue(obj);
            if (!(propValue is T))
                throw new ArgumentException("Generic argument type does not match property return type.", "<T>");
            return (T)propValue;
        }

    }
}
