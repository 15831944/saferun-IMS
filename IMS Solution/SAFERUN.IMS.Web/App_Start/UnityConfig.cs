using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Data.Entity;
using SAFERUN.IMS.Web.Models;
using Repository.Pattern.UnitOfWork;
using Repository.Pattern.Ef6;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repositories;
using SAFERUN.IMS.Web.Services;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SAFERUN.IMS.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<ApplicationDbContext>(new HierarchicalLifetimeManager());

            container.RegisterType<IRoleStore<ApplicationRole, string>, RoleStore<ApplicationRole>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));
            ////container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager());
            container.RegisterType<IDataContextAsync, ImsDbContext>(new PerRequestLifetimeManager());




            container.RegisterType<IRepositoryAsync<Message>, Repository<Message>>();
            container.RegisterType<IMessageService, MessageService>();
            container.RegisterType<IRepositoryAsync<BaseCode>, Repository<BaseCode>>();
            container.RegisterType<IBaseCodeService, BaseCodeService>();
            container.RegisterType<IRepositoryAsync<CodeItem>, Repository<CodeItem>>();



            container.RegisterType<IRepositoryAsync<MenuItem>, Repository<MenuItem>>();
            container.RegisterType<IMenuItemService, MenuItemService>();
            container.RegisterType<IRepositoryAsync<MenuItem>, Repository<MenuItem>>();

            container.RegisterType<IRepositoryAsync<RoleMenu>, Repository<RoleMenu>>();
            container.RegisterType<IRoleMenuService, RoleMenuService>();
            container.RegisterType<IRepositoryAsync<RoleMenu>, Repository<RoleMenu>>();

            container.RegisterType<IRepositoryAsync<ProcessNode>, Repository<ProcessNode>>();
            container.RegisterType<IProcessNodeService, ProcessNodeService>();
            container.RegisterType<IRepositoryAsync<Customer>, Repository<Customer>>();
            container.RegisterType<ICustomerService, CustomerService>();

            container.RegisterType<IRepositoryAsync<Multicomponent>, Repository<Multicomponent>>();
            container.RegisterType<IMulticomponentService, MulticomponentService>();
            container.RegisterType<IRepositoryAsync<ProductionProcess>, Repository<ProductionProcess>>();
            container.RegisterType<IProductionProcessService, ProductionProcessService>();
            container.RegisterType<IRepositoryAsync<ProcessStep>, Repository<ProcessStep>>();
            container.RegisterType<IProcessStepService, ProcessStepService>();
            container.RegisterType<IRepositoryAsync<Station>, Repository<Station>>();
            container.RegisterType<IStationService, StationService>();

            container.RegisterType<IRepositoryAsync<Employee>, Repository<Employee>>();
            container.RegisterType<IEmployeeService, EmployeeService>();

            container.RegisterType<IRepositoryAsync<Department>, Repository<Department>>();
          container.RegisterType<IDepartmentService, DepartmentService>();

            container.RegisterType<IRepositoryAsync<DefectCode>, Repository<DefectCode>>();
            container.RegisterType<IDefectCodeService, DefectCodeService>();

            container.RegisterType<IRepositoryAsync<DefectType>, Repository<DefectType>>();
            container.RegisterType<IDefectTypeService, DefectTypeService>();


            container.RegisterType<IRepositoryAsync<ProjectType>, Repository<ProjectType>>();
            container.RegisterType<IProjectTypeService, ProjectTypeService>();

            container.RegisterType<IRepositoryAsync<ProjectNode>, Repository<ProjectNode>>();
            container.RegisterType<IProjectNodeService, ProjectNodeService>();

            container.RegisterType<IRepositoryAsync<SKU>, Repository<SKU>>();
        container.RegisterType<ISKUService, SKUService>();

        container.RegisterType<IRepositoryAsync<BOMComponent>, Repository<BOMComponent>>();
        container.RegisterType<IBOMComponentService, BOMComponentService>();

        container.RegisterType<IRepositoryAsync<OrderDetail>, Repository<OrderDetail>>();
        container.RegisterType<IOrderDetailService, OrderDetailService>();

        container.RegisterType<IRepositoryAsync<Order>, Repository<Order>>();
        container.RegisterType<IOrderService, OrderService>();

        container.RegisterType<IRepositoryAsync<OrderAuditPlan>, Repository<OrderAuditPlan>>();
        container.RegisterType<IOrderAuditPlanService, OrderAuditPlanService>();

        container.RegisterType<IRepositoryAsync<ProductionPlan>, Repository<ProductionPlan>>();
        container.RegisterType<IProductionPlanService, ProductionPlanService>();
        container.RegisterType<IRepositoryAsync<PurchasePlan>, Repository<PurchasePlan>>();
        container.RegisterType<IPurchasePlanService, PurchasePlanService>();



        container.RegisterType<IRepositoryAsync<DataTableImportMapping>, Repository<DataTableImportMapping>>();
        container.RegisterType<IDataTableImportMappingService, DataTableImportMappingService>();


        container.RegisterType<IRepositoryAsync<DefectTracking>, Repository<DefectTracking>>();
        container.RegisterType<IDefectTrackingService, DefectTrackingService>();



        container.RegisterType<IRepositoryAsync<ProductionTask>, Repository<ProductionTask>>();
        container.RegisterType<IProductionTaskService, ProductionTaskService>();

        container.RegisterType<IRepositoryAsync<RepairJob>, Repository<RepairJob>>();
        container.RegisterType<IRepairJobService, RepairJobService>();

         container.RegisterType<IRepositoryAsync<Supplier>, Repository<Supplier>>();
        container.RegisterType<ISupplierService, SupplierService>();

        container.RegisterType<IRepositoryAsync<AssemblyPlan>, Repository<AssemblyPlan>>();
        container.RegisterType<IAssemblyPlanService, AssemblyPlanService>();
        }
    }
}
