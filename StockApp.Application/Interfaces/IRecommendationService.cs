﻿using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.Interfaces
{
    public interface IRecommendationService
    {
        Task<IEnumerable<Product>> GetRecommendationsAsync(string userId);
    }
}
