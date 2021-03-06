﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TravelAlbum.DataServices.Contracts;
using TravelAlbum.Web.Controllers;
using TravelAlbum.Web.Utils;

namespace TravelAlbum.UnitTests.Controllers.ImagesControllerTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ReturnsAnInstance_WhenParametersAreNotNull()
        {
            // Arrange
            var imageServiceMock = new Mock<IImageService>();
            var mountainsServiceMock = new Mock<IMountainsService>();
            var imageTranslationalInfoServiceMock = new Mock<IImageTranslationalInfoService>();
            var travelServiceMock = new Mock<ITravelService>();
            var utilsMock = new Mock<IUtils>();

            // Act
            ImagesController imageController = new ImagesController(
                imageServiceMock.Object, 
                mountainsServiceMock.Object,
                imageTranslationalInfoServiceMock.Object, 
                travelServiceMock.Object,
                utilsMock.Object);

            // Assert
            Assert.IsNotNull(imageController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenParameterImageServiceIsNull()
        {
            // Arrange
            var imageServiceMock = new Mock<IImageService>();
            var mountainsServiceMock = new Mock<IMountainsService>();
            var travelServiceMock = new Mock<ITravelService>();
            var utilsMock = new Mock<IUtils>();

            // Act
            ImagesController imageController = new ImagesController(
                imageServiceMock.Object,
                mountainsServiceMock.Object,
                null, 
                travelServiceMock.Object,
                utilsMock.Object);

            // Assert
            Assert.IsNotNull(imageController);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsException_WhenParameterImageTranslationalInfoIsNull()
        {
            var imageTranslationalInfoServiceMock = new Mock<IImageTranslationalInfoService>();
            var mountainsServiceMock = new Mock<IMountainsService>();
            var travelServiceMock = new Mock<ITravelService>();
            var utilsMock = new Mock<IUtils>();

            // Act
            ImagesController imageController = new ImagesController(
                null, 
                mountainsServiceMock.Object,
                imageTranslationalInfoServiceMock.Object,
                travelServiceMock.Object,
                utilsMock.Object);

            // Assert
            Assert.IsNotNull(imageController);
        }        
    }
}
