using System;
using System.Threading.Tasks;

namespace ReportAPI.Service
{
    public interface IRabbitService
    {
        Task SendMessage(Guid id, string location);
        void ReceiveMessage();
    }
}
