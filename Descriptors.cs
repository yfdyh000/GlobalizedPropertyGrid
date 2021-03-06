// 

using System;
using System.ComponentModel;
using System.Resources;


namespace GlobalizedPropertyGrid
{
    #region GlobalizedPropertyDescriptor

    /// <summary>
    /// GlobalizedPropertyDescriptor enhances the base class bay obtaining the display name for a property
    /// from the resource.
    /// </summary>
    public class GlobalizedPropertyDescriptor : PropertyDescriptor
    {
        private readonly PropertyDescriptor basePropertyDescriptor;
        private string localizedName;
        private string localizedDescription;

        public GlobalizedPropertyDescriptor(PropertyDescriptor basePropertyDescriptor) : base(basePropertyDescriptor) => this.basePropertyDescriptor = basePropertyDescriptor;

        public override bool CanResetValue(object component) => basePropertyDescriptor.CanResetValue(component);

        public override Type ComponentType => basePropertyDescriptor.ComponentType;

        public override string Category
        {
            get
            {
                // First lookup the property if GlobalizedPropertyAttribute instances are available. 
                // If yes, then try to get resource table name and display name id from that attribute.
                string tableName = null;
                string displayName = null;
                foreach (Attribute oAttrib in this.basePropertyDescriptor.Attributes)
                {
                    if (oAttrib.GetType().Equals(typeof(GlobalizedPropertyAttribute)))
                    {
                        displayName = ((GlobalizedPropertyAttribute)oAttrib).Category;
                        tableName = ((GlobalizedPropertyAttribute)oAttrib).Table;
                    }
                }

                // If no resource table specified by attribute, then build it itself by using namespace and class name.
                tableName = tableName ?? basePropertyDescriptor.ComponentType.Namespace + "." + basePropertyDescriptor.ComponentType.Name;

                // If no display name id is specified by attribute, then construct it by using default display name (usually the property name) 
                displayName = displayName ?? this.basePropertyDescriptor.Category + "Category"; ;

                // Now use table name and display name id to access the resources.  
                ResourceManager rm = new ResourceManager(tableName, basePropertyDescriptor.ComponentType.Assembly);

                // Get the string from the resources. 
                // If this fails, then use default display name (usually the property name) 
                string s = rm.GetString(displayName);
                this.localizedName = s ?? this.basePropertyDescriptor.DisplayName;

                return this.localizedName;
            }
        }

        public override string DisplayName
        {
            get
            {
                // First lookup the property if GlobalizedPropertyAttribute instances are available. 
                // If yes, then try to get resource table name and display name id from that attribute.
                string tableName = null;
                string displayName = null;
                foreach (Attribute oAttrib in this.basePropertyDescriptor.Attributes)
                {
                    if (oAttrib.GetType().Equals(typeof(GlobalizedPropertyAttribute)))
                    {
                        displayName = ((GlobalizedPropertyAttribute)oAttrib).Name;
                        tableName = ((GlobalizedPropertyAttribute)oAttrib).Table;
                    }
                }

                // If no resource table specified by attribute, then build it itself by using namespace and class name.
                tableName = tableName ?? basePropertyDescriptor.ComponentType.Namespace + "." + basePropertyDescriptor.ComponentType.Name;

                // If no display name id is specified by attribute, then construct it by using default display name (usually the property name) 
                displayName = displayName ?? this.basePropertyDescriptor.DisplayName;

                // Now use table name and display name id to access the resources.  
                ResourceManager rm = new ResourceManager(tableName, basePropertyDescriptor.ComponentType.Assembly);

                // Get the string from the resources. 
                // If this fails, then use default display name (usually the property name) 
                string s = rm.GetString(displayName);
                this.localizedName = s ?? this.basePropertyDescriptor.DisplayName;

                return this.localizedName;
            }
        }

        public override string Description
        {
            get
            {
                // First lookup the property if there are GlobalizedPropertyAttribute instances
                // are available. 
                // If yes, try to get resource table name and display name id from that attribute.
                string tableName = null;
                string displayName = null;
                foreach (Attribute oAttrib in this.basePropertyDescriptor.Attributes)
                {
                    if (oAttrib.GetType().Equals(typeof(GlobalizedPropertyAttribute)))
                    {
                        displayName = ((GlobalizedPropertyAttribute)oAttrib).Description;
                        tableName = ((GlobalizedPropertyAttribute)oAttrib).Table;
                    }
                }

                // If no resource table specified by attribute, then build it itself by using namespace and class name.
                tableName = tableName ?? basePropertyDescriptor.ComponentType.Namespace + "." + basePropertyDescriptor.ComponentType.Name;

                // If no display name id is specified by attribute, then construct it by using default display name (usually the property name) 
                displayName = displayName ?? this.basePropertyDescriptor.DisplayName + "Description";

                // Now use table name and display name id to access the resources.  
                ResourceManager rm = new ResourceManager(tableName, basePropertyDescriptor.ComponentType.Assembly);

                // Get the string from the resources. 
                // If this fails, then use default empty string indictating 'no description' 
                string s = rm.GetString(displayName);
                this.localizedDescription = s ?? "";

                return this.localizedDescription;
            }
        }

        public override object GetValue(object component) => this.basePropertyDescriptor.GetValue(component);

        public override bool IsReadOnly => this.basePropertyDescriptor.IsReadOnly;

        public override string Name => this.basePropertyDescriptor.Name;

        public override Type PropertyType => this.basePropertyDescriptor.PropertyType;

        public override void ResetValue(object component) => this.basePropertyDescriptor.ResetValue(component);

        public override bool ShouldSerializeValue(object component) => this.basePropertyDescriptor.ShouldSerializeValue(component);

        public override void SetValue(object component, object value) => this.basePropertyDescriptor.SetValue(component, value);
    }
    #endregion

    #region GlobalizedObject

    /// <summary>
    /// GlobalizedObject implements ICustomTypeDescriptor to enable 
    /// required functionality to describe a type (class).<br></br>
    /// The main task of this class is to instantiate our own property descriptor 
    /// of type GlobalizedPropertyDescriptor.  
    /// </summary>
    public class GlobalizedObject : ICustomTypeDescriptor
    {
        private PropertyDescriptorCollection globalizedProps;

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        /// <summary>
        /// Called to get the properties of a type.
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            if (globalizedProps == null)
            {
                // Get the collection of properties
                PropertyDescriptorCollection baseProps = TypeDescriptor.GetProperties(this, attributes, true);

                globalizedProps = new PropertyDescriptorCollection(null);

                // For each property use a property descriptor of our own that is able to be globalized
                foreach (PropertyDescriptor oProp in baseProps)
                {
                    globalizedProps.Add(new GlobalizedPropertyDescriptor(oProp));
                }
            }
            return globalizedProps;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            // Only do once
            if (globalizedProps == null)
            {
                // Get the collection of properties
                PropertyDescriptorCollection baseProps = TypeDescriptor.GetProperties(this, true);
                globalizedProps = new PropertyDescriptorCollection(null);

                // For each property use a property descriptor of our own that is able to be globalized
                foreach (PropertyDescriptor oProp in baseProps)
                {
                    globalizedProps.Add(new GlobalizedPropertyDescriptor(oProp));
                }
            }
            return globalizedProps;
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
    }

    #endregion
}
