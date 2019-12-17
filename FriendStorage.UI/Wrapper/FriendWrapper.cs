using FriendStorage.Model;
using FriendStorage.UI.ViewModel;
using System;
using System.Runtime.CompilerServices;

namespace FriendStorage.UI.Wrapper
{
    public class FriendWrapper : ViewModelBase
    {
        private bool _isChanged;

        public FriendWrapper(Friend friend) => Model = friend;

        public Friend Model { get; }

        public bool IsChanged
        {
            get { return _isChanged; }
            private set
            {
                _isChanged = value;
                OnPropertyChanged();
            }
        }

        public void AcceptChanges() => IsChanged = false;

        public int Id => Model.Id;

        public string FirstName
        {
            get { return Model.FirstName; }
            set
            {
                Model.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return Model.LastName; }
            set
            {
                Model.LastName = value;
                OnPropertyChanged();
            }
        }

        public DateTime? Birthday
        {
            get { return Model.Birthday; }
            set
            {
                Model.Birthday = value;
                OnPropertyChanged();
            }
        }

        public bool IsDeveloper
        {
            get { return Model.IsDeveloper; }
            set
            {
                Model.IsDeveloper = value;
                OnPropertyChanged();
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName != nameof(IsChanged))
            {
                IsChanged = true;
            }
        }
    }
}
