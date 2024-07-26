using Microsoft.EntityFrameworkCore;

namespace SecureVotingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<VoterCredentials> VoterCredentials { get; set; }
        public DbSet<AdminCredentials> AdminCredentials { get; set; }
        public DbSet<Party> Parties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public void EnsureDataSeeded()
        {
            if (!VoterCredentials.Any())
            {
                // Seed VoterCredentials data
                VoterCredentials.Add(new VoterCredentials
                {
                    CNIC = "1234567891012",
                    VerificationCode = "@12$34%_",
                    VoterName = "PM Imran Khan",
                    ContactDetails = "+9212345678900",
                    HasCastedVote = false,
                    VotedParty = ""
                });

                VoterCredentials.Add(new VoterCredentials
                {
                    CNIC = "1234567891013",
                    VerificationCode = "1_2$34%6",
                    VoterName = "Nawaz Sharif",
                    ContactDetails = "+9298765432100",
                    HasCastedVote = false,
                    VotedParty = ""
                });

                SaveChanges();
            }

            if (!Parties.Any())
            {
                Parties.Add(new Party { Name = "PTI", TotalVotes = 20, LogoPath = "/pti_logo.png" });
                Parties.Add(new Party { Name = "PMLN", LogoPath = "/pmln_logo.png" });

                SaveChanges();
            }

            if (!AdminCredentials.Any())
            {
                
                AdminCredentials.Add(new AdminCredentials
                {
                    Username = "admin",
                    Password = "H@sh_P@ssw0rd" 
                });

                SaveChanges();
            }
        }

        public void AddVoter(string cnic, string verificationCode, string voterName, string contactDetails)
        {
            var existingVoter = VoterCredentials.FirstOrDefault(v => v.CNIC == cnic);

            if (existingVoter == null)
            {

                var newVoter = new VoterCredentials
                {
                    CNIC = cnic,
                    VerificationCode = verificationCode,
                    VoterName = voterName,
                    ContactDetails = contactDetails,
                    HasCastedVote = false,
                    VotedParty = ""
                };

                VoterCredentials.Add(newVoter);
                SaveChanges();
            }
        }
    }
}
