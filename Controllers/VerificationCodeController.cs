namespace SecureVotingSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SecureVotingSystem.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class VerificationCodeController : ControllerBase
    {
        private readonly VoterAuthenticationService _voterAuthService;
        //private readonly TwilioService _twilioService;

        public VerificationCodeController(VoterAuthenticationService voterAuthService/*, TwilioService twilioService*/)
        {
            _voterAuthService = voterAuthService;
            //_twilioService = twilioService;
        }

        [HttpPost("generate-code")]
        public IActionResult GenerateCode([FromBody] VerificationCodeRequest request)
        {
            try
            {
                string verificationCode = _voterAuthService.GenerateVerificationCode(request.CNIC);

                var contactDetails = _voterAuthService.GetContactDetails(request.CNIC);

                //_twilioService.SendSms(contactDetails, $"Your verification code is: {verificationCode}");

                return Ok(new { Message = "Verification code generated and sent successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = $"Error generating and sending verification code: {ex.Message}" });
            }
        }
    }
}