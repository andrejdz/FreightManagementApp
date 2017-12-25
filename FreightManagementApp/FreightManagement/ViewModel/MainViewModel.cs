using GalaSoft.MvvmLight;
using FreightManagement.View;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using FreightManagement.Service.Interface;
using FreightManagement.Domain.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Collections.Generic;

namespace FreightManagement.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IService<Customer> customerService,
            IService<Truck> truckService,
            IService<Order> orderService,
            IService<Cargo> cargoService)
        {
            _customerService = customerService;
            _truckService = truckService;
            _orderService = orderService;
            _cargoService = cargoService;

            CustomerList = new ObservableCollection<Customer>(_customerService.GetAll().Result);
            TruckList = new ObservableCollection<Truck>(_truckService.GetAll().Result);
            OrderList = new ObservableCollection<Order>(_orderService.GetAll().Result);
            CargoList = new ObservableCollection<Cargo>(_cargoService.GetAll().Result);
        }

        public ICommand AddCommand => _addCommand ??
            (_addCommand = new RelayCommand<string>((key) => Add(key)));

        public ICommand UpdateCommand => _updateCommand ??
            (_updateCommand = new RelayCommand<string>((key) => Update(key)));

        public ICommand DeleteCommand => _deleteCommand ??
            (_deleteCommand = new RelayCommand<string>((key) => Delete(key)));

        public ObservableCollection<Customer> CustomerList { get; set; }
        public ObservableCollection<Truck> TruckList { get; set; }
        public ObservableCollection<Order> OrderList { get; set; }
        public ObservableCollection<Cargo> CargoList { get; set; }

        public Customer CustomerModel { get; set; }
        public Truck TruckModel { get; set; }
        public Order OrderModel { get; set; }
        public Cargo CargoModel { get; set; }

        private void Add(string key)
        {
            switch(key)
            {
                case "customer":
                    CustomerDialog customerDialog = new CustomerDialog();
                    MessengerInstance.Send(new Customer(), "customerModel");
                    MessengerInstance.Send<Window>(customerDialog, "customerView");
                    customerDialog.ShowDialog();

                    CustomerList.Clear();
                    foreach(var item in _customerService.GetAll().Result)
                    {
                        CustomerList.Add(item);
                    }
                    break;
                case "truck":
                    TruckDialog truckDialog = new TruckDialog();
                    MessengerInstance.Send(new Truck(), "truckModel");
                    MessengerInstance.Send<Window>(truckDialog, "truckView");
                    truckDialog.ShowDialog();

                    TruckList.Clear();
                    foreach(var item in _truckService.GetAll().Result)
                    {
                        TruckList.Add(item);
                    }
                    break;
                case "order":
                    OrderDialog orderDialog = new OrderDialog();
                    MessengerInstance.Send(new Order(), "orderModel");
                    MessengerInstance.Send<Window>(orderDialog, "orderView");
                    orderDialog.ShowDialog();

                    OrderList.Clear();
                    foreach(var item in _orderService.GetAll().Result)
                    {
                        OrderList.Add(item);
                    }

                    TruckList.Clear();
                    foreach(var item in _truckService.GetAll().Result)
                    {
                        TruckList.Add(item);
                    }

                    CargoList.Clear();
                    foreach(var item in _cargoService.GetAll().Result)
                    {
                        CargoList.Add(item);
                    }
                    break;
                case "cargo":
                    CargoDialog cargoDialog = new CargoDialog();
                    MessengerInstance.Send(new Cargo(), "cargoModel");
                    MessengerInstance.Send<Window>(cargoDialog, "cargoView");
                    cargoDialog.ShowDialog();

                    CargoList.Clear();
                    foreach(var item in _cargoService.GetAll().Result)
                    {
                        CargoList.Add(item);
                    }
                    break;
                default:
                    break;
            }
        }

        private void Update(string key)
        {
            switch(key)
            {
                case "customer":
                    CustomerDialog customerDialog = new CustomerDialog();
                    MessengerInstance.Send(CustomerModel, "customerModel");
                    MessengerInstance.Send<Window>(customerDialog, "customerView");
                    customerDialog.ShowDialog();
                    break;
                case "truck":
                    TruckDialog truckDialog = new TruckDialog();
                    MessengerInstance.Send(TruckModel, "truckModel");
                    MessengerInstance.Send<Window>(truckDialog, "truckView");
                    truckDialog.ShowDialog();
                    break;
                case "order":
                    OrderDialog orderDialog = new OrderDialog();
                    MessengerInstance.Send(OrderModel, "orderModel");
                    MessengerInstance.Send<Window>(orderDialog, "orderView");
                    orderDialog.ShowDialog();

                    OrderList.Clear();
                    foreach(var item in _orderService.GetAll().Result)
                    {
                        OrderList.Add(item);
                    }

                    TruckList.Clear();
                    foreach(var item in _truckService.GetAll().Result)
                    {
                        TruckList.Add(item);
                    }

                    CargoList.Clear();
                    foreach(var item in _cargoService.GetAll().Result)
                    {
                        CargoList.Add(item);
                    }
                    break;
                case "cargo":
                    CargoDialog cargoDialog = new CargoDialog();
                    MessengerInstance.Send(CargoModel, "cargoModel");
                    MessengerInstance.Send<Window>(cargoDialog, "cargoView");
                    cargoDialog.ShowDialog();

                    CargoList.Clear();
                    foreach(var item in _cargoService.GetAll().Result)
                    {
                        CargoList.Add(item);
                    }
                    break;
                default:
                    break;
            }
        }

        private void Delete(string key)
        {
            switch(key)
            {
                case "customer":
                    _customerService.Delete(CustomerModel.Id);
                    CustomerList.Remove(CustomerModel);
                    break;
                case "truck":
                    _truckService.Delete(TruckModel.Id);
                    TruckList.Remove(TruckModel);
                    break;
                case "order":
                    Truck freedTruck = _truckService.GetById(OrderModel.TruckId.Value).Result;
                    freedTruck.Status = AvailabilityEnum.Free;
                    _truckService.Update(freedTruck);

                    List<Cargo> listCargoes = new List<Cargo>();
                    listCargoes.AddRange(_orderService.GetById(OrderModel.Id).Result.Cargoes);

                    foreach(var item in listCargoes)
                    {
                        Cargo freedCargo = _cargoService.GetById(item.Id).Result;
                        freedCargo.OrderId = null;
                        freedCargo.Status = StatusCargoEnum.Waiting;
                        _cargoService.Update(freedCargo);
                    }

                    CargoList.Clear();
                    foreach(var item in _cargoService.GetAll().Result)
                    {
                        CargoList.Add(item);
                    }

                    TruckList.Clear();
                    foreach(var item in _truckService.GetAll().Result)
                    {
                        TruckList.Add(item);
                    }

                    _orderService.Delete(OrderModel.Id);
                    OrderList.Remove(OrderModel);
                    break;
                case "cargo":
                    _cargoService.Delete(CargoModel.Id);
                    CargoList.Remove(CargoModel);
                    break;
                default:
                    break;
            }
        }

        private ICommand _addCommand = null;
        private ICommand _deleteCommand = null;
        private ICommand _updateCommand = null;
        private IService<Customer> _customerService = null;
        private IService<Truck> _truckService = null;
        private IService<Order> _orderService = null;
        private IService<Cargo> _cargoService = null;
    }
}