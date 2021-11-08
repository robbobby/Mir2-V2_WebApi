using BCrypt.Net;

namespace Mir2_V2_WebApi.Helpers {
    public class Hashing {
        public static string GetRandomSalt() {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string HashPassword(string password, string salt) {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public static bool ValidatePassword(string password, string correctHash) {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
        
    }
}
