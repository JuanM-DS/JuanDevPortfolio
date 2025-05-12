namespace Application.Interfaces.Services
{
	public interface IEncryptationServices
	{
		string Decrypt(string srt);
		string Encrypt(string srt);
	}
}