using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using eHealthAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using static StackExchange.Redis.Role;

namespace eHealthAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicineController : Controller
    {
        private readonly IMedicineRepository _repo;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imgRepo;
        private MedicineController datastore;

        public MedicineController(IMedicineRepository repo, IMapper mapper, IImageRepository imgRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _imgRepo = imgRepo;
        }

        public MedicineController()
        {
        }

        public MedicineController(MedicineController datastore)
        {
            this.datastore = datastore;
        }

        // Kiru: Get All Medicines
        [HttpGet]
        public async Task<IActionResult> GetAllMedicinesAsync()
        {
            var medicines = await _repo.GetAllAsync();
            var medicinesDTO = _mapper.Map<List<Models.DTO.Medicine>>(medicines);
            return Ok(medicinesDTO);
        }

        // Kiru: Get Medicine by id
        [HttpGet]
        [Route("{Id:int}")]
        [ActionName("GetMedicineAsync")]
        public async Task<IActionResult> GetMedicineAsync(int Id)
        {
            var medicine = await _repo.GetAsync(Id);

            if (medicine == null)
            {
                return NotFound();
            }

            var medicinesDTO = _mapper.Map<Models.DTO.Medicine>(medicine);
            return Ok(medicinesDTO);
        }

        // Kiru: Add Medicine
        [HttpPost]
        //[Authorize]
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
            medicine =  await _repo.AddAsync(medicine);

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
        //[Authorize]
        public async Task<IActionResult> DeleteMedicineAsync(int Id)
        {
            //Get Medicine
            var medicine = await _repo.DeleteAsync(Id);

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
        //[Authorize]
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
            medicine = await _repo.UpdateAsync(Id, medicine);

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

        // Kiru: Upload Image
        [HttpPost]
        [Route("{Id:int}/upload-image")]
        //[Authorize]
        public async Task<IActionResult> UploadImage([FromRoute] int Id, IFormFile profileImage)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
            var fileImagePath = await _imgRepo.Upload(profileImage, fileName);

            if(await _repo.UpdateProfileImageAsync(Id, fileImagePath))
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
