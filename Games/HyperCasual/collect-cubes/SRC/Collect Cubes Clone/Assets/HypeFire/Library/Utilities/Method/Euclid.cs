namespace HypeFire.Library.Utilities.Method
{
    public static class Euclid
    {
        public static int GetGreatestCommonDivisor(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }

            return GetGreatestCommonDivisor(b, a % b);
        }
    }
}