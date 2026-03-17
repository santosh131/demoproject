using SampleCoreApp.Infrastructure.BaseModels;
using System;

namespace SampleCoreApp.Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public AdditionalInfoModel AdditionalInfoModel { get; set; }

        public string Name{ get; set; }

        public string AliasName { get; set; }
        
        public string Name3{ get;  set; }
        public string Name5{ get;  set; }
        public string Name6{ get;  set; }
    }
}
