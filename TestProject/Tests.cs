using SharpRSA;
using SharpRSALib;
using SharpRSALib.Utils;
using System.Numerics;
using System.Text;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ToNumber()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= 255; i++)
            {
                char c = (char)i;
                sb.Append(c);
            }
            BigInteger number = TextConverter.ToNumber(sb.ToString());
            string text = TextConverter.ToText(number);
            Assert.That(text, Is.EqualTo(sb.ToString()));
        }
        [Test]
        public void Cipher()
        {
            KeyPair kp = RSA.GenerateKeyPair(KeySize.K1024);
            string plain = "Hello RSA";
            string ciphed = RSA.Encrypt(plain, kp.public_);
            Assert.That(ciphed, Is.Not.EqualTo(plain));
        }

        [Test]
        public void Decipher()
        {
            KeyPair kp = RSA.GenerateKeyPair(KeySize.K1024);
            string ciphed = "CulTB1a3AQ775z4CYlfL9rkD1jtdiMK3qLoYgaQ3vttcE0XLue/vG45eBDRumSrK95H7BJ/4sw/GInvaCgEe1POUUR/4hB/uSw37vkJ56OojEWd/mszvdwg4lHEf3nD8+XDdLcNRT3QmKq5pyeAc6mFgWY4dGBygUdOPecCNGpkA";
            string plain = RSA.Decrypt(ciphed, kp.private_);
            Assert.That(ciphed, Is.Not.EqualTo(plain));
        }

        [Test]
        public void GenerateCoprimes()
        {
            var primes = CoPrimes.GetCoprimes(2, 7);
            Assert.That(primes.ToArray(), Is.EqualTo(new BigInteger[]
            {
                1,3,5,9,11,13
            }));
        }

        [Test]
        public void GetPhi()
        {
            var phi = CoPrimes.GetPhi(2, 7);
            Assert.That(phi, Is.EqualTo((BigInteger)6));
        }

        [Test]
        public void ProposeExponent()
        {
            BigInteger p = 2, q = 7;
            var e = CoPrimes.ProposeExponent(p, q);
            Assert.That(e, Is.GreaterThanOrEqualTo(BigInteger.One).And.LessThan(CoPrimes.GetPhi(p, q)));
        }

        [Test]
        public void ProposeKey()
        {
            BigInteger exponent = 5, phi = 6;
            BigInteger key = CoPrimes.ProposeKey(exponent, phi);
            Assert.That(Kit.Security.Encryption.Modulus.Mod(exponent * key, phi), Is.EqualTo(BigInteger.One));
        }


        [Test]
        public void MainTest()
        {
            KeyPair kp = RSA.GenerateKeyPair(KeySize.K1024);
            string plain = "Hello RSA";
            string ciphed = RSA.Encrypt(plain, kp.public_);
            string deciphed = RSA.Decrypt(ciphed, kp.private_);
            Assert.That(deciphed, Is.EqualTo(plain));
        }
    }
}