using backend.Controllers;
using Dapper;
using Npgsql;

namespace backend.Database
{
    public static class DatabaseApplicationUtils
    {
        public static NpgsqlConnection DatabaseConnection(string connectionString)
        {
            var con = new NpgsqlConnection(connectionString: connectionString);
            con.Open();
            return con;
        }

        public static CommandDefinition AddUser(CreateUserArgs request)
        {
            string sqlReq = $"INSERT INTO public.user (pseudo, email_address) " +
                            $"VALUES (@pseudo, @email_address)";

            DynamicParameters dp = new();
            dp.Add("@pseudo", request.Pseudo);
            dp.Add("@email_address", request.EmailAddress);

            return new CommandDefinition(sqlReq, dp);
        }

        public static CommandDefinition AddMail(MailArgs request)
        {
            string sqlReq = $"INSERT INTO mail (user_id, object, body, sender, receiver, inbox) " +
                            $"VALUES (@user_id, @object, @body, @sender, @receiver, @inbox)";

            DynamicParameters dp = new();
            dp.Add("@user_id", request.UserId);
            dp.Add("@Object", request.Object);
            dp.Add("@Body", request.Body);
            dp.Add("@Sender", request.Sender);
            dp.Add("@Receiver", request.Receiver);
            dp.Add("@Inbox", request.Inbox);

            return new CommandDefinition(sqlReq, dp);
        }

        public static CommandDefinition GetMails(int userId)
        {
            string sqlReq = $"SELECT object, body, sender, receiver, inbox FROM mail" +
                            $"WHERE user_id = @UserId";

            DynamicParameters dp = new();
            dp.Add("@UserId", userId);

            return new CommandDefinition(sqlReq, dp);
        }
    }
}