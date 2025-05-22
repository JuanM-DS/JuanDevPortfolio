namespace Core.Application.Interfaces.Shared
{
	public interface IEncryptationServices
	{
		string Decrypt(string srt);
		string Encrypt(string srt);
	}
}