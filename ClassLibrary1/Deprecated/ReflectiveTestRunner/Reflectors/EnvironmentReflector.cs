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

        public static Type GetFixtureType(AppDomain appdomain, Assembly environmentAssembly, string fixtureClassName)
        {
            
            var type = environmentAssembly.GetType(fixtureClassName);
            
            return type;
        }

        public static object CreateTestFixtureInstanceWithAppdomain(AppDomain appdomain, Type fixtureType, Assembly environmentAssembly,
                                                                    string fixtureClassName)
        {
            var attributes = fixtureType.CustomAttributes;
             
            foreach (var customAttributeData in attributes)
            {
                Console.WriteLine(customAttributeData.AttributeType);
            }

            var obj = appdomain.CreateInstance(environmentAssembly.FullName, fixtureType.FullName);
            return obj;
        }

        public static MethodInfo TryGetSetupMethodWithAttribute(object testFixture, string atrribute)
        {
            MethodInfo method = null;
            try
            {
                method = GetMethodWithAttributeFromObject(testFixture, atrribute);
            }
            catch (Exception)
            {
                Console.WriteLine("FailedToExecuteSetup");
            }

            return method;
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
            return wantedmethods.Count > 0 ? wantedmethods.First() : null;
        }

        public static bool ContainsAttributeInAtrributes(string attributeName, IEnumerable<CustomAttributeData> listOfAttributes)
        {
            return listOfAttributes.Any(customAttributeData => customAttributeData.AttributeType.Name.Contains(attributeName));
        }
        
        public static void ExecuteMethodFromMethodInfo(object inst, MethodInfo test)
        {
            test.Invoke(inst, null); 
        }
        
        public static void TryToExecuteSetupMethod(object testFixture, MethodInfo setupinfo)
        {
            
            try
            {
                ExecuteMethodFromMethodInfo(testFixture, setupinfo);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to execute setup");
                Console.WriteLine(e.InnerException.StackTrace);
                Console.WriteLine(e.InnerException.Message);
            }
             
            //ExecuteMethodFromMethodInfo(testFixture, setupinfo);
        }

        public static void TryToExecuteTearDown(object testFixture, MethodInfo tearDowninfo)
        {
            ///*
            try
            {
                ExecuteMethodFromMethodInfo(testFixture, tearDowninfo);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to execute TearDown");
                Console.WriteLine(e.InnerException.StackTrace);
                Console.WriteLine(e.InnerException.Message);
            }
            // */
            //ExecuteMethodFromMethodInfo(testFixture, tearDowninfo);
        }
         
    }
}
