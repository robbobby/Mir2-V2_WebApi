namespace Application.Repository.PostgresLocalDev {
    public class PostgresConfig {
        public string Server { get; set; }
        public string Name { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public string GetConnectionString() {
            return $"server={Server};database={Name};Port={Port};user id={Username}; password={Password};pooling=true;";
        } 
    }
}
