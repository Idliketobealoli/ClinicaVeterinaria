using ClinicaVeterinaria.API.Api.dto;
using ClinicaVeterinaria.API.Api.errors;
using ClinicaVeterinaria.API.Api.model;
using ClinicaVeterinaria.API.Api.repositories;
using ClinicaVeterinaria.API.Api.services;
using Moq;

namespace ClinicaVeterinaria.TEST.Api.services
{
    [TestClass]
    public class UserServiceTest
    {
        private Mock<UserRepository> Repo;
        private UserService Service;
        private List<User> List;
        private List<UserDTO> ListDTO;
        private User Entity;
        private UserDTO DTO;
        private UserDTOshort DTOShort;
        private UserDTOandToken DTOandToken;
        private UserDTOregister DTOregister;
        private UserDTOloginOrChangePassword DTOupdate;
        private UserDTOloginOrChangePassword DTOlogin;

        [TestInitialize]
        public void Init()
        {
            Repo = new Mock<UserRepository>();
            Service = new(Repo.Object);
            Entity = new(
                "test", "testeado", "uwu@gmail.com",
                "123456789", "uwu1234");
            DTO = new(
                "test", "testeado", "uwu@gmail.com", "123456789");
            DTOShort = new("test", "testeado");
            DTOandToken = new(DTO, "token");
            List = new List<User>() { Entity };
            ListDTO = new List<UserDTO>() { DTO };
            DTOregister = new(
                "test", "testeado2", "email2@gmail.com", "987654321", "uwu1234", "uwu1234");
            DTOupdate = new("uwu@gmail.com", "1234uwu");
            DTOlogin = new("uwu@gmail.com", "uwu1234");
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
                (new UserErrorNotFound($"User with Email uwu@gmail.com not found.").Message,
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
                new UserErrorNotFound($"User with Email uwu@gmail.com not found.").Message,
                res.Result._errorValue.Message);
        }

        [TestMethod]
        public void RegisterOk()
        {
            Repo.Setup(x => x.FindByEmail(It.IsAny<string>())).ReturnsAsync(null, new TimeSpan(100));
            Repo.Setup(x => x.FindByPhone(It.IsAny<string>())).ReturnsAsync(null, new TimeSpan(100));
            Repo.Setup(x => x.Create(It.IsAny<User>())).ReturnsAsync(Entity, new TimeSpan(100));

            var res = Service.Register(DTOregister);
            res.Wait();

            Assert.IsTrue(res.Result._isSuccess);
            Assert.IsNotNull(res.Result._successValue);
            Assert.IsNull(res.Result._errorValue);
            Assert.AreEqual(DTOandToken.token, res.Result._successValue.token);
        }

        [TestMethod]
        public void RegisterError()
        {
            Repo.Setup(x => x.FindByEmail(It.IsAny<string>())).ReturnsAsync(Entity, new TimeSpan(100));
            Repo.Setup(x => x.FindByPhone(It.IsAny<string>())).ReturnsAsync(Entity, new TimeSpan(100));

            var res = Service.Register(DTOregister);
            res.Wait();

            Assert.IsFalse(res.Result._isSuccess);
            Assert.IsNull(res.Result._successValue);
            Assert.IsNotNull(res.Result._errorValue);
            Assert.AreEqual(
                new UserErrorUnauthorized("Cannot use either that email or that phone number.").Message,
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
            Assert.AreEqual(DTOandToken.token, res.Result._successValue.token);
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
                new UserErrorUnauthorized("Incorrect email or password.").Message,
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
                new UserErrorNotFound($"User with email {DTOupdate.Email} not found.").Message,
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
                new UserErrorNotFound($"User with email uwu@gmail.com not found.").Message,
                res.Result._errorValue.Message);
        }
    }
}