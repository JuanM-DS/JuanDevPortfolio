namespace Core.Application.Interfaces.Helpers
{
	public interface IEncryptationServices
	{
		string Decrypt(string srt);
		string Encrypt(string srt);
	}
}