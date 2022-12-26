using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;

namespace System
{
    public static class ExtensionMethods
    {
        public static string GetControllerName(this string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Controller name is null");

            var index = value.ToLower().IndexOf("controller");
            if (index < 0)
                return value;

            return value.Substring(0, index);
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            var attribute = enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>();

            return attribute == null ? enumValue.ToString() : attribute.Name ?? "";
        }

        public static IDictionary<int, string> GetEnumValueNames(Type type)
        {
            var names = type.GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(f => f.GetCustomAttribute<DisplayAttribute>()?.Name ?? f.Name);

            var values = Enum.GetValues(type).Cast<int>();

            var dictionary = names.Zip(values, (n, v) => new KeyValuePair<int, string>(v, n))
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            return dictionary;
        }

        public static string GetEnumDescription(this Enum value)
        {
            var type = value.GetType();
            var memInfo = type.GetMember(value.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Any())
                return ((DescriptionAttribute)attributes[0]).Description;
            else
                return value.ToString();
        }

        public static string GetDisplayGroup(this Enum value)
        {
            var type = value.GetType();
            var memInfo = type.GetMember(value.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attributes.Any())
                return ((DisplayAttribute)attributes[0])?.GetGroupName() ?? "";
            else
                return string.Empty;
        }
    }
}
