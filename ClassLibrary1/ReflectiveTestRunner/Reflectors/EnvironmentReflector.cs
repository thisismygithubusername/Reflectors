using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.ReflectiveTestRunner.Reflectors;

namespace ClassLibrary1.Reflectors
{
    public class EnvironmentReflector 
    {
        public static object CreateTestFixtureInstance(Assembly environmentAssembly, string fixtureName)
        {
            return BaseReflector.GetInstanceFromClassName(environmentAssembly, fixtureName);
        }
        
        public static object CreateTestFixtureInstanceWithActivator(Assembly environmentAssembly,
                                                                    string fixtureClassName)
        {
            Type type = environmentAssembly.GetType(fixtureClassName);
            var obj = Activator.CreateInstance(type);
            return obj;
        }
        
        public static MethodInfo GetMethodWithAttributeFromObject(object instance, string atrribute)
        {
            var methods = instance.GetType().GetMethods();
            var wantedmethods = new List<MethodInfo>();
            foreach (var method in methods)
            {
                if(ContainsAttributeInAtrributes(atrribute, method.CustomAttributes))
                {
                    wantedmethods.Add(method);
                }
            }
            return wantedmethods.Count < 0 ? wantedmethods.First() : null;
        }

        public static bool ContainsAttributeInAtrributes(string attributeName, IEnumerable<CustomAttributeData> listOfAttributes)
        {
            return listOfAttributes.Any(customAttributeData => customAttributeData.AttributeType.Name.Contains(attributeName));
        }
        /*
        public static void ExecutionMethodWithAttribute(object inst, MethodInfo method)
        {
            try
            {

            }
            catch ()
            {
                
            }
            
        }
        */
         
    }
}
