namespace MyWebApi
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public bool Blocked { get; set; } = false;
        public decimal Balance { get; set; }
    }
}