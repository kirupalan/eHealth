using eHealthAPI.Controllers;
using eHealthAPI.Data;
using eHealthAPI.Models.Domain;
using eHealthAPIxUnit.MockData;
using FakeItEasy;
using Moq;
using Xunit;

namespace eHealthAPIxUnit
{
    public class MedicineTest
    {
        [Fact]
        public async Task GetAllMedicinesAsyncTest()
        {

            //Arrange
            var testdatastore = new Mock<MedicineController>();
            //testdatastore.Setup(_ => _.GetAllMedicinesAsync().ReturnsAsync(MedicineMockData.GetMedicines));

            //Act


            //Assert
        }
    }
}