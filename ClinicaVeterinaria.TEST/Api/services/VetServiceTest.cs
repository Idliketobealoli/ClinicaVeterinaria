﻿using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.errors;
using ClinicaVeterinaria.API.Api.model;
using ClinicaVeterinaria.API.Api.repositories;
using ClinicaVeterinaria.API.Api.services;
using Moq;

namespace ClinicaVeterinaria.TEST.Api.services
{
    [TestClass]
    public class VetServiceTest
    {
        private Mock<VetRepository> Repo;
        private VetService Service;
        private List<Vet> List;
        private List<VetDTO> ListDTO;
        private Vet Entity;
        private VetDTO DTO;
        private VetDTOshort DTOShort;
        private VetDTOappointment DTOappointment;
        private VetDTOandToken DTOandToken;
        private VetDTOregister DTOregister;
        private VetDTOloginOrChangePassword DTOupdate;
        private VetDTOloginOrChangePassword DTOlogin;

        [TestInitialize]
        public void Init()
        {
            Repo = new Mock<VetRepository>();
            Service = new(Repo.Object);
            Entity = new(
                "test", "testeado", "uwu@gmail.com",
                "123456789", "uwu1234", Role.VET, "qwerty");
            DTO = new(
                "test", "testeado", "uwu@gmail.com",
                "123456789", Role.VET, "qwerty");
            DTOShort = new("test", "testeado");
            DTOandToken = new(DTO, "token");
            List = new List<Vet>() { Entity };
            ListDTO = new List<VetDTO>() { DTO };
            DTOregister = new(
                "test", "testeado2", "email2@gmail.com",
                "987654321", "uwu1234", "uwu1234",
                Role.VET, "qwerty");
            DTOupdate = new("uwu@gmail.com", "1234uwu");
            DTOlogin = new("uwu@gmail.com", "uwu1234");
            DTOappointment = new("test", "testeado2", "email2@gmail.com");
        }

        [TestMethod]
        public void FindAllOK()
        {
            Repo.Setup(x => x.FindAll()).ReturnsAsync(List, new TimeSpan(100));

            var res = Service.FindAll();
            res.Wait();

            Assert.IsNotNull(res.Result);
            CollectionAssert.AllItemsAreNotNull(res.Result);
            Assert.AreEqual(ListDTO.Count, res.Result.Count);
        }

        [TestMethod]
        public void FindAllNF()
        {
            Repo.Setup(x => x.FindAll()).ReturnsAsync(new(), new TimeSpan(100));

            var res = Service.FindAll();
            res.Wait();

            Assert.IsNotNull(res.Result);
            CollectionAssert.AllItemsAreNotNull(res.Result);
            Assert.AreEqual(new List<UserDTO>().Count, res.Result.Count);
            CollectionAssert.AreEqual(new List<UserDTO>(), res.Result);
        }

        [TestMethod]
        public void FindByEmailOk()
        {
            Repo.Setup(x => x.FindByEmail(It.IsAny<string>())).ReturnsAsync(Entity, new TimeSpan(100));

            var res = Service.FindByEmail("uwu@gmail.com");
            res.Wait();

            Assert.IsTrue(res.Result._isSuccess);
            Assert.IsNotNull(res.Result._successValue);
            Assert.IsNull(res.Result._errorValue);
            Assert.AreEqual(DTO.Name, res.Result._successValue.Name);
        }

        [TestMethod]
        public void FindByEmailNF()
        {
            Repo.Setup(x => x.FindByEmail(It.IsAny<string>())).ReturnsAsync(null, new TimeSpan(100));

            var res = Service.FindByEmail("uwu@gmail.com");
            res.Wait();

            Assert.IsFalse(res.Result._isSuccess);
            Assert.IsNull(res.Result._successValue);
            Assert.IsNotNull(res.Result._errorValue);
            Assert.AreEqual
                (new VetErrorNotFound($"Vet with email uwu@gmail.com not found.").Message,
                res.Result._errorValue.Message);
        }

        [TestMethod]
        public void FindByEmailShortOk()
        {
            Repo.Setup(x => x.FindByEmail(It.IsAny<string>())).ReturnsAsync(Entity, new TimeSpan(100));

            var res = Service.FindByEmailShort("uwu@gmail.com");
            res.Wait();

            Assert.IsTrue(res.Result._isSuccess);
            Assert.IsNotNull(res.Result._successValue);
            Assert.IsNull(res.Result._errorValue);
            Assert.AreEqual(DTOShort.Name, res.Result._successValue.Name);
        }

        [TestMethod]
        public void FindByEmailShortNF()
        {
            Repo.Setup(x => x.FindByEmail(It.IsAny<string>())).ReturnsAsync(null, new TimeSpan(100));

            var res = Service.FindByEmailShort("uwu@gmail.com");
            res.Wait();

            Assert.IsFalse(res.Result._isSuccess);
            Assert.IsNull(res.Result._successValue);
            Assert.IsNotNull(res.Result._errorValue);
            Assert.AreEqual(
                new VetErrorNotFound($"Vet with email uwu@gmail.com not found.").Message,
                res.Result._errorValue.Message);
        }

        [TestMethod]
        public void FindByEmailAppointmentOk()
        {
            Repo.Setup(x => x.FindByEmail(It.IsAny<string>())).ReturnsAsync(Entity, new TimeSpan(100));

            var res = Service.FindByEmailAppointment("uwu@gmail.com");
            res.Wait();

            Assert.IsTrue(res.Result._isSuccess);
            Assert.IsNotNull(res.Result._successValue);
            Assert.IsNull(res.Result._errorValue);
            Assert.AreEqual(DTOappointment.Name, res.Result._successValue.Name);
        }

