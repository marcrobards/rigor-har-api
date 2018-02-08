namespace Rigor.HAR.API.Models
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("HarFile")]
    public class HarFile
    {
        [Key]
        public long HarFileId { get; set; }

        public string URL { get; set; }

        public DateTime StartedDateTime { get; set; }

        public string JSONString { get; set; }

        public object JSONContent
        {
            get
            {
                return this.JSONString != null ? JsonConvert.DeserializeObject(this.JSONString) : new Object();
            }
        }

    }
}
