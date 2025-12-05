namespace backend.Controllers
{
    public class CreateUserArgs
    {
        public string Pseudo { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
    }

    public class MailArgs
    {
        public string Object { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty;
        public string Receiver { get; set; } = string.Empty;
        public string Inbox { get; set; } = string.Empty;
        public int UserId { get; set; }
    }

    public class UpdateUserArgs
    {

    }

    public class UpdateMailArgs
    {
        
    }
}