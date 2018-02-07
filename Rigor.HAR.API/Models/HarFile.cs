namespace Rigor.HAR.API.Models
{
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

        public string JSONContent { get; set; }

    }
}
