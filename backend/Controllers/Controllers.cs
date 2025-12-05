using backend.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("webapp/api/")]
    public class Controller : ControllerBase
    {
        private readonly IDatabaseApplication _databaseApplication;
        public Controller(IDatabaseApplication databaseApplication)
        {
            _databaseApplication = databaseApplication;
        }

        [HttpPost("users")]
        public async Task<bool> CreateUser(CreateUserArgs request)
        {
            try
            {
                await _databaseApplication.AddUser(request);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        [HttpGet("users/{userId:int}")]
        public void GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("users/{userId:int}")]
        public void UpdateUser(UpdateUserArgs request, int userId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("users/{userId:int}")]
        public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("users/{userId:int}/mails")]
        public async Task<bool> SaveMail(MailArgs request)
        {
            try
            {
                await _databaseApplication.AddMail(request);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet("users/{userId:int}/mails")]
        public async Task<List<MailArgs>> GetUserMails(int userId)
        {
            try
            {
                var ret = await _databaseApplication.GetMails(userId);
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("users/{userId:int}/mails/{mailId:int}")]
        public void GetMail(int userId, int mailId)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("users/{userId:int}/mails/{mailId:int}")]
        public void UpdateMail(UpdateMailArgs request, int userId, int mailId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("users/{userId:int}/mails/{mailId:int}")]
        public void DeleteMail(int userId, int mailId)
        {
            throw new NotImplementedException();
        }
    }
}