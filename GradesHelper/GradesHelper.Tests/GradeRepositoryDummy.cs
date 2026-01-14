using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradesHelper.Tests
{
    class GradeRepositoryDummy : IGradeRepository
    {
        public GradeRepositoryDummy()
        {
        }

        public void AddScore(Student student, int score)
        {
            Assert.Fail("This method should not be called in this test.");
        }

        public void ClearScore(Student student)
        {
            throw new NotImplementedException();
        }

        public List<int> GetGrades(Student student)
        {
            throw new NotImplementedException();
        }

        public int GetTotalScore(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
