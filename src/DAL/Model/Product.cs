using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuitsupplyTestTask.DAL.Model
{
    public class Product
    {
        /// <summary>
        ///     Product ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        ///     Product Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Base64 encoded Photo
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        ///     Price in euros
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Date of last update in UTC
        /// </summary>
        public DateTime LastUpdated { get; set; }
    }
}