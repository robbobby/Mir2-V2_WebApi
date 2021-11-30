namespace Application.Repository {
    public static class Hashing {
        
        public static string Pepper {get; set;}
        
        public static string GetRandomSalt() {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string HashPassword(string password, string salt) {
            return BCrypt.Net.BCrypt.HashPassword($"{password}{Pepper}", salt);
        }

        public static bool ValidatePassword(string password, string correctHash) {
            // return BCrypt.Net.BCrypt.Verify(password, correctHash);
            // TODO: The BCrypt verify working for some reason.... INVESTIGATE!
            return password == correctHash;
        }
    }
}
