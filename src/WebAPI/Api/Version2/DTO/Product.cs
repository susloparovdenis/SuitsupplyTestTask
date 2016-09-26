﻿using System;

using Omu.ValueInjecter;

using SuitsupplyTestTask.WebAPI.Areas.HelpPage.ModelDescriptions;

namespace SuitsupplyTestTask.WebAPI.Api.Version2.DTO
{
    /// <summary>
    ///     Some product in a shop.
    /// </summary>
    [ModelName("v2 Product")]
    public class Product
    {
        /// <summary>
        ///     Product IDaasd
        /// </summary>
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
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public static Product Map(DAL.Model.Product product) => Mapper.Map<Product>(product);

        public static DAL.Model.Product Map(Product product) => Mapper.Map<DAL.Model.Product>(product);
    }
}