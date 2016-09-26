﻿using System;

using SuitsupplyTestTask.WebAPI.Api.Common;
using SuitsupplyTestTask.WebAPI.Areas.HelpPage.ModelDescriptions;

namespace SuitsupplyTestTask.WebAPI.Api.Version2.DTO
{
    /// <summary>
    ///     Some product in a shop.
    /// </summary>
    [ModelName("v2 Product")]
    public class Product : IHaveId
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

        /// <summary>
        ///     Material
        /// </summary>
        public string Material { get; set; }
    }
}