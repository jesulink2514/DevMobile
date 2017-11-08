﻿using Autofac;
using eShopOnContainers.Services;
using System;
using System.Globalization;
using System.Reflection;
using eShopOnContainers.Core.Services.Catalog;
using eShopOnContainers.Core.Services.OpenUrl;
using eShopOnContainers.Core.Services.Basket;
using eShopOnContainers.Core.Services.Order;
using eShopOnContainers.Core.Services.User;
using Xamarin.Forms;
using eShopOnContainers.Core.Services.Location;
using eShopOnContainers.Core.Services.Marketing;

namespace eShopOnContainers.Core.ViewModels.Base
{
    public static class ViewModelLocator
    {
        private static IContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }

        public static bool UseMockService { get; set; }

        public static void RegisterDependencies(bool useMockServices)
        {
            var builder = new ContainerBuilder();

            // View models
            builder.RegisterType<BasketViewModel>();
            builder.RegisterType<CatalogViewModel>();
            builder.RegisterType<CheckoutViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<OrderDetailViewModel>();
            builder.RegisterType<ProfileViewModel>();
            builder.RegisterType<SettingsViewModel>();
            builder.RegisterType<CampaignViewModel>();
            builder.RegisterType<CampaignDetailsViewModel>();

            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<OpenUrlService>().As<IOpenUrlService>();
            builder.RegisterType<LocationService>().As<ILocationService>().SingleInstance();

            // Services
            builder.RegisterInstance(new CatalogMockService()).As<ICatalogService>();
            builder.RegisterInstance(new BasketMockService()).As<IBasketService>();
            builder.RegisterInstance(new OrderMockService()).As<IOrderService>();
            builder.RegisterInstance(new UserMockService()).As<IUserService>();
            builder.RegisterInstance(new CampaignMockService()).As<ICampaignService>();

            UseMockService = true;


            if (_container != null)
            {
                _container.Dispose();
            }
            _container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null)
            {
                return;
            }

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Views.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null)
            {
                return;
            }
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}