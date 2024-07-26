using SecureVotingSystem.Data;

namespace SecureVotingSystem.Services
{
    public class VoterAuthenticationService
    {
        private readonly ApplicationDbContext _dbContext;
        public bool IsUserAuthenticated { get; private set; }
        public bool IsAdminAuthenticated { get; private set; }
        public bool HasUserCastedVote { get; set; }
        public string CurrentUserCnic { get; private set; }
        public string VoterName { get; private set; }
        public string CNIC { get; private set; }
        public string ContactDetails { get; private set; }

        private string ErrorMessage;

        public VoterAuthenticationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool VerifyCredentials(string cnic, string verificationCode)
        {
            var voter = _dbContext.VoterCredentials
                .FirstOrDefault(v => v.CNIC == cnic && v.VerificationCode == verificationCode);

            IsUserAuthenticated = voter != null;

            if (IsUserAuthenticated)
            {
                CurrentUserCnic = cnic;
                VoterName = voter.VoterName;
                CNIC = voter.CNIC;
                ContactDetails = voter.ContactDetails;
                HasUserCastedVote = voter.HasCastedVote;
                voter.VerificationCode = GenerateRandomCode();
                _dbContext.SaveChanges();
            }



            return IsUserAuthenticated;
        }

        public void Logout()
        {
            IsUserAuthenticated = false;
            HasUserCastedVote = false;
            CurrentUserCnic = null;
            VoterName = null;
            CNIC = null;
            ContactDetails = null;
        }

        public string GetContactDetails(string cnic)
        {
            var voter = _dbContext.VoterCredentials
                .FirstOrDefault(v => v.CNIC == cnic);

            if (voter != null)
            {
                return voter.ContactDetails;
            }
            else
            {
                throw new InvalidOperationException("User not found");
            }
        }

        public string GenerateVerificationCode(string cnic)
        {
            var voter = _dbContext.VoterCredentials.FirstOrDefault(v => v.CNIC == cnic);

            if (voter != null)
            {
                var verificationCode = GenerateRandomCode();
                voter.VerificationCode = verificationCode;
                _dbContext.SaveChanges();

                return verificationCode;
            }
            else
            {
                return "User not found";
            }
        }

        public string GenerateRandomCode()
        {
            IsAdminAuthenticated = false;
            const string chars = "ABCDEFGHIJKLMNOP!@#$%&^%QRSTUVWXYZ0123456789!@#$%&^%";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            
        }

        public bool VerifyAdminCredentials(string username, string password)
        {
            var user = _dbContext.AdminCredentials.FirstOrDefault(u => u.Username == username && u.Password == password);
            IsAdminAuthenticated = user != null;

            return IsAdminAuthenticated;
        }
    }
}
