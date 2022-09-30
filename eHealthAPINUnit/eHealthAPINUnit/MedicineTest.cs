using AutoMapper;
using eHealthAPI.Controllers;
using eHealthAPI.Models.DTO;
using eHealthAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eHealthAPI
{
    public class MedicineTest
    {
        private readonly Mock<IMedicineRepository> repo;
        private readonly Mock<IConfiguration> config;
        private readonly Mock<IMapper> mapper;
        private readonly Mock<IImageRepository> imgageRepo;

        public MedicineTest()
        {
            repo = new();
            config = new Mock<IConfiguration>();
            mapper = new Mock<IMapper>();
            imgageRepo = new Mock<IImageRepository>();

        }

        [Test]
        public async Task GetAllMedicinesAsync()
        {
            /*
            repo.Setup(m => m.GetAsync(It.IsAny<int>())).Returns((Medicine));
            var controller = new MedicineController(repo.Object, mapper.Object, imgageRepo.Object);
            var result = await controller.GetMedicineAsync(It.IsAny<int>());

            Assert.IsInstanceOf<NotFoundResult>(result);
            */
        }
    }
}
