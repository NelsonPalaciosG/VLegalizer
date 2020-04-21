using VLegalizer.Common.Models;

namespace VLegalizer.Web.Helper
{
	public interface IMailHelper
	{
		Response SendMail(string to, string subject, string body);
	}

}
