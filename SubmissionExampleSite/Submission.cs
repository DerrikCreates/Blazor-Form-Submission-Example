using System.ComponentModel.DataAnnotations;

namespace BlazorApp
{
    public class Submission
    {
        [Required]
        [StringLength(30, ErrorMessage = "Max length of 30")]
        public string Name { get; set; }


        [Required]
        [StringLength(280, MinimumLength = 1, ErrorMessage = "Post must have 1 to 280 characters")]
        public string Post { get; set; }

        /// <summary>
        /// The date in unix epoch milliseconds 
        /// </summary>
        public long Date { get; set; }

        public uint id { get; set; }
    }
}