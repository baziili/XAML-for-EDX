﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using RestaurantManager.Models;

namespace RestaurantManager.ViewModels
{
    public class OrderViewModel : ViewModel
    {
        private List<MenuItem> _menuItems;
        private MenuItem _selectedMenuItem;

        public OrderViewModel()
        {
            //_messageService = messageService;
            AddMenuItemCommand = new DelegateCommand(AddMenuItem);
            SubmitOrderCommand = new DelegateCommand(SubmitOrder);
        }

        protected override void OnDataLoaded()
        {
            this.MenuItems = base.Repository.StandardMenuItems;

            this.CurrentlySelectedMenuItems = new ObservableCollection<MenuItem> {};
            //{
            //    this.MenuItems[3],
            //    this.MenuItems[5]
            //};
        }

        public ICommand AddMenuItemCommand { get; private set; }
        public ICommand SubmitOrderCommand { get; private set; }
        private readonly IMessageService _messageService;

        private void AddMenuItem(object parameter)
        {
            if (SelectedMenuItem != null)
            {
                CurrentlySelectedMenuItems.Add(SelectedMenuItem);
            }
        }

        private void SubmitOrder(object parameter)
        {
            if (this.CurrentlySelectedMenuItems != null)
            {
                var menuItems = new List<MenuItem>(CurrentlySelectedMenuItems);
                var order = new Order
                {
                    Complete = false,
                    Expedite = true,
                    SpecialRequests = "",
                    Table = base.Repository.Tables.Last(),
                    Items = menuItems
                };
                base.Repository.Orders.Add(order);
                //_messageService.ShowDialog("The order has been submitted");
            }
        }

        public List<MenuItem> MenuItems
        {
            get { return _menuItems; }
            set
            {
                _menuItems = value;
                NotifyPropertyChanged();
            }
        }

        public MenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set { _selectedMenuItem = value; }
        }

        public ObservableCollection<MenuItem> CurrentlySelectedMenuItems
        { get; set; }

    }

}
