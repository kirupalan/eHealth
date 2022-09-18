﻿using eHealthAPI.Data;
using eHealthAPI.Models.Domain;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace eHealthAPI.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly eHealthDBContext neHealthDBContext;
        public MedicineRepository(eHealthDBContext eHealthDBContext)
        {
            this.neHealthDBContext = eHealthDBContext;
        }
        public IEnumerable<Medicine> GetAll()
        {
            return neHealthDBContext.Medicines.ToList();
        }
    }
}