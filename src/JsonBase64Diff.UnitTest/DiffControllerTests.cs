using JsonBase64Diff.Api.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace JsonBase64Diff.UnitTest
{
    [TestClass]
    public class DiffControllerTests
    {
        private DiffController _controller;

        [TestMethod]
        public void VerifyInsertLeft()
        {
        //    var employeeRepository = new Mock<IJsonBase64Repository>();
        //    employeeRepository.Setup(x => x.FindAll()).Returns(new List<JsonBase64>()
        //{
        //    new Employee() { Id = 1, Name = "Employee 1" },
        //    new Employee() { Id = 2, Name = "Employee 2" },
        //    new Employee() { Id = 3, Name = "Employee 3" }
        //});
        //    var diffController = new DiffController(employeeRepository.Object);
        //    var indexResult = employeeController.Index() as ViewResult;
        //    Assert.IsNotNull(indexResult);
        //    var employees = indexResult.ViewData.Model as List<Employee>;
        //    Assert.AreEqual(3, employees.Count);
        //    Assert.AreEqual(1, employees[0].Id);
        //    Assert.AreEqual("Employee 3", employees[2].Name);
        }
    }
}
