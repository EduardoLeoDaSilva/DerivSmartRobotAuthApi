using System.ComponentModel.DataAnnotations.Schema;


namespace AuthControl.Entities
{
    public class UserBase
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Robots { get; set; }
        public bool Active { get; set; }
        public string TokensOAuth { get; set; }

        public DateTime? TokenDateDeadLine { get; set; }
        
        [NotMapped]
        public string JwtToken { get; set; }

        public List<OperationaDay> OperationsDays { get; set; }

    }

    public class UserView
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class ConfigView
    {
        public string Email { get; set; }

        public AccountType AccountType { get; set; }
        public string ApiToken { get; set; }

    }

    public enum AccountType
    {
        Demo,
        Real
    }
}
