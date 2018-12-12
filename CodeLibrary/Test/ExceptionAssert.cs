using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Crm7.UnitTest.TestHelpers
{
    /// <summary>
    /// Used to test that an action throws an exception as expected.
    /// </summary>
    public static class ExceptionAssert
    {
        /// <summary>
        /// calls the action, catches the exception and returns it. If no exception is 
        /// caught then fails the test.
        /// </summary>
        /// <typeparam name="T">Type of the exception being thrown</typeparam>
        /// <param name="action">Action used to generate the exception</param>
        /// <param name="Message">Optional - message to be written to test output if the action 
        /// throws an unexpected exception or does not throw any exception</param>
        /// <returns>If the action behaves as expected and throws an exception this will 
        /// return the thrown exception so it can be inspected.</returns>
        public static T Throws<T>(Action action, string Message = "") where T : Exception
        {
            try
            {
                action();
            }
            catch (T ex)
            {
                //Write exception details to the trace so we can look at it if need be.
                Trace.WriteLine(string.Concat("Expected Exception Type: ", typeof(T).ToString()));
                Trace.WriteLine("Message:");
                Trace.WriteLine(ex.Message);
                Trace.WriteLine("StackTrace:");
                Trace.WriteLine(ex.StackTrace);
                //Catch the expected exception and return it for inspection
                return ex;
            }
            catch (UnitTestAssertException ex)
            {
                //if the action has an assert then rethrow the assert exception
                throw ex;
            }
            //If Action throws an exception other than T or a UnitTestAssertException, let it go so 
            //the test report has the stack trace.

            //If no exception was thrown then fail the test.
            Assert.Fail("Expected exception of type {0} to be thrown. {1}", typeof(T), Message);
            return null;
        }

    }
}
