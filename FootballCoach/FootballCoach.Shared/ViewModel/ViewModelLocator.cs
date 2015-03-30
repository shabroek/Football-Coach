using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace FootballCoach.ViewModel
{
    public class ViewModelLocator
    {
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public PlayerViewModel Player
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlayerViewModel>();
            }
        }

        public MatchViewModel Match
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MatchViewModel>();
            }
        }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<IApplicationState, ApplicationState>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MatchViewModel>();
            SimpleIoc.Default.Register<PlayerViewModel>();
        }
    }
}