        [TestMethod]
        public void FindByEmailAppointmentNF()
        {
            Repo.Setup(x => x.FindByEmail(It.IsAny<string>())).ReturnsAsync(null, new TimeSpan(100));

            var res = Service.FindByEmailAppointment("uwu@gmail.com");
            res.Wait();

            Assert.IsFalse(res.Result._isSuccess);
            Assert.IsNull(res.Result._successValue);
            Assert.IsNotNull(res.Result._errorValue);
            Assert.AreEqual(
                new VetErrorNotFound($"Vet with email uwu@gmail.com not found.").Message,
                res.Result._errorValue.Message);
        }

        [TestMethod]
        public void RegisterOk()
        {
            Repo.Setup(x => x.FindByEmail(It.IsAny<string>())).ReturnsAsync(null, new TimeSpan(100));
            Repo.Setup(x => x.FindBySSNum(It.IsAny<string>())).ReturnsAsync(null, new TimeSpan(100));
            Repo.Setup(x => x.Create(It.IsAny<Vet>())).ReturnsAsync(Entity, new TimeSpan(100));

            var res = Service.Register(DTOregister);
            res.Wait();

            Assert.IsTrue(res.Result._isSuccess);
            Assert.IsNotNull(res.Result._successValue);
            Assert.IsNull(res.Result._errorValue);
            Assert.AreEqual(DTOandToken.Token, res.Result._successValue.Token);
        }

        [TestMethod]
        public void RegisterError()
        {
            Repo.Setup(x => x.FindByEmail(It.IsAny<string>())).ReturnsAsync(Entity, new TimeSpan(100));
            Repo.Setup(x => x.FindBySSNum(It.IsAny<string>())).ReturnsAsync(Entity, new TimeSpan(100));

            var res = Service.Register(DTOregister);
            res.Wait();

            Assert.IsFalse(res.Result._isSuccess);
            Assert.IsNull(res.Result._successValue);
            Assert.IsNotNull(res.Result._errorValue);
            Assert.AreEqual(
                new VetErrorUnauthorized("Cannot use either that email or that Social Security number.").Message,
                res.Result._errorValue.Message);
        }

        [TestMethod]
        public void LoginOk()
        {
            Repo.Setup(x => x.FindByEmail(It.IsAny<string>())).ReturnsAsync(Entity, new TimeSpan(100));

            var res = Service.Login(DTOlogin);
            res.Wait();

            Assert.IsTrue(res.Result._isSuccess);
            Assert.IsNotNull(res.Result._successValue);
            Assert.IsNull(res.Result._errorValue);
            Assert.AreEqual(DTOandToken.Token, res.Result._successValue.Token);
        }

        [TestMethod]
        public void LoginError()
        {
            Repo.Setup(x => x.FindByEmail(It.IsAny<string>())).ReturnsAsync(null, new TimeSpan(100));

            var res = Service.Login(DTOlogin);
            res.Wait();

            Assert.IsFalse(res.Result._isSuccess);
            Assert.IsNull(res.Result._successValue);
            Assert.IsNotNull(res.Result._errorValue);
            Assert.AreEqual(
                new VetErrorUnauthorized("Incorrect email or password.").Message,
                res.Result._errorValue.Message);
        }

        [TestMethod]
        public void ChangePasswordOk()
        {
            Repo.Setup(x => x.UpdatePassword(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(Entity, new TimeSpan(100));

            var res = Service.ChangePassword(DTOupdate);
            res.Wait();

            Assert.IsTrue(res.Result._isSuccess);
            Assert.IsNotNull(res.Result._successValue);
            Assert.IsNull(res.Result._errorValue);
            Assert.AreEqual(DTO.Name, res.Result._successValue.Name);
        }

        [TestMethod]
        public void ChangePasswordError()
        {
            Repo.Setup(x => x.UpdatePassword(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(null, new TimeSpan(100));

            var res = Service.ChangePassword(DTOupdate);
            res.Wait();

            Assert.IsFalse(res.Result._isSuccess);
            Assert.IsNull(res.Result._successValue);
            Assert.IsNotNull(res.Result._errorValue);
            Assert.AreEqual(
                new VetErrorNotFound($"Vet with email {DTOupdate.Email} not found.").Message,
                res.Result._errorValue.Message);
        }

        [TestMethod]
        public void DeleteOk()
        {
            Repo.Setup(x => x.Delete(It.IsAny<string>())).ReturnsAsync(Entity, new TimeSpan(100));

            var res = Service.Delete("uwu@gmail.com");
            res.Wait();

            Assert.IsTrue(res.Result._isSuccess);
            Assert.IsNotNull(res.Result._successValue);
            Assert.IsNull(res.Result._errorValue);
            Assert.AreEqual(DTO.Name, res.Result._successValue.Name);
        }

        [TestMethod]
        public void DeleteError()
        {
            Repo.Setup(x => x.Delete(It.IsAny<string>())).ReturnsAsync(null, new TimeSpan(100));

            var res = Service.Delete("uwu@gmail.com");
            res.Wait();

            Assert.IsFalse(res.Result._isSuccess);
            Assert.IsNull(res.Result._successValue);
            Assert.IsNotNull(res.Result._errorValue);
            Assert.AreEqual(
                new VetErrorNotFound($"Vet with email uwu@gmail.com not found.").Message,
                res.Result._errorValue.Message);
        }
    }
}
