/*
using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

public class TwilioService
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _twilioPhoneNumber;

    public TwilioService(string accountSid, string authToken, string twilioPhoneNumber)
    {
        _accountSid = accountSid;
        _authToken = authToken;
        _twilioPhoneNumber = twilioPhoneNumber;

        TwilioClient.Init(_accountSid, _authToken);
    }

    public void SendSms(string to, string message)
    {
        var messageOptions = new CreateMessageOptions(
            new Twilio.Types.PhoneNumber(to))
        {
            Body = message,
            From = new Twilio.Types.PhoneNumber(_twilioPhoneNumber)
        };

        var messageResponse = MessageResource.Create(messageOptions);

    }
}
*/
