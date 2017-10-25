﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TravelAlbum.Data.Contracts;
using TravelAlbum.DataServices;
using TravelAlbum.Models;

namespace TravelAlbum.UnitTests.TravelServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnsAnInstance_WhenBothParametersAreNotNull()
        {
            // Arrange
            var wrapperMock = new Mock<IEfDbSetWrapper<Travel>>();
            var dbContextMock = new Mock<ITravelAlbumEfDbContextSaveChanges>();

            // Act
            TravelService travelService = new TravelService(wrapperMock.Object, dbContextMock.Object);

            // Assert
            Assert.IsNotNull(travelService);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowException_WhenBookSetWrapperIsNull()
        {
            // Arrange
            var dbContextMock = new Mock<ITravelAlbumEfDbContextSaveChanges>();

            // Act & Assert
           TravelService travelService = new TravelService(null, dbContextMock.Object);
        }

       [TestMethod]
       [ExpectedException(typeof(ArgumentNullException))]
       public void ThrowException_WhenDbContextIsNull()
       {
           // Arrange
           var wrapperMock = new Mock<IEfDbSetWrapper<Travel>>();

            // Act & Assert
            TravelService travelService = new TravelService(wrapperMock.Object, null);
        }
    }
}