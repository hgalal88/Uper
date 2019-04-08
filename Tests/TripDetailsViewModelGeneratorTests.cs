﻿using Xunit;
using Moq;
using WebApp.Models;
using WebApp.Data.Repositories;
using WebApp.Data;
using WebApp.Models.Factories;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class TripDetailsViewModelGeneratorTests
    {
        private TripDetailsViewModelGenerator viewModelGenerator;
        private Mock<ITripDetailsCreator> creatorMock;
        private Mock<ITripDetailsRepository> repoMock;
        private Mock<ITripDetailsViewModelCreatorFactory> facMock;
        private readonly TripDetails testModel;

        public TripDetailsViewModelGeneratorTests()
        {
            creatorMock = new Mock<ITripDetailsCreator>();
            repoMock = new Mock<ITripDetailsRepository>();
            facMock = new Mock<ITripDetailsViewModelCreatorFactory>();
            
            repoMock.Setup(e => e.GetById(4)).Returns(() => throw new IndexOutOfRangeException());
            repoMock.Setup(e => e.GetById(-1)).Returns(() => throw new IndexOutOfRangeException());

            facMock.Setup(e => e.CreateCreator(It.IsAny<ViewerType>())).Returns(creatorMock.Object);

            testModel = new TripDetails();

            repoMock.Setup(e => e.GetById(1)).Returns(testModel);

            viewModelGenerator = new TripDetailsViewModelGenerator(repoMock.Object, facMock.Object);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetDataModelWithGivemIdFromRepository(int type)
        {
            viewModelGenerator.GetViewModel(1, (ViewerType)type);

            repoMock.Verify(rep => rep.GetById(1),Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetViewModelWithGivemIdAndViewerTypeFromCreator(int type)
        {
            viewModelGenerator.GetViewModel(1, (ViewerType)type);

            creatorMock.Verify(rep => rep.CreateViewModel(testModel), Times.Once);
        }

        

        [Theory]
        [InlineData(-1,0)]
        [InlineData(-1,1)]
        [InlineData(-1,2)]
        [InlineData(4,0)]
        [InlineData(4,1)]
        [InlineData(4,2)]
        public void ThrowIndexOutOfRangeExceptionWhenIdIsInvalid(int id,int type)
        {
            Assert.Throws<IndexOutOfRangeException>(() => viewModelGenerator.GetViewModel(id, (ViewerType)type));
        }
    }
}