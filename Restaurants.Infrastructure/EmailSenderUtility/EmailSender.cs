using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;


namespace Restaurants.Application.Users.EmailSenderUtility
{
	public sealed class EmailSender : IEmailSender, IEmailSender<ApplicationUser>
	{
		private readonly ILogger _logger;

		public EmailSender(ILogger<EmailSender> logger)
		{
			_logger = logger;
		}
		private List<Email> Emails { get; set; } = new();

		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			_logger.LogWarning($"{email} {subject} {htmlMessage}");
			Emails.Add(new(email, subject, htmlMessage));
			return Task.CompletedTask;
		}

		Task IEmailSender<ApplicationUser>.SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
		{
			return Task.CompletedTask;
		}

		Task IEmailSender<ApplicationUser>.SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
		{
			return Task.CompletedTask;
		}

		Task IEmailSender<ApplicationUser>.SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
		{
			return Task.CompletedTask;
		}
	}


	sealed record Email(string Address, string Subject, string HtmlMessage);
}

