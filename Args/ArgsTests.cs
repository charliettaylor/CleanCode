using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Args
{
    [TestClass]
    public class ArgsTests
    {
        [TestMethod]
        public void SchemaParse_ValidateResult()
        {
            var arg = new Args("a,b*,c#", 
                new [] {"-a", "true", "-b", "Caught in a mosh", "-c", "15"}).GetArgs();
            
            Assert.IsTrue(arg.TryGetValue('a', out _));
            Assert.IsTrue(arg.TryGetValue('b', out _));
            Assert.IsTrue(arg.TryGetValue('c', out _));
        }

        [TestMethod]
        public void SchemaParse_ValidateExceptionThrown()
        {
            Assert.ThrowsException<ArgsException>(() => new Args("ab*c#", new [] {""}));
        }
    }
}