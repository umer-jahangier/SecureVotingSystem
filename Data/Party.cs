using System.ComponentModel.DataAnnotations;

namespace SecureVotingSystem.Data
{
    public class Party
    {
        [Key]
        [Required]
        public int PartyId { get; set; }

        [Required]
        public string Name { get; set; }
        public int TotalVotes { get; set; }

        [Required]
        public string LogoPath { get; set; }
    }
}
