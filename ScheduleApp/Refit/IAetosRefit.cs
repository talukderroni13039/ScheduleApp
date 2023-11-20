using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp.Refit
{
    public interface IAetosRefit
    {
        [Get("/getForEdgex/getDetails")]
        Task<ApiResponse<IEnumerable<AetosModel>>> GetAetosData();

    }
}
