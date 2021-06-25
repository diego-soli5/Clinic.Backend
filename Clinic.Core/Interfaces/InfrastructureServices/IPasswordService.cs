namespace Clinic.Core.Interfaces.InfrastructureServices
{
    public interface IPasswordService
    {
        bool Check(string hashedPassword, string plainPassword);
        string Hash(string plainPassword);
    }
}