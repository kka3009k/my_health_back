namespace MyHealth.Api.Service
{
    public class OtpProvider
    {
        private static List<Dictionary<string, string>> _sentOtps;

        public static string GenerateOtp(string pKey)
        {
            var otp = GenerateOtp();

            if (_sentOtps == null)
                _sentOtps = new();

            _sentOtps.Add(new Dictionary<string, string> { { pKey, otp } });

            return otp;
        }

        public static void RemoveOtp(string pKey)
        {
            _sentOtps.RemoveAll(w => w.Any(a => a.Key == pKey));
        }

        public static bool HasOtp(string pKey, string pOtp)
        {
            return _sentOtps.Any(p => p.Any(a => a.Key == pKey && a.Value == pOtp));
        }

        private static string GenerateOtp()
        {
            var otp = $"H-{Random.Shared.Next(1000, 9999)}";
            return otp;
        }
    }
}
