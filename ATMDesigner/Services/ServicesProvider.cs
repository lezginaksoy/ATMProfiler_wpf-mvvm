using ATMDesigner.Business;
using ATMDesigner.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMDesigner.UI.Services
{

    public interface IServicesProvider
    {
        ITransactionStateBusiness TransactionStateService { get; }
        ITransactionScreenBusiness TransactionScreenService { get; }
    }

    public class ServicesProvider : IServicesProvider
    {
        private ITransactionStateBusiness transactionstateservice = new TransactionStateBusiness();
        private ITransactionScreenBusiness transactionscreenservice = new TransactionScreenBusiness();

        public ITransactionStateBusiness TransactionStateService
        {
            get { return transactionstateservice; }
        }

        public ITransactionScreenBusiness TransactionScreenService
        {
            get { return transactionscreenservice; }
        }
               
    }
    

    public class ApplicationServicesProvider
    {
        private static Lazy<ApplicationServicesProvider> instance = new Lazy<ApplicationServicesProvider>(() => new ApplicationServicesProvider());
      
        private IServicesProvider serviceProvider = new ServicesProvider();

        private ApplicationServicesProvider()
        {

        }

        static ApplicationServicesProvider()
        {

        }

        public void SetNewServiceProvider(IServiceProvider provider)
        {
            //serviceProvider = provider;
        }

        public IServicesProvider Provider
        {
            get { return serviceProvider; }
        }

        public static ApplicationServicesProvider Instance
        {
            get { return instance.Value; }
        }


    }




}
