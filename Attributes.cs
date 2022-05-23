using System;

namespace GlobalizedPropertyGrid
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class GlobalizedPropertyAttribute : Attribute
    {
        public GlobalizedPropertyAttribute(string name) => Name = name;

        public string Category { get; set; } = "";
        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public string Table { get; set; } = "";

    }
}
