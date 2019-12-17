using FriendStorage.Model;
using System;
using System.Collections.Generic;

namespace FriendStorage.DataAccess
{
    public interface IDataService : IDisposable
    {
        bool TryGetFriend(int friendId, out Friend friend);

        void SaveFriend(Friend friend);

        void DeleteFriend(int friendId);

        IEnumerable<LookupItem> GetAllFriends();
    }
}
