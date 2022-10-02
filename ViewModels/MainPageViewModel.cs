using System;
using System.ComponentModel;
using MauiCRUDSqlCE.Models;
using MauiCRUDSqlCE.Services;

namespace MauiCRUDSqlCE.ViewModels
{
    public class MainPageViewModel //: INotifyPropertyChanged
    {
        readonly IDataService _dataService;

        private List<ApplicationUser> usersCollection;

        public List<ApplicationUser> UsersCollection
        {
            get { return usersCollection; }
            set { usersCollection = value; }
        }


        public MainPageViewModel(IDataService dataService)
        {
            _dataService = dataService;
            usersCollection = new List<ApplicationUser>(_dataService.getAllUsers());
        }
    }
}

