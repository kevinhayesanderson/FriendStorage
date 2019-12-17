﻿using FriendStorage.Model;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace FriendStorage.DataAccess
{
    public class FileDataService : IDataService, IDisposable
    {
        private const string StorageFile = "Friends.json";
        private bool disposed = false;
        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public bool TryGetFriend(int friendId, out Friend friend)
        {
            if (GetFriendById(friendId) == null)
            {
                friend = null;
                return false;
            }
            try
            {
                friend = GetFriendById(friendId);
                return true;
            }
            catch (ArgumentNullException)
            {
                friend = null;
                return false;
            }
        }

        public Friend GetFriendById(int friendId) => ReadFromFile().SingleOrDefault(f => f.Id == friendId);

        public void SaveFriend(Friend friend)
        {
            if (friend.Id <= 0)
            {
                InsertFriend(friend);
            }
            else
            {
                UpdateFriend(friend);
            }
        }

        public void DeleteFriend(int friendId)
        {
            var friends = ReadFromFile();
            var existing = friends.Single(f => f.Id == friendId);
            friends.Remove(existing);
            SaveToFile(friends);
        }

        private void UpdateFriend(Friend friend)
        {
            var friends = ReadFromFile();
            var existing = friends.Single(f => f.Id == friend.Id);
            var indexOfExisting = friends.IndexOf(existing);
            friends.Insert(indexOfExisting, friend);
            friends.Remove(existing);
            SaveToFile(friends);
        }

        private void InsertFriend(Friend friend)
        {
            var friends = ReadFromFile();
            var maxFriendId = friends.Count == 0 ? 0 : friends.Max(f => f.Id);
            friend.Id = maxFriendId + 1;
            friends.Add(friend);
            SaveToFile(friends);
        }

        public IEnumerable<LookupItem> GetAllFriends() => 
            ReadFromFile().Select(f => new LookupItem
            {
                Id = f.Id,
                DisplayMember = $"{f.FirstName} {f.LastName}"
            });

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }

        private void SaveToFile(List<Friend> friendList)
        {
            string json = JsonConvert.SerializeObject(friendList, Formatting.Indented);
            File.WriteAllText(StorageFile, json);
        }

        private List<Friend> ReadFromFile()
        {
            if (!File.Exists(StorageFile))
            {
                return new List<Friend>
                {
                    new Friend{Id=1,FirstName = "Thomas",LastName="Huber",
                        Birthday = new DateTime(1980,10,28), IsDeveloper = true},
                    new Friend{Id=2,FirstName = "Julia",LastName="Huber",
                        Birthday = new DateTime(1982,10,10)},
                    new Friend{Id=3,FirstName="Anna",LastName="Huber",
                        Birthday = new DateTime(2011,05,13)},
                    new Friend{Id=4,FirstName="Sara",LastName="Huber",
                        Birthday = new DateTime(2013,02,25)},
                    new Friend{Id=5,FirstName = "Andreas",LastName="Böhler",
                        Birthday = new DateTime(1981,01,10), IsDeveloper = true},
                    new Friend{Id=6,FirstName="Urs",LastName="Meier",
                        Birthday = new DateTime(1970,03,5), IsDeveloper = true},
                    new Friend{Id=7,FirstName="Chrissi",LastName="Heuberger",
                        Birthday = new DateTime(1987,07,16)},
                    new Friend{Id=8,FirstName="Erkan",LastName="Egin",
                        Birthday = new DateTime(1983,05,23)},
                };
            }

            string json = File.ReadAllText(StorageFile);
            return JsonConvert.DeserializeObject<List<Friend>>(json);
        }
    }
}