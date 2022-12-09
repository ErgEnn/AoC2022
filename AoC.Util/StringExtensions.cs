
namespace AoC.Util
{
    public static class StringExtensions
    {

        public static string[] Lines(this string s)
        {
            return s.Split(Environment.NewLine);
        }

        public static (T1 s1, T2 s2) Deconstruct<T1, T2>(this string s)
        {
            return s.Split(' ') switch
            {
                [var s1, var s2] => (Convert<T1>(s1), Convert<T2>(s2)),
            };
        }
        public static (T1 s1, T2 s2, T3 s3) Deconstruct<T1, T2, T3>(this string s)
        {
            return s.Split(' ') switch
            {
                [var s1, var s2, var s3] => (Convert<T1>(s1), Convert<T2>(s2), Convert<T3>(s3)),
            };
        }
        public static (T1 s1, T2 s2, T3 s3, T4 s4) Deconstruct<T1, T2, T3, T4>(this string s)
        {
            return s.Split(' ') switch
            {
                [var s1, var s2, var s3, var s4] => (Convert<T1>(s1), Convert<T2>(s2), Convert<T3>(s3), Convert<T4>(s4)),
            };
        }
        public static (T1 s1, T2 s2, T3 s3, T4 s4, T5 s5) Deconstruct<T1, T2, T3, T4, T5>(this string s)
        {
            return s.Split(' ') switch
            {
                [var s1, var s2, var s3, var s4, var s5] => (Convert<T1>(s1), Convert<T2>(s2), Convert<T3>(s3), Convert<T4>(s4), Convert<T5>(s5)),
            };
        }
        public static (T1 s1, T2 s2, T3 s3, T4 s4, T5 s5, T6 s6) Deconstruct<T1, T2, T3, T4, T5, T6>(this string s)
        {
            return s.Split(' ') switch
            {
                [var s1, var s2, var s3, var s4, var s5, var s6] => (Convert<T1>(s1), Convert<T2>(s2), Convert<T3>(s3), Convert<T4>(s4), Convert<T5>(s5), Convert<T6>(s6)),
            };
        }

        private static T Convert<T>(this string s)
        {
            return (T) System.Convert.ChangeType(s, typeof(T));
        }

        public static int ToInt(this string s)
        {
            return int.Parse(s);
        }

    }

    public class Parts
    {
        
    }
}
