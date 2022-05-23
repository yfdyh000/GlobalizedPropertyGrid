using System;

namespace GlobalizedPropertyGrid
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class GlobalizedPropertyAttribute : Attribute
    {
        public GlobalizedPropertyAttribute(String name)
        {
            Name = name;
        }

        public String Category { get; set; } = "";
        public String Name { get; set; } = "";

        public String Description { get; set; } = "";

        public String Table { get; set; } = "";

    }
}
