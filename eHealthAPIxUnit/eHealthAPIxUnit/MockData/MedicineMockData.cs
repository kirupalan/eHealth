using eHealthAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace eHealthAPIxUnit.MockData
{
    public class MedicineMockData
    {
        public static List<Medicine> GetMedicines()
        {
            return new List<Medicine>
            {
                new Medicine
                {
                    Id=1,
                    MedicineName = "Test",
                    Manufacturer = "Test",
                    UnitPrice = 100,
                    Discount = 10,
                    Quantity = 1000,
                    Disease = "Test",
                    Uses = "Test",
                    ExpDate = new DateTime(),
                    ImageUrl = "Test",
                    Status = "Active"
                },
                new Medicine
                {
                    Id=2,
                    MedicineName = "Test",
                    Manufacturer = "Test",
                    UnitPrice = 100,
                    Discount = 10,
                    Quantity = 1000,
                    Disease = "Test",
                    Uses = "Test",
                    ExpDate = new DateTime(),
                    ImageUrl = "Test",
                    Status = "Active"
                }
            };
        }
    }
}
