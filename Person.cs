namespace GlobalizedPropertyGrid
{

    /// <summary>
    /// Person is the test class defining two properties: first name and last name .
    /// By deriving from GlobalizedObject the displaying of property names are language aware.
    /// GlobalizedObject implements the interface ICustomTypeDescriptor. 
    /// </summary>
    public class Person : GlobalizedObject
    {
        private int age = 0;

        public Person() { }

        public string FirstName { get; set; } = "";

        // Uncomment the next line to see the attribute in action: 
        [GlobalizedProperty("Surname", Description = "ADescription", Table = "GlobalizedPropertyGrid.SpecialStringTable")]
        public string LastName { get; set; } = "";

        public int Age
        {
            get { return age; }
            set { age = value; }
        }
    }
}

