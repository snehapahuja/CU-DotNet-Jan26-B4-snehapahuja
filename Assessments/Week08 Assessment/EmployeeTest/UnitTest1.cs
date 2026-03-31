using EmployeeBonusLibrary;
namespace EmployeeTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var employee = new EmployeeBonus
            {
                BaseSalary = 500000,
                PerformanceRating = 5,
                YearsOfExperience = 6,
                DepartmentMultiplier = 1.1m,
                AttendancePercentage = 95
            };
            Assert.AreEqual(123200.00, employee.NetAnnualBonus);
            //Assert.Pass();
        }
        [Test]
        public void Test2()
        {
            var employee = new EmployeeBonus
            {
                BaseSalary = 1000000,
                PerformanceRating = 5,
                YearsOfExperience = 15,
                DepartmentMultiplier = 1.5m,
                AttendancePercentage = 95
            };

            Assert.AreEqual(280000.00m, employee.NetAnnualBonus);
        }
        [Test]
        public void Test3()
        {
            var employee = new EmployeeBonus
            {
                BaseSalary = 1000000,
                PerformanceRating = 5,
                YearsOfExperience = 15,
                DepartmentMultiplier = 1.5m,
                AttendancePercentage = 95
            };

            Assert.AreEqual(280000.00m, employee.NetAnnualBonus);
        }
        [Test]
        public void Test4()
        {
            var employee = new EmployeeBonus
            {
                BaseSalary = 0,
                PerformanceRating = 5,
                YearsOfExperience = 10,
                DepartmentMultiplier = 1.2m,
                AttendancePercentage = 100
            };

            Assert.AreEqual(0.00m, employee.NetAnnualBonus);
        }

        [Test]
        public void Test5()
        {
            var employee = new EmployeeBonus
            {
                BaseSalary = 300000,
                PerformanceRating = 2,
                YearsOfExperience = 3,
                DepartmentMultiplier = 1.0m,
                AttendancePercentage = 90
            };

            Assert.AreEqual(13500.00m, employee.NetAnnualBonus);
        }
        [Test]
        public void Test6()
        {
            var employee = new EmployeeBonus
            {
                BaseSalary = 600000,
                PerformanceRating = 3,
                YearsOfExperience = 0,
                DepartmentMultiplier = 1.0m,
                AttendancePercentage = 100
            };

            Assert.AreEqual(64800.00m, employee.NetAnnualBonus);
        }

       
        [Test]
        public void Test7()
        {
            var employee = new EmployeeBonus
            {
                BaseSalary = 900000,
                PerformanceRating = 5,
                YearsOfExperience = 11,
                DepartmentMultiplier = 1.2m,
                AttendancePercentage = 100
            };

            Assert.AreEqual(226800.00m, employee.NetAnnualBonus);
        }

        [Test]
        public void Test8()
        {
            var employee = new EmployeeBonus
            {
                BaseSalary = 555555,
                PerformanceRating = 4,
                YearsOfExperience = 6,
                DepartmentMultiplier = 1.13m,
                AttendancePercentage = 92
            };

            Assert.AreEqual(118649.88m, employee.NetAnnualBonus);
        }

        }
}