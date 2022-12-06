using System.Text;

namespace Tasko.Server.Infrastructure.Services
{
    internal static class AesService
    {
        internal static byte[] AesKey { get { return Encoding.UTF8.GetBytes("da983189246a4520a94764a751fa466a"); } }
        internal static byte[] IV { get { return new byte[16]; } }
    }
}
