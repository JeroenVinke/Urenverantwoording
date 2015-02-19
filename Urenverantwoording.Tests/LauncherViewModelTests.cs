using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Urenverantwoording.DataLayer;
using Urenverantwoording.Interfaces;
using Urenverantwoording.Models;
using Urenverantwoording.ViewModels;

namespace Urenverantwoording.Tests
{
    [TestClass]
    public class LauncherViewModelTests
    {
        private Mock<IEventAggregator> eventAggregator;
        private Mock<IDatastore> dataStore;
        private Mock<IFileHelper> fileHelper;
        private Mock<IRecentFilesHelper> recentFilesHelper;

        public LauncherViewModelTests()
        {
            eventAggregator = new Mock<IEventAggregator>();
            dataStore = new Mock<IDatastore>();
            fileHelper = new Mock<IFileHelper>();

            recentFilesHelper = new Mock<IRecentFilesHelper>();
            recentFilesHelper.Setup(i => i.GetFiles()).Returns(new List<File>());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullArgumentFileHelper_ThrowsException()
        {
            var vm = new LauncherViewModel(null, null,null,null);
        }



        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullArgumentRecentFilesHelper_ThrowsException()
        {
            var vm = new LauncherViewModel(eventAggregator.Object,dataStore.Object, fileHelper.Object, null);
        }



        [TestMethod]
        public void Constructor_RecentFilesViewModelIsSet()
        {
            var recentFilesVM = new RecentFilesViewModel(recentFilesHelper.Object);

            var vm = new LauncherViewModel(eventAggregator.Object, dataStore.Object, fileHelper.Object, recentFilesVM);

            Assert.IsNotNull(vm.RecentFilesViewModel);
        }

        [TestMethod]
        public void Constructor_CanOpen()
        {
            var recentFilesVM = new RecentFilesViewModel(recentFilesHelper.Object);
            recentFilesVM.SelectedFile = new File("");

            var vm = new LauncherViewModel(eventAggregator.Object, dataStore.Object, fileHelper.Object, recentFilesVM);

            Assert.IsTrue(vm.CanOpen);




            recentFilesVM.SelectedFile = null;
            Assert.IsFalse(vm.CanOpen);
        }




        [TestMethod]
        public void Constructor_ImportFile()
        {
            fileHelper.Setup(i => i.OpenFile()).Returns("");
            var recentFilesVM = new RecentFilesViewModel(recentFilesHelper.Object);
            var vm = new LauncherViewModel(eventAggregator.Object, dataStore.Object, fileHelper.Object, recentFilesVM);


            vm.ImportFile();

            // nothing was added because selected file path was null or empty
            Assert.IsTrue(recentFilesVM.Files.Count == 0);


            fileHelper.Setup(i => i.OpenFile()).Returns("notEmpty");
            recentFilesHelper.Setup(i => i.GetFiles()).Returns(new List<File> { new File("notEmpty") });
            vm.ImportFile();
            Assert.IsTrue(recentFilesVM.Files.Count > 0);
        }




        [TestMethod]
        public void Constructor_CreateFile()
        {
            fileHelper.Setup(i => i.CreateFile()).Returns("");
            var recentFilesVM = new RecentFilesViewModel(recentFilesHelper.Object);
            var vm = new LauncherViewModel(eventAggregator.Object, dataStore.Object, fileHelper.Object, recentFilesVM);


            vm.CreateFile();

            // nothing was added because selected file path was null or empty
            Assert.IsTrue(recentFilesVM.Files.Count == 0);


            fileHelper.Setup(i => i.CreateFile()).Returns("notEmpty");
            recentFilesHelper.Setup(i => i.GetFiles()).Returns(new List<File> { new File("notEmpty") });
            vm.CreateFile();
            Assert.IsTrue(recentFilesVM.Files.Count > 0);
        }




        [TestMethod]
        public void Constructor_FileHelperIsSet()
        {
            var recentFilesVM = new RecentFilesViewModel(recentFilesHelper.Object);

            var vm = new LauncherViewModel(eventAggregator.Object, dataStore.Object, fileHelper.Object, recentFilesVM);

            Assert.IsNotNull(vm.FileHelper);
        }
    }
}
