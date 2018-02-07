namespace Rigor.HAR.API.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class HarFile
    {
        [Key]
        public long HarFileId { get; set; }

        public string URL { get; set; }

        public DateTime StartedDateTime { get; set; }

        public string JSONContent { get; set; }

    }
}
