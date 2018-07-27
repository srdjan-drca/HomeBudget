using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HomeBudget.DataAccess.Core;
using HomeBudget.DataAccess.Enums;
using HomeBudget.DataAccess.Models;
using HomeBudget.DataAccess.Repositories.Implementation;
using HomeBudget.DataAccess.Repositories.Interfaces;
using Moq;
using Xunit;

namespace DataAccess.Tests.Repositories {

   public class CriteriaValueRepositoryShould {

      [Fact]
      public void ReturnCriteriaValues_IfCriteriaExists() {
         var sqlServerDatabaseMock = new Mock<ISqlServerDatabase>();
         var criteriaRepositoryMock = new Mock<ICriteriaRepository>();

         sqlServerDatabaseMock.Setup(sqlServer =>
            sqlServer.GetCustomDataRowCollection(It.IsAny<SqlCommand>(), It.IsAny<Action>()))
               .Returns(new List<CustomDataRow> {
                  new CustomDataRow(new Dictionary<string, int> {
                     { "IdCriteriaValue", 0 }, { "CodeCriteriaValue", 1 }, { "IsActive", 2 },
                     { "SequenceOrder", 3 }, { "RefCriteria", 4 }, { "RefLabelCriteriaValueName", 5 },
                     { "DateTimeCreatedOn", 6 }
                  },
                  new object[] { 1, "SOME_CODE", 1, 10, 1, 1, DateTime.Now })
               });

         var criteriaValueRepository = new CriteriaValueRepository(sqlServerDatabaseMock.Object, criteriaRepositoryMock.Object);

         List<CriteriaValueDbModel> criteriaValues = criteriaValueRepository.GetAllValues(CriteriaCode.Language.ToString());

         Assert.Equal(criteriaValues.Count, 1);
      }
   }
}