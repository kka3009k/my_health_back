namespace MyHealth.Api.Service
{
    public class OtpProvider
    {
        private static List<Dictionary<string, string>> _sentOtps;

        public static void SetOtp(string pKey, string pOtp)
        {
            if (_sentOtps == null)
                _sentOtps = new();

            _sentOtps.Add(new Dictionary<string, string> { { pKey, pOtp } });
        }

        public static void RemoveOtp(string pKey)
        {
            _sentOtps.RemoveAll(w => w.Any(a => a.Key == pKey));
        }

        public static bool HasOtp(string pKey, string pOtp)
        {
            return _sentOtps.Any(p => p.Any(a => a.Key == pKey && a.Value == pOtp));
        }
    }
}
