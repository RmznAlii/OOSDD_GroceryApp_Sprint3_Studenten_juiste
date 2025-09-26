using Grocery.Core.Helpers;

namespace TestCore
{
    public class TestHelpers
    {
        [SetUp]
        public void Setup()
        {
        }


        // Happy flow
        [Test]
        public void VerifyPassword_ReturnsTrue_ForSinglePair()
        {
            string plain = "user3";
            string hash  = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            Assert.IsTrue(PasswordHelper.VerifyPassword(plain, hash));
        }

// Happy flow (meerdere paren)
        [TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        [TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        public void VerifyPassword_ReturnsTrue_ForKnownPairs(string plain, string hash)
        {
            Assert.IsTrue(PasswordHelper.VerifyPassword(plain, hash));
        }

// Unhappy flow 
        [Test]
        public void VerifyPassword_ReturnsFalse_ForSingleWrongPair()
        {
            string plain = "verkeerd";
            string hash  = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            bool ok = PasswordHelper.VerifyPassword(plain, hash);
            Assert.IsFalse(ok, "Het wachtwoord zou onjuist moeten zijn.");
        }

// Unhappy flow (meerdere foute paren)
        [TestCase("verkeerd",       "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        [TestCase("foutwachtwoord", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        public void VerifyPassword_ReturnsFalse_ForInvalidPairs(string plain, string hash)
        {
            bool ok = PasswordHelper.VerifyPassword(plain, hash);
            Assert.IsFalse(ok, "Het wachtwoord zou onjuist moeten zijn.");
        }

    }
}