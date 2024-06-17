using System;
using System.Collections.Generic;

namespace participantHandler
{
    public interface IManage
    {
        public static void Add() { }
        public static void Remove() { }
        IList<Person> GetParticipants();
    }


    public class Manage : IManage
    {
        public static IList<Person> participants = new List<Person>();
        public static void Add(string name, string LastName)
        {
            participants.Add(new Person(name, LastName));
        }

        public static bool Remove(string name, string lastName)
        {
            for (int i = 0; i < participants.Count; i++)
            {
                if (participants[i].Name == name && participants[i].LastName == lastName)
                {
                    participants.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public IList<Person> GetParticipants()
        {
            return participants;
        }
    }
}
