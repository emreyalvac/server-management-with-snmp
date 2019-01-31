using System.Threading.Tasks;

namespace server_managment_system.Repostories.Cpu
{
    public interface ICpu
    {
        Task<string> GetAllCpuInformation();
    }
}