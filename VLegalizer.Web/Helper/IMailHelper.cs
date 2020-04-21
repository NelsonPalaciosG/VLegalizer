using VLegalizer.Common.Models;

namespace VLegalizer.Web.Helper
{
	public interface IMailHelper
	{
		void SendMail(string to, string subject, string body);
	}

}
