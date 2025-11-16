using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourierApp.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Program_ShouldRunWithMockInput()
        {
            string input =
                 "100 2\n" +
                 "PKG1 50 30 OFR001\n" +
                 "PKG2 75 125 OFR003\n" +
                 "1 70 200\n";

            using var reader = new StringReader(input);
            Console.SetIn(reader);

            using var writer = new StringWriter();
            Console.SetOut(writer);


            string output = writer.ToString();

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(output.Contains("PKG1"));
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(output.Contains("PKG2"));
        }
    }
}