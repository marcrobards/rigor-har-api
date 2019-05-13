namespace Rigor.HAR.API.Models
{
    using HarSharp;
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("HarFiles")]
    public class HarFile
    {
        [Key]
        public long HarFileId { get; set; }

        public string URL { get; set; }

        public DateTime StartedDateTime { get; set; }

        public string HarContentString { get; set; }

        public Har HarContent
        {
            get
            {
                return this.HarContentString != null ? JsonConvert.DeserializeObject<Har>(this.HarContentString) : new Har();
            }
        }

    }
}
