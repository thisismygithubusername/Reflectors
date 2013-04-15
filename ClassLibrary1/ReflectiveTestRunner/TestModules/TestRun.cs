using System;
using System.Collections.Generic;
using ClassLibrary1.ReflectiveTestRunner.TestModules.@abstract;

namespace ClassLibrary1.ReflectiveTestRunner.TestModules
{
    public class TestRun 
    {
        public TestRun()
        {
        }

        public ITest Test { get;  set; }
        public string Status { get;  set; }
        public string TestOutput { get;  set; }
        public string SetupOutput { get; set; }
        public string TearDowmOutput { get; set; }
        public Exception Exception { get;  set; }

        public override string ToString()
        {
            var FirstLine = "Test: " + Test.TestName + "      Fixture: " + Test.FixtureName;
            FirstLine += Environment.NewLine;
            var SecondLine = "Test Run Status : " + Status;
            SecondLine += Environment.NewLine;
            var failureInfo = "";
            if (Exception != null)
            {
                failureInfo = GenerateExceptionOutput();
            }

            return FirstLine + SecondLine + failureInfo;
        }

        private string GenerateExceptionOutput()
        {
            var output = "Setup Output : ";
            output += GetValidOutputString(SetupOutput);
            output += "Test Output :  ";
            output += GetValidOutputString(TestOutput);
            output += "TearDown Output :  ";
            output += GetValidOutputString(TestOutput);
            output += "FailureException :  ";
            output += GetValidOutputString(Exception.Message);
            output += GetValidOutputString(Exception.StackTrace);
            return output;
        }

        private static string GetValidOutputString(string output)
        {
            return output + Environment.NewLine ??  Environment.NewLine;
        }
    }
}
