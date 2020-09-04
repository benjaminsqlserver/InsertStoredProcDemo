using Microsoft.AspNetCore.Mvc;
using InsertStoredProcDemo.Data;

namespace InsertStoredProcDemo
{
    public partial class ExportConDataController : ExportController
    {
        private readonly ConDataContext context;

        public ExportConDataController(ConDataContext context)
        {
            this.context = context;
        }

        [HttpGet("/export/ConData/markets/csv")]
        public FileStreamResult ExportMarketsToCSV()
        {
            return ToCSV(ApplyQuery(context.Markets, Request.Query));
        }

        [HttpGet("/export/ConData/markets/excel")]
        public FileStreamResult ExportMarketsToExcel()
        {
            return ToExcel(ApplyQuery(context.Markets, Request.Query));
        }
    }
}
