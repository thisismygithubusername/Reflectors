using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Reflectors
{
    public abstract class BaseReflector 
    {
        public static object GetInstanceFromClassName(Assembly assembly, string className)
        {
            return TryGetInstanceFromClass(assembly, className);
        }

        public static List<MethodInfo> GetMethodsFromType(Type type)
        {
            return GetMethodsFromTypeObject(type);
        } 

        public static List<MethodInfo> GetMethodsFromObject(object obj)
        {
            return (GetMethodsFromType(obj.GetType()));
        }

        public static List<MethodInfo> GetMethodsFromGeneric<T>(T obj)
        {
            return (GetMethodsFromType(typeof(T)));
        }

        public static List<MethodInfo> GetMethodsFromTypeWithAttribute(Type type, string attributeType)
        {
            return GetMethodsWithAttribute(type, attributeType);
        }

        public static List<MethodInfo> GetMethodsFromObjectWithAttribute(object obj, string attributeType)
        {
            return GetMethodsWithAttribute(obj.GetType(), attributeType);
        } 

        public static void ExcecuteStaticMethod(string className, string methodName)
        {
            TryExcecuteStaticMethod(className, methodName);
        }

        public static void ExecuteInstanceMethodFromInstance(object instanceObj, string method)
        {
            TryExecuteInstanceMethodFromInstance(instanceObj, method);
        } 

        public static void ExecuteInstanceMethodFromClassName(string className, string methodName, Assembly assembly)
        {
            TryExecuteInstanceMethodFromClassName(className, methodName, assembly);
        }

        public static bool ContainsMethodsInGeneric<T>(T obj, string methodName)
        {
            return HasMethodInGeneric(obj, methodName);
        }

        public static bool ContainsMethodInObjectInstance(object obj, string methodName)
        {
            return HasMethodInObjectInstance(obj, methodName);
        }

        //Todo
        private static object TryGetInstanceFromClass(Assembly assembly, string className)
        {
            return assembly.CreateInstance(className);
        }

        private static List<MethodInfo> GetMethodsFromTypeObject(Type type)
        {
            return type.GetMethods().ToList();
        }

        private static List<MethodInfo> GetMethodsWithAttribute(Type type, string attributeType)
        {
            var methods = GetMethodsFromType(type);
            return
                methods.Where(
                    methodInfo =>
                    methodInfo.GetCustomAttributes().Any(attribute => attribute.GetType().Name.Equals(attributeType)))
                       .ToList();
        }

        private static void TryExcecuteStaticMethod(string className, string methodName)
        {
            var type = Type.GetType(className);
            if (type != null) type.GetMethod(methodName).Invoke(null, null);
        }

        private static void TryExecuteInstanceMethodFromInstance(object instanceObj, string method)
        {
             instanceObj.GetType().InvokeMember(method, BindingFlags.InvokeMethod, null, instanceObj, null);
        }

        private static void TryExecuteInstanceMethodFromClassName(string className, string methodName, Assembly assembly)
        {
            TryExecuteInstanceMethodFromInstance(TryGetInstanceFromClass(assembly, className), methodName);
        }

        private static bool HasMethodInGeneric<T>(T inst, string methodName)
        {
            var tp = typeof (T);
            return tp.GetMethods().Any(method => method.Name.Equals(methodName));
        }

        private static bool HasMethodInObjectInstance(object obj, string methodName)
        {
            return obj.GetType().GetMethods().Any(method => method.Name.Equals(methodName));
        }

        /*
        private static List<MethodInfo> GetMethodsFromTypeWithAttribute(Type type, string attributeType)
        {
            var methods = GetMethodsFromType(type);
            var newMethods = new List<MethodInfo>();
            foreach (var methodInfo in methods)
            {
                if (methodInfo.GetCustomAttributes().Any(attribute => attribute.GetType().Name.Equals(attributeType)))
                {
                    newMethods.Add(methodInfo);
                }
            }
            return newMethods;
        }
         */
    }
}
