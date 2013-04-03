﻿using System;
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
 
    }
}
