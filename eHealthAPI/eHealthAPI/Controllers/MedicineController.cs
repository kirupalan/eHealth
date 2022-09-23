using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using eHealthAPI.Models.Domain;
using eHealthAPI.Repositories;
using Microsoft.AspNetCore.Http;


using eHealthAPI.Models.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace eHealthAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicineController : Controller
    {
        private readonly IMedicineRepository medicineRepository;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        //public MedicineController(IMedicineRepository medicineRepository)
        public MedicineController(IMedicineRepository medicineRepository, IMapper mapper, IImageRepository imageRepository)
        {
            this.medicineRepository = medicineRepository;
            this.mapper = mapper;
        }


        // Kiru: Get All Medicines
        [HttpGet]
        //public IActionResult GetAllMedicines()
        public async Task<IActionResult> GetAllMedicinesAsync()
        {
            var medicines = await medicineRepository.GetAllAsync();

            //var medicines = medicineRepository.GetAll();

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

        // Kiru: Get Medicine by id
        [HttpGet]
        [Route("{Id:int}")]
        [ActionName("GetMedicineAsync")]
        public async Task<IActionResult> GetMedicineAsync(int Id)
        {
            var medicine = await medicineRepository.GetAsync(Id);

            if (medicine == null)
            {
                return NotFound();
            }

            var medicinesDTO = mapper.Map<Models.DTO.Medicine>(medicine);
            return Ok(medicinesDTO);
        }

        // Kiru: Add Medicine
        [HttpPost]
        public async Task<IActionResult> AddMedicineAsync(Models.DTO.AddMedicineRequest addMedicineRequest)
        {
            // Request(DTO) to Domain Model
            var medicine = new Models.Domain.Medicine()
            {
                MedicineName = addMedicineRequest.MedicineName,
                Manufacturer = addMedicineRequest.Manufacturer,
                UnitPrice = addMedicineRequest.UnitPrice,
                Discount = addMedicineRequest.Discount,
                Quantity = addMedicineRequest.Quantity,
                Disease = addMedicineRequest.Disease,
                Uses = addMedicineRequest.Uses,
                ExpDate = addMedicineRequest.ExpDate,
                ImageUrl = addMedicineRequest.ImageUrl,
                Status = addMedicineRequest.Status
            };


            //Pass details to repository
            medicine =  await medicineRepository.AddAsync(medicine);

            //Comnvert the data back to DTO
            var medicineDTO = new Models.Domain.Medicine
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

            return CreatedAtAction(nameof(GetMedicineAsync), new { Id = medicineDTO.Id}, medicineDTO);
        }

        // Kiru: Delete Medicine
        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteMedicineAsync(int Id)
        {
            //Get Medicine
            var medicine = await medicineRepository.DeleteAsync(Id);

            //If null NotFound
            if (medicine == null)
            {
                return NotFound();
            }

            //Comnvert the data back to DTO
            var medicineDTO = new Models.Domain.Medicine
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

            //Return OK Response
            return Ok(medicineDTO);
        }

        // Kiru: Update Medicine
        [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateMedicineAsync([FromRoute] int Id, [FromBody] Models.DTO.UpdateMedicineRequest updateMedicineRequest)
        {
            // Request(DTO) to Domain Model
            var medicine = new Models.Domain.Medicine()
            {
                MedicineName = updateMedicineRequest.MedicineName,
                Manufacturer = updateMedicineRequest.Manufacturer,
                UnitPrice = updateMedicineRequest.UnitPrice,
                Discount = updateMedicineRequest.Discount,
                Quantity = updateMedicineRequest.Quantity,
                Disease = updateMedicineRequest.Disease,
                Uses = updateMedicineRequest.Uses,
                ExpDate = updateMedicineRequest.ExpDate,
                ImageUrl = updateMedicineRequest.ImageUrl,
                Status = updateMedicineRequest.Status
            };

            //Pass details to repository
            medicine = await medicineRepository.UpdateAsync(Id, medicine);

            //If null NotFound
            if (medicine == null)
            {
                return NotFound();
            }

            //Comnvert the data back to DTO
            var medicineDTO = new Models.Domain.Medicine
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


            //Return OK Response
            return Ok(medicineDTO);

        }

        [HttpPost]
        [Route("{Id:int}/upload-image")]
        // Kiru: Upload Image
        public async Task<IActionResult> UploadImage([FromRoute] int Id, IFormFile profileImage)
        {
            //Upload the image to local storage

            //var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);

            var fileName =  Path.GetFileName(profileImage.FileName);

            var fileImagePath = await imageRepository.Upload(profileImage, fileName);

            if(await medicineRepository.UpdateProfileImageAsync(Id, fileImagePath))
            {
                return Ok(fileImagePath);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Uploading Image");
            }
        }
    }
}
