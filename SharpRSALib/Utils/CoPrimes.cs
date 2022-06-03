using Kit;
using System.Numerics;

namespace SharpRSALib.Utils
{
    public static class CoPrimes
    {
        public static IEnumerable<BigInteger> GetCoprimes(BigInteger p, BigInteger q)
        {
            BigInteger limit = p * q;
            for (BigInteger i = 1; i < limit; i += 2)
            {
                if (i != p && i != q)
                    yield return i;
            }
        }

        public static BigInteger GetPhi(BigInteger p, BigInteger q)
        {
            return (p - 1) * (q - 1);
        }

        public static BigInteger ProposeKey(BigInteger exponent, BigInteger phi)
        {
            BigInteger sum = exponent * Rand.Random.NextInt64();
            BigInteger mod;
            do
            {
                sum += exponent;
                mod = Kit.Security.Encryption.Modulus.Mod(ba: sum, bb: phi);
            } while (!mod.IsZero);
            return sum / exponent - 1;
        }
        public static BigInteger ProposeExponent(BigInteger p, BigInteger q)
        {
            BigInteger n = p * q;
            BigInteger phi = GetPhi(p, q);
            BigInteger[] proposed = GetCoprimes(p, q)
                 .Where(x => x > 1 && x < phi && phi % x != BigInteger.Zero)
                 .Where(x => GetCoprimes(n, phi).Contains(x))
                 .ToArray();
            if (proposed.Length > 1)
                Console.WriteLine("Multiple proposals found");
            return proposed.Shuffle(Rand.Random).First();
        }
    }
}
