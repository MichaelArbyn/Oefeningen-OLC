using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GradesHelper.Tests.NSubstitute
{
    [TestFixture]
    public class GradesHelperTests
    {
        [Test]
        public void CalcAverageGrade_SomeNumbersEntered_ReturnsAverage()
        {
            //Arrange
            IGradeRepository stub = Substitute.For<IGradeRepository>();
            GradesHelper gradesHelper = new GradesHelper(stub);

            List<int> grades = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> grades2 = new List<int>() { 8, 8, 8, 8 };
            stub.GetGrades(default).ReturnsForAnyArgs(grades, grades2);

            //Act
            double average = gradesHelper.CalcAverageGrade(null);
            double average2 = gradesHelper.CalcAverageGrade(null);
            double average3 = gradesHelper.CalcAverageGrade(null);

            //Assert
            Assert.AreEqual(average, 5);
            Assert.AreEqual(average2, 8);
            Assert.AreEqual(average3, 8);
        }

        [Test]
        public void DidStudentPerformBetterWithNewScore_WithBadScore_ReturnsFalse()
        {
            //Arrange
            IGradeRepository fake = Substitute.For<IGradeRepository>();
            GradesHelper sut = new GradesHelper(fake);
            Student s = null;
            int score = 0;
            List<int> grades = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            fake.When(x => x.AddScore(s, score)).Do(x => grades.Add(x.ArgAt<int>(1)));
            fake.GetGrades(s).Returns(grades);

            //Act
            bool result = sut.DidStudentPerformBetterWithNewScore(s, score);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void DidStudentPerformBetterWithNewScore_WithBadScore_ReturnsTrue()
        {
            //Arrange
            IGradeRepository fake = Substitute.For<IGradeRepository>();
            GradesHelper sut = new GradesHelper(fake);
            Student s = null;
            int score = 10;

            List<int> grades = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            fake.When(x => x.AddScore(s, score)).Do(x => grades.Add(x.ArgAt<int>(1)));
            fake.GetGrades(s).Returns(grades);

            //Act
            bool result = sut.DidStudentPerformBetterWithNewScore(s, score);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void RemoveAllScores_ClearScoreIsCalled()
        {
            //Arrange
            IGradeRepository mock = Substitute.For<IGradeRepository>();
            GradesHelper sut = new GradesHelper(mock);
            Student s = null;
            mock.When(x => x.ClearScore(s)).Do(x => Assert.Pass());

            //Act
            sut.RemoveAllScores(s);

            //Assert
            Assert.Fail();
        }

        [Test]
        public void AddScore_WithValidData_AddScoreIsCalled()
        {
            //Arrange
            IGradeRepository spy = Substitute.For<IGradeRepository>();
            GradesHelper sut = new GradesHelper(spy);
            Student s = null;
            int score = 5;

            //Act
            sut.AddScore(s, score);

            //Assert
            spy.Received(1).AddScore(Arg.Any<Student>(), score);
        }

        [Test]
        public void AddScore_WithInvalidData_AddScoreNotCalled()
        {
            //Arrange
            IGradeRepository spy = Substitute.For<IGradeRepository>();
            GradesHelper sut = new GradesHelper(spy);
            Student s = null;
            int score = -5;

            //Act
            Assert.Throws<Exception>(() =>
                { sut.AddScore(s, score); }
            );

            //Assert
            spy.DidNotReceive().AddScore(s, score);

            var substitute = Substitute.For<IGradeRepository, IList>();

        }
    }
}
