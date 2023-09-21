using ContactManager.Controllers;
using ContactManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;

namespace ContactManager.Tests.Controllers
{
    [TestClass]
    public class ContactControllerTest
    {
        private Mock<IContactManagerService> _mockContactManagerService;
        [TestInitialize]
        public void Initialize()
        {
            _mockContactManagerService = new Mock<IContactManagerService>();
        }
        [TestMethod]
        public void CreateValidContact()
        {
            // Arrange
            var contact = new Contact();
            _mockContactManagerService.Setup(s => s.CreateContact(contact)).Returns(true);
            var controller = new ContactController(_mockContactManagerService.Object);

            // Act
            var result = (RedirectToRouteResult)controller.Create(contact);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateInvalidContact()
        {
            // Arrange
            var contact = new Contact();
            _mockContactManagerService.Setup(s => s.CreateContact(contact)).Returns(false);
            var controller = new ContactController(_mockContactManagerService.Object);

            // Act
            var result = (ViewResult)controller.Create(contact);

            // Assert
            Assert.AreEqual(0, result.ViewData.Count);
        }
        [TestMethod]
        public void EditValidContact()
        {
            // Arrange
            var contact = new Contact();
            _mockContactManagerService.Setup(s => s.EditContact(contact)).Returns(true);
          
            var IdToEdit = contact.Id;
         
         
            var controller = new ContactController(_mockContactManagerService.Object);

            // Act
            var result = (RedirectToRouteResult)controller.Edit(IdToEdit, contact);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        [TestMethod]
        public void EditInValidContact()
        {
            // Arrange
            var contact = new Contact();
            
            _mockContactManagerService.Setup(s => s.EditContact(contact)).Returns(false);

            var IdToEdit = contact.Id;


            var controller = new ContactController(_mockContactManagerService.Object);

            // Act
            var result = (ViewResult)controller.Edit(IdToEdit,contact);

            // Assert
            Assert.AreEqual(0, result.ViewData.Count);
        }
        [TestMethod]
        public void DeleteContact()
        {
            // Arrange
            var contact = new Contact();
           
            _mockContactManagerService.Setup(s => s.DeleteContact(contact)).Returns(true);

            var IdToBeDeleted = contact.Id;


            var controller = new ContactController(_mockContactManagerService.Object);

            // Act
            var result = (RedirectToRouteResult)controller.Delete(contact);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

    }
}
