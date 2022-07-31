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

        [TestMethod]
        public void ArgumentParse_ValidateResult()
        {
            var arg = new Args("a,b*,c#", 
                new [] {"-a", "true", "-b", "Caught in a mosh", "-c", "15"});
            
            Assert.IsTrue(arg.GetBool('a') == true);
            Assert.IsTrue(arg.GetString('b') == "Caught in a mosh");
            Assert.IsTrue(arg.GetInt('c') == 15);
        }

        [TestMethod]
        public void ArgumentParse_ExtraData_ValidateResult()
        {
            var arg = new Args("a,b*,c#", 
                new [] {"C:/some/path/for/dll", "-a", "true", 
                "-b", "Caught in a mosh", "-c", "15", "extra info"});

            Assert.IsTrue(arg.GetBool('a') == true);
            Assert.IsTrue(arg.GetString('b') == "Caught in a mosh");
            Assert.IsTrue(arg.GetInt('c') == 15);
        }
    }
}