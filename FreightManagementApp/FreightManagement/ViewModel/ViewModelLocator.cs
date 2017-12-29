using FreightManagement.Domain.Model;
using FreightManagement.ORM;
using FreightManagement.Service.Concrete;
using FreightManagement.Service.Interface;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System.Data.Entity;

namespace FreightManagement.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<DbContext, FreightManagementContext>();
            SimpleIoc.Default.Register<IService<Customer>, CustomerService>();
            SimpleIoc.Default.Register<IService<Truck>, TruckService>();
            SimpleIoc.Default.Register<IService<Order>, OrderService>();
            SimpleIoc.Default.Register<IService<Cargo>, CargoService>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<TruckDialogViewModel>();
            SimpleIoc.Default.Register<CustomerDialogViewModel>();
            SimpleIoc.Default.Register<OrderDialogViewModel>();
            SimpleIoc.Default.Register<CargoDialogViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public TruckDialogViewModel TruckDialog
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TruckDialogViewModel>();
            }
        }

        public CustomerDialogViewModel CustomerDialog
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CustomerDialogViewModel>();
            }
        }

        public OrderDialogViewModel OrderDialog
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OrderDialogViewModel>(System.Guid.NewGuid().ToString());
            }
        }

        public CargoDialogViewModel CargoDialog
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CargoDialogViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}