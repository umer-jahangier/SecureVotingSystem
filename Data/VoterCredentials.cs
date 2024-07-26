using System.ComponentModel.DataAnnotations;

namespace SecureVotingSystem.Data
{
    public class VoterCredentials
    {
        [Key]
        [Required]
        [RegularExpression("^[0-9]{13}$", ErrorMessage = "CNIC must be 13 digits.")]
        public string CNIC { get; set; }

        [Required]
        public string VerificationCode { get; set; }
        [Required]
        public string VoterName { get; set; }
        [Required]
        public string ContactDetails { get; set; }

        public bool HasCastedVote { get; set; }

        public string VotedParty { get; set; }
    }
}
