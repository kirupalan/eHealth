using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using eHealthAPI.Models.Domain;
using eHealthAPI.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace eHealthAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicineController : Controller
    {
        private readonly IMedicineRepository medicineRepository;
        private readonly IMapper  mapper;

        //public MedicineController(IMedicineRepository medicineRepository)
        public MedicineController(IMedicineRepository medicineRepository,IMapper mapper)
        {
            this.medicineRepository = medicineRepository;
            this.mapper = mapper;
        }

        

        [HttpGet]
        public IActionResult GetAllMedicines()
        {
            var medicines = medicineRepository.GetAll();

            //return DTO medicines

            /*
            var medicinesDTO = new List<Models.DTO.Medicine>();

            medicines.ToList().ForEach(medicine =>
            {
                var medicineDTO = new Models.DTO.Medicine()
                {
                    Id = medicine.Id,
                    MedicineName = medicine.MedicineName,
                    Manufacturer = medicine.Manufacturer,
                    UnitPrice = medicine.UnitPrice,
                    Discount = medicine.Discount,
                    Quantity = medicine.Quantity,
                    Disease = medicine.Disease,
                    Uses = medicine.Uses,
                    ExpDate = medicine.ExpDate,
                    ImageUrl = medicine.ImageUrl,
                    Status = medicine.Status
                };

                medicinesDTO.Add(medicineDTO);

            });
            */
            var medicinesDTO = mapper.Map<List<Models.DTO.Medicine>>(medicines);

            return Ok(medicinesDTO);
        }
    }
}
