using SampleCoreApp.Infrastructure.BaseModels;
using System;

namespace SampleCoreApp.Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public AdditionalInfoModel AdditionalInfoModel { get; set; }
    }
}
