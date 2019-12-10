using FriendStorage.DataAccess;
using FriendStorage.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace FriendStorage.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        public ObservableCollection<Friend> Friends { get; private set; }
        public NavigationViewModel() => Friends = new ObservableCollection<Friend>();
        public void Load() => new FileDataService().GetAllFriends().ToList().ForEach(x => Friends.Add(x));
    }
}
