using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using TurfSyncTurfSchedulingSportsSystem.DataDLL;
using TurfSyncTurfSchedulingSportsSystem.Models;

namespace TurfSyncTurfSchedulingSportsSystem.ServicesBLL
{
    public class PendingRequestService
    {
        private readonly PendingRequestRepository repo;

        public PendingRequestService()
        {
            repo = new PendingRequestRepository();
        }

        public List<PendingRequest> GetAllPending()
        {
            return repo.GetPendingRequests();
        }

        public void Approve(int requestId)
        {
            repo.UpdateRequestStatus(requestId, "Approved");
        }

        public void Cancel(int requestId)
        {
            repo.UpdateRequestStatus(requestId, "Cancelled");
        }
    }
}
